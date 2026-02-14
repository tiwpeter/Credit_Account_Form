//import { ApplicationStatus } from '@prisma/client';

import {
  Injectable,
  Logger,
  ForbiddenException,
} from '@nestjs/common';
import { PrismaService } from '../prisma/prisma.service';
import { AuditLogService } from '../audit-log/audit-log.service';
import { WorkflowService } from '../workflow/workflow.service';
import {
  CreateCreditApplicationDto,
  UpdateCreditApplicationDto,
  ApplicationQueryDto,
  TransitionStatusDto,
  AssignOfficerDto,
  SubmitApplicationDto,
} from './dto/credit-application.dto';
import { ICurrentUser } from '../../common/decorators';
import {
  ApplicationStatus,
  ApplicationType,
  AuditAction,
  RoleCode,
} from '../../common/enums';
import {
  ApplicationNotFoundException,
  ApplicationAccessDeniedException,
  ApplicationAlreadySubmittedException,
  OptimisticLockException,
} from '../../common/exceptions';
import { CreditAnalysisService } from '../credit-analysis/credit-analysis.service';
// Prisma client in SQLite dev schema does not export enums; use domain enums (strings)
import { ApplicationStatus as DomainStatus } from '../../common/enums'

export function prismaToDomainStatus(s: string): DomainStatus {
  return s as DomainStatus;
}

export function domainToPrismaStatus(s: DomainStatus): string {
  return s as string;
}

// ============================================================
// APPLICATION NUMBER GENERATION
// ============================================================
async function generateApplicationNumber(prisma: PrismaService): Promise<string> {
  const year = new Date().getFullYear();
  const count = await prisma.creditApplication.count({
    where: {
      applicationNumber: { startsWith: `APP-${year}-` },
    },
  });
  const seq = String(count + 1).padStart(6, '0');
  return `APP-${year}-${seq}`;
}

@Injectable()
export class CreditApplicationService {
  private readonly logger = new Logger(CreditApplicationService.name);

  constructor(
    private readonly prisma: PrismaService,
    private readonly auditLog: AuditLogService,
    private readonly workflowService: WorkflowService,
    private readonly creditAnalysisService: CreditAnalysisService,
  ) {}

  // ============================================================
  // CREATE (DRAFT)
  // ============================================================
  async create(
    dto: CreateCreditApplicationDto,
    user: ICurrentUser,
    context: { ipAddress?: string; userAgent?: string },
  ) {
    this.validateApplicantData(dto);

    return this.prisma.executeInTransaction(async (tx) => {
      const applicationNumber = await generateApplicationNumber(this.prisma);

      // Create base application
      const application = await tx.creditApplication.create({
        data: {
          applicationNumber,
          type: dto.type,
          customerId: user.id,
          requestedAmount: dto.requestedAmount,
          requestedTenureMonths: dto.requestedTenureMonths,
          loanPurpose: dto.loanPurpose,
          collateralDescription: dto.collateralDescription,
        },
      });

      // Create applicant details
      if (dto.type === ApplicationType.PERSONAL_LOAN && dto.personalApplicant) {
        await this.createPersonalApplicant(
          tx,
          application.id,
          dto.personalApplicant,
        );
      } else if (
        dto.type === ApplicationType.CORPORATE_LOAN &&
        dto.corporateApplicant
      ) {
        await this.createCorporateApplicant(
          tx,
          application.id,
          dto.corporateApplicant,
        );
      }

      // Create guarantors if provided
      if (dto.guarantors && dto.guarantors.length > 0) {
        await tx.guarantor.createMany({
          data: dto.guarantors.map((g) => ({
            applicationId: application.id,
            nationalId: g.nationalId,
            firstName: g.firstName,
            lastName: g.lastName,
            relationship: g.relationship,
            phoneNumber: g.phoneNumber,
            monthlyIncome: g.monthlyIncome,
            existingDebt: g.existingDebt || 0,
          })),
        });
      }

      // Audit log
      await tx.auditLog.create({
        data: {
          userId: user.id,
          action: AuditAction.CREATE,
          entityType: 'CreditApplication',
          entityId: application.id,
          applicationId: application.id,
          afterState: JSON.stringify({ status: ApplicationStatus.DRAFT, applicationNumber }),
          changedFields: JSON.stringify(['all']),
          remark: 'Application draft created',
          ipAddress: context.ipAddress,
          userAgent: context.userAgent,
        },
      });

      // Initial status history
      await tx.applicationStatusHistory.create({
        data: {
          applicationId: application.id,
          fromStatus: null,
          toStatus: ApplicationStatus.DRAFT,
          changedBy: user.id,
          changedByName: `${user.firstName} ${user.lastName}`,
          remark: 'Application created as draft',
        },
      });

      this.logger.log(
        `Application ${applicationNumber} created by user ${user.id}`,
      );
      return this.findById(application.id, user);
    });
  }

  // ============================================================
  // UPDATE DRAFT (Multi-step saving)
  // ============================================================
  async update(
    id: string,
    dto: UpdateCreditApplicationDto,
    user: ICurrentUser,
    context: { ipAddress?: string; userAgent?: string },
  ) {
    const application = await this.findAndAssertOwnership(id, user);

    if (
      application.status !== ApplicationStatus.DRAFT &&
      application.status !== ApplicationStatus.NEED_MORE_INFO
    ) {
      throw new ApplicationAlreadySubmittedException(id);
    }

    // Optimistic locking check
    if (dto.version !== undefined && dto.version !== application.version) {
      throw new OptimisticLockException();
    }

    const beforeState = {
      requestedAmount: application.requestedAmount,
      requestedTenureMonths: application.requestedTenureMonths,
      loanPurpose: application.loanPurpose,
      currentStep: application.currentStep,
    };

    const updated = await this.prisma.creditApplication.update({
      where: { id, deletedAt: null },
      data: {
        ...(dto.requestedAmount !== undefined && {
          requestedAmount: dto.requestedAmount,
        }),
        ...(dto.requestedTenureMonths !== undefined && {
          requestedTenureMonths: dto.requestedTenureMonths,
        }),
        ...(dto.loanPurpose !== undefined && { loanPurpose: dto.loanPurpose }),
        ...(dto.collateralDescription !== undefined && {
          collateralDescription: dto.collateralDescription,
        }),
        ...(dto.currentStep !== undefined && {
          currentStep: dto.currentStep,
        }),
        version: { increment: 1 },
      },
    });

    await this.auditLog.log({
      userId: user.id,
      action: AuditAction.UPDATE,
      entityType: 'CreditApplication',
      entityId: id,
      applicationId: id,
      beforeState,
      afterState: {
        requestedAmount: updated.requestedAmount,
        requestedTenureMonths: updated.requestedTenureMonths,
        loanPurpose: updated.loanPurpose,
      },
      changedFields: Object.keys(dto).filter((k) => dto[k] !== undefined),
      remark: `Draft updated at step ${dto.currentStep || application.currentStep}`,
      ipAddress: context.ipAddress,
    });

    return this.findById(id, user);
  }

  // ============================================================
  // SUBMIT APPLICATION
  // ============================================================
  async submit(
    id: string,
    dto: SubmitApplicationDto,
    user: ICurrentUser,
    context: { ipAddress?: string; userAgent?: string },
  ) {
    if (!dto.confirmAccuracy) {
      throw new ForbiddenException(
        'You must confirm the accuracy of information before submitting',
      );
    }

    const application = await this.findAndAssertOwnership(id, user);

    if (application.status !== ApplicationStatus.DRAFT) {
      throw new ApplicationAlreadySubmittedException(id);
    }

    // Validate required data exists
    this.validateApplicationCompleteness(application);

    return this.prisma.executeInTransaction(async (tx) => {
      await this.workflowService.transition({
        tx,
        applicationId: id,
        fromStatus: ApplicationStatus.DRAFT,
        toStatus: ApplicationStatus.SUBMITTED,
        userId: user.id,
        userFullName: `${user.firstName} ${user.lastName}`,
        userRoles: user.roles,
        remark: dto.notes || 'Application submitted by customer',
        context,
      });

      return this.findById(id, user);
    });
  }

  // ============================================================
  // TRANSITION STATUS (Officer/Approver actions)
  // ============================================================
  async transitionStatus(
    id: string,
    toStatus: ApplicationStatus,
    dto: TransitionStatusDto,
    user: ICurrentUser,
    context: { ipAddress?: string; userAgent?: string },
  ) {
    const application = await this.findByIdForOfficer(id, user);

    return this.prisma.executeInTransaction(async (tx) => {
      await this.workflowService.transition({
        tx,
        applicationId: id,
        fromStatus: prismaToDomainStatus(application.status),
        toStatus,
        userId: user.id,
        userFullName: `${user.firstName} ${user.lastName}`,
        userRoles: user.roles,
        remark: dto.remark,
        context,
      });

      return this.findByIdForOfficer(id, user);
    });
  }

  // ============================================================
  // ASSIGN OFFICER
  // ============================================================
  async assignOfficer(
    id: string,
    dto: AssignOfficerDto,
    user: ICurrentUser,
    context: { ipAddress?: string },
  ) {
    const application = await this.findByIdForOfficer(id, user);

    await this.prisma.creditApplication.update({
      where: { id },
      data: { assignedOfficerId: dto.officerId },
    });

    await this.auditLog.log({
      userId: user.id,
      action: AuditAction.UPDATE,
      entityType: 'CreditApplication',
      entityId: id,
      applicationId: id,
      beforeState: { assignedOfficerId: application.assignedOfficerId },
      afterState: { assignedOfficerId: dto.officerId },
      changedFields: ['assignedOfficerId'],
      remark: 'Officer assigned to application',
      ipAddress: context.ipAddress,
    });

    return this.findByIdForOfficer(id, user);
  }

  // ============================================================
  // QUERY APPLICATIONS
  // ============================================================
  async findAll(query: ApplicationQueryDto, user: ICurrentUser) {
    const { page = 1, limit = 20, type, status, customerId, search } = query;
    const skip = (page - 1) * limit;

    // Build where clause based on role
    const where: any = { deletedAt: null };

    // Customers can only see their own applications
    if (user.roles.includes(RoleCode.CUSTOMER)) {
      where.customerId = user.id;
    } else if (customerId) {
      where.customerId = customerId;
    }

    if (type) where.type = type;
    if (status) where.status = status;

    if (search) {
      where.OR = [
        { applicationNumber: { contains: search, mode: 'insensitive' } },
        {
          personalApplicant: {
            OR: [
              { firstName: { contains: search, mode: 'insensitive' } },
              { lastName: { contains: search, mode: 'insensitive' } },
            ],
          },
        },
        {
          corporateApplicant: {
            company: {
              companyNameEn: { contains: search, mode: 'insensitive' },
            },
          },
        },
      ];
    }

    const [items, total] = await Promise.all([
      this.prisma.creditApplication.findMany({
        where,
        include: this.getApplicationIncludes(user),
        orderBy: { createdAt: 'desc' },
        skip,
        take: limit,
      }),
      this.prisma.creditApplication.count({ where }),
    ]);

    return { items, total, page, limit };
  }

  async findById(id: string, user: ICurrentUser) {
    const application = await this.prisma.creditApplication.findFirst({
      where: { id, deletedAt: null },
      include: {
        ...this.getApplicationIncludes(user),
        statusHistory: {
          orderBy: { createdAt: 'asc' },
        },
        creditAnalysis: true,
        approvals: {
          include: {
            approver: {
              select: { id: true, firstName: true, lastName: true },
            },
          },
          orderBy: { decidedAt: 'desc' },
        },
        guarantors: true,
        documents: {
          where: { isDeleted: false },
          orderBy: { createdAt: 'desc' },
        },
      },
    });

    if (!application) {
      throw new ApplicationNotFoundException(id);
    }

    // Access control
    if (
      user.roles.includes(RoleCode.CUSTOMER) &&
      application.customerId !== user.id
    ) {
      throw new ApplicationAccessDeniedException();
    }

    return application;
  }

  // ============================================================
  // SOFT DELETE
  // ============================================================
  async softDelete(id: string, user: ICurrentUser, context: { ipAddress?: string }) {
    const application = await this.findAndAssertOwnership(id, user);

    if (application.status !== ApplicationStatus.DRAFT) {
      throw new ForbiddenException('Only DRAFT applications can be deleted');
    }

    await this.prisma.creditApplication.update({
      where: { id },
      data: { deletedAt: new Date() },
    });

    await this.auditLog.log({
      userId: user.id,
      action: AuditAction.DELETE,
      entityType: 'CreditApplication',
      entityId: id,
      applicationId: id,
      remark: 'Application deleted by customer',
      ipAddress: context.ipAddress,
    });
  }

  // ============================================================
  // AVAILABLE TRANSITIONS
  // ============================================================
  async getAvailableTransitions(id: string, user: ICurrentUser) {
    const application = await this.findById(id, user);
    return this.workflowService.getAvailableTransitions(
      prismaToDomainStatus(application.status),  
      user.roles,
    );
    
  }

  // ============================================================
  // PRIVATE HELPERS
  // ============================================================

  private async findAndAssertOwnership(id: string, user: ICurrentUser) {
    const application = await this.prisma.creditApplication.findFirst({
      where: { id, deletedAt: null },
    });

    if (!application) throw new ApplicationNotFoundException(id);

    if (
      user.roles.includes(RoleCode.CUSTOMER) &&
      application.customerId !== user.id
    ) {
      throw new ApplicationAccessDeniedException();
    }

    return application;
  }

  private async findByIdForOfficer(id: string, user: ICurrentUser) {
    const application = await this.prisma.creditApplication.findFirst({
      where: { id, deletedAt: null },
      include: this.getApplicationIncludes(user),
    });

    if (!application) throw new ApplicationNotFoundException(id);
    return application;
  }

  private validateApplicantData(dto: CreateCreditApplicationDto): void {
    if (
      dto.type === ApplicationType.PERSONAL_LOAN &&
      !dto.personalApplicant
    ) {
      throw new ForbiddenException(
        'Personal applicant information is required for personal loans',
      );
    }
    if (
      dto.type === ApplicationType.CORPORATE_LOAN &&
      !dto.corporateApplicant
    ) {
      throw new ForbiddenException(
        'Corporate applicant information is required for corporate loans',
      );
    }
  }

  private validateApplicationCompleteness(application: any): void {
    const missing: string[] = [];

    if (
      application.type === ApplicationType.PERSONAL_LOAN &&
      !application.personalApplicant
    ) {
      missing.push('Personal applicant information');
    }

    if (
      application.type === ApplicationType.CORPORATE_LOAN &&
      !application.corporateApplicant
    ) {
      missing.push('Corporate applicant information');
    }

    if (missing.length > 0) {
      throw new ForbiddenException(
        `Application is incomplete. Missing: ${missing.join(', ')}`,
      );
    }
  }

  private getApplicationIncludes(user: ICurrentUser) {
    return {
      customer: {
        select: { id: true, firstName: true, lastName: true, email: true },
      },
      assignedOfficer: {
        select: { id: true, firstName: true, lastName: true },
      },
      personalApplicant: {
        include: { addresses: true },
      },
      corporateApplicant: {
        include: {
          company: { include: { addresses: true } },
        },
      },
    };
  }

  private async createPersonalApplicant(
    tx: any,
    applicationId: string,
    dto: any,
  ) {
    const { addresses, ...applicantData } = dto;

    const applicant = await tx.personalApplicant.create({
      data: {
        applicationId,
        ...applicantData,
        dateOfBirth: new Date(applicantData.dateOfBirth),
      },
    });

    if (addresses && addresses.length > 0) {
      await tx.address.createMany({
        data: addresses.map((addr) => ({
          personalApplicantId: applicant.id,
          ...addr,
        })),
      });
    }

    return applicant;
  }

  private async createCorporateApplicant(
    tx: any,
    applicationId: string,
    dto: any,
  ) {
    const { company, ...contactData } = dto;
    const { addresses, ...companyData } = company;

    const applicant = await tx.corporateApplicant.create({
      data: {
        applicationId,
        ...contactData,
      },
    });

    const createdCompany = await tx.company.create({
      data: {
        corporateApplicantId: applicant.id,
        ...companyData,
        incorporationDate: new Date(companyData.incorporationDate),
      },
    });

    if (addresses && addresses.length > 0) {
      await tx.address.createMany({
        data: addresses.map((addr) => ({
          companyId: createdCompany.id,
          ...addr,
        })),
      });
    }

    return applicant;
  }
}
