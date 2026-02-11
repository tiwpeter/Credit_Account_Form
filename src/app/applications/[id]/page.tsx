'use client';

import { useEffect, useState } from 'react';
import type { CreditApplication } from '@/components/credit/types/entities';

export default function ApplicationDetailPage({
  params,
}: {
  params: { id: string };
}) {
  const [application, setApplication] = useState<CreditApplication | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchApplication = async () => {
      try {
        const response = await fetch(`/api/v1/applications?page=1&pageSize=100`);
        const data = await response.json();

        if (data.success && data.data) {
          const app = data.data.items.find((a: CreditApplication) => a.id === params.id);
          if (app) {
            setApplication(app);
          } else {
            setError('Application not found');
          }
        }
      } catch (err) {
        setError('Failed to load application');
        console.error(err);
      } finally {
        setLoading(false);
      }
    };

    fetchApplication();
  }, [params.id]);

  if (loading) {
    return <div className="h-96 bg-white rounded-lg animate-pulse" />;
  }

  if (error || !application) {
    return (
      <div className="bg-red-50 border border-red-200 rounded-lg p-4">
        <p className="text-red-800">{error || 'Application not found'}</p>
      </div>
    );
  }

  return (
    <div className="max-w-4xl mx-auto">
      {/* Header */}
      <div className="bg-white rounded-lg shadow p-6 mb-6">
        <div className="flex justify-between items-start mb-4">
          <div>
            <h1 className="text-3xl font-bold text-gray-900">
              {application.personalInfo.firstName} {application.personalInfo.lastName}
            </h1>
            <p className="text-lg text-gray-600">{application.applicationNumber}</p>
          </div>
          <span className={`px-4 py-2 rounded-lg text-sm font-semibold ${
            application.status === 'APPROVED' ? 'bg-green-100 text-green-800' :
            application.status === 'REJECTED' ? 'bg-red-100 text-red-800' :
            application.status === 'DRAFT' ? 'bg-gray-100 text-gray-800' :
            'bg-blue-100 text-blue-800'
          }`}>
            {application.status}
          </span>
        </div>

        <div className="grid grid-cols-2 gap-6 text-sm">
          <div>
            <p className="text-gray-600 mb-1">Loan Type</p>
            <p className="font-semibold text-gray-900">{application.loanType}</p>
          </div>
          <div>
            <p className="text-gray-600 mb-1">Loan Amount</p>
            <p className="font-semibold text-gray-900">
              ฿{application.loanAmount.toLocaleString()}
            </p>
          </div>
          <div>
            <p className="text-gray-600 mb-1">Email</p>
            <p className="font-semibold text-gray-900">{application.personalInfo.email}</p>
          </div>
          <div>
            <p className="text-gray-600 mb-1">Phone</p>
            <p className="font-semibold text-gray-900">{application.personalInfo.mobilePhone}</p>
          </div>
        </div>
      </div>

      {/* Personal Information */}
      <div className="bg-white rounded-lg shadow p-6 mb-6">
        <h2 className="text-2xl font-bold text-gray-900 mb-4">Personal Information</h2>
        <div className="grid grid-cols-2 gap-6 text-sm">
          <div>
            <p className="text-gray-600 mb-1">ID Card Number</p>
            <p className="font-semibold text-gray-900">{application.personalInfo.idCardNumber}</p>
          </div>
          <div>
            <p className="text-gray-600 mb-1">Date of Birth</p>
            <p className="font-semibold text-gray-900">
              {new Date(application.personalInfo.dateOfBirth).toLocaleDateString('th-TH')}
            </p>
          </div>
          <div>
            <p className="text-gray-600 mb-1">Gender</p>
            <p className="font-semibold text-gray-900">{application.personalInfo.gender}</p>
          </div>
          <div>
            <p className="text-gray-600 mb-1">Marital Status</p>
            <p className="font-semibold text-gray-900">{application.personalInfo.maritalStatus}</p>
          </div>
          <div>
            <p className="text-gray-600 mb-1">Dependents</p>
            <p className="font-semibold text-gray-900">{application.personalInfo.dependents}</p>
          </div>
          <div>
            <p className="text-gray-600 mb-1">Nationality</p>
            <p className="font-semibold text-gray-900">{application.personalInfo.nationality}</p>
          </div>
        </div>
      </div>

      {/* Application Timeline */}
      <div className="bg-white rounded-lg shadow p-6">
        <h2 className="text-2xl font-bold text-gray-900 mb-4">Timeline</h2>
        <div className="space-y-4">
          <div className="flex gap-4">
            <div className="flex flex-col items-center gap-2">
              <div className="w-4 h-4 bg-blue-600 rounded-full" />
              <div className="w-1 h-12 bg-gray-300" />
            </div>
            <div>
              <p className="font-semibold text-gray-900">Application Created</p>
              <p className="text-sm text-gray-600">
                {new Date(application.createdAt).toLocaleString('th-TH')}
              </p>
            </div>
          </div>
          <div className="flex gap-4">
            <div className="flex flex-col items-center gap-2">
              <div className="w-4 h-4 bg-gray-300 rounded-full" />
            </div>
            <div>
              <p className="font-semibold text-gray-900">Last Updated</p>
              <p className="text-sm text-gray-600">
                {new Date(application.updatedAt).toLocaleString('th-TH')}
              </p>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
