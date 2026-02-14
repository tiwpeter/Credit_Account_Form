'use client';

import { ReactNode } from 'react';

export default function ApplicationsLayout({
  children,
}: {
  children: ReactNode;
}) {
  return (
    <div className="min-h-screen bg-gradient-to-br from-slate-50 to-slate-100">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        {/* Header */}
        <div className="mb-8">
          <h1 className="text-4xl font-bold text-slate-900 mb-2">
            Credit Applications
          </h1>
          <p className="text-lg text-slate-600">
            Manage your credit applications and loan requests
          </p>
        </div>

        {/* Content */}
        {children}
      </div>
    </div>
  );
}
