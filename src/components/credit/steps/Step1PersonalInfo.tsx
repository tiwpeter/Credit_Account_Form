"use client";

import { UseFormReturn } from "react-hook-form";
import { PersonalInfoFormData } from "../types/form";
import { ApplicantType, Gender, MaritalStatus } from "../types/entities";

interface Step1Props {
  form: UseFormReturn<PersonalInfoFormData>;
  onNext: () => void;
}

export function Step1PersonalInfo({ form, onNext }: Step1Props) {
  const {
    register,
    formState: { errors, isValid, isSubmitting },
    watch,
  } = form;

  const applicantType = watch("applicantType");

  return (
    <div className="bg-white rounded-2xl shadow-sm border border-[#E1E4ED] p-6 md:p-8 space-y-8">
      {/* Header */}
      <div className="border-b border-[#E1E4ED] pb-6">
        <p className="text-sm text-gray-500">ขั้นตอนที่ 1 จาก 8</p>
        <h2 className="text-2xl font-bold text-[#1E3A5F]">
          ข้อมูลส่วนตัวผู้สมัคร
        </h2>
        <p className="text-sm text-gray-500 mt-1">
          กรุณากรอกข้อมูลให้ครบถ้วนและถูกต้องตามบัตรประชาชน
        </p>
      </div>
      <div className="bg-white p-8 rounded-2xl shadow-xl text-center max-w-sm">
        <h1 className="text-3xl font-bold text-blue-600 mb-4">
          ทดสอบ Tailwind CSS 🚀
        </h1>
        <p className="text-gray-600 mb-6">
          ถ้าคุณเห็นกล่องนี้สวย ๆ แสดงว่า Tailwind ทำงานแล้ว
        </p>
        <button className="bg-blue-500 hover:bg-blue-700 text-white font-semibold py-2 px-4 rounded-lg transition duration-300">
          ปุ่มทดสอบ
        </button>
      </div>
      {/* Applicant Type */}
      <FormSection title="ประเภทผู้สมัคร">
        <FormSelect
          label="ประเภทผู้สมัคร"
          required
          error={errors.applicantType?.message}
          {...register("applicantType")}
        >
          <option value="">เลือกประเภทผู้สมัคร</option>
          <option value={ApplicantType.PERSONAL}>บุคคลธรรมชาติ</option>
          <option value={ApplicantType.CORPORATE}>นิติบุคคล</option>
        </FormSelect>
      </FormSection>

      {/* Personal Info */}
      {applicantType === ApplicantType.PERSONAL && (
        <FormSection title="ข้อมูลบัตรประชาชน">
          <FormSelect
            label="คำนำหน้า"
            required
            error={errors.titleTh?.message}
            {...register("titleTh")}
          >
            <option value="">เลือกคำนำหน้า</option>
            <option value="นาย">นาย</option>
            <option value="นาง">นาง</option>
            <option value="นางสาว">นางสาว</option>
          </FormSelect>

          <div className="grid md:grid-cols-2 gap-6">
            <FormInput
              label="ชื่อ (ไทย)"
              required
              error={errors.firstNameTh?.message}
              {...register("firstNameTh")}
            />
            <FormInput
              label="นามสกุล (ไทย)"
              required
              error={errors.lastNameTh?.message}
              {...register("lastNameTh")}
            />
          </div>

          <FormInput
            label="เลขประจำตัวประชาชน"
            required
            maxLength={13}
            error={errors.idCardNumber?.message}
            {...register("idCardNumber")}
          />

          <div className="grid md:grid-cols-2 gap-6">
            <FormInput
              label="วันเดือนปีเกิด"
              type="date"
              required
              error={errors.dateOfBirth?.message}
              {...register("dateOfBirth")}
            />

            <FormSelect
              label="เพศ"
              required
              error={errors.gender?.message}
              {...register("gender")}
            >
              <option value="">เลือกเพศ</option>
              <option value={Gender.MALE}>ชาย</option>
              <option value={Gender.FEMALE}>หญิง</option>
              <option value={Gender.OTHER}>อื่นๆ</option>
            </FormSelect>
          </div>
        </FormSection>
      )}

      {/* Contact Info */}
      <FormSection title="ข้อมูลติดต่อ">
        <div className="grid md:grid-cols-2 gap-6">
          <FormInput
            label="เบอร์โทรศัพท์มือถือ"
            type="tel"
            required
            error={errors.mobilePhone?.message}
            {...register("mobilePhone")}
          />

          <FormInput
            label="อีเมล"
            type="email"
            required
            error={errors.email?.message}
            {...register("email")}
          />
        </div>

        <FormSelect
          label="สถานะสมรส"
          required
          error={errors.maritalStatus?.message}
          {...register("maritalStatus")}
        >
          <option value="">เลือกสถานะสมรส</option>
          <option value={MaritalStatus.SINGLE}>โสด</option>
          <option value={MaritalStatus.MARRIED}>สมรส</option>
          <option value={MaritalStatus.DIVORCED}>หย่า</option>
          <option value={MaritalStatus.WIDOW}>หม้าย</option>
        </FormSelect>
      </FormSection>

      {/* Navigation */}
      <FormNavigation
        currentStep={1}
        totalSteps={8}
        onNext={onNext}
        canGoNext={isValid}
        canGoPrev={false}
        isSubmitting={isSubmitting}
      />
    </div>
  );
}
function FormSection({
  title,
  children,
}: {
  title: string;
  children: React.ReactNode;
}) {
  return (
    <div className="space-y-6">
      <h3 className="text-lg font-semibold text-[#1E3A5F] border-l-4 border-[#D4AF37] pl-3">
        {title}
      </h3>
      {children}
    </div>
  );
}

export const FormInput = forwardRef<HTMLInputElement, any>(
  ({ label, required, error, ...props }, ref) => (
    <div className="space-y-2">
      <label className="block text-sm font-medium text-gray-700">
        {label} {required && <span className="text-red-500">*</span>}
      </label>

      <input
        ref={ref}
        {...props}
        aria-invalid={!!error}
        className={`
        w-full px-4 py-3 rounded-lg border text-sm
        transition-all duration-200
        ${
          error
            ? "border-red-500 focus:ring-red-500"
            : "border-[#E1E4ED] focus:ring-[#D4AF37] focus:border-[#D4AF37]"
        }
        focus:outline-none focus:ring-2
      `}
      />

      {error && <p className="text-red-500 text-sm">{error}</p>}
    </div>
  ),
);
import { forwardRef } from "react";

export const FormSelect = forwardRef<HTMLSelectElement, any>(
  ({ label, required, error, children, ...props }, ref) => (
    <div className="space-y-2">
      <label className="block text-sm font-medium text-gray-700">
        {label} {required && <span className="text-red-500">*</span>}
      </label>

      <select
        ref={ref}
        {...props}
        aria-invalid={!!error}
        className={`
        w-full px-4 py-3 rounded-lg border text-sm bg-white
        transition-all duration-200
        ${
          error
            ? "border-red-500 focus:ring-red-500"
            : "border-[#E1E4ED] focus:ring-[#D4AF37] focus:border-[#D4AF37]"
        }
        focus:outline-none focus:ring-2
      `}
      >
        {children}
      </select>

      {error && <p className="text-red-500 text-sm">{error}</p>}
    </div>
  ),
);

interface FormNavigationProps {
  currentStep: number;
  totalSteps: number;
  onNext: () => void;
  onPrev?: () => void;
  canGoNext?: boolean;
  canGoPrev?: boolean;
  isSubmitting?: boolean;
}

export function FormNavigation({
  currentStep,
  totalSteps,
  onNext,
  onPrev,
  canGoNext = true,
  canGoPrev = true,
  isSubmitting = false,
}: FormNavigationProps) {
  return (
    <div className="flex items-center justify-between pt-8 border-t border-[#E1E4ED]">
      {/* Prev Button */}
      <button
        type="button"
        onClick={onPrev}
        disabled={!canGoPrev}
        className="px-6 py-2 rounded-lg border border-[#E1E4ED] text-sm
          disabled:opacity-50 disabled:cursor-not-allowed
          hover:bg-gray-50 transition"
      >
        ย้อนกลับ
      </button>

      {/* Step Indicator */}
      <div className="text-sm text-gray-500">
        ขั้นตอน {currentStep} / {totalSteps}
      </div>

      {/* Next Button */}
      <button
        type="button"
        onClick={onNext}
        disabled={!canGoNext || isSubmitting}
        className="px-6 py-2 rounded-lg text-sm text-white
          bg-[#1E3A5F]
          hover:bg-[#162d49]
          disabled:opacity-50 disabled:cursor-not-allowed
          transition"
      >
        {isSubmitting ? "กำลังดำเนินการ..." : "ถัดไป"}
      </button>
    </div>
  );
}
