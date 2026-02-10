'use client';

import { useState, useEffect } from 'react';
import { useCreditForm } from '@/components/credit/hooks/use-credit-form';
import { ProgressIndicator } from '@/components/credit/ui/ProgressIndicator';
import { Step1PersonalInfo } from '@/components/credit/steps/Step1PersonalInfo';
import { Step2AddressInfo } from '@/components/credit/steps/Step2AddressInfo';
import { Step3IncomeEmployment } from '@/components/credit/steps/Step3IncomeEmployment';
import { Step4CreditDetails } from '@/components/credit/steps/Step4CreditDetails';
import { Step5Documents } from '@/components/credit/steps/Step5Documents';
import { Step6Guarantors } from '@/components/credit/steps/Step6Guarantors';
import { Step7CompanyInfo } from '@/components/credit/steps/Step7CompanyInfo';
import { Step8Review } from '@/components/credit/steps/Step8Review';

export default function NewApplicationPage() {
  const [showSuccess, setShowSuccess] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const {
    form,
    currentStep,
    totalSteps,
    goToNextStep,
    goToPrevStep,
    canGoNext,
    canGoPrev,
    submitApplication
  } = useCreditForm();

  const renderStep = () => {
    switch (currentStep) {
      case 1:
        return <Step1PersonalInfo form={form} onNext={goToNextStep} />;
      case 2:
        return <Step2AddressInfo form={form} onNext={goToNextStep} onPrev={goToPrevStep} />;
      case 3:
        return <Step3IncomeEmployment form={form} onNext={goToNextStep} onPrev={goToPrevStep} />;
      case 4:
        return <Step4CreditDetails form={form} onNext={goToNextStep} onPrev={goToPrevStep} />;
      case 5:
        return <Step5Documents form={form} onNext={goToNextStep} onPrev={goToPrevStep} />;
      case 6:
        return <Step6Guarantors form={form} onNext={goToNextStep} onPrev={goToPrevStep} />;
      case 7:
        return <Step7CompanyInfo form={form} onNext={goToNextStep} onPrev={goToPrevStep} />;
      case 8:
        return (
          <Step8Review
            form={form}
            onNext={goToNextStep}
            onPrev={goToPrevStep}
            onSubmit={handleSubmit}
            isSubmitting={form.formState.isSubmitting}
          />
        );
      default:
        return null;
    }
  };

  const handleSubmit = async () => {
    try {
      setError(null);
      await submitApplication();
      setShowSuccess(true);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'เกิดข้อผิดพลาด');
    }
  };

  if (showSuccess) {
    return (
      <div className="container mx-auto px-4 py-12">
        <div className="max-w-2xl mx-auto text-center">
          <div className="bg-green-50 border border-green-200 rounded-lg p-8">
            <div className="text-6xl mb-4">✓</div>
            <h1 className="text-3xl font-bold text-green-800 mb-4">ส่งใบสมัครสำเร็จ</h1>
            <p className="text-gray-700 mb-6">
              ขอบคุณที่ส่งใบสมัครขอสินเชื่อ เราจะติดต่อคุณในไม่ช้า
            </p>
            <div className="bg-white rounded p-4 text-left mb-6">
              <p className="text-sm text-gray-600 mb-2">เลขที่อ้างอิง:</p>
              <p className="text-lg font-mono font-bold text-gray-900">APP-2026-{Math.random().toString(36).slice(2, 8).toUpperCase()}</p>
            </div>
            <a
              href="/applications"
              className="inline-block px-6 py-3 bg-navy-dark text-white rounded-lg font-medium hover:bg-blue-900"
            >
              ดูรายการสมัครของฉัน
            </a>
          </div>
        </div>
      </div>
    );
  }

  return (
    <div className="container mx-auto px-4 py-8">
      <div className="max-w-3xl mx-auto">
        {/* Header */}
        <div className="mb-12">
          <h1 className="text-4xl font-bold text-navy-dark mb-2">ขอสินเชื่อออนไลน์</h1>
          <p className="text-gray-600">กรอกแบบฟอร์มขอสินเชื่อในเพียงไม่กี่ขั้นตอน</p>
        </div>

        {/* Progress */}
        <div className="mb-12">
          <ProgressIndicator
            currentStep={currentStep}
            totalSteps={totalSteps}
          />
        </div>

        {/* Error Message */}
        {error && (
          <div className="mb-8 bg-red-50 border border-red-200 rounded-lg p-4">
            <p className="text-red-800">{error}</p>
          </div>
        )}

        {/* Form Content */}
        <div className="bg-white rounded-lg shadow-lg p-8">
          {renderStep()}
        </div>

        {/* Footer */}
        <div className="mt-8 text-center text-sm text-gray-600">
          <p>หากต้องการความช่วยเหลือ <a href="#" className="text-gold hover:underline">ติดต่อเรา</a></p>
        </div>
      </div>
    </div>
  );
}
