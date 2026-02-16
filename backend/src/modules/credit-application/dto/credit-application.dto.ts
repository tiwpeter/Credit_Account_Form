import {
  IsString,
  IsEnum,
  IsNumber,
  IsOptional,
  IsNotEmpty,
  Min,
  Max,
  IsEmail,
  IsDateString,
  IsArray,
  ValidateNested,
  IsBoolean,
  MaxLength,
  IsUUID,
  IsPositive,
} from 'class-validator';
import { Type, Transform } from 'class-transformer';
import { ApiProperty, ApiPropertyOptional, PartialType } from '@nestjs/swagger';
import {
  ApplicationType,
  Gender,
  MaritalStatus,
  EmploymentType,
  AddressType,
} from '../../../common/enums';

// ============================================================
// ADDRESS DTO
// ============================================================
export class CreateAddressDto {
  @ApiProperty({ enum: AddressType })
  @IsEnum(AddressType)
  addressType: AddressType;

  @ApiPropertyOptional({ default: false })
  @IsOptional()
  @IsBoolean()
  isPrimary?: boolean;

  @ApiProperty({ example: '123 Sukhumvit Rd' })
  @IsString()
  @IsNotEmpty()
  @MaxLength(200)
  addressLine1: string;

  @ApiPropertyOptional()
  @IsOptional()
  @IsString()
  @MaxLength(200)
  addressLine2?: string;

  @ApiProperty({ example: 'Khlong Toei' })
  @IsString()
  @IsNotEmpty()
  subDistrict: string;

  @ApiProperty({ example: 'Khlong Toei' })
  @IsString()
  @IsNotEmpty()
  district: string;

  @ApiProperty({ example: 'Bangkok' })
  @IsString()
  @IsNotEmpty()
  province: string;

  @ApiProperty({ example: '10110' })
  @IsString()
  @IsNotEmpty()
  postalCode: string;

  @ApiPropertyOptional({ default: 'TH' })
  @IsOptional()
  @IsString()
  country?: string;
}

// ============================================================
// PERSONAL APPLICANT DTO
// ============================================================
export class CreatePersonalApplicantDto {
  @ApiProperty({ example: '1234567890123', description: 'Thai national ID number' })
  @IsString()
  @IsNotEmpty()
  @MaxLength(20)
  nationalId: string;

  @ApiPropertyOptional({ example: 'นาย' })
  @IsOptional()
  @IsString()
  titleName?: string;

  @ApiProperty({ example: 'Somchai' })
  @IsString()
  @IsNotEmpty()
  @MaxLength(100)
  firstName: string;

  @ApiProperty({ example: 'Jaidee' })
  @IsString()
  @IsNotEmpty()
  @MaxLength(100)
  lastName: string;

  @ApiPropertyOptional({ example: 'สมชาย' })
  @IsOptional()
  @IsString()
  firstNameTh?: string;

  @ApiPropertyOptional({ example: 'ใจดี' })
  @IsOptional()
  @IsString()
  lastNameTh?: string;

  @ApiProperty({ example: '1990-06-15', description: 'Date of birth (ISO 8601)' })
  @IsDateString()
  dateOfBirth: string;

  @ApiProperty({ enum: Gender })
  @IsEnum(Gender)
  gender: Gender;

  @ApiProperty({ enum: MaritalStatus })
  @IsEnum(MaritalStatus)
  maritalStatus: MaritalStatus;

  @ApiPropertyOptional({ default: 'TH' })
  @IsOptional()
  @IsString()
  nationality?: string;

  @ApiProperty({ example: '0812345678' })
  @IsString()
  @IsNotEmpty()
  phoneNumber: string;

  @ApiProperty({ example: 'somchai@example.com' })
  @IsEmail()
  email: string;

  @ApiProperty({ enum: EmploymentType })
  @IsEnum(EmploymentType)
  employmentType: EmploymentType;

  @ApiPropertyOptional({ example: 'Bangkok Bank Co., Ltd.' })
  @IsOptional()
  @IsString()
  @MaxLength(200)
  employerName?: string;

  @ApiPropertyOptional({ example: 'Software Engineer' })
  @IsOptional()
  @IsString()
  @MaxLength(100)
  jobTitle?: string;

  @ApiPropertyOptional({ example: 5.5 })
  @IsOptional()
  @IsNumber()
  @Min(0)
  yearsEmployed?: number;

  @ApiProperty({ example: 50000, description: 'Monthly income in THB' })
  @IsNumber()
  @IsPositive()
  @Min(1)
  monthlyIncome: number;

  @ApiPropertyOptional({ example: 5000 })
  @IsOptional()
  @IsNumber()
  @Min(0)
  otherIncome?: number;

  @ApiPropertyOptional({ example: 10000 })
  @IsOptional()
  @IsNumber()
  @Min(0)
  monthlyExpenses?: number;

  @ApiPropertyOptional({ example: 5000, description: 'Sum of all existing monthly debt payments' })
  @IsOptional()
  @IsNumber()
  @Min(0)
  existingDebtPayment?: number;

  @ApiPropertyOptional({ type: [CreateAddressDto] })
  @IsOptional()
  @IsArray()
  @ValidateNested({ each: true })
  @Type(() => CreateAddressDto)
  addresses?: CreateAddressDto[];
}

// ============================================================
// COMPANY / CORPORATE DTO
// ============================================================
export class CreateCompanyDto {
  @ApiProperty({ example: '0105561234567' })
  @IsString()
  @IsNotEmpty()
  registrationNumber: string;

  @ApiProperty({ example: '0105561234567' })
  @IsString()
  @IsNotEmpty()
  taxId: string;

  @ApiProperty({ example: 'ABC Company Limited' })
  @IsString()
  @IsNotEmpty()
  @MaxLength(200)
  companyNameEn: string;

  @ApiPropertyOptional({ example: 'บริษัท เอบีซี จำกัด' })
  @IsOptional()
  @IsString()
  companyNameTh?: string;

  @ApiProperty({ example: 'Manufacturing' })
  @IsString()
  @IsNotEmpty()
  businessType: string;

  @ApiPropertyOptional({ example: '3559' })
  @IsOptional()
  @IsString()
  industryCode?: string;

  @ApiProperty({ example: '2010-01-15' })
  @IsDateString()
  incorporationDate: string;

  @ApiPropertyOptional({ example: 50 })
  @IsOptional()
  @IsNumber()
  @Min(1)
  numberOfEmployees?: number;

  @ApiPropertyOptional({ example: 12000000 })
  @IsOptional()
  @IsNumber()
  @Min(0)
  annualRevenue?: number;

  @ApiPropertyOptional({ example: 1500000 })
  @IsOptional()
  @IsNumber()
  annualNetProfit?: number;

  @ApiPropertyOptional({ example: 20000000 })
  @IsOptional()
  @IsNumber()
  @Min(0)
  totalAssets?: number;

  @ApiPropertyOptional({ example: 5000000 })
  @IsOptional()
  @IsNumber()
  @Min(0)
  totalLiabilities?: number;

  @ApiPropertyOptional({ example: 1000000 })
  @IsOptional()
  @IsNumber()
  @Min(0)
  monthlyRevenue?: number;

  @ApiPropertyOptional({ example: 50000 })
  @IsOptional()
  @IsNumber()
  @Min(0)
  existingDebtPayment?: number;

  @ApiPropertyOptional()
  @IsOptional()
  @IsString()
  phoneNumber?: string;

  @ApiPropertyOptional()
  @IsOptional()
  @IsString()
  website?: string;

  @ApiPropertyOptional()
  @IsOptional()
  @IsEmail()
  email?: string;

  @ApiPropertyOptional({ type: [CreateAddressDto] })
  @IsOptional()
  @IsArray()
  @ValidateNested({ each: true })
  @Type(() => CreateAddressDto)
  addresses?: CreateAddressDto[];
}

export class CreateCorporateApplicantDto {
  @ApiProperty()
  @IsString()
  @IsNotEmpty()
  contactFirstName: string;

  @ApiProperty()
  @IsString()
  @IsNotEmpty()
  contactLastName: string;

  @ApiPropertyOptional()
  @IsOptional()
  @IsString()
  contactTitle?: string;

  @ApiProperty()
  @IsString()
  @IsNotEmpty()
  contactPhone: string;

  @ApiProperty()
  @IsEmail()
  contactEmail: string;

  @ApiProperty()
  @IsString()
  @IsNotEmpty()
  contactNationalId: string;

  @ApiProperty({ type: CreateCompanyDto })
  @ValidateNested()
  @Type(() => CreateCompanyDto)
  company: CreateCompanyDto;
}

// ============================================================
// GUARANTOR DTO
// ============================================================
export class CreateGuarantorDto {
  @ApiProperty()
  @IsString()
  @IsNotEmpty()
  nationalId: string;

  @ApiProperty()
  @IsString()
  @IsNotEmpty()
  firstName: string;

  @ApiProperty()
  @IsString()
  @IsNotEmpty()
  lastName: string;

  @ApiProperty({ example: 'Spouse' })
  @IsString()
  @IsNotEmpty()
  relationship: string;

  @ApiProperty()
  @IsString()
  @IsNotEmpty()
  phoneNumber: string;

  @ApiProperty({ example: 40000 })
  @IsNumber()
  @IsPositive()
  monthlyIncome: number;

  @ApiPropertyOptional({ example: 5000 })
  @IsOptional()
  @IsNumber()
  @Min(0)
  existingDebt?: number;
}

// ============================================================
// CREATE APPLICATION DTO
// ============================================================
export class CreateCreditApplicationDto {
  @ApiProperty({ enum: ApplicationType })
  @IsEnum(ApplicationType)
  type: ApplicationType;

  @ApiProperty({ example: 500000, description: 'Requested loan amount in THB' })
  @IsNumber()
  @IsPositive()
  @Min(10000)
  requestedAmount: number;

  @ApiProperty({ example: 36, description: 'Loan tenure in months' })
  @IsNumber()
  @IsPositive()
  @Min(1)
  @Max(120)
  requestedTenureMonths: number;

  @ApiProperty({ example: 'Home renovation' })
  @IsString()
  @IsNotEmpty()
  @MaxLength(500)
  loanPurpose: string;

  @ApiPropertyOptional({ example: 'Condominium at Sukhumvit valued at 3M THB' })
  @IsOptional()
  @IsString()
  @MaxLength(500)
  collateralDescription?: string;

  @ApiPropertyOptional({ type: CreatePersonalApplicantDto })
  @IsOptional()
  @ValidateNested()
  @Type(() => CreatePersonalApplicantDto)
  personalApplicant?: CreatePersonalApplicantDto;

  @ApiPropertyOptional({ type: CreateCorporateApplicantDto })
  @IsOptional()
  @ValidateNested()
  @Type(() => CreateCorporateApplicantDto)
  corporateApplicant?: CreateCorporateApplicantDto;

  @ApiPropertyOptional({ type: [CreateGuarantorDto] })
  @IsOptional()
  @IsArray()
  @ValidateNested({ each: true })
  @Type(() => CreateGuarantorDto)
  guarantors?: CreateGuarantorDto[];
}

// ============================================================
// UPDATE APPLICATION DTO (All fields optional for draft saving)
// ============================================================
export class UpdateCreditApplicationDto {
  @ApiPropertyOptional({ example: 600000 })
  @IsOptional()
  @IsNumber()
  @IsPositive()
  @Min(10000)
  requestedAmount?: number;

  @ApiPropertyOptional({ example: 48 })
  @IsOptional()
  @IsNumber()
  @IsPositive()
  @Min(1)
  @Max(120)
  requestedTenureMonths?: number;

  @ApiPropertyOptional()
  @IsOptional()
  @IsString()
  @MaxLength(500)
  loanPurpose?: string;

  @ApiPropertyOptional()
  @IsOptional()
  @IsString()
  @MaxLength(500)
  collateralDescription?: string;

  @ApiPropertyOptional({ description: 'Current step in multi-step form (1-5)' })
  @IsOptional()
  @IsNumber()
  @Min(1)
  @Max(5)
  currentStep?: number;

  @ApiPropertyOptional({ description: 'Optimistic locking version' })
  @IsOptional()
  @IsNumber()
  version?: number;
}

// ============================================================
// QUERY / FILTER DTOs
// ============================================================
export class ApplicationQueryDto {
  @ApiPropertyOptional({ enum: ApplicationType })
  @IsOptional()
  @IsEnum(ApplicationType)
  type?: ApplicationType;

  @ApiPropertyOptional({ description: 'Application status filter' })
  @IsOptional()
  @IsString()
  status?: string;

  @ApiPropertyOptional({ description: 'Customer ID (admin/officer use)' })
  @IsOptional()
  @IsUUID()
  customerId?: string;

  @ApiPropertyOptional({ default: 1, minimum: 1 })
  @IsOptional()
  @IsNumber()
  @Min(1)
  @Transform(({ value }) => parseInt(value))
  page?: number = 1;

  @ApiPropertyOptional({ default: 20, minimum: 1, maximum: 100 })
  @IsOptional()
  @IsNumber()
  @Min(1)
  @Max(100)
  @Transform(({ value }) => parseInt(value))
  limit?: number = 20;

  @ApiPropertyOptional({ description: 'Search by application number or applicant name' })
  @IsOptional()
  @IsString()
  search?: string;
}

// ============================================================
// WORKFLOW ACTION DTOs
// ============================================================
export class TransitionStatusDto {
  @ApiProperty({ description: 'Remark for the status transition' })
  @IsString()
  @IsNotEmpty()
  @MaxLength(1000)
  remark: string;
}

export class AssignOfficerDto {
  @ApiProperty({ description: 'ID of the bank officer to assign' })
  @IsUUID()
  @IsNotEmpty()
  officerId: string;
}

export class SubmitApplicationDto {
  @ApiProperty({ description: 'Confirmation that all information is accurate' })
  @IsBoolean()
  confirmAccuracy: boolean;

  @ApiPropertyOptional({ description: 'Additional notes from customer' })
  @IsOptional()
  @IsString()
  @MaxLength(500)
  notes?: string;
}
