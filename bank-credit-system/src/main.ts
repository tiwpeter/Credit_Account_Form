import { NestFactory, Reflector } from '@nestjs/core';
import { ValidationPipe, Logger } from '@nestjs/common';
import { SwaggerModule, DocumentBuilder } from '@nestjs/swagger';
import { ConfigService } from '@nestjs/config';
import * as compression from 'compression';
import * as helmet from 'helmet';
import * as morgan from 'morgan';
import { AppModule } from './app.module';

async function bootstrap() {
  const logger = new Logger('Bootstrap');

  const app = await NestFactory.create(AppModule, {
    logger: ['error', 'warn', 'log', 'debug', 'verbose'],
  });

  const configService = app.get(ConfigService);
  const port = configService.get<number>('app.port', 3000);
  const apiPrefix = configService.get<string>('app.apiPrefix', 'api/v1');
  const nodeEnv = configService.get<string>('app.nodeEnv', 'development');

  // ============================================================
  // SECURITY MIDDLEWARE
  // ============================================================
  app.use(
    (helmet as any)({
      contentSecurityPolicy: {
        directives: {
          defaultSrc: ["'self'"],
          styleSrc: ["'self'", "'unsafe-inline'"],
          imgSrc: ["'self'", 'data:', 'validator.swagger.io'],
          scriptSrc: ["'self'", "https: 'unsafe-inline'"],
        },
      },
      crossOriginEmbedderPolicy: true,
    }),
  );

  // CORS configuration
  app.enableCors({
    origin: nodeEnv === 'production'
      ? [process.env.ALLOWED_ORIGINS || 'https://bank.th']
      : '*',
    methods: ['GET', 'POST', 'PUT', 'PATCH', 'DELETE', 'OPTIONS'],
    allowedHeaders: ['Content-Type', 'Authorization', 'X-Request-ID'],
    credentials: true,
  });

  // Compression
  app.use(compression());

  // HTTP logging
  if (nodeEnv === 'development') {
    app.use(morgan('dev'));
  } else {
    app.use(morgan('combined'));
  }

  // ============================================================
  // GLOBAL PIPES - Validation
  // ============================================================
  app.useGlobalPipes(
    new ValidationPipe({
      whitelist: true,          // Strip unknown properties
      forbidNonWhitelisted: true, // Throw error if unknown properties
      transform: true,          // Auto-transform types
      transformOptions: {
        enableImplicitConversion: true,
      },
      errorHttpStatusCode: 400,
      stopAtFirstError: false,   // Return all validation errors
      exceptionFactory: (errors) => {
        const messages = errors.map((err) => ({
          field: err.property,
          constraints: Object.values(err.constraints || {}),
          value: err.value,
        }));
        const { BadRequestException } = require('@nestjs/common');
        return new BadRequestException({
          message: 'Validation failed',
          errorCode: 'VALIDATION_ERROR',
          details: messages,
          statusCode: 400,
        });
      },
    }),
  );

  // ============================================================
  // API PREFIX
  // ============================================================
  app.setGlobalPrefix(apiPrefix);

  // ============================================================
  // SWAGGER DOCUMENTATION
  // ============================================================
  if (nodeEnv !== 'production') {
    const config = new DocumentBuilder()
      .setTitle('Bank Credit Application System API')
      .setDescription(
        `
## Overview
Production-grade bank credit application system supporting Personal and Corporate loans.

## Authentication
All endpoints require JWT Bearer token except \`/auth/login\` and \`/auth/register\`.

**To authenticate:**
1. POST \`/auth/login\` with your credentials
2. Copy the \`accessToken\` from the response
3. Click "Authorize" and enter: \`Bearer <your-token>\`

## Workflow
\`\`\`
DRAFT → SUBMITTED → DOCUMENT_CHECK → CREDIT_ANALYSIS → APPROVAL
     → APPROVED / REJECTED / NEED_MORE_INFO
     → CONTRACT_SIGNED → DISBURSED
\`\`\`

## Business Rules
- DTI ≤ 50%
- Min income: 15,000 THB/month
- Personal loan max: 5× monthly income
- Personal loan max tenure: 60 months
- SME loan max tenure: 120 months

## Seed Credentials
| Role | Email | Password |
|------|-------|----------|
| ADMIN | admin@bank.th | Admin@1234! |
| OFFICER | officer.somchai@bank.th | Officer@1234! |
| APPROVER | approver.wanchai@bank.th | Approver@1234! |
| CUSTOMER | customer.napat@email.com | Customer@1234! |
      `,
      )
      .setVersion('1.0.0')
      .addBearerAuth({
        type: 'http',
        scheme: 'bearer',
        bearerFormat: 'JWT',
        name: 'Authorization',
        description: 'Enter: Bearer <JWT token>',
        in: 'header',
      })
      .addServer(`http://localhost:${port}`, 'Development')
      .addTag('Authentication', 'Login, register, token management')
      .addTag('Credit Applications', 'Full lifecycle credit application management')
      .addTag('Documents', 'Document upload and verification')
      .addTag('Credit Analysis', 'DTI, eligibility, and risk assessment')
      .addTag('Approvals', 'Approval decisions and history')
      .build();

    const document = SwaggerModule.createDocument(app, config, {
      operationIdFactory: (controllerKey, methodKey) => `${controllerKey}_${methodKey}`,
    });

    SwaggerModule.setup('docs', app, document, {
      swaggerOptions: {
        persistAuthorization: true,
        docExpansion: 'none',
        filter: true,
        showRequestDuration: true,
        tagsSorter: 'alpha',
        operationsSorter: 'alpha',
      },
      customSiteTitle: 'Bank Credit API Docs',
    });

    logger.log(`📚 Swagger documentation: http://localhost:${port}/docs`);
  }

  // ============================================================
  // REQUEST ID MIDDLEWARE
  // ============================================================
  app.use((req: any, _res: any, next: any) => {
    if (!req.headers['x-request-id']) {
      req.headers['x-request-id'] = require('uuid').v4();
    }
    next();
  });

  // ============================================================
  // GRACEFUL SHUTDOWN
  // ============================================================
  app.enableShutdownHooks();

  process.on('SIGTERM', async () => {
    logger.log('SIGTERM received. Shutting down gracefully...');
    await app.close();
    process.exit(0);
  });

  // ============================================================
  // START SERVER
  // ============================================================
  await app.listen(port);

  logger.log(`🚀 Application running: http://localhost:${port}/${apiPrefix}`);
  logger.log(`📋 Environment: ${nodeEnv}`);
  logger.log(`🔒 Security: JWT + RBAC + Helmet + Rate Limiting active`);
}

bootstrap().catch((err) => {
  new Logger('Bootstrap').error('Failed to start application', err);
  process.exit(1);
});
