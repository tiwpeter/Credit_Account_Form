"use client";

import { UseFormReturn } from "react-hook-form";
import { PersonalInfoFormData } from "../types/form";
import { ApplicantType, Gender, MaritalStatus } from "../types/entities";
import { FormNavigation } from "../ui/FormNavigation";

interface Step1Props {
  form: UseFormReturn<PersonalInfoFormData>;
  onNext: () => void;
}

export function Step1PersonalInfo({ form, onNext }: Step1Props) {
  const {
    register,
    formState: { errors },
    watch,
  } = form;
  const isSubmitting = form.formState.isSubmitting;

  return (
    <div className="space-y-6">
      <div>
        <h2 className="text-2xl font-bold text-navy-dark mb-6">
          ขั้นตอนที่ 1: ข้อมูลส่วนตัว
        </h2>
      </div>

      {/* Applicant Type */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-2">
          ประเภทผู้สมัคร <span className="text-red-500">*</span>
        </label>
        <select
          {...register("applicantType")}
          className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
        >
          <option value="">เลือกประเภท</option>
          <option value={ApplicantType.PERSONAL}>บุคคลธรรมชาติ</option>
          <option value={ApplicantType.CORPORATE}>นิติบุคคล</option>
        </select>
        {errors.applicantType && (
          <p className="text-red-500 text-sm mt-1">
            {errors.applicantType.message}
          </p>
        )}
      </div>

      {/* Title */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-2">
          คำนำหน้า <span className="text-red-500">*</span>
        </label>
        <select
          {...register("titleTh")}
          className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
        >
          <option value="">เลือกคำนำหน้า</option>
          <option value="นาย">นาย</option>
          <option value="นาง">นาง</option>
          <option value="นางสาว">นางสาว</option>
        </select>
        {errors.titleTh && (
          <p className="text-red-500 text-sm mt-1">{errors.titleTh.message}</p>
        )}
      </div>

      {/* Name */}
      <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-2">
            ชื่อ (ไทย) <span className="text-red-500">*</span>
          </label>
          <input
            {...register("firstNameTh")}
            type="text"
            placeholder="ชื่อ"
            className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
          />
          {errors.firstNameTh && (
            <p className="text-red-500 text-sm mt-1">
              {errors.firstNameTh.message}
            </p>
          )}
        </div>
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-2">
            นามสกุล (ไทย) <span className="text-red-500">*</span>
          </label>
          <input
            {...register("lastNameTh")}
            type="text"
            placeholder="นามสกุล"
            className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
          />
          {errors.lastNameTh && (
            <p className="text-red-500 text-sm mt-1">
              {errors.lastNameTh.message}
            </p>
          )}
        </div>
      </div>

      {/* ID Card */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-2">
          เลขประจำตัวประชาชน <span className="text-red-500">*</span>
        </label>
        <input
          {...register("idCardNumber")}
          type="text"
          placeholder="0000000000000"
          maxLength={13}
          className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
        />
        {errors.idCardNumber && (
          <p className="text-red-500 text-sm mt-1">
            {errors.idCardNumber.message}
          </p>
        )}
      </div>

      {/* Date of Birth & Gender */}
      <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-2">
            วันเดือนปีเกิด <span className="text-red-500">*</span>
          </label>
          <input
            {...register("dateOfBirth")}
            type="date"
            className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
          />
          {errors.dateOfBirth && (
            <p className="text-red-500 text-sm mt-1">
              {errors.dateOfBirth.message}
            </p>
          )}
        </div>
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-2">
            เพศ <span className="text-red-500">*</span>
          </label>
          <select
            {...register("gender")}
            className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
          >
            <option value="">เลือกเพศ</option>
            <option value={Gender.MALE}>ชาย</option>
            <option value={Gender.FEMALE}>หญิง</option>
            <option value={Gender.OTHER}>อื่นๆ</option>
          </select>
          {errors.gender && (
            <p className="text-red-500 text-sm mt-1">{errors.gender.message}</p>
          )}
        </div>
      </div>

      {/* Contact */}
      <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-2">
            เบอร์โทรศัพท์มือถือ <span className="text-red-500">*</span>
          </label>
          <input
            {...register("mobilePhone")}
            type="tel"
            placeholder="0000000000"
            className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
          />
          {errors.mobilePhone && (
            <p className="text-red-500 text-sm mt-1">
              {errors.mobilePhone.message}
            </p>
          )}
        </div>
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-2">
            อีเมล <span className="text-red-500">*</span>
          </label>
          <input
            {...register("email")}
            type="email"
            placeholder="email@example.com"
            className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
          />
          {errors.email && (
            <p className="text-red-500 text-sm mt-1">{errors.email.message}</p>
          )}
        </div>
      </div>

      {/* Marital Status */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-2">
          สถานะสมรส <span className="text-red-500">*</span>
        </label>
        <select
          {...register("maritalStatus")}
          className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
        >
          <option value="">เลือกสถานะสมรส</option>
          <option value={MaritalStatus.SINGLE}>โสด</option>
          <option value={MaritalStatus.MARRIED}>สมรส</option>
          <option value={MaritalStatus.DIVORCED}>หย่า</option>
          <option value={MaritalStatus.WIDOW}>หม้าย</option>
        </select>
        {errors.maritalStatus && (
          <p className="text-red-500 text-sm mt-1">
            {errors.maritalStatus.message}
          </p>
        )}
      </div>

      <FormNavigation
        currentStep={1}
        totalSteps={8}
        onNext={onNext}
        onPrev={() => {}}
        canGoNext={true}
        canGoPrev={false}
        isSubmitting={isSubmitting}
      />
    </div>
  );
}
