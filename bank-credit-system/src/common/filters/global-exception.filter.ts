import {
  ExceptionFilter,
  Catch,
  ArgumentsHost,
  HttpException,
  HttpStatus,
  Logger,
} from '@nestjs/common';
import { Request, Response } from 'express';
import { Prisma } from '@prisma/client';

interface ErrorResponse {
  statusCode: number;
  errorCode: string;
  message: string;
  details?: any;
  timestamp: string;
  path: string;
  requestId?: string;
}

@Catch()
export class GlobalExceptionFilter implements ExceptionFilter {
  private readonly logger = new Logger(GlobalExceptionFilter.name);

  catch(exception: unknown, host: ArgumentsHost) {
    const ctx = host.switchToHttp();
    const response = ctx.getResponse<Response>();
    const request = ctx.getRequest<Request>();

    const requestId = request.headers['x-request-id'] as string;
    const errorResponse = this.buildErrorResponse(exception, request, requestId);

    this.logError(exception, errorResponse, request);

    response.status(errorResponse.statusCode).json(errorResponse);
  }

  private buildErrorResponse(
    exception: unknown,
    request: Request,
    requestId?: string,
  ): ErrorResponse {
    const timestamp = new Date().toISOString();
    const path = request.url;

    // Handle our custom BankException (extends HttpException)
    if (exception instanceof HttpException) {
      const response = exception.getResponse();
      const status = exception.getStatus();

      if (typeof response === 'object' && response !== null) {
        const res = response as any;
        return {
          statusCode: status,
          errorCode: res.errorCode || this.getDefaultErrorCode(status),
          message: res.message || exception.message,
          details: res.details,
          timestamp,
          path,
          requestId,
        };
      }

      return {
        statusCode: status,
        errorCode: this.getDefaultErrorCode(status),
        message: typeof response === 'string' ? response : exception.message,
        timestamp,
        path,
        requestId,
      };
    }

    // Handle Prisma errors
    if (exception instanceof Prisma.PrismaClientKnownRequestError) {
      return this.handlePrismaError(exception, timestamp, path, requestId);
    }

    if (exception instanceof Prisma.PrismaClientValidationError) {
      return {
        statusCode: HttpStatus.BAD_REQUEST,
        errorCode: 'VALIDATION_ERROR',
        message: 'Database validation error',
        timestamp,
        path,
        requestId,
      };
    }

    // Unknown errors
    return {
      statusCode: HttpStatus.INTERNAL_SERVER_ERROR,
      errorCode: 'INTERNAL_SERVER_ERROR',
      message: 'An unexpected error occurred',
      timestamp,
      path,
      requestId,
    };
  }

  private handlePrismaError(
    error: Prisma.PrismaClientKnownRequestError,
    timestamp: string,
    path: string,
    requestId?: string,
  ): ErrorResponse {
    switch (error.code) {
      case 'P2002':
        return {
          statusCode: HttpStatus.CONFLICT,
          errorCode: 'DUPLICATE_ENTRY',
          message: `Unique constraint violation on field: ${(error.meta?.target as string[])?.join(', ')}`,
          timestamp,
          path,
          requestId,
        };
      case 'P2025':
        return {
          statusCode: HttpStatus.NOT_FOUND,
          errorCode: 'RECORD_NOT_FOUND',
          message: 'Record not found',
          timestamp,
          path,
          requestId,
        };
      case 'P2003':
        return {
          statusCode: HttpStatus.BAD_REQUEST,
          errorCode: 'FOREIGN_KEY_VIOLATION',
          message: 'Referenced record does not exist',
          timestamp,
          path,
          requestId,
        };
      case 'P2034':
        return {
          statusCode: HttpStatus.CONFLICT,
          errorCode: 'TRANSACTION_CONFLICT',
          message: 'Transaction conflict. Please retry.',
          timestamp,
          path,
          requestId,
        };
      default:
        return {
          statusCode: HttpStatus.INTERNAL_SERVER_ERROR,
          errorCode: `PRISMA_${error.code}`,
          message: 'Database operation failed',
          timestamp,
          path,
          requestId,
        };
    }
  }

  private getDefaultErrorCode(status: HttpStatus): string {
    const codes: Record<number, string> = {
      400: 'BAD_REQUEST',
      401: 'UNAUTHORIZED',
      403: 'FORBIDDEN',
      404: 'NOT_FOUND',
      409: 'CONFLICT',
      422: 'UNPROCESSABLE_ENTITY',
      429: 'TOO_MANY_REQUESTS',
      500: 'INTERNAL_SERVER_ERROR',
    };
    return codes[status] || 'UNKNOWN_ERROR';
  }

  private logError(
    exception: unknown,
    errorResponse: ErrorResponse,
    request: Request,
  ) {
    const { statusCode, message, errorCode } = errorResponse;
    const logContext = {
      method: request.method,
      url: request.url,
      statusCode,
      errorCode,
      userId: (request as any).user?.id,
    };

    if (statusCode >= 500) {
      this.logger.error(
        `[${errorCode}] ${message}`,
        exception instanceof Error ? exception.stack : String(exception),
        JSON.stringify(logContext),
      );
    } else if (statusCode >= 400) {
      this.logger.warn(`[${errorCode}] ${message}`, JSON.stringify(logContext));
    }
  }
}
