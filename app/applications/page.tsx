'use client';

import { useState, useEffect } from 'react';
import { ApplicationCard } from '@/components/credit/ui/ApplicationCard';
import { StatusBadge } from '@/components/credit/ui/StatusBadge';
import { ApplicationStatus } from '@/components/credit/types/entities';
import Link from 'next/link';

interface MockApplication {
  id: string;
  applicationNumber: string;
  applicantName: string;
  loanType: string;
  requestedAmount: number;
  status: ApplicationStatus;
  createdAt: string;
}

export default function ApplicationsPage() {
  const [applications, setApplications] = useState<MockApplication[]>([]);
  const [filter, setFilter] = useState<ApplicationStatus | 'ALL'>('ALL');
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    // Mock data
    const mockApps: MockApplication[] = [
      {
        id: '1',
        applicationNumber: 'APP-2026-001',
        applicantName: 'สมชาย ใจดี',
        loanType: 'สินเชื่อส่วนบุคคล',
        requestedAmount: 500000,
        status: ApplicationStatus.SUBMITTED,
        createdAt: '2026-02-09'
      },
      {
        id: '2',
        applicationNumber: 'APP-2026-002',
        applicantName: 'สมสมัย เรียนได้',
        loanType: 'สินเชื่อเพื่อซื้อบ้าน',
        requestedAmount: 2000000,
        status: ApplicationStatus.DOCUMENT_CHECK,
        createdAt: '2026-02-08'
      },
      {
        id: '3',
        applicationNumber: 'APP-2026-003',
        applicantName: 'สมหญิง ใจเต็ม',
        loanType: 'สินเชื่อเพื่อซื้อรถยนต์',
        requestedAmount: 800000,
        status: ApplicationStatus.APPROVED,
        createdAt: '2026-02-07'
      },
      {
        id: '4',
        applicationNumber: 'APP-2026-004',
        applicantName: 'สมคนธุรกิจ ลงทุน',
        loanType: 'สินเชื่อเพื่อวิสาหกิจ',
        requestedAmount: 3000000,
        status: ApplicationStatus.CREDIT_ANALYSIS,
        createdAt: '2026-02-06'
      }
    ];

    setApplications(mockApps);
    setLoading(false);
  }, []);

  const filteredApplications = filter === 'ALL' 
    ? applications
    : applications.filter(app => app.status === filter);

  return (
    <div className="container mx-auto px-4 py-8">
      {/* Header */}
      <div className="flex justify-between items-center mb-8">
        <div>
          <h1 className="text-4xl font-bold text-navy-dark mb-2">ใบสมัครของฉัน</h1>
          <p className="text-gray-600">ตรวจสอบสถานะใบสมัครขอสินเชื่อของคุณ</p>
        </div>
        <Link
          href="/applications/new"
          className="px-6 py-3 bg-gold text-white font-semibold rounded-lg hover:bg-yellow-600 transition-colors"
        >
          + สมัครใหม่
        </Link>
      </div>

      {/* Filters */}
      <div className="mb-8 flex gap-2 flex-wrap">
        {['ALL', ApplicationStatus.DRAFT, ApplicationStatus.SUBMITTED, ApplicationStatus.APPROVED, ApplicationStatus.REJECTED].map((status) => (
          <button
            key={status}
            onClick={() => setFilter(status as any)}
            className={`px-4 py-2 rounded-lg font-medium transition-colors ${
              filter === status
                ? 'bg-navy-dark text-white'
                : 'bg-gray-200 text-gray-700 hover:bg-gray-300'
            }`}
          >
            {status === 'ALL' ? 'ทั้งหมด' : <StatusBadge status={status as ApplicationStatus} size="sm" />}
          </button>
        ))}
      </div>

      {/* Applications List */}
      {loading ? (
        <div className="text-center py-12">
          <p className="text-gray-600">กำลังโหลด...</p>
        </div>
      ) : filteredApplications.length === 0 ? (
        <div className="text-center py-12 bg-gray-50 rounded-lg">
          <p className="text-gray-600 mb-4">ไม่พบใบสมัครลุกลาด</p>
          <Link
            href="/applications/new"
            className="inline-block px-6 py-3 bg-gold text-white rounded-lg font-medium hover:bg-yellow-600"
          >
            สมัครขอสินเชื่อ
          </Link>
        </div>
      ) : (
        <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
          {filteredApplications.map((app) => (
            <ApplicationCard key={app.id} application={app} />
          ))}
        </div>
      )}

      {/* Stats */}
      {applications.length > 0 && (
        <div className="mt-12 grid grid-cols-1 md:grid-cols-4 gap-4">
          {[
            { label: 'ทั้งหมด', value: applications.length, icon: '📋' },
            { label: 'รอการตรวจสอบ', value: applications.filter(a => a.status === ApplicationStatus.DOCUMENT_CHECK).length, icon: '⏳' },
            { label: 'อนุมัติแล้ว', value: applications.filter(a => a.status === ApplicationStatus.APPROVED).length, icon: '✓' },
            { label: 'ปฏิเสธ', value: applications.filter(a => a.status === ApplicationStatus.REJECTED).length, icon: '✗' }
          ].map((stat) => (
            <div key={stat.label} className="bg-white rounded-lg shadow p-6 text-center">
              <div className="text-4xl mb-2">{stat.icon}</div>
              <p className="text-gray-600 text-sm">{stat.label}</p>
              <p className="text-3xl font-bold text-navy-dark">{stat.value}</p>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}
