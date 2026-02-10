"use client";

import Link from "next/link";
import { StatusBadge } from "./StatusBadge";
import { ApplicationStatus } from "../types/entities";

interface ApplicationSummary {
  id: string;
  applicationNumber: string;
  applicantName: string;
  loanType: string;
  requestedAmount: number;
  status: ApplicationStatus;
  createdAt: string;
}

interface ApplicationCardProps {
  application: ApplicationSummary;
}

export function ApplicationCard({ application }: ApplicationCardProps) {
  const formatCurrency = (amount: number) => {
    return new Intl.NumberFormat("th-TH", {
      style: "currency",
      currency: "THB",
    }).format(amount);
  };

  return (
    <Link href={`/applications/${application.id}`}>
      <div className="bg-white rounded-lg shadow hover:shadow-lg transition-shadow p-6 cursor-pointer">
        <div className="flex justify-between items-start mb-4">
          <div>
            <p className="text-sm text-gray-600">
              {application.applicationNumber}
            </p>
            <h3 className="text-lg font-semibold text-gray-900">
              {application.applicantName}
            </h3>
          </div>
          <StatusBadge status={application.status} />
        </div>

        <div className="space-y-2 text-sm text-gray-600 mb-4">
          <p>
            <span className="font-medium">ประเภทสินเชื่อ:</span>{" "}
            {application.loanType}
          </p>
          <p>
            <span className="font-medium">จำนวนที่ขอ:</span>{" "}
            {formatCurrency(application.requestedAmount)}
          </p>
          <p>
            <span className="font-medium">วันที่สมัคร:</span>{" "}
            {new Date(application.createdAt).toLocaleDateString("th-TH", {
              year: "numeric",
              month: "long",
              day: "numeric",
            })}
          </p>
        </div>

        <button className="w-full px-4 py-2 bg-navy-dark text-white rounded font-medium hover:bg-blue-900 transition-colors">
          ดูรายละเอียด
        </button>
      </div>
    </Link>
  );
}
