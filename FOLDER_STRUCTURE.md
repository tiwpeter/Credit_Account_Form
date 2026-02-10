# Credit Application System - Folder Structure

```
credit-app-system/
├── README.md
├── package.json
├── tsconfig.json
├── tailwind.config.ts
├── next.config.js
├── .env.example
│
├── public/
│   └── assets/
│
├── src/
│   ├── app/
│   │   ├── layout.tsx
│   │   ├── page.tsx
│   │   ├── globals.css
│   │   │
│   │   ├── applications/
│   │   │   ├── page.tsx                    # Application list
│   │   │   ├── new/
│   │   │   │   └── page.tsx                # Multi-step form wizard
│   │   │   └── [id]/
│   │   │       ├── page.tsx                # Application detail
│   │   │       └── print/
│   │   │           └── page.tsx            # Printable view
│   │   │
│   │   └── api/
│   │       └── v1/
│   │           ├── applications/
│   │           │   └── route.ts            # Mock API endpoint
│   │           └── master-data/
│   │               └── route.ts
│   │
│   ├── components/
│   │   ├── credit/
│   │   │   ├── constants/
│   │   │   │   ├── workflow.ts
│   │   │   │   ├── business-rules.ts
│   │   │   │   └── master-data.ts
│   │   │   │
│   │   │   ├── types/
│   │   │   │   ├── entities.ts
│   │   │   │   ├── form.ts
│   │   │   │   ├── api.ts
│   │   │   │   └── index.ts
│   │   │   │
│   │   │   ├── schemas/
│   │   │   │   ├── step1-schema.ts
│   │   │   │   ├── step2-schema.ts
│   │   │   │   ├── step3-schema.ts
│   │   │   │   ├── step4-schema.ts
│   │   │   │   ├── step5-schema.ts
│   │   │   │   ├── step6-schema.ts
│   │   │   │   ├── step7-schema.ts
│   │   │   │   ├── step8-schema.ts
│   │   │   │   └── index.ts
│   │   │   │
│   │   │   ├── utils/
│   │   │   │   ├── validation.ts
│   │   │   │   ├── calculation.ts
│   │   │   │   ├── formatting.ts
│   │   │   │   └── workflow.ts
│   │   │   │
│   │   │   ├── hooks/
│   │   │   │   ├── use-credit-form.ts
│   │   │   │   ├── use-application-status.ts
│   │   │   │   ├── use-document-upload.ts
│   │   │   │   └── use-workflow.ts
│   │   │   │
│   │   │   ├── ui/
│   │   │   │   ├── ApplicationCard.tsx
│   │   │   │   ├── StatusBadge.tsx
│   │   │   │   ├── StatusTimeline.tsx
│   │   │   │   ├── DocumentUploader.tsx
│   │   │   │   ├── WorkflowActions.tsx
│   │   │   │   ├── FormNavigation.tsx
│   │   │   │   └── ProgressIndicator.tsx
│   │   │   │
│   │   │   └── steps/
│   │   │       ├── Step1PersonalInfo.tsx
│   │   │       ├── Step2AddressInfo.tsx
│   │   │       ├── Step3IncomeEmployment.tsx
│   │   │       ├── Step4CreditDetails.tsx
│   │   │       ├── Step5Documents.tsx
│   │   │       ├── Step6Guarantors.tsx
│   │   │       ├── Step7CompanyInfo.tsx    # For corporate
│   │   │       └── Step8Review.tsx
│   │   │
│   │   └── ui/
│   │       ├── button.tsx
│   │       ├── input.tsx
│   │       ├── select.tsx
│   │       ├── textarea.tsx
│   │       ├── card.tsx
│   │       ├── badge.tsx
│   │       ├── dialog.tsx
│   │       └── form.tsx
│   │
│   ├── lib/
│   │   ├── api-client.ts
│   │   ├── auth.ts
│   │   └── utils.ts
│   │
│   └── styles/
│       └── theme.ts
│
└── docs/
    ├── ER_DIAGRAM.md
    ├── API_SPEC.md
    ├── WORKFLOW.md
    └── DEPLOYMENT.md
```

## Key Design Principles

### 1. Domain-Driven Structure
- All credit-related logic under `components/credit/`
- Clear separation: constants, types, schemas, utils, hooks, UI
- Easy to locate and modify business logic

### 2. Type Safety
- Comprehensive TypeScript interfaces for all entities
- Zod schemas for runtime validation
- Strict null checks enabled

### 3. Separation of Concerns
- UI components are presentational
- Business logic in utils and hooks
- Validation schemas separate from components
- API types separate from form types

### 4. Scalability
- Modular step components
- Reusable UI components
- Easy to add new steps or fields
- Mock API ready for backend integration

### 5. Production Ready
- Error boundaries
- Loading states
- Accessibility compliance
- Mobile-responsive
- Print-friendly layouts
