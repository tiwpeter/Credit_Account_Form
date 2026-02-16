import {
  Injectable,
  Logger,
  ConflictException,
} from '@nestjs/common';
import { JwtService } from '@nestjs/jwt';
import { ConfigService } from '@nestjs/config';
import * as bcrypt from 'bcrypt';
import { v4 as uuidv4 } from 'uuid';
import { PrismaService } from '../prisma/prisma.service';
import { AuditLogService } from '../audit-log/audit-log.service';
import {
  LoginDto,
  RegisterDto,
  RefreshTokenDto,
  AuthResponseDto,
  ChangePasswordDto,
} from './dto/auth.dto';
import {
  InvalidCredentialsException,
  AccountLockedException,
  TokenExpiredException,
  TokenRevokedException,
} from '../../common/exceptions';
import { AuditAction, RoleCode, UserStatus } from '../../common/enums';
import { JwtPayload } from './strategies/jwt.strategy';

const MAX_FAILED_ATTEMPTS = 5;

@Injectable()
export class AuthService {
  private readonly logger = new Logger(AuthService.name);

  constructor(
    private readonly prisma: PrismaService,
    private readonly jwtService: JwtService,
    private readonly configService: ConfigService,
    private readonly auditLog: AuditLogService,
  ) {}

  async login(
    dto: LoginDto,
    context: { ipAddress?: string; userAgent?: string },
  ): Promise<AuthResponseDto> {
    const user = await this.prisma.user.findUnique({
      where: { email: dto.email.toLowerCase(), deletedAt: null },
      include: {
        userRoles: { include: { role: true } },
      },
    });

    if (!user) {
      throw new InvalidCredentialsException();
    }

    if (user.status === UserStatus.LOCKED) {
      throw new AccountLockedException();
    }

    if (user.status === UserStatus.SUSPENDED) {
      throw new InvalidCredentialsException();
    }

    const passwordValid = await bcrypt.compare(dto.password, user.password);

    if (!passwordValid) {
      await this.handleFailedLogin(user.id, user.failedLoginCount);
      throw new InvalidCredentialsException();
    }

    // Reset failed attempts on successful login
    await this.prisma.user.update({
      where: { id: user.id },
      data: {
        failedLoginCount: 0,
        lastLoginAt: new Date(),
      },
    });

    const roles = user.userRoles.map((ur) => ur.role.code);
    const tokens = await this.generateTokens(user.id, user.email, roles);

    // Save refresh token
    await this.saveRefreshToken(
      user.id,
      tokens.refreshToken,
      context.ipAddress,
      context.userAgent,
    );

    await this.auditLog.log({
      userId: user.id,
      action: AuditAction.LOGIN,
      entityType: 'User',
      entityId: user.id,
      remark: 'Successful login',
      ipAddress: context.ipAddress,
      userAgent: context.userAgent,
    });

    return {
      accessToken: tokens.accessToken,
      refreshToken: tokens.refreshToken,
      expiresIn: 900, // 15 minutes in seconds
      user: {
        id: user.id,
        email: user.email,
        firstName: user.firstName,
        lastName: user.lastName,
        roles,
      },
    };
  }

  async mockLogin(email: string): Promise<AuthResponseDto> {
    const user = await this.prisma.user.findUnique({
      where: { email: email.toLowerCase(), deletedAt: null },
      include: {
        userRoles: { include: { role: true } },
      },
    });

    if (!user) {
      throw new InvalidCredentialsException();
    }

    const roles = user.userRoles.map((ur) => ur.role.code);
    const tokens = await this.generateTokens(user.id, user.email, roles);

    this.logger.log(`Mock login for user: ${user.email}`);

    return {
      accessToken: tokens.accessToken,
      refreshToken: tokens.refreshToken,
      expiresIn: 900, // 15 minutes in seconds
      user: {
        id: user.id,
        email: user.email,
        firstName: user.firstName,
        lastName: user.lastName,
        roles,
      },
    };
  }

  async register(dto: RegisterDto): Promise<{ id: string; email: string }> {
    const existing = await this.prisma.user.findUnique({
      where: { email: dto.email.toLowerCase() },
    });

    if (existing) {
      throw new ConflictException('Email address already registered');
    }

    const saltRounds = this.configService.get<number>('bcrypt.saltRounds', 12);
    const hashedPassword = await bcrypt.hash(dto.password, saltRounds);

    const user = await this.prisma.$transaction(async (tx) => {
      const customerRole = await tx.role.findUnique({
        where: { code: RoleCode.CUSTOMER },
      });

      if (!customerRole) {
        throw new Error('CUSTOMER role not found in database');
      }

      const newUser = await tx.user.create({
        data: {
          email: dto.email.toLowerCase(),
          password: hashedPassword,
          firstName: dto.firstName,
          lastName: dto.lastName,
          phoneNumber: dto.phoneNumber,
          userRoles: {
            create: {
              roleId: customerRole.id,
            },
          },
        },
      });

      await tx.auditLog.create({
        data: {
          userId: newUser.id,
          action: AuditAction.CREATE,
          entityType: 'User',
          entityId: newUser.id,
          remark: 'User registration',
          changedFields: JSON.stringify([]),
        },
      });

      return newUser;
    });

    this.logger.log(`New user registered: ${user.email}`);
    return { id: user.id, email: user.email };
  }

  async refreshToken(dto: RefreshTokenDto): Promise<{ accessToken: string }> {
    const storedToken = await this.prisma.refreshToken.findUnique({
      where: { token: dto.refreshToken },
      include: {
        user: {
          include: { userRoles: { include: { role: true } } },
        },
      },
    });

    if (!storedToken) {
      throw new TokenRevokedException();
    }

    if (storedToken.isRevoked) {
      throw new TokenRevokedException();
    }

    if (storedToken.expiresAt < new Date()) {
      throw new TokenExpiredException();
    }

    if (
      !storedToken.user ||
      storedToken.user.status !== UserStatus.ACTIVE ||
      storedToken.user.deletedAt
    ) {
      throw new InvalidCredentialsException();
    }

    const roles = storedToken.user.userRoles.map((ur) => ur.role.code);
    const payload: JwtPayload = {
      sub: storedToken.user.id,
      email: storedToken.user.email,
      roles,
    };

    const accessToken = this.jwtService.sign(payload);
    return { accessToken };
  }

  async logout(userId: string, refreshToken?: string): Promise<void> {
    if (refreshToken) {
      await this.prisma.refreshToken.updateMany({
        where: { userId, token: refreshToken },
        data: { isRevoked: true },
      });
    } else {
      // Revoke all tokens for this user
      await this.prisma.refreshToken.updateMany({
        where: { userId, isRevoked: false },
        data: { isRevoked: true },
      });
    }

    await this.auditLog.log({
      userId,
      action: AuditAction.LOGOUT,
      entityType: 'User',
      entityId: userId,
      remark: 'User logout',
    });
  }

  async changePassword(
    userId: string,
    dto: ChangePasswordDto,
    context: { ipAddress?: string },
  ): Promise<void> {
    const user = await this.prisma.user.findUnique({
      where: { id: userId },
    });

    if (!user) {
      throw new InvalidCredentialsException();
    }

    const valid = await bcrypt.compare(dto.currentPassword, user.password);
    if (!valid) {
      throw new InvalidCredentialsException();
    }

    const saltRounds = this.configService.get<number>('bcrypt.saltRounds', 12);
    const newHash = await bcrypt.hash(dto.newPassword, saltRounds);

    await this.prisma.$transaction(async (tx) => {
      await tx.user.update({
        where: { id: userId },
        data: {
          password: newHash,
          passwordChangedAt: new Date(),
          mustChangePassword: false,
        },
      });

      // Revoke all existing refresh tokens on password change
      await tx.refreshToken.updateMany({
        where: { userId, isRevoked: false },
        data: { isRevoked: true },
      });
    });

    await this.auditLog.log({
      userId,
      action: AuditAction.UPDATE,
      entityType: 'User',
      entityId: userId,
      changedFields: ['password'],
      remark: 'Password changed',
      ipAddress: context.ipAddress,
    });
  }

  // ============================================================
  // PRIVATE HELPERS
  // ============================================================

  private async generateTokens(
    userId: string,
    email: string,
    roles: string[],
  ) {
    const payload: JwtPayload = { sub: userId, email, roles };

    const [accessToken, refreshToken] = await Promise.all([
      this.jwtService.signAsync(payload, {
        secret: this.configService.get('jwt.secret'),
        expiresIn: this.configService.get('jwt.expiresIn'),
      }),
      this.jwtService.signAsync(
        { sub: userId, tokenId: uuidv4() },
        {
          secret: this.configService.get('jwt.refreshSecret'),
          expiresIn: this.configService.get('jwt.refreshExpiresIn'),
        },
      ),
    ]);

    return { accessToken, refreshToken };
  }

  private async saveRefreshToken(
    userId: string,
    token: string,
    ipAddress?: string,
    userAgent?: string,
  ) {
    const refreshExpiry = this.configService.get('jwt.refreshExpiresIn', '7d');
    const days = parseInt(refreshExpiry.replace('d', ''));
    const expiresAt = new Date();
    expiresAt.setDate(expiresAt.getDate() + days);

    await this.prisma.refreshToken.create({
      data: {
        userId,
        token,
        expiresAt,
        ipAddress,
        userAgent,
      },
    });
  }

  private async handleFailedLogin(
    userId: string,
    currentFailedCount: number,
  ): Promise<void> {
    const newCount = currentFailedCount + 1;
    const shouldLock = newCount >= MAX_FAILED_ATTEMPTS;

    await this.prisma.user.update({
      where: { id: userId },
      data: {
        failedLoginCount: newCount,
        ...(shouldLock && { status: UserStatus.LOCKED }),
      },
    });

    if (shouldLock) {
      this.logger.warn(`Account locked after ${newCount} failed attempts: ${userId}`);
    }
  }
}
