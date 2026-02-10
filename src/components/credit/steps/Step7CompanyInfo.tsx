"use client";

import { UseFormReturn } from "react-hook-form";
import { CompanyInfoFormData } from "../types/form";
import { FormNavigation } from "../ui/FormNavigation";
import { PROVINCES } from "../constants/master-data";

interface Step7Props {
  form: UseFormReturn<CompanyInfoFormData>;
  onNext: () => void;
  onPrev: () => void;
}

export function Step7CompanyInfo({ form, onNext, onPrev }: Step7Props) {
  const {
    register,
    formState: { errors },
  } = form;

  return (
    <div className="space-y-6">
      <div>
        <h2 className="text-2xl font-bold text-navy-dark mb-2">
          ขั้นตอนที่ 7: ข้อมูลบริษัท
        </h2>
        <p className="text-gray-600 text-sm">(สำหรับผู้สมัครประเภทนิติบุคคล)</p>
      </div>

      {/* Company Name */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-2">
          ชื่อบริษัท <span className="text-red-500">*</span>
        </label>
        <input
          {...register("companyName")}
          type="text"
          placeholder="ชื่อบริษัท"
          className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
        />
        {errors.companyName && (
          <p className="text-red-500 text-sm mt-1">
            {errors.companyName.message}
          </p>
        )}
      </div>

      {/* Tax ID */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-2">
          เลขประจำตัวผู้เสียภาษี <span className="text-red-500">*</span>
        </label>
        <input
          {...register("taxId")}
          type="text"
          placeholder="0000000000000"
          maxLength={13}
          className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
        />
        {errors.taxId && (
          <p className="text-red-500 text-sm mt-1">{errors.taxId.message}</p>
        )}
      </div>

      {/* Registration Number */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-2">
          เลขจดทะเบียนบริษัท <span className="text-red-500">*</span>
        </label>
        <input
          {...register("registrationNumber")}
          type="text"
          placeholder="เลขจดทะเบียน"
          className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
        />
        {errors.registrationNumber && (
          <p className="text-red-500 text-sm mt-1">
            {errors.registrationNumber.message}
          </p>
        )}
      </div>

      {/* Business Type */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-2">
          ประเภทธุรกิจ <span className="text-red-500">*</span>
        </label>
        <input
          {...register("businessType")}
          type="text"
          placeholder="ประเภทธุรกิจ"
          className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
        />
        {errors.businessType && (
          <p className="text-red-500 text-sm mt-1">
            {errors.businessType.message}
          </p>
        )}
      </div>

      {/* Company Address */}
      <div className="space-y-4">
        <h3 className="text-lg font-semibold text-gray-900">ที่ตั้งสำนักงาน</h3>

        <div>
          <label className="block text-sm font-medium text-gray-700 mb-2">
            เลขที่อาคาร <span className="text-red-500">*</span>
          </label>
          <input
            {...register("companyAddressNumber")}
            type="text"
            placeholder="เลขที่"
            className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
          />
          {errors.companyAddressNumber && (
            <p className="text-red-500 text-sm mt-1">
              {errors.companyAddressNumber.message}
            </p>
          )}
        </div>

        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-2">
              จังหวัด <span className="text-red-500">*</span>
            </label>
            <select
              {...register("companyAddressProvince")}
              className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
            >
              <option value="">เลือกจังหวัด</option>
              {PROVINCES.map((province) => (
                <option key={province.code} value={province.code}>
                  {province.nameTh}
                </option>
              ))}
            </select>
            {errors.companyAddressProvince && (
              <p className="text-red-500 text-sm mt-1">
                {errors.companyAddressProvince.message}
              </p>
            )}
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-2">
              รหัสไปรษณีย์ <span className="text-red-500">*</span>
            </label>
            <input
              {...register("companyAddressPostalCode")}
              type="text"
              placeholder="10000"
              maxLength={5}
              className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
            />
            {errors.companyAddressPostalCode && (
              <p className="text-red-500 text-sm mt-1">
                {errors.companyAddressPostalCode.message}
              </p>
            )}
          </div>
        </div>
      </div>

      {/* Annual Revenue */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-2">
          รายได้ประจำปี (บาท) <span className="text-red-500">*</span>
        </label>
        <input
          {...register("annualRevenue", { valueAsNumber: true })}
          type="number"
          placeholder="1000000"
          min="0"
          className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
        />
        {errors.annualRevenue && (
          <p className="text-red-500 text-sm mt-1">
            {errors.annualRevenue.message}
          </p>
        )}
      </div>

      {/* Number of Employees */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-2">
          จำนวนพนักงาน <span className="text-red-500">*</span>
        </label>
        <input
          {...register("numberOfEmployees", { valueAsNumber: true })}
          type="number"
          placeholder="10"
          min="1"
          className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
        />
        {errors.numberOfEmployees && (
          <p className="text-red-500 text-sm mt-1">
            {errors.numberOfEmployees.message}
          </p>
        )}
      </div>

      <FormNavigation
        currentStep={7}
        totalSteps={8}
        onNext={onNext}
        onPrev={onPrev}
        canGoNext={true}
        canGoPrev={true}
      />
    </div>
  );
}
