"use client";

import { UseFormReturn } from "react-hook-form";
import { GuarantorsFormData } from "../types/form";
import { RelationType, Gender, MaritalStatus } from "../types/entities";
import { FormNavigation } from "../ui/FormNavigation";

interface Step6Props {
  form: UseFormReturn<GuarantorsFormData>;
  onNext: () => void;
  onPrev: () => void;
}

export function Step6Guarantors({ form, onNext, onPrev }: Step6Props) {
  const {
    register,
    formState: { errors },
    watch,
  } = form;
  const guarantorCount = watch("guarantors")?.length || 0;

  return (
    <div className="space-y-6">
      <div>
        <h2 className="text-2xl font-bold text-navy-dark mb-2">
          ขั้นตอนที่ 6: ผู้ค้ำประกันและการอ้างอิง
        </h2>
        <p className="text-gray-600 text-sm">
          กรุณาระบุผู้ค้ำประกันอย่างน้อยหนึ่งคน
        </p>
      </div>

      {/* Guarantor 1 */}
      <div className="space-y-4 border border-gray-200 rounded-lg p-6">
        <h3 className="text-lg font-semibold text-gray-900">ผู้ค้ำประกัน</h3>

        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-2">
              ชื่อ-นามสกุล <span className="text-red-500">*</span>
            </label>
            <input
              {...register("guarantors.0.fullName")}
              type="text"
              placeholder="ชื่อ-นามสกุล"
              className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
            />
            {errors.guarantors?.[0]?.fullName && (
              <p className="text-red-500 text-sm mt-1">
                {errors.guarantors[0].fullName.message}
              </p>
            )}
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-2">
              เลขประจำตัวประชาชน <span className="text-red-500">*</span>
            </label>
            <input
              {...register("guarantors.0.idCardNumber")}
              type="text"
              placeholder="0000000000000"
              maxLength={13}
              className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
            />
            {errors.guarantors?.[0]?.idCardNumber && (
              <p className="text-red-500 text-sm mt-1">
                {errors.guarantors[0].idCardNumber.message}
              </p>
            )}
          </div>
        </div>

        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-2">
              เบอร์โทรศัพท์ <span className="text-red-500">*</span>
            </label>
            <input
              {...register("guarantors.0.phone")}
              type="tel"
              placeholder="0000000000"
              className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
            />
            {errors.guarantors?.[0]?.phone && (
              <p className="text-red-500 text-sm mt-1">
                {errors.guarantors[0].phone.message}
              </p>
            )}
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-2">
              ความสัมพันธ์ <span className="text-red-500">*</span>
            </label>
            <select
              {...register("guarantors.0.relationship")}
              className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
            >
              <option value="">เลือกความสัมพันธ์</option>
              <option value="พ่อแม่">พ่อแม่</option>
              <option value="สามี/ภรรยา">สามี/ภรรยา</option>
              <option value="บุตร">บุตร</option>
              <option value="พี่น้อง">พี่น้อง</option>
              <option value="ญาติ">ญาติ</option>
              <option value="เพื่อน">เพื่อน</option>
            </select>
            {errors.guarantors?.[0]?.relationship && (
              <p className="text-red-500 text-sm mt-1">
                {errors.guarantors[0].relationship.message}
              </p>
            )}
          </div>
        </div>

        <div>
          <label className="block text-sm font-medium text-gray-700 mb-2">
            ที่อยู่ <span className="text-red-500">*</span>
          </label>
          <textarea
            {...register("guarantors.0.address")}
            placeholder="ที่อยู่"
            rows={3}
            className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
          />
          {errors.guarantors?.[0]?.address && (
            <p className="text-red-500 text-sm mt-1">
              {errors.guarantors[0].address.message}
            </p>
          )}
        </div>
      </div>

      {/* References */}
      <div className="space-y-4 border border-gray-200 rounded-lg p-6">
        <h3 className="text-lg font-semibold text-gray-900">
          การอ้างอิง (Contact References)
        </h3>
        <p className="text-sm text-gray-600">กรุณาระบุผู้ที่สามารถจำได้ได้</p>

        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-2">
              ชื่อ <span className="text-red-500">*</span>
            </label>
            <input
              {...register("references.0.fullName")}
              type="text"
              placeholder="ชื่อ"
              className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
            />
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-2">
              เบอร์โทรศัพท์ <span className="text-red-500">*</span>
            </label>
            <input
              {...register("references.0.phone")}
              type="tel"
              placeholder="0000000000"
              className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
            />
          </div>
        </div>
      </div>

      <FormNavigation
        currentStep={6}
        totalSteps={8}
        onNext={onNext}
        onPrev={onPrev}
        canGoNext={true}
        canGoPrev={true}
      />
    </div>
  );
}
