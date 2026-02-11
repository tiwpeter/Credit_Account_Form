'use client';

import Link from 'next/link';

export default function Home() {
  return (
    <div className="bg-gradient-to-br from-slate-900 via-blue-900 to-slate-900 min-h-[calc(100vh-64px)] flex items-center">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-20 w-full">
        <div className="text-center mb-16">
          <h1 className="text-5xl sm:text-6xl font-bold text-white mb-6">
            Credit Application System
          </h1>
          <p className="text-xl text-blue-100 mb-8 max-w-2xl mx-auto">
            Complete, production-grade banking credit application solution. Manage loans,
            verify documents, and process applications with comprehensive business rules.
          </p>
          <div className="flex gap-4 justify-center">
            <Link
              href="/applications/new"
              className="px-8 py-4 bg-blue-600 text-white rounded-lg font-semibold hover:bg-blue-700 transition"
            >
              Start New Application
            </Link>
            <Link
              href="/applications"
              className="px-8 py-4 bg-white text-blue-600 rounded-lg font-semibold hover:bg-gray-100 transition"
            >
              View Applications
            </Link>
          </div>
        </div>

        {/* Features Grid */}
        <div className="grid md:grid-cols-3 gap-8 mt-20">
          {/* Feature 1 */}
          <div className="bg-white/10 backdrop-blur border border-white/20 rounded-lg p-8">
            <div className="w-12 h-12 rounded-lg bg-blue-500/20 flex items-center justify-center mb-4">
              <svg className="w-6 h-6 text-blue-300" fill="currentColor" viewBox="0 0 20 20">
                <path d="M11 3a1 1 0 10-2 0v1a1 1 0 102 0V3zM15.657 5.757a1 1 0 00-1.414-1.414l-.707.707a1 1 0 001.414 1.414l.707-.707zM18 10a1 1 0 01-1 1h-1a1 1 0 110-2h1a1 1 0 011 1zM15.657 14.243a1 1 0 001.414-1.414l-.707-.707a1 1 0 00-1.414 1.414l.707.707zM11 17a1 1 0 102 0v-1a1 1 0 10-2 0v1zM5.757 15.657a1 1 0 00-1.414-1.414l-.707.707a1 1 0 101.414 1.414l.707-.707zM2 10a1 1 0 011 1v1a1 1 0 11-2 0v-1a1 1 0 011-1zM5.757 4.343a1 1 0 00-1.414 1.414l.707.707a1 1 0 101.414-1.414l-.707-.707z" />
              </svg>
            </div>
            <h3 className="text-lg font-semibold text-white mb-2">Multi-Step Forms</h3>
            <p className="text-blue-150">8-step comprehensive form wizard with real-time validation</p>
          </div>

          {/* Feature 2 */}
          <div className="bg-white/10 backdrop-blur border border-white/20 rounded-lg p-8">
            <div className="w-12 h-12 rounded-lg bg-blue-500/20 flex items-center justify-center mb-4">
              <svg className="w-6 h-6 text-blue-300" fill="currentColor" viewBox="0 0 20 20">
                <path d="M13 7H7v6h6V7z" />
                <path fillRule="evenodd" d="M7 2a1 1 0 012 0v1h2V2a1 1 0 012 0v1h2V2a1 1 0 012 0v1h1a2 2 0 012 2v2h1a1 1 0 110 2h-1v2h1a1 1 0 110 2h-1v2h1a1 1 0 110 2h-1v1a2 2 0 01-2 2h-1v1a1 1 0 11-2 0v-1h-2v1a1 1 0 11-2 0v-1H9a2 2 0 01-2-2v-1H6a1 1 0 110-2h1v-2H6a1 1 0 010-2h1V9H6a1 1 0 010-2h1V6a2 2 0 012-2h1V2z" clipRule="evenodd" />
              </svg>
            </div>
            <h3 className="text-lg font-semibold text-white mb-2">Business Rules</h3>
            <p className="text-blue-150">DTI calculations, risk grading, and eligibility checking</p>
          </div>

          {/* Feature 3 */}
          <div className="bg-white/10 backdrop-blur border border-white/20 rounded-lg p-8">
            <div className="w-12 h-12 rounded-lg bg-blue-500/20 flex items-center justify-center mb-4">
              <svg className="w-6 h-6 text-blue-300" fill="currentColor" viewBox="0 0 20 20">
                <path d="M5 3a2 2 0 00-2 2v6h6V5a2 2 0 00-2-2H5zm6 0a2 2 0 00-2 2v6h6V5a2 2 0 00-2-2h-2zM5 13a2 2 0 00-2 2v2h2v-2h2v2h2v-2a2 2 0 00-2-2H5zm6 0a2 2 0 00-2 2v2h2v-2h2v2h2v-2a2 2 0 00-2-2h-2z" />
              </svg>
            </div>
            <h3 className="text-lg font-semibold text-white mb-2">Type Safety</h3>
            <p className="text-blue-150">Full TypeScript with comprehensive type definitions</p>
          </div>
        </div>

        {/* Stats */}
        <div className="grid md:grid-cols-4 gap-8 mt-20 text-center">
          <div>
            <p className="text-4xl font-bold text-blue-400 mb-2">8</p>
            <p className="text-blue-100">Form Steps</p>
          </div>
          <div>
            <p className="text-4xl font-bold text-blue-400 mb-2">40+</p>
            <p className="text-blue-100">Form Fields</p>
          </div>
          <div>
            <p className="text-4xl font-bold text-blue-400 mb-2">5</p>
            <p className="text-blue-100">Loan Types</p>
          </div>
          <div>
            <p className="text-4xl font-bold text-blue-400 mb-2">10+</p>
            <p className="text-blue-100">Business Rules</p>
          </div>
        </div>
      </div>
    </div>
  );
}
