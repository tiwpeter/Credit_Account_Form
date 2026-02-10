import { NextResponse } from 'next/server';

export async function GET(
  request: Request,
  { params }: { params: { id: string } }
) {
  const { id } = params;

  // Mock workflow data
  const mockWorkflow = {
    status: 'SUBMITTED',
    availableActions: ['SUBMIT_DOCUMENTS', 'CANCEL'],
    history: [
      {
        status: 'DRAFT',
        timestamp: '2026-02-09T08:00:00Z'
      },
      {
        status: 'SUBMITTED',
        timestamp: '2026-02-09T10:30:00Z'
      }
    ]
  };

  return NextResponse.json({
    success: true,
    data: mockWorkflow
  });
}

export async function POST(
  request: Request,
  { params }: { params: { id: string } }
) {
  try {
    const body = await request.json();
    const { action, data } = body;

    // Mock action handler
    const result = {
      success: true,
      status: 'DOCUMENT_CHECK',
      availableActions: ['REVIEW_DOCUMENTS', 'REQUEST_MORE_INFO'],
      message: `Action '${action}' executed successfully`
    };

    return NextResponse.json({
      success: true,
      data: result
    });
  } catch (error) {
    return NextResponse.json({
      success: false,
      error: 'Failed to perform workflow action'
    }, { status: 400 });
  }
}
