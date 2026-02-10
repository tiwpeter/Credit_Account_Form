# Implementation Guide

## 🚀 Quick Start Guide

### Step 1: Setup Environment

```bash
# Clone or create project directory
cd credit-app-system

# Install dependencies
npm install

# Copy environment variables
cp .env.example .env

# Update .env with your values
```

### Step 2: Start Development Server

```bash
npm run dev
```

Visit `http://localhost:3000`

---

## 📝 Implementation Checklist

### ✅ Phase 1: Foundation (COMPLETED)

**Type System**
- [x] Entity types (`types/entities.ts`)
- [x] Form types (`types/form.ts`)
- [x] API types (`types/api.ts`)

**Validation**
- [x] Zod schemas for all 8 steps (`schemas/index.ts`)
- [x] Custom validators (Thai ID, phone, email)
- [x] Business rule validation

**Business Logic**
- [x] Workflow constants (`constants/workflow.ts`)
- [x] Business rules (`constants/business-rules.ts`)
- [x] Master data (`constants/master-data.ts`)

**Utilities**
- [x] Financial calculations (`utils/calculation.ts`)
- [x] DTI calculator
- [x] Eligibility checker
- [x] Payment calculator

---

### 🔨 Phase 2: UI Components (TO IMPLEMENT)

#### Core Form Components

**1. Form Navigation** (`ui/FormNavigation.tsx`)
```typescript
interface FormNavigationProps {
  currentStep: number;
  totalSteps: number;
  onNext: () => void;
  onPrev: () => void;
  canGoNext: boolean;
  canGoPrev: boolean;
}
```

**2. Progress Indicator** (`ui/ProgressIndicator.tsx`)
```typescript
interface ProgressIndicatorProps {
  steps: FormStep[];
  currentStep: number;
  onStepClick?: (step: number) => void;
}
```

**3. Status Badge** (`ui/StatusBadge.tsx`)
```typescript
interface StatusBadgeProps {
  status: ApplicationStatus;
  size?: 'sm' | 'md' | 'lg';
}
```

**4. Document Uploader** (`ui/DocumentUploader.tsx`)
```typescript
interface DocumentUploaderProps {
  documentType: DocumentType;
  onUpload: (file: File) => Promise<void>;
  onRemove: () => void;
  maxSize?: number;
  acceptedFormats?: string[];
}
```

#### Step Components (Template)

```typescript
// Step Template
interface StepProps {
  form: UseFormReturn<StepNFormData>;
  onNext: () => void;
  onPrev?: () => void;
}

export function StepNComponent({ form, onNext, onPrev }: StepProps) {
  const { register, formState: { errors }, watch } = form;
  
  return (
    <div className="space-y-6">
      {/* Step content */}
      
      <div className="flex justify-between pt-6">
        {onPrev && (
          <button onClick={onPrev}>Previous</button>
        )}
        <button onClick={onNext}>Next</button>
      </div>
    </div>
  );
}
```

**Remaining Steps to Implement:**
- [ ] Step 2: Address Information
- [ ] Step 3: Income & Employment
- [ ] Step 4: Credit Details
- [ ] Step 5: Documents
- [ ] Step 6: Guarantors & References
- [ ] Step 7: Company Information
- [ ] Step 8: Review & Submit

---

### 🔌 Phase 3: Hooks (TO IMPLEMENT)

#### 1. `use-credit-form.ts`

```typescript
export function useCreditForm() {
  const [currentStep, setCurrentStep] = useState(1);
  const [formData, setFormData] = useState<CreditApplicationFormData>();
  
  const form = useForm<CreditApplicationFormData>({
    resolver: zodResolver(completeFormSchema),
    mode: 'onBlur'
  });

  const goToNextStep = async () => {
    const stepSchema = getSchemaForStep(currentStep);
    const stepData = getDataForStep(currentStep);
    
    const isValid = await stepSchema.safeParseAsync(stepData);
    if (isValid.success) {
      setCurrentStep(prev => prev + 1);
    }
  };

  const saveDraft = async () => {
    // Auto-save logic
  };

  return {
    form,
    currentStep,
    goToNextStep,
    saveDraft
  };
}
```

#### 2. `use-document-upload.ts`

```typescript
export function useDocumentUpload() {
  const [uploading, setUploading] = useState(false);
  const [progress, setProgress] = useState(0);

  const uploadDocument = async (file: File, type: DocumentType) => {
    setUploading(true);
    try {
      // Upload logic with progress
      const formData = new FormData();
      formData.append('file', file);
      formData.append('type', type);
      
      const response = await axios.post('/api/v1/documents', formData, {
        onUploadProgress: (e) => {
          setProgress(Math.round((e.loaded * 100) / (e.total || 100)));
        }
      });
      
      return response.data;
    } finally {
      setUploading(false);
      setProgress(0);
    }
  };

  return { uploadDocument, uploading, progress };
}
```

#### 3. `use-workflow.ts`

```typescript
export function useWorkflow(applicationId: string) {
  const [status, setStatus] = useState<ApplicationStatus>();
  const [availableActions, setAvailableActions] = useState<WorkflowAction[]>([]);

  const performAction = async (action: string, data: any) => {
    // Workflow action logic
  };

  return { status, availableActions, performAction };
}
```

---

### 📄 Phase 4: Pages (TO IMPLEMENT)

#### 1. Application List Page

**File**: `src/app/applications/page.tsx`

```typescript
export default function ApplicationsPage() {
  const [applications, setApplications] = useState<ApplicationSummary[]>([]);
  const [filters, setFilters] = useState<ApplicationListQuery>({});

  return (
    <div className="container mx-auto px-4 py-8">
      <h1>Credit Applications</h1>
      
      {/* Filters */}
      <div className="filters">
        <select onChange={(e) => setFilters({...filters, status: [e.target.value]})}>
          <option value="">All Status</option>
          {/* Status options */}
        </select>
      </div>

      {/* Application Cards */}
      <div className="grid gap-4">
        {applications.map(app => (
          <ApplicationCard key={app.id} application={app} />
        ))}
      </div>
    </div>
  );
}
```

#### 2. New Application Page

**File**: `src/app/applications/new/page.tsx`

```typescript
export default function NewApplicationPage() {
  const { form, currentStep, goToNextStep, goToPrevStep } = useCreditForm();

  const renderStep = () => {
    switch(currentStep) {
      case 1: return <Step1PersonalInfo form={form} onNext={goToNextStep} />;
      case 2: return <Step2AddressInfo form={form} onNext={goToNextStep} onPrev={goToPrevStep} />;
      // ... more steps
      default: return null;
    }
  };

  return (
    <div className="container mx-auto px-4 py-8">
      <ProgressIndicator currentStep={currentStep} totalSteps={8} />
      <div className="mt-8">
        {renderStep()}
      </div>
    </div>
  );
}
```

#### 3. Application Detail Page

**File**: `src/app/applications/[id]/page.tsx`

```typescript
export default function ApplicationDetailPage({ params }: { params: { id: string } }) {
  const [application, setApplication] = useState<CreditApplication>();
  const [timeline, setTimeline] = useState<TimelineEvent[]>([]);

  return (
    <div className="container mx-auto px-4 py-8">
      <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
        {/* Main Content */}
        <div className="lg:col-span-2">
          <ApplicationDetailsCard application={application} />
          <DocumentsList documents={application?.documents} />
        </div>

        {/* Sidebar */}
        <div>
          <StatusTimeline events={timeline} />
          <WorkflowActions applicationId={params.id} />
        </div>
      </div>
    </div>
  );
}
```

---

### 🔧 Phase 5: API Routes (TO IMPLEMENT)

#### Mock API Implementation

**File**: `src/app/api/v1/applications/route.ts`

```typescript
export async function GET(request: Request) {
  const { searchParams } = new URL(request.url);
  const page = parseInt(searchParams.get('page') || '1');
  const pageSize = parseInt(searchParams.get('pageSize') || '20');

  // Mock data
  const applications: ApplicationSummary[] = [
    // ... mock data
  ];

  return Response.json({
    success: true,
    data: {
      items: applications,
      total: applications.length,
      page,
      pageSize,
      totalPages: Math.ceil(applications.length / pageSize)
    }
  });
}

export async function POST(request: Request) {
  const body = await request.json();
  
  // Validate
  const validated = await createApplicationSchema.parseAsync(body);
  
  // Mock save
  const application = {
    id: generateId(),
    applicationNumber: generateApplicationNumber(),
    ...validated,
    status: ApplicationStatus.DRAFT,
    createdAt: new Date().toISOString()
  };

  return Response.json({
    success: true,
    data: application
  });
}
```

---

### 🎨 Phase 6: Styling & Theme

#### Global Styles

**File**: `src/app/globals.css`

```css
@tailwind base;
@tailwind components;
@tailwind utilities;

@layer base {
  body {
    @apply bg-bg-light text-gray-900;
  }
  
  h1 {
    @apply text-3xl font-bold text-navy-dark;
  }
  
  h2 {
    @apply text-2xl font-semibold text-navy-dark;
  }
  
  h3 {
    @apply text-xl font-semibold text-navy-dark;
  }
}

@layer components {
  .btn-primary {
    @apply px-6 py-3 bg-navy-dark text-white font-semibold rounded-lg 
           hover:bg-navy-medium transition-colors
           focus:outline-none focus:ring-2 focus:ring-gold focus:ring-offset-2;
  }
  
  .btn-secondary {
    @apply px-6 py-3 bg-white text-navy-dark font-semibold rounded-lg border-2 border-navy-dark
           hover:bg-gray-50 transition-colors;
  }
  
  .input-field {
    @apply w-full px-4 py-3 border border-gray-300 rounded-lg
           focus:ring-2 focus:ring-gold focus:border-transparent
           disabled:bg-gray-100 disabled:cursor-not-allowed;
  }
  
  .input-error {
    @apply border-red-500;
  }
  
  .card {
    @apply bg-white rounded-card shadow-card p-6;
  }
  
  .card-hover {
    @apply hover:shadow-card-hover transition-shadow cursor-pointer;
  }
}
```

---

### 📊 Phase 7: State Management

For complex applications, consider adding:

```bash
npm install zustand
```

**Store**: `src/store/application-store.ts`

```typescript
import create from 'zustand';

interface ApplicationStore {
  applications: ApplicationSummary[];
  currentApplication: CreditApplication | null;
  setApplications: (apps: ApplicationSummary[]) => void;
  setCurrentApplication: (app: CreditApplication) => void;
}

export const useApplicationStore = create<ApplicationStore>((set) => ({
  applications: [],
  currentApplication: null,
  setApplications: (apps) => set({ applications: apps }),
  setCurrentApplication: (app) => set({ currentApplication: app })
}));
```

---

### 🧪 Phase 8: Testing

#### Unit Tests

```bash
npm install --save-dev @testing-library/react @testing-library/jest-dom jest
```

**Test**: `src/components/credit/utils/__tests__/calculation.test.ts`

```typescript
import { calculateDTI } from '../calculation';

describe('calculateDTI', () => {
  it('should calculate DTI correctly', () => {
    const result = calculateDTI({
      monthlyIncome: 50000,
      existingLoans: [
        { monthlyPayment: 10000 }
      ],
      monthlyExpenses: 15000
    });

    expect(result.debtToIncomeRatio).toBe(0.20);
    expect(result.isWithinLimit).toBe(true);
  });

  it('should flag DTI over limit', () => {
    const result = calculateDTI({
      monthlyIncome: 50000,
      existingLoans: [
        { monthlyPayment: 30000 }
      ],
      monthlyExpenses: 10000
    });

    expect(result.debtToIncomeRatio).toBe(0.60);
    expect(result.isWithinLimit).toBe(false);
  });
});
```

---

### 🚀 Phase 9: Deployment

#### Production Build

```bash
# Build
npm run build

# Test production build locally
npm start
```

#### Deploy to Vercel

```bash
npm install -g vercel
vercel
```

#### Deploy to AWS/GCP/Azure

1. Build Docker image
2. Push to container registry
3. Deploy to Kubernetes/ECS/App Service

---

## 🎯 Next Steps Priority

### High Priority
1. ✅ Complete Step 2-8 components
2. ✅ Implement form navigation
3. ✅ Add auto-save functionality
4. ✅ Create document uploader

### Medium Priority
5. ✅ Build application list page
6. ✅ Create detail view
7. ✅ Add workflow actions
8. ✅ Implement print view

### Low Priority
9. ⏳ Add loading states
10. ⏳ Error boundaries
11. ⏳ Accessibility improvements
12. ⏳ Performance optimization

---

## 💡 Pro Tips

### 1. Form Validation Best Practices

```typescript
// Use mode: 'onBlur' for better UX
const form = useForm({
  mode: 'onBlur', // Validate on blur, not on every keystroke
  reValidateMode: 'onChange' // Re-validate on change after first error
});
```

### 2. Performance Optimization

```typescript
// Memoize expensive calculations
const eligibility = useMemo(() => 
  checkEligibility(formData),
  [formData.monthlyIncome, formData.requestedAmount]
);
```

### 3. Error Handling

```typescript
// Always handle errors gracefully
try {
  await submitApplication(data);
} catch (error) {
  if (error instanceof ZodError) {
    // Validation error
    showValidationErrors(error);
  } else if (error.response?.status === 409) {
    // Business rule violation
    showBusinessRuleError(error);
  } else {
    // Unexpected error
    showGenericError();
  }
}
```

---

## 📚 Resources

- [Next.js Documentation](https://nextjs.org/docs)
- [React Hook Form](https://react-hook-form.com/)
- [Zod](https://zod.dev/)
- [Tailwind CSS](https://tailwindcss.com/)

---

**Happy Coding! 🚀**
