'use client';

import { UseFormReturn, Controller } from 'react-hook-form';
import type { Step1PersonalInfoInput } from '@/components/credit/schemas/step-schemas';

interface Step1PersonalInfoProps {
  form: UseFormReturn<Step1PersonalInfoInput>;
  onSubmit: (data: Step1PersonalInfoInput) => Promise<void>;
  isLoading: boolean;
  isNewApplication?: boolean;
}

export default function Step1PersonalInfo({
  form,
  onSubmit,
  isLoading,
  isNewApplication = true,
}: Step1PersonalInfoProps) {
  const { control, handleSubmit, formState: { errors }, watch } = form;

  const watchedData = watch();

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="space-y-6">
      {/* Form Container */}
      <div className="bg-white rounded-lg shadow p-8">
        <h2 className="text-2xl font-bold text-gray-900 mb-8">Personal Information</h2>

        {/* Title and Names Row */}
        <div className="grid grid-cols-1 md:grid-cols-4 gap-4 mb-6">
          {/* Title */}
          <div>
            <label htmlFor="title" className="block text-sm font-medium text-gray-700 mb-2">
              Title <span className="text-red-500">*</span>
            </label>
            <Controller
              name="title"
              control={control}
              render={({ field }) => (
                <select
                  {...field}
                  className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent bg-white"
                >
                  <option value="">Select Title</option>
                  <option value="Mr">Mr.</option>
                  <option value="Mrs">Mrs.</option>
                  <option value="Ms">Ms.</option>
                  <option value="Dr">Dr.</option>
                  <option value="Prof">Prof.</option>
                </select>
              )}
            />
            {errors.title && (
              <p className="text-red-500 text-xs mt-1">{errors.title.message}</p>
            )}
          </div>

          {/* First Name */}
          <div className="md:col-span-2">
            <label htmlFor="firstName" className="block text-sm font-medium text-gray-700 mb-2">
              First Name <span className="text-red-500">*</span>
            </label>
            <Controller
              name="firstName"
              control={control}
              render={({ field }) => (
                <input
                  {...field}
                  type="text"
                  placeholder="Somchai"
                  className={`w-full px-4 py-2 border rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent ${
                    errors.firstName ? 'border-red-500' : 'border-gray-300'
                  }`}
                />
              )}
            />
            {errors.firstName && (
              <p className="text-red-500 text-xs mt-1">{errors.firstName.message}</p>
            )}
          </div>

          {/* Last Name */}
          <div>
            <label htmlFor="lastName" className="block text-sm font-medium text-gray-700 mb-2">
              Last Name <span className="text-red-500">*</span>
            </label>
            <Controller
              name="lastName"
              control={control}
              render={({ field }) => (
                <input
                  {...field}
                  type="text"
                  placeholder="Noomsai"
                  className={`w-full px-4 py-2 border rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent ${
                    errors.lastName ? 'border-red-500' : 'border-gray-300'
                  }`}
                />
              )}
            />
            {errors.lastName && (
              <p className="text-red-500 text-xs mt-1">{errors.lastName.message}</p>
            )}
          </div>
        </div>

        {/* Gender and Date of Birth Row */}
        <div className="grid grid-cols-1 md:grid-cols-2 gap-4 mb-6">
          {/* Gender */}
          <div>
            <label htmlFor="gender" className="block text-sm font-medium text-gray-700 mb-2">
              Gender <span className="text-red-500">*</span>
            </label>
            <Controller
              name="gender"
              control={control}
              render={({ field }) => (
                <select
                  {...field}
                  className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent bg-white"
                >
                  <option value="">Select Gender</option>
                  <option value="MALE">Male</option>
                  <option value="FEMALE">Female</option>
                  <option value="OTHER">Other</option>
                </select>
              )}
            />
            {errors.gender && (
              <p className="text-red-500 text-xs mt-1">{errors.gender.message}</p>
            )}
          </div>

          {/* Date of Birth */}
          <div>
            <label htmlFor="dateOfBirth" className="block text-sm font-medium text-gray-700 mb-2">
              Date of Birth <span className="text-red-500">*</span>
            </label>
            <Controller
              name="dateOfBirth"
              control={control}
              render={({ field }) => (
                <input
                  {...field}
                  type="date"
                  className={`w-full px-4 py-2 border rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent ${
                    errors.dateOfBirth ? 'border-red-500' : 'border-gray-300'
                  }`}
                />
              )}
            />
            {errors.dateOfBirth && (
              <p className="text-red-500 text-xs mt-1">{errors.dateOfBirth.message}</p>
            )}
          </div>
        </div>

        {/* Nationality and ID Card Row */}
        <div className="grid grid-cols-1 md:grid-cols-2 gap-4 mb-6">
          {/* Nationality */}
          <div>
            <label htmlFor="nationality" className="block text-sm font-medium text-gray-700 mb-2">
              Nationality <span className="text-red-500">*</span>
            </label>
            <Controller
              name="nationality"
              control={control}
              render={({ field }) => (
                <select
                  {...field}
                  className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent bg-white"
                >
                  <option value="">Select Nationality</option>
                  <option value="Thai">Thai</option>
                  <option value="Laotian">Laotian</option>
                  <option value="Cambodian">Cambodian</option>
                  <option value="Vietnamese">Vietnamese</option>
                  <option value="American">American</option>
                  <option value="European">European</option>
                  <option value="Other">Other</option>
                </select>
              )}
            />
            {errors.nationality && (
              <p className="text-red-500 text-xs mt-1">{errors.nationality.message}</p>
            )}
          </div>

          {/* ID Card Number */}
          <div>
            <label htmlFor="idCardNumber" className="block text-sm font-medium text-gray-700 mb-2">
              Thai ID Card Number <span className="text-red-500">*</span>
            </label>
            <Controller
              name="idCardNumber"
              control={control}
              render={({ field }) => (
                <input
                  {...field}
                  type="text"
                  placeholder="1234567890123"
                  className={`w-full px-4 py-2 border rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent ${
                    errors.idCardNumber ? 'border-red-500' : 'border-gray-300'
                  }`}
                  maxLength={13}
                />
              )}
            />
            {errors.idCardNumber && (
              <p className="text-red-500 text-xs mt-1">{errors.idCardNumber.message}</p>
            )}
          </div>
        </div>

        {/* ID Card Expiry */}
        <div className="mb-6">
          <label htmlFor="idCardExpire" className="block text-sm font-medium text-gray-700 mb-2">
            ID Card Expiration Date
          </label>
          <Controller
            name="idCardExpire"
            control={control}
            render={({ field }) => (
              <input
                {...field}
                type="date"
                className={`w-full px-4 py-2 border rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent ${
                  errors.idCardExpire ? 'border-red-500' : 'border-gray-300'
                }`}
              />
            )}
          />
          {errors.idCardExpire && (
            <p className="text-red-500 text-xs mt-1">{errors.idCardExpire.message}</p>
          )}
        </div>

        {/* Marital Status and Dependents Row */}
        <div className="grid grid-cols-1 md:grid-cols-2 gap-4 mb-6">
          {/* Marital Status */}
          <div>
            <label htmlFor="maritalStatus" className="block text-sm font-medium text-gray-700 mb-2">
              Marital Status <span className="text-red-500">*</span>
            </label>
            <Controller
              name="maritalStatus"
              control={control}
              render={({ field }) => (
                <select
                  {...field}
                  className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent bg-white"
                >
                  <option value="">Select Marital Status</option>
                  <option value="SINGLE">Single</option>
                  <option value="MARRIED">Married</option>
                  <option value="DIVORCED">Divorced</option>
                  <option value="WIDOWED">Widowed</option>
                </select>
              )}
            />
            {errors.maritalStatus && (
              <p className="text-red-500 text-xs mt-1">{errors.maritalStatus.message}</p>
            )}
          </div>

          {/* Dependents */}
          <div>
            <label htmlFor="dependents" className="block text-sm font-medium text-gray-700 mb-2">
              Number of Dependents <span className="text-red-500">*</span>
            </label>
            <Controller
              name="dependents"
              control={control}
              render={({ field }) => (
                <input
                  {...field}
                  type="number"
                  min="0"
                  max="20"
                  onChange={(e) => field.onChange(parseInt(e.target.value) || 0)}
                  className={`w-full px-4 py-2 border rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent ${
                    errors.dependents ? 'border-red-500' : 'border-gray-300'
                  }`}
                />
              )}
            />
            {errors.dependents && (
              <p className="text-red-500 text-xs mt-1">{errors.dependents.message}</p>
            )}
          </div>
        </div>

        {/* Mobile Phone and Email Row */}
        <div className="grid grid-cols-1 md:grid-cols-2 gap-4 mb-6">
          {/* Mobile Phone */}
          <div>
            <label htmlFor="mobilePhone" className="block text-sm font-medium text-gray-700 mb-2">
              Mobile Phone Number <span className="text-red-500">*</span>
            </label>
            <Controller
              name="mobilePhone"
              control={control}
              render={({ field }) => (
                <input
                  {...field}
                  type="tel"
                  placeholder="0891234567"
                  className={`w-full px-4 py-2 border rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent ${
                    errors.mobilePhone ? 'border-red-500' : 'border-gray-300'
                  }`}
                />
              )}
            />
            {errors.mobilePhone && (
              <p className="text-red-500 text-xs mt-1">{errors.mobilePhone.message}</p>
            )}
          </div>

          {/* Email */}
          <div>
            <label htmlFor="email" className="block text-sm font-medium text-gray-700 mb-2">
              Email Address <span className="text-red-500">*</span>
            </label>
            <Controller
              name="email"
              control={control}
              render={({ field }) => (
                <input
                  {...field}
                  type="email"
                  placeholder="user@example.com"
                  className={`w-full px-4 py-2 border rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent ${
                    errors.email ? 'border-red-500' : 'border-gray-300'
                  }`}
                />
              )}
            />
            {errors.email && (
              <p className="text-red-500 text-xs mt-1">{errors.email.message}</p>
            )}
          </div>
        </div>

        {/* Form Summary */}
        {Object.values(watchedData).some(v => v) && (
          <div className="bg-blue-50 rounded-lg p-4 mb-6">
            <p className="text-sm text-blue-900 font-semibold mb-2">Summary:</p>
            <p className="text-sm text-blue-800">
              {watchedData.title} {watchedData.firstName} {watchedData.lastName}
              {watchedData.email && ` • ${watchedData.email}`}
            </p>
          </div>
        )}

        {/* Submit Button */}
        <button
          type="submit"
          disabled={isLoading}
          className={`w-full py-3 rounded-lg font-semibold text-white transition ${
            isLoading
              ? 'bg-gray-400 cursor-not-allowed'
              : 'bg-blue-600 hover:bg-blue-700'
          }`}
        >
          {isLoading ? 'Processing...' : isNewApplication ? 'Create Application' : 'Next Step'}
        </button>

        {/* Helper Text */}
        <p className="text-xs text-gray-600 mt-4 text-center">
          <span className="text-red-500">*</span> Indicates required fields
        </p>
      </div>
    </form>
  );
}
