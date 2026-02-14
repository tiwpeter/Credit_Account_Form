import {
  createParamDecorator,
  ExecutionContext,
  SetMetadata,
  applyDecorators,
  UseGuards,
} from '@nestjs/common';
import { ApiBearerAuth, ApiUnauthorizedResponse, ApiForbiddenResponse } from '@nestjs/swagger';
import { RoleCode } from '../enums';

// ============================================================
// ROLES DECORATOR
// ============================================================
export const ROLES_KEY = 'roles';
export const Roles = (...roles: RoleCode[]) => SetMetadata(ROLES_KEY, roles);

// ============================================================
// PUBLIC ROUTE DECORATOR
// ============================================================
export const IS_PUBLIC_KEY = 'isPublic';
export const Public = () => SetMetadata(IS_PUBLIC_KEY, true);

// ============================================================
// CURRENT USER DECORATOR
// ============================================================
export interface ICurrentUser {
  id: string;
  email: string;
  roles: RoleCode[];
  firstName: string;
  lastName: string;
}

export const CurrentUser = createParamDecorator(
  (data: keyof ICurrentUser | undefined, ctx: ExecutionContext) => {
    const request = ctx.switchToHttp().getRequest();
    const user = request.user as ICurrentUser;
    return data ? user?.[data] : user;
  },
);

// ============================================================
// REQUEST ID DECORATOR
// ============================================================
export const RequestId = createParamDecorator(
  (_data: unknown, ctx: ExecutionContext) => {
    const request = ctx.switchToHttp().getRequest();
    return request.headers['x-request-id'] || request.id;
  },
);

// ============================================================
// AUDIT INFO DECORATOR - Extracts request context for audit logging
// ============================================================
export const AuditInfo = createParamDecorator(
  (_data: unknown, ctx: ExecutionContext) => {
    const request = ctx.switchToHttp().getRequest();
    return {
      ipAddress: request.ip || request.connection?.remoteAddress,
      userAgent: request.headers['user-agent'],
      requestId: request.headers['x-request-id'],
    };
  },
);

// ============================================================
// COMBINED AUTH DECORATOR
// ============================================================
export const Auth = (...roles: RoleCode[]) => {
  // Import guards inline to avoid circular dependency
  const { JwtAuthGuard } = require('../guards/jwt-auth.guard');
  const { RolesGuard } = require('../guards/roles.guard');

  return applyDecorators(
    Roles(...roles),
    UseGuards(JwtAuthGuard, RolesGuard),
    ApiBearerAuth(),
    ApiUnauthorizedResponse({ description: 'Authentication required' }),
    ApiForbiddenResponse({ description: 'Insufficient permissions' }),
  );
};

// ============================================================
// PAGINATION DECORATOR
// ============================================================
export const SKIP_TRANSFORM = 'skipTransform';
export const SkipTransform = () => SetMetadata(SKIP_TRANSFORM, true);
