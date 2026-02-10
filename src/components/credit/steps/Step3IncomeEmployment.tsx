"use client";

import { UseFormReturn } from "react-hook-form";
import { IncomeEmploymentFormData } from "../types/form";
import { EmploymentType, IncomeType } from "../types/entities";
import { FormNavigation } from "../ui/FormNavigation";

interface Step3Props {
  form: UseFormReturn<IncomeEmploymentFormData>;
  onNext: () => void;
  onPrev: () => void;
}

export function Step3IncomeEmployment({ form, onNext, onPrev }: Step3Props) {
  const {
    register,
    formState: { errors },
    watch,
  } = form;
  const employmentType = watch("employmentType");

  return (
    <div className="space-y-6">
      <div>
        <h2 className="text-2xl font-bold text-navy-dark mb-6">
          ขั้นตอนที่ 3: รายได้และการจ้างงาน
        </h2>
      </div>

      {/* Employment Type */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-2">
          ประเภทการจ้างงาน <span className="text-red-500">*</span>
        </label>
        <select
          {...register("employmentType")}
          className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
        >
          <option value="">เลือกประเภท</option>
          <option value={EmploymentType.PERMANENT}>พนักงานวิสาหกิจเอกชน</option>
          <option value={EmploymentType.CONTRACT}>พนักงานสัญญาจ้าง</option>
          <option value={EmploymentType.SELF_EMPLOYED}>ประกอบอาชีพอิสระ</option>
          <option value={EmploymentType.BUSINESS_OWNER}>เจ้าของธุรกิจ</option>
          <option value={EmploymentType.RETIRED}>เกษียณอายุ</option>
        </select>
        {errors.employmentType && (
          <p className="text-red-500 text-sm mt-1">
            {errors.employmentType.message}
          </p>
        )}
      </div>

      {/* Company/Organization */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-2">
          บริษัท/องค์กร <span className="text-red-500">*</span>
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

      {/* Position */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-2">
          ตำแหน่ง <span className="text-red-500">*</span>
        </label>
        <input
          {...register("position")}
          type="text"
          placeholder="ตำแหน่งงาน"
          className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
        />
        {errors.position && (
          <p className="text-red-500 text-sm mt-1">{errors.position.message}</p>
        )}
      </div>

      {/* Income */}
      <div className="space-y-4">
        <h3 className="text-lg font-semibold text-gray-900">รายได้</h3>

        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-2">
              ประเภทรายได้ <span className="text-red-500">*</span>
            </label>
            <select
              {...register("incomeType")}
              className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
            >
              <option value="">เลือกประเภท</option>
              <option value={IncomeType.SALARY}>เงินเดือน</option>
              <option value={IncomeType.BUSINESS}>รายได้จากธุรกิจ</option>
              <option value={IncomeType.COMMISSION}>ค่าคอมมิชชั่น</option>
              <option value={IncomeType.RENTAL}>รายได้จากการเช่า</option>
              <option value={IncomeType.PENSION}>บำนาญ</option>
              <option value={IncomeType.OTHER}>อื่นๆ</option>
            </select>
            {errors.incomeType && (
              <p className="text-red-500 text-sm mt-1">
                {errors.incomeType.message}
              </p>
            )}
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-2">
              รายได้ต่อเดือน (บาท) <span className="text-red-500">*</span>
            </label>
            <input
              {...register("monthlyIncome", { valueAsNumber: true })}
              type="number"
              placeholder="50000"
              min="0"
              className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
            />
            {errors.monthlyIncome && (
              <p className="text-red-500 text-sm mt-1">
                {errors.monthlyIncome.message}
              </p>
            )}
          </div>
        </div>

        {/* Other Income */}
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-2">
            รายได้อื่นต่อเดือน (บาท)
          </label>
          <input
            {...register("otherMonthlyIncome", { valueAsNumber: true })}
            type="number"
            placeholder="0"
            min="0"
            className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
          />
        </div>
      </div>

      {/* Employment Duration */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-2">
          ระยะเวลาในการทำงาน (เดือน) <span className="text-red-500">*</span>
        </label>
        <input
          {...register("employmentDurationMonths", { valueAsNumber: true })}
          type="number"
          placeholder="0"
          min="0"
          className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
        />
        {errors.employmentDurationMonths && (
          <p className="text-red-500 text-sm mt-1">
            {errors.employmentDurationMonths.message}
          </p>
        )}
      </div>

      <FormNavigation
        currentStep={3}
        totalSteps={8}
        onNext={onNext}
        onPrev={onPrev}
        canGoNext={true}
        canGoPrev={true}
      />
    </div>
  );
}
