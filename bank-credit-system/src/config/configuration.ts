import { registerAs } from '@nestjs/config';

export const appConfig = registerAs('app', () => ({
  nodeEnv: process.env.NODE_ENV || 'development',
  port: parseInt(process.env.PORT ?? '3000', 10),

  name: process.env.APP_NAME || 'BankCreditSystem',
  apiPrefix: process.env.API_PREFIX || 'api/v1',
}));

export const jwtConfig = registerAs('jwt', () => ({
  secret: process.env.JWT_SECRET,
  expiresIn: process.env.JWT_EXPIRES_IN || '15m',
  refreshSecret: process.env.JWT_REFRESH_SECRET,
  refreshExpiresIn: process.env.JWT_REFRESH_EXPIRES_IN || '7d',
}));

export const databaseConfig = registerAs('database', () => ({
  url: process.env.DATABASE_URL,
}));

export const uploadConfig = registerAs('upload', () => ({
  dest: process.env.UPLOAD_DEST || './uploads',
  maxFileSizeMb: parseInt(process.env.MAX_FILE_SIZE_MB ?? '10', 10) || 10,
  allowedMimeTypes: (
    process.env.ALLOWED_MIME_TYPES ||
    'application/pdf,image/jpeg,image/png,image/tiff'
  ).split(','),
}));

export const bcryptConfig = registerAs('bcrypt', () => ({
  saltRounds: parseInt(process.env.BCRYPT_SALT_ROUNDS ?? '12', 10) || 12,
}));

export const throttleConfig = registerAs('throttle', () => ({
  ttl: parseInt(process.env.THROTTLE_TTL ?? '60', 10) || 60,
  limit: parseInt(process.env.THROTTLE_LIMIT ?? '100', 10) || 100,
}));
