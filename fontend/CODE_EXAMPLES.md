# Code Examples - Credit Application System

## 1. Using the Validation Schemas

### Validating Step 1 Data

```typescript
import { Step1PersonalInfoSchema, type Step1PersonalInfoInput } from '@/components/credit/schemas/step-schemas';

const formData: Step1PersonalInfoInput = {
  title: 'Mr',
  firstName: 'Somchai',
  lastName: 'Noomsai',
  gender: 'MALE',
  dateOfBirth: '1985-06-15',
  nationality: 'Thai',
  idCardNumber: '1234567890123', // Must be valid Thai ID (13 digits with checksum)
  maritalStatus: 'MARRIED',
  dependents: 2,
  mobilePhone: '0891234567', // Must be valid Thai phone
  email: 'somchai@example.com',
};

// Validate
const result = await Step1PersonalInfoSchema.parseAsync(formData);
console.log('Valid:', result);

// Or use safe parsing
const safeParse = Step1PersonalInfoSchema.safeParse(formData);
if (!safeParse.success) {
  console.error('Validation errors:', safeParse.error.flatten());
}
```

## 2. Using Business Rules

### Checking Eligibility

```typescript
import { checkEligibility, calculateDTI, getRiskGradeByDTI } from '@/components/credit/utils/calculation';
import { BUSINESS_RULES } from '@/components/credit/constants/business-rules';

const applicantData = {
  monthlyIncome: 50000,
  monthlyDebt: 15000,
  age: 35,
  employmentMonths: 24,
  loanType: 'PERSONAL',
};

// Check eligibility
const eligibility = checkEligibility(
  applicantData.monthlyIncome,
  applicantData.monthlyDebt,
  applicantData.age,
  applicantData.employmentMonths,
  applicantData.loanType
);

console.log('Eligible:', eligibility.isEligible); // true or false
console.log('Reasons:', eligibility.reasons); // Any rejection reasons

// Calculate DTI
const dti = calculateDTI(applicantData.monthlyIncome, applicantData.monthlyDebt);
console.log(`DTI: ${dti.toFixed(2)}%`); // e.g., "30.00%"

// Get risk grade
const riskGrade = getRiskGradeByDTI(dti);
console.log(`Risk Grade: ${riskGrade}`); // EXCELLENT, GOOD, ACCEPTABLE, FAIR, or POOR
```

### Getting Interest Rate by Risk Grade

```typescript
import { getInterestRateByRiskGrade } from '@/components/credit/utils/calculation';

const riskGrade = 'GOOD';
const annualRate = getInterestRateByRiskGrade(riskGrade);
console.log(`Interest Rate: ${(annualRate * 100).toFixed(2)}%`); // e.g., "4.00%"
```

## 3. Financial Calculations

### Calculating Monthly Payment

```typescript
import { 
  calculateMonthlyPayment, 
  calculateTotalInterest,
  generateAmortizationSchedule 
} from '@/components/credit/utils/calculation';

const loanDetails = {
  principal: 500000, // ฿500,000
  annualRate: 0.045, // 4.5% per annum
  tenorMonths: 60, // 5 years
};

// Calculate monthly payment
const monthlyPayment = calculateMonthlyPayment(
  loanDetails.principal,
  loanDetails.annualRate,
  loanDetails.tenorMonths
);
console.log(`Monthly Payment: ฿${monthlyPayment.toFixed(2)}`);

// Calculate total interest
const totalInterest = calculateTotalInterest(
  monthlyPayment,
  loanDetails.tenorMonths,
  loanDetails.principal
);
console.log(`Total Interest: ฿${totalInterest.toFixed(2)}`);

// Generate amortization schedule
const schedule = generateAmortizationSchedule(
  loanDetails.principal,
  loanDetails.annualRate,
  loanDetails.tenorMonths
);

// First 3 months
schedule.slice(0, 3).forEach(row => {
  console.log(`Month ${row.month}: Payment ฿${row.payment.toFixed(2)}, Interest ฿${row.interest.toFixed(2)}, Balance ฿${row.balance.toFixed(2)}`);
});
```

## 4. Loan Amount and Tenor Validation

### Validating Loan Limits

```typescript
import { validateLoanAmount, validateTenor } from '@/components/credit/utils/calculation';
import { LOAN_TYPE_CONFIGS } from '@/components/credit/constants/business-rules';

// Check loan amount
const amountCheck = validateLoanAmount(1500000, 'PERSONAL');
if (!amountCheck.isValid) {
  console.error(amountCheck.message);
  // Output: "Maximum loan amount for Personal Loan: ฿1,000,000"
}

// Check tenor
const tenorCheck = validateTenor(24, 'AUTO');
if (!tenorCheck.isValid) {
  console.error(tenorCheck.message);
  // Output: "Minimum tenor for Auto Loan: 36 months"
}

// Get loan limits
const loanConfig = LOAN_TYPE_CONFIGS['HOME'];
console.log(`Home Loan: ฿${loanConfig.minAmount} - ฿${loanConfig.maxAmount}`);
console.log(`Tenor: ${loanConfig.minTenor} - ${loanConfig.maxTenor} months`);
console.log(`Requires Guarantor: ${loanConfig.requireGuarantor}`);
```

## 5. Using the API

### Create Application

```typescript
const response = await fetch('/api/v1/applications', {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify({
    customerId: 'CUST-001',
    loanType: 'PERSONAL',
    loanAmount: 500000,
    step1Data: {
      title: 'Mr',
      firstName: 'Somchai',
      lastName: 'Noomsai',
      gender: 'MALE',
      dateOfBirth: '1985-06-15',
      nationality: 'Thai',
      idCardNumber: '1234567890123',
      maritalStatus: 'MARRIED',
      dependents: 2,
      mobilePhone: '0891234567',
      email: 'somchai@example.com',
    },
  }),
});

const result = await response.json();
if (result.success) {
  console.log('Application Created:', result.data.applicationNumber);
  console.log('Application ID:', result.data.id);
}
```

### Get Applications

```typescript
const response = await fetch('/api/v1/applications?page=1&pageSize=10&status=SUBMITTED');
const result = await response.json();

if (result.success) {
  console.log('Total Applications:', result.data.total);
  console.log('Page Info:', {
    current: result.data.page,
    totalPages: result.data.totalPages,
    itemsPerPage: result.data.pageSize,
  });

  result.data.items.forEach(app => {
    console.log(`${app.applicationNumber}: ${app.personalInfo.firstName} ${app.personalInfo.lastName} - ${app.status}`);
  });
}
```

### Get Master Data

```typescript
const response = await fetch('/api/v1/master-data');
const result = await response.json();

if (result.success) {
  const { provinces, districts, loanPurposes, industries } = result.data;

  // Use in dropdowns
  console.log('Provinces:', provinces.map(p => ({ label: p.name, value: p.code })));
  console.log('Loan Purposes:', loanPurposes.map(p => ({ label: p.label, value: p.code })));
  console.log('Industries:', industries.map(i => ({ label: i.label, value: i.code })));
}
```

## 6. React Hook Form Integration

### Using with Step 1 Component

```typescript
'use client';

import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import Step1PersonalInfo from '@/components/credit/steps/Step1PersonalInfo';
import { Step1PersonalInfoSchema } from '@/components/credit/schemas/step-schemas';
import type { Step1PersonalInfoInput } from '@/components/credit/schemas/step-schemas';

export default function MyForm() {
  const form = useForm<Step1PersonalInfoInput>({
    resolver: zodResolver(Step1PersonalInfoSchema),
    defaultValues: {
      title: 'Mr',
      firstName: '',
      lastName: '',
      gender: 'MALE',
      dateOfBirth: '',
      nationality: 'Thai',
      idCardNumber: '',
      maritalStatus: 'SINGLE',
      dependents: 0,
      mobilePhone: '',
      email: '',
    },
    mode: 'onBlur', // Validate on blur
  });

  const onSubmit = async (data: Step1PersonalInfoInput) => {
    // Data is type-safe and pre-validated
    try {
      const response = await fetch('/api/v1/applications', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          customerId: 'CUST-' + Date.now(),
          loanType: 'PERSONAL',
          loanAmount: 500000,
          step1Data: data,
        }),
      });

      const result = await response.json();
      if (result.success) {
        console.log('Application created:', result.data.applicationNumber);
      }
    } catch (error) {
      console.error('Error:', error);
    }
  };

  return (
    <Step1PersonalInfo
      form={form}
      onSubmit={onSubmit}
      isLoading={form.formState.isSubmitting}
      isNewApplication={true}
    />
  );
}
```

## 7. Maximum Loan Calculation

### Calculate Max Loan Amount Based on Income

```typescript
import { calculateMaximumLoanAmount } from '@/components/credit/utils/calculation';

const applicant = {
  monthlyIncome: 50000,
  monthlyDebtObligation: 10000,
  maxDtiRatio: 0.50,
};

const maxLoan = calculateMaximumLoanAmount(
  applicant.monthlyIncome,
  applicant.monthlyDebtObligation,
  applicant.maxDtiRatio
);

console.log(`Maximum Loan Amount: ฿${maxLoan.toFixed(0)}`);
// Output: Maximum Loan Amount: ฿1,234,567
```

## 8. Workflow and Status Management

### Checking Available Actions for Status

```typescript
import { WORKFLOW_STATES, WORKFLOW_TRANSITIONS } from '@/components/credit/constants/workflow';

const currentStatus = 'SUBMITTED';

// Get status info
const statusInfo = WORKFLOW_STATES[currentStatus];
console.log(`Status: ${statusInfo.label}`);
console.log(`Color: ${statusInfo.color}`);
console.log(`Description: ${statusInfo.description}`);
console.log(`Allowed Actions: ${statusInfo.allowedActions.join(', ')}`);

// Get possible next states
const possibleNextStates = WORKFLOW_TRANSITIONS[currentStatus];
console.log(`Can transition to: ${possibleNextStates.join(', ')}`);
```

## 9. Role-Based Permissions

### Checking User Permissions

```typescript
import { ROLE_PERMISSIONS } from '@/components/credit/constants/workflow';

const userRole = 'BANK_OFFICER';
const permissions = ROLE_PERMISSIONS[userRole];

// Check if user can perform action
const canVerifyDocuments = permissions.includes('verify_documents');
const canApprove = permissions.includes('approve_application');

console.log(`Can verify documents: ${canVerifyDocuments}`); // true
console.log(`Can approve: ${canApprove}`); // false

// Show available actions
permissions.forEach(perm => {
  console.log(`- ${perm}`);
});
```

## 10. Currency and Date Formatting

### Using Formatter Utilities

```typescript
import { formatBaht, formatPercentage } from '@/components/credit/utils/calculation';

const amount = 1500000;
const rate = 0.045;

console.log(formatBaht(amount)); // ฿1,500,000
console.log(formatPercentage(rate * 100, 2)); // 4.50%
```

---

**These examples demonstrate:**
- Type-safe form handling
- Business rule validation
- Financial calculations
- API integration
- Thai-specific validation
- Real-world use cases

For more details, see the individual file implementations and type definitions.
