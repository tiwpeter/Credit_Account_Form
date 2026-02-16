-- CreateEnum
CREATE TYPE "UserStatus" AS ENUM ('ACTIVE', 'INACTIVE', 'SUSPENDED', 'LOCKED');
CREATE TYPE "RoleCode" AS ENUM ('CUSTOMER', 'BANK_OFFICER', 'APPROVER', 'ADMIN');
CREATE TYPE "ApplicationType" AS ENUM ('PERSONAL_LOAN', 'CORPORATE_LOAN');
CREATE TYPE "ApplicationStatus" AS ENUM ('DRAFT', 'SUBMITTED', 'DOCUMENT_CHECK', 'CREDIT_ANALYSIS', 'APPROVAL', 'APPROVED', 'REJECTED', 'NEED_MORE_INFO', 'CONTRACT_SIGNED', 'DISBURSED', 'CANCELLED');
CREATE TYPE "Gender" AS ENUM ('MALE', 'FEMALE', 'OTHER');
CREATE TYPE "MaritalStatus" AS ENUM ('SINGLE', 'MARRIED', 'DIVORCED', 'WIDOWED');
CREATE TYPE "EmploymentType" AS ENUM ('EMPLOYED', 'SELF_EMPLOYED', 'BUSINESS_OWNER', 'RETIRED', 'UNEMPLOYED');
CREATE TYPE "DocumentType" AS ENUM ('NATIONAL_ID', 'PASSPORT', 'INCOME_STATEMENT', 'BANK_STATEMENT', 'TAX_RETURN', 'BUSINESS_REGISTRATION', 'FINANCIAL_STATEMENT', 'COLLATERAL_DOCS', 'CREDIT_REPORT', 'GUARANTOR_ID', 'OTHER');
CREATE TYPE "DocumentStatus" AS ENUM ('PENDING', 'VERIFIED', 'REJECTED', 'EXPIRED');
CREATE TYPE "ApprovalDecision" AS ENUM ('APPROVED', 'REJECTED', 'NEED_MORE_INFO', 'ESCALATED');
CREATE TYPE "RiskGrade" AS ENUM ('A_PLUS', 'A', 'B_PLUS', 'B', 'C', 'D');
CREATE TYPE "AddressType" AS ENUM ('HOME', 'WORK', 'MAILING', 'REGISTERED');
CREATE TYPE "AuditAction" AS ENUM ('CREATE', 'UPDATE', 'DELETE', 'STATUS_CHANGE', 'LOGIN', 'LOGOUT', 'DOCUMENT_UPLOAD', 'DOCUMENT_VERIFY', 'CREDIT_ANALYSIS', 'APPROVAL_DECISION', 'DATA_ACCESS');

-- CreateTable: users
CREATE TABLE "users" (
    "id" TEXT NOT NULL,
    "email" TEXT NOT NULL,
    "password" TEXT NOT NULL,
    "first_name" TEXT NOT NULL,
    "last_name" TEXT NOT NULL,
    "phone_number" TEXT,
    "status" "UserStatus" NOT NULL DEFAULT 'ACTIVE',
    "failed_login_count" INTEGER NOT NULL DEFAULT 0,
    "last_login_at" TIMESTAMP(3),
    "password_changed_at" TIMESTAMP(3),
    "must_change_password" BOOLEAN NOT NULL DEFAULT false,
    "created_at" TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updated_at" TIMESTAMP(3) NOT NULL,
    "deleted_at" TIMESTAMP(3),
    CONSTRAINT "users_pkey" PRIMARY KEY ("id")
);

-- Indexes
CREATE UNIQUE INDEX "users_email_key" ON "users"("email");
CREATE INDEX "users_status_idx" ON "users"("status");
CREATE INDEX "users_deleted_at_idx" ON "users"("deleted_at");

-- CreateTable: roles
CREATE TABLE "roles" (
    "id" TEXT NOT NULL,
    "code" "RoleCode" NOT NULL,
    "name" TEXT NOT NULL,
    "description" TEXT,
    "created_at" TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updated_at" TIMESTAMP(3) NOT NULL,
    CONSTRAINT "roles_pkey" PRIMARY KEY ("id")
);

CREATE UNIQUE INDEX "roles_code_key" ON "roles"("code");
CREATE UNIQUE INDEX "roles_name_key" ON "roles"("name");

-- CreateTable: user_roles
CREATE TABLE "user_roles" (
    "id" TEXT NOT NULL,
    "user_id" TEXT NOT NULL,
    "role_id" TEXT NOT NULL,
    "granted_at" TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "granted_by" TEXT,
    CONSTRAINT "user_roles_pkey" PRIMARY KEY ("id")
);

CREATE UNIQUE INDEX "user_roles_user_id_role_id_key" ON "user_roles"("user_id", "role_id");
CREATE INDEX "user_roles_user_id_idx" ON "user_roles"("user_id");

-- CreateTable: refresh_tokens
CREATE TABLE "refresh_tokens" (
    "id" TEXT NOT NULL,
    "user_id" TEXT NOT NULL,
    "token" TEXT NOT NULL,
    "expires_at" TIMESTAMP(3) NOT NULL,
    "is_revoked" BOOLEAN NOT NULL DEFAULT false,
    "created_at" TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "ip_address" TEXT,
    "user_agent" TEXT,
    CONSTRAINT "refresh_tokens_pkey" PRIMARY KEY ("id")
);

CREATE UNIQUE INDEX "refresh_tokens_token_key" ON "refresh_tokens"("token");
CREATE INDEX "refresh_tokens_user_id_idx" ON "refresh_tokens"("user_id");

-- CreateTable: credit_applications
CREATE TABLE "credit_applications" (
    "id" TEXT NOT NULL,
    "application_number" TEXT NOT NULL,
    "type" "ApplicationType" NOT NULL,
    "status" "ApplicationStatus" NOT NULL DEFAULT 'DRAFT',
    "customer_id" TEXT NOT NULL,
    "assigned_officer_id" TEXT,
    "requested_amount" DECIMAL(15,2) NOT NULL,
    "requested_tenure_months" INTEGER NOT NULL,
    "loan_purpose" TEXT NOT NULL,
    "collateral_description" TEXT,
    "approved_amount" DECIMAL(15,2),
    "approved_tenure_months" INTEGER,
    "approved_interest_rate" DECIMAL(5,4),
    "approval_conditions" TEXT,
    "submitted_at" TIMESTAMP(3),
    "document_check_start_at" TIMESTAMP(3),
    "credit_analysis_start_at" TIMESTAMP(3),
    "approval_start_at" TIMESTAMP(3),
    "decision_at" TIMESTAMP(3),
    "contract_signed_at" TIMESTAMP(3),
    "disbursed_at" TIMESTAMP(3),
    "disbursed_amount" DECIMAL(15,2),
    "rejection_reason" TEXT,
    "internal_note" TEXT,
    "current_step" INTEGER NOT NULL DEFAULT 1,
    "version" INTEGER NOT NULL DEFAULT 1,
    "created_at" TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updated_at" TIMESTAMP(3) NOT NULL,
    "deleted_at" TIMESTAMP(3),
    CONSTRAINT "credit_applications_pkey" PRIMARY KEY ("id")
);

CREATE UNIQUE INDEX "credit_applications_application_number_key" ON "credit_applications"("application_number");
CREATE INDEX "credit_applications_status_idx" ON "credit_applications"("status");
CREATE INDEX "credit_applications_customer_id_idx" ON "credit_applications"("customer_id");
CREATE INDEX "credit_applications_type_idx" ON "credit_applications"("type");
CREATE INDEX "credit_applications_created_at_idx" ON "credit_applications"("created_at");

-- CreateTable: audit_logs
CREATE TABLE "audit_logs" (
    "id" TEXT NOT NULL,
    "user_id" TEXT,
    "action" "AuditAction" NOT NULL,
    "entity_type" TEXT NOT NULL,
    "entity_id" TEXT NOT NULL,
    "application_id" TEXT,
    "before_state" JSONB,
    "after_state" JSONB,
    "changed_fields" TEXT[],
    "remark" TEXT,
    "ip_address" TEXT,
    "user_agent" TEXT,
    "request_id" TEXT,
    "created_at" TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT "audit_logs_pkey" PRIMARY KEY ("id")
);

CREATE INDEX "audit_logs_user_id_idx" ON "audit_logs"("user_id");
CREATE INDEX "audit_logs_entity_type_entity_id_idx" ON "audit_logs"("entity_type", "entity_id");
CREATE INDEX "audit_logs_application_id_idx" ON "audit_logs"("application_id");
CREATE INDEX "audit_logs_action_idx" ON "audit_logs"("action");
CREATE INDEX "audit_logs_created_at_idx" ON "audit_logs"("created_at");

-- Foreign Keys
ALTER TABLE "user_roles" ADD CONSTRAINT "user_roles_user_id_fkey" FOREIGN KEY ("user_id") REFERENCES "users"("id") ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE "user_roles" ADD CONSTRAINT "user_roles_role_id_fkey" FOREIGN KEY ("role_id") REFERENCES "roles"("id") ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE "refresh_tokens" ADD CONSTRAINT "refresh_tokens_user_id_fkey" FOREIGN KEY ("user_id") REFERENCES "users"("id") ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE "credit_applications" ADD CONSTRAINT "credit_applications_customer_id_fkey" FOREIGN KEY ("customer_id") REFERENCES "users"("id") ON UPDATE CASCADE;
ALTER TABLE "credit_applications" ADD CONSTRAINT "credit_applications_assigned_officer_id_fkey" FOREIGN KEY ("assigned_officer_id") REFERENCES "users"("id") ON UPDATE CASCADE;
