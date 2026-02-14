# 🏦 Bank Credit Application System

A **production-grade** backend for a bank-level credit application system built with NestJS, TypeScript, PostgreSQL, and Prisma ORM.

---

## 📋 Table of Contents
- [Architecture Overview](#architecture-overview)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [Workflow State Machine](#workflow-state-machine)
- [Business Rules](#business-rules)
- [API Reference](#api-reference)
- [Quick Start](#quick-start)
- [Security Model](#security-model)
- [Database Schema](#database-schema)
- [Credit Analysis Engine](#credit-analysis-engine)

---

## 🏗 Architecture Overview

```
┌─────────────────────────────────────────────────────────────┐
│                      API Gateway Layer                       │
│        Rate Limiting │ JWT Auth │ RBAC │ Request ID         │
├──────────────┬────────────────────┬───────────────────────────┤
│   Auth       │  Credit Application│  Credit Analysis          │
│   Module     │  Module            │  Module                   │
│              │                    │  - DTI Calculation        │
│  JWT Tokens  │  - DRAFT           │  - Eligibility Check      │
│  Bcrypt hash │  - Multi-step save │  - Risk Grading           │
│  Refresh tok │  - Status workflow │  - Interest Rate          │
├──────────────┴────────────────────┴───────────────────────────┤
│  Approval Module  │  Document Module  │  Workflow Module       │
│  - Decisions      │  - Multipart      │  - State Machine       │
│  - Conditions     │  - OCR Metadata   │  - Transition Rules    │
│  - Interest rates │  - Verification   │  - Audit Trail         │
├─────────────────────────────────────────────────────────────┤
│              Audit Log Module (Global)                       │
│         Every action logged with before/after state         │
├─────────────────────────────────────────────────────────────┤
│              Prisma ORM + PostgreSQL                         │
└─────────────────────────────────────────────────────────────┘
```

---

## 🛠 Tech Stack

| Component | Technology |
|-----------|-----------|
| Runtime | Node.js 20 |
| Framework | NestJS 10 |
| Language | TypeScript 5 |
| Database | PostgreSQL 16 |
| ORM | Prisma 5 |
| Authentication | JWT (access + refresh tokens) |
| Password Hashing | bcrypt (12 rounds) |
| Validation | class-validator + class-transformer |
| Documentation | Swagger / OpenAPI 3 |
| Rate Limiting | @nestjs/throttler |
| Security Headers | helmet |
| Containerization | Docker + Docker Compose |

---

## 📁 Project Structure

```
src/
├── common/
│   ├── decorators/          # @Roles, @CurrentUser, @Public, @Auth
│   ├── enums/               # ApplicationStatus, RoleCode, DocumentType, etc.
│   ├── exceptions/          # Custom exceptions (InvalidTransition, DTI, etc.)
│   ├── filters/             # GlobalExceptionFilter
│   ├── guards/              # JwtAuthGuard, RolesGuard
│   └── interceptors/        # ResponseTransformInterceptor
├── config/                  # Configuration (JWT, DB, upload, bcrypt)
└── modules/
    ├── auth/                # Login, register, refresh, logout
    ├── credit-application/  # Core CRUD + workflow transitions
    ├── credit-analysis/     # DTI, eligibility, risk grading
    ├── approval/            # Approval decisions
    ├── document/            # File upload, verification, OCR
    ├── workflow/            # State machine rules
    ├── audit-log/           # Comprehensive audit trail
    ├── prisma/              # Database service
    └── seed/                # Seed data
prisma/
└── schema.prisma            # Full normalized schema
```

---

## 🔄 Workflow State Machine

```
              Customer submits
  ┌──────┐  ────────────────►  ┌───────────┐
  │DRAFT │                     │ SUBMITTED │
  └──────┘                     └─────┬─────┘
                                     │ Officer begins review
                                     ▼
                              ┌──────────────┐
                              │DOCUMENT_CHECK│◄─── NEED_MORE_INFO
                              └──────┬───────┘         ▲
                                     │ All docs verified│
                                     ▼                  │
                              ┌───────────────┐         │
                              │CREDIT_ANALYSIS│─────────┘
                              └──────┬────────┘
                                     │ Analysis complete
                                     ▼
                              ┌──────────────┐
                              │   APPROVAL   │
                              └──────┬───────┘
                         ┌──────────┼──────────┐
                         ▼          ▼           ▼
                     ┌────────┐ ┌────────┐ ┌──────────────┐
                     │APPROVED│ │REJECTED│ │NEED_MORE_INFO│
                     └───┬────┘ └────────┘ └──────────────┘
                         │
                         ▼
                  ┌──────────────┐
                  │CONTRACT_SIGNED│
                  └──────┬───────┘
                         │
                         ▼
                    ┌──────────┐
                    │ DISBURSED│
                    └──────────┘
```

**Rules:**
- Every transition validates role permissions
- Every transition creates an immutable `ApplicationStatusHistory` record
- Every transition creates an `AuditLog` record with full context
- Invalid transitions throw `InvalidStatusTransitionException` (HTTP 422)

---

## 📊 Business Rules

| Rule | Value |
|------|-------|
| Maximum DTI | ≤ 50% |
| Minimum monthly income | 15,000 THB |
| Personal loan max amount | 5× monthly income |
| Personal loan max tenure | 60 months |
| SME loan max tenure | 120 months |
| Max failed login attempts | 5 (then account locked) |
| JWT access token expiry | 15 minutes |
| JWT refresh token expiry | 7 days |

### Interest Rates by Risk Grade

| Grade | Score | Personal | Corporate |
|-------|-------|----------|-----------|
| A+ | ≥85 | 4.5% | 3.5% |
| A | ≥72 | 6.5% | 5.5% |
| B+ | ≥60 | 8.5% | 7.5% |
| B | ≥48 | 11.0% | 9.5% |
| C | ≥35 | 15.0% | 13.0% |
| D | <35 | **REJECT** | **REJECT** |

---

## 🚀 Quick Start

### Prerequisites
- Node.js 20+
- PostgreSQL 16+
- Docker (optional)

### Option A: Docker Compose (Recommended)
```bash
git clone <repo>
cd bank-credit-system
cp .env.example .env
docker-compose up -d
docker-compose exec api npx prisma migrate dev
docker-compose exec api npx ts-node src/modules/seed/seed.ts
```

### Option B: Local Development
```bash
# Install dependencies
npm install

# Set up environment
cp .env.example .env
# Edit .env with your database credentials

# Run migrations
npx prisma migrate dev --name init

# Generate Prisma client
npx prisma generate

# Seed database
npm run prisma:seed

# Start development server
npm run start:dev
```

### Option C: Local Development with SQLite (fast, zero-config)
If you prefer a file-based SQLite DB for quick local development (no PostgreSQL required), run:

```bash
# generate a SQLite-friendly schema and Prisma client, then push to local dev.db
npm run prisma:dev

# open Prisma Studio to inspect data
npm run prisma:studio
```

The `prisma:dev` script generates `prisma/schema.dev.prisma` from your main schema by switching the datasource to SQLite and stripping provider-specific native annotations, then runs `prisma generate` and `prisma db push` against `prisma/schema.dev.prisma`.

### Access Points
- **API**: http://localhost:3000/api/v1
- **Swagger Docs**: http://localhost:3000/docs
- **Prisma Studio**: `npx prisma studio`

---

## 🔑 API Reference

### Authentication
```
POST   /auth/login               - Login (returns JWT)
POST   /auth/register            - Customer registration
POST   /auth/refresh             - Refresh access token
POST   /auth/logout              - Logout (revoke token)
POST   /auth/change-password     - Change password
```

### Credit Applications
```
GET    /credit-applications                    - List (paginated, role-filtered)
POST   /credit-applications                    - Create draft
GET    /credit-applications/:id                - Get details with history
PUT    /credit-applications/:id                - Update draft
DELETE /credit-applications/:id                - Soft delete draft

# Workflow Transitions
POST   /credit-applications/:id/submit                - DRAFT → SUBMITTED
POST   /credit-applications/:id/start-document-check  - SUBMITTED → DOCUMENT_CHECK
POST   /credit-applications/:id/start-credit-analysis - DOC_CHECK → CREDIT_ANALYSIS
POST   /credit-applications/:id/send-for-approval     - ANALYSIS → APPROVAL
POST   /credit-applications/:id/request-more-info     - → NEED_MORE_INFO
POST   /credit-applications/:id/reject-at-analysis    - → REJECTED
POST   /credit-applications/:id/sign-contract         - APPROVED → CONTRACT_SIGNED
POST   /credit-applications/:id/disburse              - CONTRACT → DISBURSED
GET    /credit-applications/:id/available-transitions - Current user's valid actions
PATCH  /credit-applications/:id/assign-officer        - Assign bank officer
```

### Documents
```
GET    /credit-applications/:id/documents              - List documents
POST   /credit-applications/:id/documents              - Upload (multipart)
PATCH  /credit-applications/:id/documents/:docId/verify - Verify/reject
DELETE /credit-applications/:id/documents/:docId        - Soft delete
```

### Credit Analysis
```
POST   /credit-applications/:id/credit-analysis        - Run analysis
GET    /credit-applications/:id/credit-analysis        - Get results
```

### Approvals
```
POST   /credit-applications/:id/approvals              - Make decision
GET    /credit-applications/:id/approvals              - Approval history
```

---

## 🔒 Security Model

### Authentication Flow
```
Client → POST /auth/login
       ← { accessToken (15m), refreshToken (7d), user }
       
Client → GET /endpoint (Authorization: Bearer <accessToken>)
       ← data OR 401 if expired

Client → POST /auth/refresh { refreshToken }
       ← { accessToken (new 15m) }
```

### RBAC Matrix

| Action | CUSTOMER | BANK_OFFICER | APPROVER | ADMIN |
|--------|----------|--------------|----------|-------|
| View own applications | ✅ | ❌ | ❌ | ✅ |
| View all applications | ❌ | ✅ | ✅ | ✅ |
| Create/edit draft | ✅ | ❌ | ❌ | ✅ |
| Submit application | ✅ | ❌ | ❌ | ✅ |
| Upload documents | ✅ | ✅ | ❌ | ✅ |
| Verify documents | ❌ | ✅ | ❌ | ✅ |
| Run credit analysis | ❌ | ✅ | ❌ | ✅ |
| Make approval decision | ❌ | ❌ | ✅ | ✅ |
| Disburse loan | ❌ | ✅ | ❌ | ✅ |

### Security Features
- **Password policy**: min 8 chars, uppercase + lowercase + number + special char
- **Account lockout**: Locks after 5 failed login attempts
- **Refresh token rotation**: Password change revokes all refresh tokens
- **Soft deletes**: No financial data is hard-deleted
- **Optimistic locking**: Version field prevents concurrent edit conflicts
- **Input sanitization**: `whitelist: true` strips undeclared fields
- **Request ID tracing**: Every request tagged with UUID for log correlation

---

## 🗄 Database Schema Highlights

- **Fully normalized** - No data duplication
- **Soft deletes** - `deletedAt` on critical tables
- **Audit timestamps** - `createdAt` / `updatedAt` on all tables
- **Status indexing** - All status fields indexed for performance
- **Foreign keys** - Referential integrity enforced at DB level
- **Decimal precision** - `DECIMAL(15,2)` for all monetary values
- **JSON columns** - OCR metadata and analysis factors stored as flexible JSON
- **Optimistic locking** - `version` column on `CreditApplication`

---

## 📈 Credit Analysis Engine

The `CreditAnalysisService` implements a **5-factor weighted scoring model**:

| Factor | Weight | Description |
|--------|--------|-------------|
| DTI Ratio | 30% | Lower DTI = higher score |
| Loan-to-Income | 25% | Request vs annual income |
| Employment Stability | 20% | Type + years of employment |
| Tenure Risk | 15% | Months requested vs maximum |
| Income Level | 10% | Absolute income classification |

**Scoring:** 0–100 → Maps to credit score 300–850 → Determines risk grade → Sets interest rate

---

## 📊 Example API Responses

### Successful Login
```json
{
  "success": true,
  "data": {
    "accessToken": "eyJhbGciOiJIUzI1NiJ9...",
    "refreshToken": "eyJhbGciOiJIUzI1NiJ9...",
    "expiresIn": 900,
    "user": {
      "id": "uuid",
      "email": "customer@email.com",
      "roles": ["CUSTOMER"]
    }
  },
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### Invalid Workflow Transition
```json
{
  "statusCode": 422,
  "errorCode": "INVALID_STATUS_TRANSITION",
  "message": "Invalid status transition from 'DRAFT' to 'APPROVAL': This transition is not defined in the workflow",
  "details": {
    "fromStatus": "DRAFT",
    "toStatus": "APPROVAL"
  },
  "timestamp": "2024-01-15T10:30:00Z",
  "path": "/api/v1/credit-applications/uuid/send-for-approval"
}
```

### DTI Exceeded
```json
{
  "statusCode": 422,
  "errorCode": "DTI_EXCEEDED",
  "message": "Debt-to-Income ratio 65.00% exceeds maximum allowed 50.00%",
  "details": { "dti": 0.65, "maxDti": 0.50 }
}
```

---

## 🧪 Running Tests

```bash
npm run test           # Unit tests
npm run test:cov       # Coverage report
npm run test:e2e       # End-to-end tests
```
