import { z } from 'zod';

// Step 1: Personal Information Schema
export const Step1PersonalInfoSchema = z.object({
  title: z.string().min(1, 'Title is required'),
  firstName: z.string().min(1, 'First name is required').min(2, 'First name must be at least 2 characters'),
  lastName: z.string().min(1, 'Last name is required').min(2, 'Last name must be at least 2 characters'),
  gender: z.enum(['MALE', 'FEMALE', 'OTHER'], {
    errorMap: () => ({ message: 'Please select a valid gender' }),
  }),
  dateOfBirth: z
    .string()
    .min(1, 'Date of birth is required')
    .refine((date) => {
      const dob = new Date(date);
      const age = new Date().getFullYear() - dob.getFullYear();
      return age >= 18 && age <= 120;
    }, 'You must be between 18 and 120 years old'),
  nationality: z.string().min(1, 'Nationality is required'),
  idCardNumber: z
    .string()
    .min(1, 'ID card number is required')
    .regex(/^\d{13}$/, 'Thai ID card number must be exactly 13 digits'),
  idCardExpire: z
    .string()
    .optional()
    .refine(
      (date) => {
        if (!date) return true;
        const expireDate = new Date(date);
        return expireDate > new Date();
      },
      'ID card must not be expired'
    ),
  maritalStatus: z.enum(['SINGLE', 'MARRIED', 'DIVORCED', 'WIDOWED'], {
    errorMap: () => ({ message: 'Please select a valid marital status' }),
  }),
  dependents: z
    .number()
    .min(0, 'Number of dependents cannot be negative')
    .max(20, 'Number of dependents cannot exceed 20'),
  mobilePhone: z
    .string()
    .min(1, 'Mobile phone number is required')
    .regex(
      /^[0-9\-\+\(\)\s]{10,20}$/,
      'Please enter a valid mobile phone number'
    ),
  email: z
    .string()
    .min(1, 'Email is required')
    .email('Please enter a valid email address'),
});

export type Step1PersonalInfoInput = z.infer<typeof Step1PersonalInfoSchema>;
