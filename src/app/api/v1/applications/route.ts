import { NextRequest, NextResponse } from 'next/server';
import type { ApiResponse, PaginatedResponse } from '@/components/credit/types/api';
import type { CreditApplication } from '@/components/credit/types/entities';

// Mock database
const mockApplications: Record<string, CreditApplication> = {
  'APP-001': {
    id: 'APP-001',
    applicationNumber: 'APP-2026-001',
    customerId: 'CUST-001',
    status: 'SUBMITTED',
    loanType: 'PERSONAL',
    loanAmount: 500000,
    personalInfo: {
      id: 'PI-001',
      title: 'Mr',
      firstName: 'Somchai',
      lastName: 'Noomsai',
      gender: 'MALE',
      dateOfBirth: '1985-06-15',
      nationality: 'Thai',
      idCardNumber: '1234567890123',
      mobilePhone: '0891234567',
      email: 'somchai@example.com',
      maritalStatus: 'MARRIED',
      dependents: 2,
      createdAt: '2026-02-01T10:00:00Z',
      updatedAt: '2026-02-01T10:00:00Z',
    },
    documents: [],
    guarantors: [],
    approvalHistory: [],
    createdAt: '2026-02-01T10:00:00Z',
    updatedAt: '2026-02-05T14:30:00Z',
  },
  'APP-002': {
    id: 'APP-002',
    applicationNumber: 'APP-2026-002',
    customerId: 'CUST-002',
    status: 'DRAFT',
    loanType: 'HOME',
    loanAmount: 5000000,
    personalInfo: {
      id: 'PI-002',
      title: 'Mrs',
      firstName: 'Siriporn',
      lastName: 'Kaewsiree',
      gender: 'FEMALE',
      dateOfBirth: '1988-03-22',
      nationality: 'Thai',
      idCardNumber: '9876543210123',
      mobilePhone: '0898765432',
      email: 'siriporn@example.com',
      maritalStatus: 'MARRIED',
      dependents: 1,
      createdAt: '2026-02-08T09:00:00Z',
      updatedAt: '2026-02-08T09:00:00Z',
    },
    documents: [],
    guarantors: [],
    approvalHistory: [],
    createdAt: '2026-02-08T09:00:00Z',
    updatedAt: '2026-02-08T09:00:00Z',
  },
};

/**
 * GET /api/v1/applications
 * List all applications for a customer or admin
 */
export async function GET(request: NextRequest) {
  try {
    const searchParams = request.nextUrl.searchParams;
    const page = parseInt(searchParams.get('page') || '1');
    const pageSize = parseInt(searchParams.get('pageSize') || '10');
    const customerId = searchParams.get('customerId');
    const status = searchParams.get('status');

    // Filter applications
    let filtered = Object.values(mockApplications);

    if (customerId) {
      filtered = filtered.filter((app) => app.customerId === customerId);
    }

    if (status) {
      filtered = filtered.filter((app) => app.status === status);
    }

    // Pagination
    const total = filtered.length;
    const startIndex = (page - 1) * pageSize;
    const items = filtered.slice(startIndex, startIndex + pageSize);

    const response: ApiResponse<PaginatedResponse<CreditApplication>> = {
      success: true,
      data: {
        items,
        total,
        page,
        pageSize,
        totalPages: Math.ceil(total / pageSize),
      },
      timestamp: new Date().toISOString(),
    };

    return NextResponse.json(response);
  } catch (error) {
    const errorResponse: ApiResponse<null> = {
      success: false,
      error: {
        code: 'GET_APPLICATIONS_ERROR',
        message: 'Failed to fetch applications',
      },
      timestamp: new Date().toISOString(),
    };

    return NextResponse.json(errorResponse, { status: 500 });
  }
}

/**
 * POST /api/v1/applications
 * Create a new application
 */
export async function POST(request: NextRequest) {
  try {
    const body = await request.json();

    // Generate new ID
    const newId = `APP-${String(Object.keys(mockApplications).length + 1).padStart(3, '0')}`;
    const now = new Date().toISOString();

    const newApplication: CreditApplication = {
      id: newId,
      applicationNumber: `APP-2026-${String(Object.keys(mockApplications).length + 1).padStart(3, '0')}`,
      customerId: body.customerId || 'CUST-' + newId,
      status: 'DRAFT',
      loanType: body.loanType,
      loanAmount: body.loanAmount,
      personalInfo: {
        id: 'PI-' + newId,
        title: body.step1Data.title,
        firstName: body.step1Data.firstName,
        lastName: body.step1Data.lastName,
        gender: body.step1Data.gender,
        dateOfBirth: body.step1Data.dateOfBirth,
        nationality: body.step1Data.nationality,
        idCardNumber: body.step1Data.idCardNumber,
        mobilePhone: body.step1Data.mobilePhone,
        email: body.step1Data.email,
        maritalStatus: body.step1Data.maritalStatus,
        dependents: body.step1Data.dependents,
        createdAt: now,
        updatedAt: now,
      },
      documents: [],
      guarantors: [],
      approvalHistory: [],
      createdAt: now,
      updatedAt: now,
    };

    mockApplications[newId] = newApplication;

    const response: ApiResponse<CreditApplication> = {
      success: true,
      data: newApplication,
      timestamp: now,
    };

    return NextResponse.json(response, { status: 201 });
  } catch (error) {
    const errorResponse: ApiResponse<null> = {
      success: false,
      error: {
        code: 'CREATE_APPLICATION_ERROR',
        message: 'Failed to create application',
        details: {
          error: String(error),
        },
      },
      timestamp: new Date().toISOString(),
    };

    return NextResponse.json(errorResponse, { status: 400 });
  }
}
