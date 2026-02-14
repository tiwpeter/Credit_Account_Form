import {
  HttpException,
  HttpStatus,
  BadRequestException,
  ForbiddenException,
  NotFoundException,
  UnauthorizedException,
  ConflictException,
} from '@nestjs/common';

// ============================================================
// BASE BANK EXCEPTION
// ============================================================
export class BankException extends HttpException {
  constructor(
    message: string,
    statusCode: HttpStatus,
    public readonly errorCode: string,
    public readonly details?: any,
  ) {
    super({ message, errorCode, details, statusCode }, statusCode);
  }
}

// ============================================================
// WORKFLOW EXCEPTIONS
// ============================================================
export class InvalidStatusTransitionException extends BankException {
  constructor(fromStatus: string, toStatus: string, reason?: string) {
    super(
      `Invalid status transition from '${fromStatus}' to '${toStatus}'${reason ? ': ' + reason : ''}`,
      HttpStatus.UNPROCESSABLE_ENTITY,
      'INVALID_STATUS_TRANSITION',
      { fromStatus, toStatus },
    );
  }
}

export class WorkflowPermissionException extends BankException {
  constructor(requiredRole: string, action: string) {
    super(
      `Role '${requiredRole}' is required to perform action: ${action}`,
      HttpStatus.FORBIDDEN,
      'WORKFLOW_PERMISSION_DENIED',
      { requiredRole, action },
    );
  }
}

// ============================================================
// CREDIT EXCEPTIONS
// ============================================================
export class InsufficientIncomeException extends BankException {
  constructor(required: number, provided: number) {
    super(
      `Minimum monthly income requirement not met. Required: ${required} THB, Provided: ${provided} THB`,
      HttpStatus.UNPROCESSABLE_ENTITY,
      'INSUFFICIENT_INCOME',
      { required, provided },
    );
  }
}

export class DtiExceededException extends BankException {
  constructor(dti: number, maxDti: number) {
    super(
      `Debt-to-Income ratio ${(dti * 100).toFixed(2)}% exceeds maximum allowed ${(maxDti * 100).toFixed(2)}%`,
      HttpStatus.UNPROCESSABLE_ENTITY,
      'DTI_EXCEEDED',
      { dti, maxDti },
    );
  }
}

export class LoanAmountExceededException extends BankException {
  constructor(requestedAmount: number, maxAmount: number) {
    super(
      `Requested loan amount ${requestedAmount} THB exceeds maximum eligible amount ${maxAmount} THB`,
      HttpStatus.UNPROCESSABLE_ENTITY,
      'LOAN_AMOUNT_EXCEEDED',
      { requestedAmount, maxAmount },
    );
  }
}

export class TenureExceededException extends BankException {
  constructor(requestedTenure: number, maxTenure: number, loanType: string) {
    super(
      `Requested tenure ${requestedTenure} months exceeds maximum ${maxTenure} months for ${loanType}`,
      HttpStatus.UNPROCESSABLE_ENTITY,
      'TENURE_EXCEEDED',
      { requestedTenure, maxTenure, loanType },
    );
  }
}

// ============================================================
// DOCUMENT EXCEPTIONS
// ============================================================
export class DocumentNotFoundException extends NotFoundException {
  constructor(documentId: string) {
    super(`Document with ID '${documentId}' not found`);
  }
}

export class InvalidDocumentTypeException extends BadRequestException {
  constructor(mimeType: string) {
    super(`File type '${mimeType}' is not allowed. Allowed types: PDF, JPEG, PNG, TIFF`);
  }
}

export class FileSizeExceededException extends BadRequestException {
  constructor(fileSize: number, maxSize: number) {
    super(`File size ${(fileSize / 1024 / 1024).toFixed(2)}MB exceeds maximum ${maxSize}MB`);
  }
}

// ============================================================
// APPLICATION EXCEPTIONS
// ============================================================
export class ApplicationNotFoundException extends NotFoundException {
  constructor(id: string) {
    super(`Credit application '${id}' not found`);
  }
}

export class ApplicationAccessDeniedException extends ForbiddenException {
  constructor() {
    super('You do not have permission to access this application');
  }
}

export class ApplicationAlreadySubmittedException extends ConflictException {
  constructor(applicationId: string) {
    super(`Application '${applicationId}' has already been submitted and cannot be edited`);
  }
}

export class DuplicateApplicationException extends ConflictException {
  constructor() {
    super('A pending application already exists for this customer');
  }
}

// ============================================================
// AUTH EXCEPTIONS
// ============================================================
export class InvalidCredentialsException extends UnauthorizedException {
  constructor() {
    super('Invalid email or password');
  }
}

export class AccountLockedException extends UnauthorizedException {
  constructor() {
    super('Account has been locked due to too many failed login attempts. Contact support.');
  }
}

export class TokenExpiredException extends UnauthorizedException {
  constructor() {
    super('Authentication token has expired. Please login again.');
  }
}

export class TokenRevokedException extends UnauthorizedException {
  constructor() {
    super('Authentication token has been revoked.');
  }
}

// ============================================================
// CONCURRENCY
// ============================================================
export class OptimisticLockException extends ConflictException {
  constructor() {
    super('The record was modified by another user. Please refresh and try again.');
  }
}
