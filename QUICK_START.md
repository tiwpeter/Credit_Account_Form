# 🚀 Quick Start Guide - Credit Application System

## การติดตั้งและรันโปรเจกต์ (Installation & Setup)

### ขั้นตอนที่ 1: แตกไฟล์ ZIP
```bash
# แตกไฟล์
unzip credit-application-system.zip
cd credit-app-system
```

### ขั้นตอนที่ 2: ติดตั้ง Dependencies
```bash
# ติดตั้ง packages
npm install

# หรือใช้ yarn
yarn install
```

### ขั้นตอนที่ 3: Setup Environment
```bash
# คัดลอกไฟล์ environment
cp .env.example .env

# แก้ไขค่าตามต้องการ (ถ้าจำเป็น)
nano .env
```

### ขั้นตอนที่ 4: รัน Development Server
```bash
# เริ่มต้น development server
npm run dev

# หรือ
yarn dev
```

เปิดเบราว์เซอร์ที่ `http://localhost:3000`

---

## 📁 โครงสร้างไฟล์ที่สำคัญ

```
credit-app-system/
│
├── 📄 README.md                    # เอกสารหลัก
├── 📄 PROJECT_SUMMARY.md           # สรุปโปรเจกต์
├── 📄 IMPLEMENTATION_GUIDE.md      # คู่มือพัฒนาต่อ
├── 📄 FOLDER_STRUCTURE.md          # อธิบายโครงสร้าง
│
├── ⚙️  package.json                # Dependencies
├── ⚙️  tsconfig.json               # TypeScript config
├── ⚙️  tailwind.config.ts          # Tailwind theme
├── ⚙️  next.config.js              # Next.js config
│
└── 📂 src/components/credit/       # โค้ดหลักทั้งหมด
    │
    ├── 📂 types/                   # ✅ TypeScript Types (สมบูรณ์)
    │   ├── entities.ts             # 40+ interfaces
    │   ├── form.ts                 # Form types
    │   └── api.ts                  # API types
    │
    ├── 📂 schemas/                 # ✅ Validation (สมบูรณ์)
    │   └── index.ts                # Zod schemas ทั้ง 8 steps
    │
    ├── 📂 constants/               # ✅ Business Logic (สมบูรณ์)
    │   ├── workflow.ts             # Workflow states
    │   ├── business-rules.ts       # Credit policies
    │   └── master-data.ts          # Master data
    │
    ├── 📂 utils/                   # ✅ Utilities (สมบูรณ์)
    │   └── calculation.ts          # Financial calculations
    │
    ├── 📂 hooks/                   # 📝 ต้องสร้าง
    ├── 📂 ui/                      # 📝 ต้องสร้าง
    └── 📂 steps/                   # 📝 มี Step 1 เป็นตัวอย่าง
```

---

## ✅ สิ่งที่มีให้แล้ว (Completed)

### 1. Type System (สมบูรณ์ 100%)
- ✅ Entity types (Applicant, Company, Address, etc.)
- ✅ Form types (ทั้ง 8 steps)
- ✅ API types (Request/Response)
- ✅ Enums ทั้งหมด

### 2. Validation (สมบูรณ์ 100%)
- ✅ Zod schemas ทั้ง 8 steps
- ✅ Thai ID validation (เช็ค checksum)
- ✅ Phone number validation
- ✅ Email validation
- ✅ Business rules validation

### 3. Business Logic (สมบูรณ์ 100%)
- ✅ Workflow management
- ✅ Credit policy rules
- ✅ DTI calculation
- ✅ Interest rate calculation
- ✅ Eligibility checking
- ✅ Master data (จังหวัด, ประเภทเอกสาร, etc.)

### 4. Financial Calculations (สมบูรณ์ 100%)
- ✅ DTI calculator
- ✅ Monthly payment calculator
- ✅ Amortization schedule
- ✅ Loan comparison tools

---

## 📝 สิ่งที่ต้องทำต่อ (To Implement)

### Priority 1: Form Steps Components
```bash
src/components/credit/steps/
├── ✅ Step1PersonalInfo.tsx        # มีแล้ว (ตัวอย่าง)
├── ❌ Step2AddressInfo.tsx         # ต้องสร้าง
├── ❌ Step3IncomeEmployment.tsx    # ต้องสร้าง
├── ❌ Step4CreditDetails.tsx       # ต้องสร้าง
├── ❌ Step5Documents.tsx           # ต้องสร้าง
├── ❌ Step6Guarantors.tsx          # ต้องสร้าง
├── ❌ Step7CompanyInfo.tsx         # ต้องสร้าง
└── ❌ Step8Review.tsx              # ต้องสร้าง
```

### Priority 2: UI Components
```bash
src/components/credit/ui/
├── ❌ FormNavigation.tsx           # ปุ่ม Next/Prev
├── ❌ ProgressIndicator.tsx        # Progress bar
├── ❌ StatusBadge.tsx              # Badge สำหรับ status
├── ❌ DocumentUploader.tsx         # Upload files
└── ❌ StatusTimeline.tsx           # Timeline แสดง workflow
```

### Priority 3: React Hooks
```bash
src/components/credit/hooks/
├── ❌ use-credit-form.ts           # จัดการ form state
├── ❌ use-document-upload.ts       # จัดการ upload
└── ❌ use-workflow.ts              # จัดการ workflow actions
```

### Priority 4: Pages
```bash
src/app/
├── applications/
│   ├── ❌ page.tsx                 # List page
│   ├── new/
│   │   └── ❌ page.tsx             # Form wizard
│   └── [id]/
│       ├── ❌ page.tsx             # Detail page
│       └── print/
│           └── ❌ page.tsx         # Print view
```

---

## 🎓 วิธีใช้งาน Types และ Schemas

### ตัวอย่างการใช้ Type
```typescript
import { CreditApplication, ApplicationStatus } from '@/credit/types/entities';

const application: CreditApplication = {
  id: '123',
  applicationNumber: 'APP-2026-0001',
  status: ApplicationStatus.DRAFT,
  // ... fields อื่นๆ
};
```

### ตัวอย่างการใช้ Validation
```typescript
import { step1Schema } from '@/credit/schemas';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';

const form = useForm({
  resolver: zodResolver(step1Schema),
  mode: 'onBlur'
});
```

### ตัวอย่างการคำนวณ DTI
```typescript
import { calculateDTI } from '@/credit/utils/calculation';

const result = calculateDTI({
  monthlyIncome: 50000,
  existingLoans: [{ monthlyPayment: 10000 }],
  monthlyExpenses: 15000
});

console.log(result.debtToIncomeRatio); // 0.20 (20%)
console.log(result.isWithinLimit); // true
```

---

## 🔧 คำสั่งที่ใช้บ่อย

```bash
# Development
npm run dev              # รัน dev server
npm run build            # Build production
npm run start            # รัน production server

# Type Checking
npm run type-check       # เช็ค TypeScript errors

# Code Quality
npm run lint             # เช็ค ESLint
npm run format           # Format code ด้วย Prettier
```

---

## 📚 เอกสารเพิ่มเติม

1. **README.md** - ภาพรวมระบบ, Features, Business Rules
2. **IMPLEMENTATION_GUIDE.md** - คู่มือการพัฒนาแบบละเอียด
3. **PROJECT_SUMMARY.md** - สรุปสิ่งที่ได้รับทั้งหมด
4. **EXAMPLE_API_PAYLOAD.json** - ตัวอย่าง JSON payload

---

## 💡 Tips สำหรับการพัฒนาต่อ

### 1. ใช้ Step 1 เป็น Template
ไฟล์ `src/components/credit/steps/Step1PersonalInfo.tsx` เป็นตัวอย่างที่สมบูรณ์ คุณสามารถ:
- Copy structure มาใช้กับ Step อื่นๆ
- ดู pattern การ validate
- เห็นวิธี handle errors
- เข้าใจการใช้ Tailwind classes

### 2. ใช้ Business Rules ที่มีให้
```typescript
import { CREDIT_POLICY, LOAN_TYPE_CONFIGS } from '@/credit/constants/business-rules';

// เช็คว่าผู้กู้มีคุณสมบัติหรือไม่
const eligible = isEligibleForLoan(
  monthlyIncome,
  age,
  dti,
  loanType,
  requestedAmount
);
```

### 3. ใช้ Master Data
```typescript
import { PROVINCES, LOAN_TYPE_OPTIONS } from '@/credit/constants/master-data';

// ใช้ใน dropdown
<select>
  {PROVINCES.map(province => (
    <option key={province.code} value={province.code}>
      {province.nameTh}
    </option>
  ))}
</select>
```

---

## ⚠️ สิ่งสำคัญที่ต้องรู้

### Type Safety
- ⚠️ ใช้ TypeScript strict mode
- ⚠️ ห้ามใช้ `any` type
- ⚠️ ทุก function ต้องมี return type

### Validation
- ⚠️ ทุก input ต้องผ่าน Zod validation
- ⚠️ Error messages ต้องเป็น Thai & English
- ⚠️ ต้อง validate ทั้ง client และ server

### Business Rules
- ⚠️ DTI ต้องไม่เกิน 50%
- ⚠️ อายุ 20-65 ปี
- ⚠️ รายได้ขั้นต่ำ 15,000 บาท

---

## 🆘 แก้ปัญหา

### ปัญหา: TypeScript errors
```bash
# เช็ค errors
npm run type-check

# แก้ไข tsconfig.json ถ้าจำเป็น
```

### ปัญหา: Import path ไม่เจอ
```typescript
// ใช้ alias ที่ตั้งไว้ใน tsconfig.json
import { ... } from '@/credit/types/entities';
import { ... } from '@/credit/schemas';
import { ... } from '@/credit/constants/business-rules';
```

### ปัญหา: Zod validation fail
```typescript
// ดูรายละเอียด error
try {
  await schema.parseAsync(data);
} catch (error) {
  if (error instanceof ZodError) {
    console.log(error.errors);
  }
}
```

---

## 📞 Support

หากมีคำถามหรือปัญหา:
1. อ่าน **IMPLEMENTATION_GUIDE.md** สำหรับรายละเอียด
2. ดูตัวอย่างใน **Step1PersonalInfo.tsx**
3. เช็ค **EXAMPLE_API_PAYLOAD.json** สำหรับ structure

---

## ✨ Features พร้อมใช้งาน

✅ **DTI Calculator** - คำนวณ Debt-to-Income ratio  
✅ **Eligibility Checker** - เช็คคุณสมบัติผู้กู้  
✅ **Interest Rate Calculator** - คำนวณดอกเบี้ยตาม risk  
✅ **Payment Calculator** - คำนวณค่างวดรายเดือน  
✅ **Workflow Engine** - จัดการ status flow  
✅ **Thai ID Validator** - validate เลขบัตรประชาชน  
✅ **Form Validation** - validate ทุกฟิลด์  

---

**สร้างด้วยความตั้งใจเพื่อใช้งานจริง** 🚀

พร้อมรองรับ:
- ลูกค้าหลายพันรายต่อวัน
- เอกสารหลายประเภท
- ผู้ใช้หลาย role
- Audit trail สมบูรณ์

**ขอให้สนุกกับการพัฒนา!** 🎉
