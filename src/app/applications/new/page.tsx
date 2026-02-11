'use client';

import { useState, useEffect } from 'react';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import Step1PersonalInfo from '@/components/credit/steps/Step1PersonalInfo';
import { Step1PersonalInfoSchema } from '@/components/credit/schemas/step-schemas';
import type { Step1PersonalInfoInput } from '@/components/credit/schemas/step-schemas';
import type { CreditApplication } from '@/components/credit/types/entities';

export default function NewApplicationPage() {
  const [currentStep, setCurrentStep] = useState(1);
  const [applicationData, setApplicationData] = useState<Partial<CreditApplication> | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);
  const [loanType, setLoanType] = useState<string>('PERSONAL');
  const [loanAmount, setLoanAmount] = useState<string>('100000');

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
    mode: 'onBlur',
  });

  // Handle Step 1 submission
  const onSubmitStep1 = async (data: Step1PersonalInfoInput) => {
    try {
      setLoading(true);
      setError(null);

      // Create application with Step 1 data
      const response = await fetch('/api/v1/applications', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          customerId: `CUST-${Date.now()}`,
          loanType,
          loanAmount: parseInt(loanAmount),
          step1Data: data,
        }),
      });

      const result = await response.json();

      if (result.success) {
        setApplicationData(result.data);
        setSuccess(`Application created successfully! ID: ${result.data.applicationNumber}`);
        
        // Move to next step
        setTimeout(() => {
          setCurrentStep(2);
        }, 1500);
      } else {
        setError(result.error?.message || 'Failed to create application');
      }
    } catch (err) {
      setError('Error creating application: ' + String(err));
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="max-w-3xl mx-auto">
      {/* Progress Indicator */}
      <div className="bg-white rounded-lg shadow p-6 mb-8">
        <div className="flex items-center justify-between">
          {[1, 2, 3, 4, 5, 6, 7, 8].map((step) => (
            <div key={step} className="flex flex-col items-center flex-1">
              <div
                className={`w-12 h-12 rounded-full flex items-center justify-center text-sm font-bold transition-all ${
                  step <= currentStep
                    ? 'bg-blue-600 text-white'
                    : 'bg-gray-200 text-gray-600'
                }`}
              >
                {step}
              </div>
              <p className="text-xs text-gray-600 mt-2 text-center">
                {step === 1 && 'Personal'}
                {step === 2 && 'Address'}
                {step === 3 && 'Income'}
                {step === 4 && 'Credit'}
                {step === 5 && 'Docs'}
                {step === 6 && 'Guarantors'}
                {step === 7 && 'Company'}
                {step === 8 && 'Review'}
              </p>
            </div>
          ))}
        </div>
      </div>

      {/* Success Message */}
      {success && (
        <div className="bg-green-50 border border-green-200 rounded-lg p-4 mb-6">
          <p className="text-green-800">{success}</p>
        </div>
      )}

      {/* Error Message */}
      {error && (
        <div className="bg-red-50 border border-red-200 rounded-lg p-4 mb-6">
          <p className="text-red-800">{error}</p>
        </div>
      )}

      {/* Loan Type and Amount Selection */}
      {currentStep === 1 && !applicationData && (
        <div className="bg-white rounded-lg shadow p-6 mb-6">
          <h2 className="text-2xl font-bold text-gray-900 mb-6">Loan Setup</h2>
          <div className="space-y-6">
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-3">
                Loan Type
              </label>
              <select
                value={loanType}
                onChange={(e) => setLoanType(e.target.value)}
                className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent"
              >
                <option value="PERSONAL">Personal Loan</option>
                <option value="HOME">Home Loan</option>
                <option value="AUTO">Auto Loan</option>
                <option value="SME">SME Loan</option>
                <option value="CORPORATE">Corporate Loan</option>
              </select>
            </div>

            <div>
              <label className="block text-sm font-medium text-gray-700 mb-3">
                Loan Amount (฿)
              </label>
              <input
                type="number"
                value={loanAmount}
                onChange={(e) => setLoanAmount(e.target.value)}
                className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                min="1000"
                step="10000"
              />
            </div>
          </div>
        </div>
      )}

      {/* Step 1: Personal Information */}
      {currentStep === 1 && (
        <Step1PersonalInfo
          form={form}
          onSubmit={onSubmitStep1}
          isLoading={loading}
          isNewApplication={!applicationData}
        />
      )}

      {/* Steps 2-8: Placeholder */}
      {currentStep > 1 && currentStep <= 8 && (
        <div className="bg-white rounded-lg shadow p-6">
          <div className="text-center py-12">
            <h2 className="text-2xl font-bold text-gray-900 mb-4">Step {currentStep}</h2>
            <p className="text-gray-600 mb-6">
              This step is under development. Implementation coming soon.
            </p>
            <div className="space-x-4">
              <button
                onClick={() => setCurrentStep(currentStep - 1)}
                className="px-6 py-2 border border-gray-300 rounded-lg text-gray-700 hover:bg-gray-50 transition"
              >
                Previous
              </button>
              <button
                onClick={() => setCurrentStep(currentStep + 1)}
                disabled={currentStep === 8}
                className="px-6 py-2 bg-blue-600 text-white rounded-lg hover:bg-blue-700 transition disabled:opacity-50"
              >
                Next
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
