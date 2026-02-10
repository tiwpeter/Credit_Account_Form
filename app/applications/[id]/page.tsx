'use client';

import { useState, useEffect } from 'react';
import { StatusBadge } from '@/components/credit/ui/StatusBadge';
import { StatusTimeline } from '@/components/credit/ui/StatusTimeline';
import { ApplicationStatus } from '@/components/credit/types/entities';
import Link from 'next/link';

interface ApplicationDetail {
  id: string;
  applicationNumber: string;
  applicantName: string;
  loanType: string;
  requestedAmount: number;
  status: ApplicationStatus;
  createdAt: string;
  idCardNumber: string;
  email: string;
  phone: string;
  monthlyIncome: number;
  companyName: string;
}

interface TimelineEvent {
  status: ApplicationStatus;
  timestamp: string;
  note?: string;
}

export default function ApplicationDetailPage({
  params
}: {
  params: { id: string }
}) {
  const [application, setApplication] = useState<ApplicationDetail | null>(null);
  const [timeline, setTimeline] = useState<TimelineEvent[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    // Mock data
    const mockApp: ApplicationDetail = {
      id: params.id,
      applicationNumber: `APP-2026-00${params.id}`,
      applicantName: 'สมชาย ใจดี',
      loanType: 'สินเชื่อส่วนบุคคล',
      requestedAmount: 500000,
      status: ApplicationStatus.SUBMITTED,
      createdAt: '2026-02-09T10:30:00Z',
      idCardNumber: '1234567890123',
      email: 'somchai@example.com',
      phone: '0812345678',
      monthlyIncome: 50000,
      companyName: 'บริษัท ABC จำกัด'
    };

    const mockTimeline: TimelineEvent[] = [
      {
        status: ApplicationStatus.DRAFT,
        timestamp: '2026-02-09T08:00:00Z'
      },
      {
        status: ApplicationStatus.SUBMITTED,
        timestamp: '2026-02-09T10:30:00Z',
        note: 'ส่งใบสมัครเรียบร้อย'
      }
    ];

    setApplication(mockApp);
    setTimeline(mockTimeline);
    setLoading(false);
  }, [params.id]);

  const formatCurrency = (amount: number) => {
    return new Intl.NumberFormat('th-TH', {
      style: 'currency',
      currency: 'THB'
    }).format(amount);
  };

  if (loading) {
    return (
      <div className="container mx-auto px-4 py-8 text-center">
        <p className="text-gray-600">กำลังโหลด...</p>
      </div>
    );
  }

  if (!application) {
    return (
      <div className="container mx-auto px-4 py-8 text-center">
        <p className="text-gray-600">ไม่พบใบสมัคร</p>
      </div>
    );
  }

  return (
    <div className="container mx-auto px-4 py-8">
      {/* Back Button */}
      <Link href="/applications" className="text-gold hover:underline mb-4 inline-block">
        ← กลับไปหน้ารายการ
      </Link>

      <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
        {/* Main Content */}
        <div className="lg:col-span-2 space-y-8">
          {/* Header */}
          <div className="bg-white rounded-lg shadow p-6">
            <div className="flex justify-between items-start mb-4">
              <div>
                <p className="text-sm text-gray-600 mb-1">{application.applicationNumber}</p>
                <h1 className="text-3xl font-bold text-navy-dark">{application.applicantName}</h1>
              </div>
              <StatusBadge status={application.status} size="lg" />
            </div>

            <div className="grid grid-cols-2 gap-4 mt-6 pt-6 border-t border-gray-200">
              <div>
                <p className="text-sm text-gray-600">ประเภทสินเชื่อ</p>
                <p className="text-lg font-medium text-gray-900">{application.loanType}</p>
              </div>
              <div>
                <p className="text-sm text-gray-600">จำนวนที่ขอ</p>
                <p className="text-lg font-medium text-gray-900">{formatCurrency(application.requestedAmount)}</p>
              </div>
              <div>
                <p className="text-sm text-gray-600">รายได้ต่อเดือน</p>
                <p className="text-lg font-medium text-gray-900">{formatCurrency(application.monthlyIncome)}</p>
              </div>
              <div>
                <p className="text-sm text-gray-600">วันที่สมัคร</p>
                <p className="text-lg font-medium text-gray-900">
                  {new Date(application.createdAt).toLocaleDateString('th-TH', {
                    year: 'numeric',
                    month: 'long',
                    day: 'numeric'
                  })}
                </p>
              </div>
            </div>
          </div>

          {/* Personal Information */}
          <div className="bg-white rounded-lg shadow p-6">
            <h2 className="text-2xl font-bold text-navy-dark mb-6">ข้อมูลส่วนตัว</h2>
            <div className="grid grid-cols-2 gap-6">
              <div>
                <p className="text-sm text-gray-600 mb-1">เลขประจำตัวประชาชน</p>
                <p className="font-medium text-gray-900">{application.idCardNumber}</p>
              </div>
              <div>
                <p className="text-sm text-gray-600 mb-1">อีเมล</p>
                <p className="font-medium text-gray-900">{application.email}</p>
              </div>
              <div>
                <p className="text-sm text-gray-600 mb-1">เบอร์โทรศัพท์</p>
                <p className="font-medium text-gray-900">{application.phone}</p>
              </div>
              <div>
                <p className="text-sm text-gray-600 mb-1">สถานที่ทำงาน</p>
                <p className="font-medium text-gray-900">{application.companyName}</p>
              </div>
            </div>
          </div>

          {/* Documents */}
          <div className="bg-white rounded-lg shadow p-6">
            <h2 className="text-2xl font-bold text-navy-dark mb-6">เอกสารประกอบ</h2>
            <div className="space-y-3">
              {[
                { name: 'บัตรประชาชน', status: 'verified' },
                { name: 'ทะเบียนบ้าน', status: 'verified' },
                { name: 'หลักฐานรายได้', status: 'pending' }
              ].map((doc) => (
                <div key={doc.name} className="flex items-center justify-between p-3 border border-gray-200 rounded">
                  <span className="font-medium text-gray-900">{doc.name}</span>
                  <span className={`text-sm px-3 py-1 rounded-full ${
                    doc.status === 'verified'
                      ? 'bg-green-100 text-green-700'
                      : 'bg-yellow-100 text-yellow-700'
                  }`}>
                    {doc.status === 'verified' ? '✓ ยืนยันแล้ว' : '⏳ รอการยืนยัน'}
                  </span>
                </div>
              ))}
            </div>
          </div>
        </div>

        {/* Sidebar */}
        <div className="lg:col-span-1 space-y-8">
          {/* Timeline */}
          <div className="bg-white rounded-lg shadow p-6">
            <h2 className="text-lg font-bold text-navy-dark mb-6">ประวัติการดำเนินการ</h2>
            <StatusTimeline events={timeline} />
          </div>

          {/* Actions */}
          <div className="bg-white rounded-lg shadow p-6">
            <h2 className="text-lg font-bold text-navy-dark mb-4">การกระทำ</h2>
            <div className="space-y-3">
              <button className="w-full px-4 py-2 bg-navy-dark text-white rounded-lg hover:bg-blue-900 transition-colors">
                📄 ดูเอกสารประกอบ
              </button>
              <button className="w-full px-4 py-2 bg-gray-200 text-gray-900 rounded-lg hover:bg-gray-300 transition-colors">
                📋 แก้ไขข้อมูล
              </button>
              <button className="w-full px-4 py-2 bg-gray-200 text-gray-900 rounded-lg hover:bg-gray-300 transition-colors">
                🖨️ พิมพ์เอกสาร
              </button>
            </div>
          </div>

          {/* Contact Support */}
          <div className="bg-blue-50 border border-blue-200 rounded-lg p-6">
            <h3 className="font-semibold text-blue-900 mb-3">ต้องการความช่วยเหลือ?</h3>
            <p className="text-sm text-blue-800 mb-4">
              หากมีคำถามเกี่ยวกับใบสมัครของคุณ กรุณาติดต่อทีมสนับสนุนของเรา
            </p>
            <button className="w-full px-4 py-2 bg-blue-600 text-white rounded-lg hover:bg-blue-700 text-sm font-medium">
              📧 ติดต่อเรา
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}
