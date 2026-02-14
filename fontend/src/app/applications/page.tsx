'use client';

import { useEffect, useState } from 'react';
import Link from 'next/link';
import type { CreditApplication } from '@/components/credit/types/entities';

export default function ApplicationsPage() {
  const [applications, setApplications] = useState<CreditApplication[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchApplications = async () => {
      try {
        const response = await fetch('/api/v1/applications?page=1&pageSize=10');
        const data = await response.json();

        if (data.success && data.data) {
          setApplications(data.data.items);
        } else {
          setError('Failed to load applications');
        }
      } catch (err) {
        setError('Error loading applications');
        console.error(err);
      } finally {
        setLoading(false);
      }
    };

    fetchApplications();
  }, []);

  if (loading) {
    return (
      <div className="space-y-4">
        {[...Array(3)].map((_, i) => (
          <div key={i} className="h-24 bg-white rounded-lg animate-pulse" />
        ))}
      </div>
    );
  }

  if (error) {
    return (
      <div className="bg-red-50 border border-red-200 rounded-lg p-4">
        <p className="text-red-800">{error}</p>
      </div>
    );
  }

  return (
    <div className="space-y-6">
      {/* Create New Application Button */}
      <Link
        href="/applications/new"
        className="inline-flex items-center px-6 py-3 bg-blue-600 text-white rounded-lg font-semibold hover:bg-blue-700 transition"
      >
        <span className="mr-2">+</span>
        New Application
      </Link>

      {/* Applications Grid */}
      <div className="grid gap-6">
        {applications.length === 0 ? (
          <div className="text-center py-12 bg-white rounded-lg">
            <p className="text-gray-600 text-lg mb-4">No applications yet</p>
            <Link
              href="/applications/new"
              className="text-blue-600 hover:text-blue-700 font-semibold"
            >
              Start a new application →
            </Link>
          </div>
        ) : (
          applications.map((app) => (
            <Link
              key={app.id}
              href={`/applications/${app.id}`}
              className="bg-white p-6 rounded-lg border border-gray-200 hover:border-blue-500 transition hover:shadow-md"
            >
              <div className="flex justify-between items-start mb-4">
                <div>
                  <h3 className="text-lg font-semibold text-gray-900">
                    {app.personalInfo.firstName} {app.personalInfo.lastName}
                  </h3>
                  <p className="text-sm text-gray-600">{app.applicationNumber}</p>
                </div>
                <span className={`px-3 py-1 rounded-full text-sm font-semibold ${
                  app.status === 'APPROVED' ? 'bg-green-100 text-green-800' :
                  app.status === 'REJECTED' ? 'bg-red-100 text-red-800' :
                  app.status === 'DRAFT' ? 'bg-gray-100 text-gray-800' :
                  'bg-blue-100 text-blue-800'
                }`}>
                  {app.status}
                </span>
              </div>
              <div className="grid grid-cols-2 gap-4 text-sm">
                <div>
                  <p className="text-gray-600">Loan Type</p>
                  <p className="font-semibold text-gray-900">{app.loanType}</p>
                </div>
                <div>
                  <p className="text-gray-600">Amount</p>
                  <p className="font-semibold text-gray-900">
                    ฿{app.loanAmount.toLocaleString()}
                  </p>
                </div>
              </div>
            </Link>
          ))
        )}
      </div>
    </div>
  );
}
