"use client";

import { ApplicationStatus } from "../types/entities";

interface TimelineEvent {
  status: ApplicationStatus;
  timestamp: string;
  note?: string;
}

const STATUS_COLORS: Record<ApplicationStatus, string> = {
  DRAFT: "gray",
  SUBMITTED: "blue",
  DOCUMENT_CHECK: "yellow",
  CREDIT_ANALYSIS: "purple",
  APPROVAL: "indigo",
  APPROVED: "green",
  REJECTED: "red",
  NEED_MORE_INFO: "orange",
  CONTRACT_SIGNED: "teal",
  DISBURSED: "emerald",
};

const STATUS_LABELS: Record<ApplicationStatus, string> = {
  DRAFT: "ร่างเอกสาร",
  SUBMITTED: "ส่งแล้ว",
  DOCUMENT_CHECK: "ตรวจสอบเอกสาร",
  CREDIT_ANALYSIS: "วิเคราะห์สินเชื่อ",
  APPROVAL: "รอการอนุมัติ",
  APPROVED: "อนุมัติแล้ว",
  REJECTED: "ปฏิเสธ",
  NEED_MORE_INFO: "ต้องการข้อมูลเพิ่มเติม",
  CONTRACT_SIGNED: "ลงนามสัญญาแล้ว",
  DISBURSED: "โอนเงินแล้ว",
};

interface StatusTimelineProps {
  events: TimelineEvent[];
}

export function StatusTimeline({ events }: StatusTimelineProps) {
  return (
    <div className="space-y-6">
      {events.map((event, index) => {
        const color = STATUS_COLORS[event.status];
        const colorClasses = {
          gray: "bg-gray-100 border-gray-300",
          blue: "bg-blue-100 border-blue-300",
          yellow: "bg-yellow-100 border-yellow-300",
          purple: "bg-purple-100 border-purple-300",
          indigo: "bg-indigo-100 border-indigo-300",
          green: "bg-green-100 border-green-300",
          red: "bg-red-100 border-red-300",
          orange: "bg-orange-100 border-orange-300",
          teal: "bg-teal-100 border-teal-300",
          emerald: "bg-emerald-100 border-emerald-300",
        };

        return (
          <div key={index} className="flex gap-4">
            {/* Timeline Dot */}
            <div className="flex flex-col items-center">
              <div
                className={`w-4 h-4 rounded-full border-2 ${colorClasses[color as keyof typeof colorClasses]}`}
              />
              {index < events.length - 1 && (
                <div className="w-0.5 h-12 bg-gray-300 my-2" />
              )}
            </div>

            {/* Content */}
            <div className="flex-1 pb-2">
              <p className="font-medium text-gray-900">
                {STATUS_LABELS[event.status]}
              </p>
              <p className="text-sm text-gray-600">
                {new Date(event.timestamp).toLocaleString("th-TH", {
                  year: "numeric",
                  month: "long",
                  day: "numeric",
                  hour: "2-digit",
                  minute: "2-digit",
                })}
              </p>
              {event.note && (
                <p className="text-sm text-gray-700 mt-1">{event.note}</p>
              )}
            </div>
          </div>
        );
      })}
    </div>
  );
}
