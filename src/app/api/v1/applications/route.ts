import { NextRequest, NextResponse } from 'next/server';

// Mock database
const mockApplications: any[] = [];

export async function GET(request: NextRequest) {
  const { searchParams } = new URL(request.url);
  const page = parseInt(searchParams.get('page') || '1');
  const pageSize = parseInt(searchParams.get('pageSize') || '20');
  const status = searchParams.get('status');

  let filtered = mockApplications;
  if (status) {
    filtered = mockApplications.filter(app => app.status === status);
  }

  const total = filtered.length;
  const totalPages = Math.ceil(total / pageSize);
  const items = filtered.slice((page - 1) * pageSize, page * pageSize);

  return NextResponse.json({
    success: true,
    data: {
      items,
      total,
      page,
      pageSize,
      totalPages
    }
  });
}

export async function POST(request: NextRequest) {
  try {
    const body = await request.json();

    const application = {
      id: `${Date.now()}`,
      applicationNumber: `APP-2026-${String(mockApplications.length + 1).padStart(3, '0')}`,
      ...body,
      status: 'DRAFT',
      createdAt: new Date().toISOString(),
      updatedAt: new Date().toISOString()
    };

    mockApplications.push(application);

    return NextResponse.json({
      success: true,
      data: application
    }, { status: 201 });
  } catch (error) {
    return NextResponse.json({
      success: false,
      error: 'Failed to create application'
    }, { status: 400 });
  }
}
