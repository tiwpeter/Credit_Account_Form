import { Module } from '@nestjs/common';
import { ConfigModule } from '@nestjs/config';
import { ThrottlerModule, ThrottlerGuard } from '@nestjs/throttler';
import { APP_GUARD, APP_FILTER, APP_INTERCEPTOR } from '@nestjs/core';
import { Reflector } from '@nestjs/core';

// Config
import {
  appConfig,
  jwtConfig,
  databaseConfig,
  uploadConfig,
  bcryptConfig,
  throttleConfig,
} from './config/configuration';

// Infrastructure modules
import { PrismaModule } from './modules/prisma/prisma.module';
import { AuditLogModule } from './modules/audit-log/audit-log.module';

// Feature modules
import { AuthModule } from './modules/auth/auth.module';
import { CreditApplicationModule } from './modules/credit-application/credit-application.module';
import { WorkflowModule } from './modules/workflow/workflow.module';
import { CreditAnalysisModule } from './modules/credit-analysis/credit-analysis.module';
import { ApprovalModule } from './modules/approval/approval.module';
import { DocumentModule } from './modules/document/document.module';

// Guards & Filters
import { JwtAuthGuard } from './common/guards/jwt-auth.guard';
import { RolesGuard } from './common/guards/roles.guard';
import { GlobalExceptionFilter } from './common/filters/global-exception.filter';
import { ResponseTransformInterceptor } from './common/interceptors/response-transform.interceptor';

@Module({
  imports: [
    // Configuration
    ConfigModule.forRoot({
      isGlobal: true,
      load: [appConfig, jwtConfig, databaseConfig, uploadConfig, bcryptConfig, throttleConfig],
      envFilePath: ['.env.local', '.env'],
    }),

    // Rate limiting
    ThrottlerModule.forRoot([
      {
        ttl: 60000,  // 60 seconds
        limit: 100,  // max 100 requests per TTL
      },
    ]),

    // Infrastructure
    PrismaModule,
    AuditLogModule,

    // Feature modules
    AuthModule,
    WorkflowModule,
    CreditApplicationModule,
    CreditAnalysisModule,
    ApprovalModule,
    DocumentModule,
  ],
  providers: [
    // Global rate limit guard
    {
      provide: APP_GUARD,
      useClass: ThrottlerGuard,
    },
    // Global JWT guard (all routes protected by default)
    {
      provide: APP_GUARD,
      useClass: JwtAuthGuard,
    },
    // Global roles guard
    {
      provide: APP_GUARD,
      useClass: RolesGuard,
    },
    // Global exception filter
    {
      provide: APP_FILTER,
      useClass: GlobalExceptionFilter,
    },
    // Global response transform
    {
      provide: APP_INTERCEPTOR,
      useFactory: (reflector: Reflector) =>
        new ResponseTransformInterceptor(reflector),
      inject: [Reflector],
    },
  ],
})
export class AppModule {}
