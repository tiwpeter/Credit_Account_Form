"use client";

import { UseFormReturn } from "react-hook-form";
import { ReviewFormData } from "../types/form";
import { FormNavigation } from "../ui/FormNavigation";
import { StatusBadge } from "../ui/StatusBadge";
import { ApplicationStatus } from "../types/entities";

interface Step8Props {
  form: UseFormReturn<ReviewFormData>;
  onNext: () => void;
  onPrev: () => void;
  onSubmit: () => Promise<void>;
  isSubmitting?: boolean;
}

export function Step8Review({
  form,
  onNext,
  onPrev,
  onSubmit,
  isSubmitting = false,
}: Step8Props) {
  const {
    register,
    formState: { errors },
  } = form;

  return (
    <div className="space-y-6">
      <div>
        <h2 className="text-2xl font-bold text-navy-dark mb-2">
          ขั้นตอนที่ 8: ตรวจสอบและส่งสมัคร
        </h2>
        <p className="text-gray-600 text-sm">
          โปรดตรวจสอบข้อมูลทั้งหมดให้ถูกต้องก่อนส่งสมัคร
        </p>
      </div>

      {/* Terms and Conditions */}
      <div className="border border-gray-200 rounded-lg p-6 bg-gray-50">
        <label className="flex items-start gap-3 cursor-pointer">
          <input
            {...register("agreeToTerms")}
            type="checkbox"
            className="mt-1 w-5 h-5 border-gray-300 rounded focus:ring-gold"
          />
          <div className="flex-1">
            <span className="text-sm font-medium text-gray-900">
              ฉันยินยอมและตกลงตามเงื่อนไขการให้บริการ
              <span className="text-red-500"> *</span>
            </span>
            <p className="text-xs text-gray-600 mt-1">
              ฉันได้อ่านและเข้าใจเงื่อนไขและข้อกำหนดทั้งหมดปรasto
            </p>
          </div>
        </label>
        {errors.agreeToTerms && (
          <p className="text-red-500 text-sm mt-2">
            {errors.agreeToTerms.message}
          </p>
        )}
      </div>

      {/* Data Privacy */}
      <div className="border border-gray-200 rounded-lg p-6 bg-gray-50">
        <label className="flex items-start gap-3 cursor-pointer">
          <input
            {...register("agreeToPrivacy")}
            type="checkbox"
            className="mt-1 w-5 h-5 border-gray-300 rounded focus:ring-gold"
          />
          <div className="flex-1">
            <span className="text-sm font-medium text-gray-900">
              ฉันตกลงให้ธนาคารใช้ข้อมูลส่วนบุคคลของฉัน
              <span className="text-red-500"> *</span>
            </span>
            <p className="text-xs text-gray-600 mt-1">
              ฉันตกลงให้ธนาคารเก็บและใช้ข้อมูลส่วนบุคคลของฉันเพื่อการตรวจสอบสินเชื่อ
            </p>
          </div>
        </label>
        {errors.agreeToPrivacy && (
          <p className="text-red-500 text-sm mt-2">
            {errors.agreeToPrivacy.message}
          </p>
        )}
      </div>

      {/* Summary */}
      <div className="bg-blue-50 border border-blue-200 rounded-lg p-6">
        <h3 className="font-semibold text-gray-900 mb-4">สรุปข้อมูลการสมัคร</h3>
        <div className="space-y-3 text-sm">
          <p>
            <span className="text-gray-600">ประเภทผู้สมัคร:</span>
            <span className="font-medium text-gray-900"> บุคคลธรรมชาติ</span>
          </p>
          <p>
            <span className="text-gray-600">ประเภทสินเชื่อ:</span>
            <span className="font-medium text-gray-900">
              {" "}
              สินเชื่อส่วนบุคคล
            </span>
          </p>
          <p>
            <span className="text-gray-600">จำนวนที่ขอ:</span>
            <span className="font-medium text-gray-900"> ฿ 100,000.00</span>
          </p>
          <p>
            <span className="text-gray-600">สถานะ:</span>
            <StatusBadge status={ApplicationStatus.DRAFT} size="sm" />
          </p>
        </div>
      </div>

      {/* Notes */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-2">
          หมายเหตุเพิ่มเติม
        </label>
        <textarea
          {...register("notes")}
          placeholder="หากมีข้อมูลเพิ่มเติมที่ต้องการบอก"
          rows={4}
          className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
        />
      </div>

      <div className="flex justify-between pt-8 border-t border-gray-200">
        <button
          onClick={onPrev}
          className="px-6 py-3 bg-white text-navy-dark font-semibold rounded-lg border-2 border-navy-dark
                   hover:bg-gray-50 transition-colors disabled:opacity-50"
          disabled={isSubmitting}
        >
          ← ย้อนกลับ
        </button>

        <button
          onClick={onSubmit}
          disabled={isSubmitting}
          className="px-6 py-3 bg-gold text-white font-semibold rounded-lg 
                   hover:bg-yellow-600 transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
        >
          {isSubmitting ? "กำลังส่ง..." : "✓ ส่งใบสมัครขอสินเชื่อ"}
        </button>
      </div>
    </div>
  );
}
