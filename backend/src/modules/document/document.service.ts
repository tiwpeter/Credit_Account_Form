import {
  Injectable,
  Logger,
  ForbiddenException,
  BadRequestException,
} from '@nestjs/common';
import {
  Controller,
  Post,
  Get,
  Patch,
  Delete,
  Param,
  Body,
  UseGuards,
  UseInterceptors,
  UploadedFile,
  HttpCode,
  HttpStatus,
  ParseUUIDPipe,
  Req,
} from '@nestjs/common';
import {
  ApiTags,
  ApiOperation,
  ApiResponse,
  ApiBearerAuth,
  ApiConsumes,
  ApiBody,
  ApiParam,
} from '@nestjs/swagger';
import { FileInterceptor } from '@nestjs/platform-express';
import { diskStorage } from 'multer';
import { extname, join } from 'path';
import { createHash } from 'crypto';
import { readFileSync } from 'fs';
import { v4 as uuidv4 } from 'uuid';
import {
  IsEnum,
  IsString,
  IsOptional,
  IsNotEmpty,
  IsDateString,
} from 'class-validator';
import { ApiPropertyOptional, ApiProperty } from '@nestjs/swagger';
import { Request } from 'express';
import { PrismaService } from '../prisma/prisma.service';
import { AuditLogService } from '../audit-log/audit-log.service';
import { JwtAuthGuard } from '../../common/guards/jwt-auth.guard';
import { RolesGuard } from '../../common/guards/roles.guard';
import { Roles, CurrentUser } from '../../common/decorators';
import { ICurrentUser } from '../../common/decorators';
import { DocumentType, DocumentStatus, AuditAction, RoleCode } from '../../common/enums';
import { DocumentNotFoundException, ApplicationNotFoundException } from '../../common/exceptions';

const ALLOWED_MIME_TYPES = [
  'application/pdf',
  'image/jpeg',
  'image/png',
  'image/tiff',
];
const MAX_FILE_SIZE = 10 * 1024 * 1024; // 10MB

// ============================================================
// DOCUMENT DTOs
// ============================================================
export class UploadDocumentDto {
  @ApiProperty({ enum: DocumentType })
  @IsEnum(DocumentType)
  documentType: DocumentType;

  @ApiPropertyOptional({ example: '2026-12-31' })
  @IsOptional()
  @IsDateString()
  expiryDate?: string;
}

export class VerifyDocumentDto {
  @ApiProperty({ enum: [DocumentStatus.VERIFIED, DocumentStatus.REJECTED] })
  @IsEnum([DocumentStatus.VERIFIED, DocumentStatus.REJECTED])
  status: DocumentStatus.VERIFIED | DocumentStatus.REJECTED;

  @ApiPropertyOptional({ description: 'Required if status is REJECTED' })
  @IsOptional()
  @IsString()
  @IsNotEmpty()
  rejectionReason?: string;

  @ApiPropertyOptional({
    description: 'OCR metadata JSON (can be provided by OCR system)',
    type: 'object',
  })
  @IsOptional()
  ocrMetadata?: Record<string, any>;
}

// ============================================================
// DOCUMENT SERVICE
// ============================================================
@Injectable()
export class DocumentService {
  private readonly logger = new Logger(DocumentService.name);

  constructor(
    private readonly prisma: PrismaService,
    private readonly auditLog: AuditLogService,
  ) {}

  async uploadDocument(
    applicationId: string,
    dto: UploadDocumentDto,
    file: Express.Multer.File,
    user: ICurrentUser,
    context: { ipAddress?: string; userAgent?: string },
  ) {
    // Verify application exists and user has access
    const application = await this.prisma.creditApplication.findFirst({
      where: { id: applicationId, deletedAt: null },
    });

    if (!application) throw new ApplicationNotFoundException(applicationId);

    if (
      user.roles.includes(RoleCode.CUSTOMER) &&
      application.customerId !== user.id
    ) {
      throw new ForbiddenException('Access denied');
    }

    // Validate file type
    if (!ALLOWED_MIME_TYPES.includes(file.mimetype)) {
      throw new BadRequestException(
        `File type '${file.mimetype}' not allowed. Allowed: PDF, JPEG, PNG, TIFF`,
      );
    }

    // Validate file size
    if (file.size > MAX_FILE_SIZE) {
      throw new BadRequestException(
        `File size ${(file.size / 1024 / 1024).toFixed(2)}MB exceeds limit of ${MAX_FILE_SIZE / 1024 / 1024}MB`,
      );
    }

    // Compute SHA-256 checksum for integrity
    const checksum = createHash('sha256').update(file.buffer || readFileSync(file.path)).digest('hex');

    // In production: upload to S3/GCS here and get the storage key
    // For mock: use the disk path
    const storedFileName = file.filename || `${uuidv4()}${extname(file.originalname)}`;
    const filePath = `uploads/${applicationId}/${storedFileName}`;

    const document = await this.prisma.executeInTransaction(async (tx) => {
      const doc = await tx.document.create({
        data: {
          applicationId,
          documentType: dto.documentType,
          originalFileName: file.originalname,
          storedFileName,
          filePath,
          fileSize: file.size,
          mimeType: file.mimetype,
          checksum,
          uploadedById: user.id,
          expiryDate: dto.expiryDate ? new Date(dto.expiryDate) : null,
        },
      });

      await this.auditLog.logInTransaction(tx, {
        userId: user.id,
        action: AuditAction.DOCUMENT_UPLOAD,
        entityType: 'Document',
        entityId: doc.id,
        applicationId,
        afterState: {
          documentType: dto.documentType,
          fileName: file.originalname,
          fileSize: file.size,
        },
        changedFields: ['all'],
        remark: `Document uploaded: ${dto.documentType}`,
        ipAddress: context.ipAddress,
        userAgent: context.userAgent,
      });

      return doc;
    });

    this.logger.log(
      `Document ${document.id} (${dto.documentType}) uploaded for application ${applicationId}`,
    );

    return document;
  }

  async verifyDocument(
    documentId: string,
    dto: VerifyDocumentDto,
    user: ICurrentUser,
    context: { ipAddress?: string },
  ) {
    const document = await this.prisma.document.findFirst({
      where: { id: documentId, isDeleted: false },
    });

    if (!document) throw new DocumentNotFoundException(documentId);

    if (dto.status === DocumentStatus.REJECTED && !dto.rejectionReason) {
      throw new BadRequestException('Rejection reason is required when rejecting a document');
    }

    const before = { status: document.status };

    const updated = await this.prisma.executeInTransaction(async (tx) => {
      const doc = await tx.document.update({
        where: { id: documentId },
        data: {
          status: dto.status,
          verifiedById: user.id,
          verifiedAt: new Date(),
          rejectionReason: dto.rejectionReason,
          ocrMetadata: dto.ocrMetadata as any,
          ocrProcessedAt: dto.ocrMetadata ? new Date() : undefined,
        },
      });

      await this.auditLog.logInTransaction(tx, {
        userId: user.id,
        action: AuditAction.DOCUMENT_VERIFY,
        entityType: 'Document',
        entityId: documentId,
        applicationId: document.applicationId,
        beforeState: before,
        afterState: { status: dto.status, verifiedById: user.id },
        changedFields: ['status', 'verifiedById', 'verifiedAt'],
        remark: dto.rejectionReason || `Document ${dto.status}`,
        ipAddress: context.ipAddress,
      });

      return doc;
    });

    return updated;
  }

  async getDocumentsByApplication(applicationId: string, user: ICurrentUser) {
    const application = await this.prisma.creditApplication.findFirst({
      where: { id: applicationId, deletedAt: null },
    });

    if (!application) throw new ApplicationNotFoundException(applicationId);

    if (
      user.roles.includes(RoleCode.CUSTOMER) &&
      application.customerId !== user.id
    ) {
      throw new ForbiddenException('Access denied');
    }

    return this.prisma.document.findMany({
      where: { applicationId, isDeleted: false },
      orderBy: { createdAt: 'desc' },
    });
  }

  async softDeleteDocument(documentId: string, user: ICurrentUser) {
    const document = await this.prisma.document.findFirst({
      where: { id: documentId, isDeleted: false },
      include: { application: true },
    });

    if (!document) throw new DocumentNotFoundException(documentId);

    // Only customer who owns the application or admin can delete
    if (
      user.roles.includes(RoleCode.CUSTOMER) &&
      document.application.customerId !== user.id
    ) {
      throw new ForbiddenException('Access denied');
    }

    await this.prisma.document.update({
      where: { id: documentId },
      data: { isDeleted: true },
    });
  }
}

// ============================================================
// DOCUMENT CONTROLLER
// ============================================================
const multerOptions = {
  storage: diskStorage({
    destination: './uploads',
    filename: (_req, file, cb) => {
      const uniqueName = `${uuidv4()}${extname(file.originalname)}`;
      cb(null, uniqueName);
    },
  }),
  limits: { fileSize: MAX_FILE_SIZE },
};

@ApiTags('Documents')
@ApiBearerAuth()
@UseGuards(JwtAuthGuard, RolesGuard)
@Controller('credit-applications/:applicationId/documents')
export class DocumentController {
  constructor(private readonly documentService: DocumentService) {}

  @Get()
  @Roles(RoleCode.CUSTOMER, RoleCode.BANK_OFFICER, RoleCode.APPROVER, RoleCode.ADMIN)
  @ApiOperation({ summary: 'Get all documents for an application' })
  getDocuments(
    @Param('applicationId', ParseUUIDPipe) applicationId: string,
    @CurrentUser() user: ICurrentUser,
  ) {
    return this.documentService.getDocumentsByApplication(applicationId, user);
  }

  @Post()
  @Roles(RoleCode.CUSTOMER, RoleCode.BANK_OFFICER, RoleCode.ADMIN)
  @UseInterceptors(FileInterceptor('file', multerOptions))
  @ApiOperation({
    summary: 'Upload document',
    description: 'Upload a document for the application. Supported: PDF, JPEG, PNG, TIFF. Max: 10MB.',
  })
  @ApiConsumes('multipart/form-data')
  @ApiBody({
    schema: {
      type: 'object',
      required: ['file', 'documentType'],
      properties: {
        file: { type: 'string', format: 'binary' },
        documentType: { type: 'string', enum: Object.values(DocumentType) },
        expiryDate: { type: 'string', format: 'date', description: 'Optional document expiry' },
      },
    },
  })
  @ApiResponse({ status: 201, description: 'Document uploaded successfully' })
  uploadDocument(
    @Param('applicationId', ParseUUIDPipe) applicationId: string,
    @Body() dto: UploadDocumentDto,
    @UploadedFile() file: Express.Multer.File,
    @CurrentUser() user: ICurrentUser,
    @Req() req: Request,
  ) {
    if (!file) {
      throw new BadRequestException('No file provided');
    }
    return this.documentService.uploadDocument(
      applicationId,
      dto,
      file,
      user,
      { ipAddress: req.ip, userAgent: req.headers['user-agent'] },
    );
  }

  @Patch(':documentId/verify')
  @Roles(RoleCode.BANK_OFFICER, RoleCode.ADMIN)
  @ApiOperation({ summary: 'Verify or reject a document' })
  @ApiParam({ name: 'documentId', type: String, format: 'uuid' })
  verifyDocument(
    @Param('documentId', ParseUUIDPipe) documentId: string,
    @Body() dto: VerifyDocumentDto,
    @CurrentUser() user: ICurrentUser,
    @Req() req: Request,
  ) {
    return this.documentService.verifyDocument(documentId, dto, user, {
      ipAddress: req.ip,
    });
  }

  @Delete(':documentId')
  @Roles(RoleCode.CUSTOMER, RoleCode.ADMIN)
  @HttpCode(HttpStatus.NO_CONTENT)
  @ApiOperation({ summary: 'Soft delete a document' })
  @ApiParam({ name: 'documentId', type: String, format: 'uuid' })
  deleteDocument(
    @Param('documentId', ParseUUIDPipe) documentId: string,
    @CurrentUser() user: ICurrentUser,
  ) {
    return this.documentService.softDeleteDocument(documentId, user);
  }
}
