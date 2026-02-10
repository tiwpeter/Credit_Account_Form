"use client";

import { UseFormReturn } from "react-hook-form";
import { AddressInfoFormData } from "../types/form";
import { AddressType } from "../types/entities";
import { FormNavigation } from "../ui/FormNavigation";
import { PROVINCES } from "../constants/master-data";

interface Step2Props {
  form: UseFormReturn<AddressInfoFormData>;
  onNext: () => void;
  onPrev: () => void;
}

export function Step2AddressInfo({ form, onNext, onPrev }: Step2Props) {
  const {
    register,
    formState: { errors },
    watch,
  } = form;
  const selectedProvince = watch("currentAddressProvince");

  return (
    <div className="space-y-6">
      <div>
        <h2 className="text-2xl font-bold text-navy-dark mb-6">
          ขั้นตอนที่ 2: ที่อยู่
        </h2>
      </div>

      {/* Address Type */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-2">
          ประเภทที่อยู่ <span className="text-red-500">*</span>
        </label>
        <select
          {...register("addressType")}
          className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
        >
          <option value="">เลือกประเภท</option>
          <option value={AddressType.CURRENT}>ที่อยู่ปัจจุบัน</option>
          <option value={AddressType.PERMANENT}>ที่อยู่ถาวร</option>
        </select>
        {errors.addressType && (
          <p className="text-red-500 text-sm mt-1">
            {errors.addressType.message}
          </p>
        )}
      </div>

      {/* Current Address */}
      <div>
        <h3 className="text-lg font-semibold text-gray-900 mb-4">
          ที่อยู่ปัจจุบัน
        </h3>

        <div className="space-y-4">
          {/* House Number */}
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-2">
              เลขที่บ้าน <span className="text-red-500">*</span>
            </label>
            <input
              {...register("currentAddressHouseNumber")}
              type="text"
              placeholder="เลขที่บ้าน"
              className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
            />
            {errors.currentAddressHouseNumber && (
              <p className="text-red-500 text-sm mt-1">
                {errors.currentAddressHouseNumber.message}
              </p>
            )}
          </div>

          {/* Street */}
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-2">
              ถนน
            </label>
            <input
              {...register("currentAddressStreet")}
              type="text"
              placeholder="ถนน"
              className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
            />
          </div>

          {/* Province */}
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-2">
              จังหวัด <span className="text-red-500">*</span>
            </label>
            <select
              {...register("currentAddressProvince")}
              className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
            >
              <option value="">เลือกจังหวัด</option>
              {PROVINCES.map((province) => (
                <option key={province.code} value={province.code}>
                  {province.nameTh}
                </option>
              ))}
            </select>
            {errors.currentAddressProvince && (
              <p className="text-red-500 text-sm mt-1">
                {errors.currentAddressProvince.message}
              </p>
            )}
          </div>

          {/* District & Subdistrict */}
          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-2">
                อำเภอ <span className="text-red-500">*</span>
              </label>
              <input
                {...register("currentAddressDistrict")}
                type="text"
                placeholder="อำเภอ"
                className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
              />
              {errors.currentAddressDistrict && (
                <p className="text-red-500 text-sm mt-1">
                  {errors.currentAddressDistrict.message}
                </p>
              )}
            </div>
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-2">
                ตำบล <span className="text-red-500">*</span>
              </label>
              <input
                {...register("currentAddressSubdistrict")}
                type="text"
                placeholder="ตำบล"
                className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
              />
              {errors.currentAddressSubdistrict && (
                <p className="text-red-500 text-sm mt-1">
                  {errors.currentAddressSubdistrict.message}
                </p>
              )}
            </div>
          </div>

          {/* Postal Code */}
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-2">
              รหัสไปรษณีย์ <span className="text-red-500">*</span>
            </label>
            <input
              {...register("currentAddressPostalCode")}
              type="text"
              placeholder="10000"
              maxLength={5}
              className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
            />
            {errors.currentAddressPostalCode && (
              <p className="text-red-500 text-sm mt-1">
                {errors.currentAddressPostalCode.message}
              </p>
            )}
          </div>

          {/* Years at Address */}
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-2">
              ระยะเวลาอยู่อาศัย (ปี) <span className="text-red-500">*</span>
            </label>
            <input
              {...register("currentAddressYears")}
              type="number"
              placeholder="0"
              min="0"
              className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-gold focus:border-transparent"
            />
            {errors.currentAddressYears && (
              <p className="text-red-500 text-sm mt-1">
                {errors.currentAddressYears.message}
              </p>
            )}
          </div>
        </div>
      </div>

      <FormNavigation
        currentStep={2}
        totalSteps={8}
        onNext={onNext}
        onPrev={onPrev}
        canGoNext={true}
        canGoPrev={true}
      />
    </div>
  );
}
