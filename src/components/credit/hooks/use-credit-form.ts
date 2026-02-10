'use client';

import { useState, useCallback } from 'react';
import { useForm, UseFormReturn } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';
import { step1Schema, step2Schema, step3Schema, step4Schema, step5Schema, step6Schema, step7Schema, step8Schema } from '../schemas';
import { CreditApplicationFormData } from '../types/form';

const SCHEMAS = [step1Schema, step2Schema, step3Schema, step4Schema, step5Schema, step6Schema, step7Schema, step8Schema];

interface UseCreditFormReturn {
  form: UseFormReturn<any>;
  currentStep: number;
  totalSteps: number;
  goToNextStep: () => Promise<boolean>;
  goToPrevStep: () => void;
  canGoNext: boolean;
  canGoPrev: boolean;
  saveDraft: () => Promise<void>;
  submitApplication: () => Promise<boolean>;
}

export function useCreditForm(): UseCreditFormReturn {
  const [currentStep, setCurrentStep] = useState(1);
  const [formData, setFormData] = useState<Partial<CreditApplicationFormData>>({});

  const form = useForm({
    resolver: zodResolver(SCHEMAS[currentStep - 1]),
    mode: 'onBlur',
    defaultValues: formData
  });

  const totalSteps = SCHEMAS.length;
  const canGoPrev = currentStep > 1;
  const canGoNext = form.formState.isDirty || currentStep > 1;

  const goToNextStep = useCallback(async () => {
    const isValid = await form.trigger();
    if (isValid) {
      const data = form.getValues();
      setFormData(prev => ({ ...prev, ...data }));
      if (currentStep < totalSteps) {
        setCurrentStep(prev => prev + 1);
      }
      return true;
    }
    return false;
  }, [form, currentStep, totalSteps]);

  const goToPrevStep = useCallback(() => {
    if (currentStep > 1) {
      const data = form.getValues();
      setFormData(prev => ({ ...prev, ...data }));
      setCurrentStep(prev => prev - 1);
    }
  }, [form, currentStep]);

  const saveDraft = useCallback(async () => {
    const data = form.getValues();
    const allData = { ...formData, ...data };
    try {
      // Save to localStorage
      localStorage.setItem('creditApplicationDraft', JSON.stringify(allData));
      // You can also call API here
      console.log('Draft saved');
    } catch (error) {
      console.error('Failed to save draft:', error);
      throw error;
    }
  }, [form, formData]);

  const submitApplication = useCallback(async () => {
    // Validate all steps
    for (let i = 0; i < totalSteps; i++) {
      // This is simplified; in real app, validate each step's data
    }
    
    const allData = { ...formData, ...form.getValues() };
    try {
      const response = await fetch('/api/v1/applications', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(allData)
      });

      if (!response.ok) {
        throw new Error('Failed to submit application');
      }

      // Clear draft on success
      localStorage.removeItem('creditApplicationDraft');
      return true;
    } catch (error) {
      console.error('Failed to submit application:', error);
      throw error;
    }
  }, [formData, form, totalSteps]);

  return {
    form,
    currentStep,
    totalSteps,
    goToNextStep,
    goToPrevStep,
    canGoNext,
    canGoPrev,
    saveDraft,
    submitApplication
  };
}
