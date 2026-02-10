"use client";

import { ApplicationStatus } from "../types/entities";

const STATUS_CONFIG: Record<
  ApplicationStatus,
  { label: string; bgColor: string; textColor: string }
> = {
  [ApplicationStatus.DRAFT]: {
    label: "ร่างเอกสาร",
    bgColor: "bg-gray-100",
    textColor: "text-gray-700",
  },
  [ApplicationStatus.SUBMITTED]: {
    label: "ส่งแล้ว",
    bgColor: "bg-blue-100",
    textColor: "text-blue-700",
  },
  [ApplicationStatus.DOCUMENT_CHECK]: {
    label: "ตรวจสอบเอกสาร",
    bgColor: "bg-yellow-100",
    textColor: "text-yellow-700",
  },
  [ApplicationStatus.CREDIT_ANALYSIS]: {
    label: "วิเคราะห์สินเชื่อ",
    bgColor: "bg-purple-100",
    textColor: "text-purple-700",
  },
  [ApplicationStatus.APPROVAL]: {
    label: "รอการอนุมัติ",
    bgColor: "bg-indigo-100",
    textColor: "text-indigo-700",
  },
  [ApplicationStatus.APPROVED]: {
    label: "อนุมัติแล้ว",
    bgColor: "bg-green-100",
    textColor: "text-green-700",
  },
  [ApplicationStatus.REJECTED]: {
    label: "ปฏิเสธ",
    bgColor: "bg-red-100",
    textColor: "text-red-700",
  },
  [ApplicationStatus.NEED_MORE_INFO]: {
    label: "ต้องการข้อมูลเพิ่มเติม",
    bgColor: "bg-orange-100",
    textColor: "text-orange-700",
  },
  [ApplicationStatus.CONTRACT_SIGNED]: {
    label: "ลงนามสัญญาแล้ว",
    bgColor: "bg-teal-100",
    textColor: "text-teal-700",
  },
  [ApplicationStatus.DISBURSED]: {
    label: "โอนเงินแล้ว",
    bgColor: "bg-emerald-100",
    textColor: "text-emerald-700",
  },
};

interface StatusBadgeProps {
  status: ApplicationStatus;
  size?: "sm" | "md" | "lg";
}

export function StatusBadge({ status, size = "md" }: StatusBadgeProps) {
  const config = STATUS_CONFIG[status];

  const sizeClasses = {
    sm: "px-2 py-1 text-xs",
    md: "px-3 py-1.5 text-sm",
    lg: "px-4 py-2 text-base",
  };

  return (
    <span
      className={`${config.bgColor} ${config.textColor} font-semibold rounded-full inline-block ${sizeClasses[size]}`}
    >
      {config.label}
    </span>
  );
}
