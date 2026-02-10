"use client";

import { UseFormReturn } from "react-hook-form";
import { CreditDetailsFormData } from "../types/form";
import { LoanType } from "../types/entities";
import { FormNavigation } from "../ui/FormNavigation";

interface Step4Props {
  form: UseFormReturn<CreditDetailsFormData>;
  onNext: () => void;
  onPrev: () => void;
}

export function Step4CreditDetails({ form, onNext, onPrev }: Step4Props) {
  const {
    register,
    formState: { errors },
    watch,
  } = form;

  return (
    <div className="space-y-6">
      <div>
        <h2 className="text-2xl font-bold text-navy-dark mb-6">
          ขั้นตอนที่ 4: รายละเอียดการกู้
        </h2>
      </div>

      {/* Loan Type */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-2">
          ประเภทสินเชื่อ <span className="text-red-500">*</span>
        </label>
        <select
          {...register("loanType")}
          className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
        >
          <option value="">เลือกประเภท</option>
          <option value={LoanType.PERSONAL}>สินเชื่อส่วนบุคคล</option>
          <option value={LoanType.HOME}>สินเชื่อเพื่อซื้อบ้าน</option>
          <option value={LoanType.AUTO}>สินเชื่อเพื่อซื้อรถยนต์</option>
          <option value={LoanType.SME}>สินเชื่อเพื่อวิสาหกิจ</option>
          <option value={LoanType.CORPORATE}>สินเชื่อเพื่อนิติบุคคล</option>
        </select>
        {errors.loanType && (
          <p className="text-red-500 text-sm mt-1">{errors.loanType.message}</p>
        )}
      </div>

      {/* Requested Amount */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-2">
          วงเงินที่ขอ (บาท) <span className="text-red-500">*</span>
        </label>
        <input
          {...register("requestedAmount", { valueAsNumber: true })}
          type="number"
          placeholder="100000"
          min="0"
          className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
        />
        {errors.requestedAmount && (
          <p className="text-red-500 text-sm mt-1">
            {errors.requestedAmount.message}
          </p>
        )}
      </div>

      {/* Loan Tenure */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-2">
          ระยะเวลาการกู้ (เดือน) <span className="text-red-500">*</span>
        </label>
        <input
          {...register("loanTenureMonths", { valueAsNumber: true })}
          type="number"
          placeholder="12"
          min="1"
          className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
        />
        {errors.loanTenureMonths && (
          <p className="text-red-500 text-sm mt-1">
            {errors.loanTenureMonths.message}
          </p>
        )}
      </div>

      {/* Loan Purpose */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-2">
          วัตถุประสงค์ของการกู้ <span className="text-red-500">*</span>
        </label>
        <input
          {...register("loanPurpose")}
          type="text"
          placeholder="วัตถุประสงค์"
          className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
        />
        {errors.loanPurpose && (
          <p className="text-red-500 text-sm mt-1">
            {errors.loanPurpose.message}
          </p>
        )}
      </div>

      {/* Existing Loans */}
      <div className="space-y-4">
        <h3 className="text-lg font-semibold text-gray-900">
          หนี้สินที่มีอยู่ในปัจจุบัน
        </h3>

        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-2">
              ยอดหนี้สินรวม (บาท) <span className="text-red-500">*</span>
            </label>
            <input
              {...register("totalExistingDebt", { valueAsNumber: true })}
              type="number"
              placeholder="0"
              min="0"
              className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
            />
            {errors.totalExistingDebt && (
              <p className="text-red-500 text-sm mt-1">
                {errors.totalExistingDebt.message}
              </p>
            )}
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-2">
              ค่างวดรายเดือน (บาท) <span className="text-red-500">*</span>
            </label>
            <input
              {...register("totalMonthlyDebtPayment", { valueAsNumber: true })}
              type="number"
              placeholder="0"
              min="0"
              className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
            />
            {errors.totalMonthlyDebtPayment && (
              <p className="text-red-500 text-sm mt-1">
                {errors.totalMonthlyDebtPayment.message}
              </p>
            )}
          </div>
        </div>
      </div>

      {/* Monthly Expenses */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-2">
          ค่าใช้จ่ายรายเดือน (บาท) <span className="text-red-500">*</span>
        </label>
        <input
          {...register("monthlyExpenses", { valueAsNumber: true })}
          type="number"
          placeholder="0"
          min="0"
          className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
        />
        {errors.monthlyExpenses && (
          <p className="text-red-500 text-sm mt-1">
            {errors.monthlyExpenses.message}
          </p>
        )}
      </div>

      <FormNavigation
        currentStep={4}
        totalSteps={8}
        onNext={onNext}
        onPrev={onPrev}
        canGoNext={true}
        canGoPrev={true}
      />
    </div>
  );
}
