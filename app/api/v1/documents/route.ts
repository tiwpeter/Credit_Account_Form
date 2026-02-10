import { NextRequest, NextResponse } from 'next/server';

// Mock documents storage
const mockDocuments: any[] = [];

export async function POST(request: NextRequest) {
  try {
    const formData = await request.formData();
    const file = formData.get('file') as File;
    const type = formData.get('type') as string;

    if (!file) {
      return NextResponse.json({
        success: false,
        error: 'No file provided'
      }, { status: 400 });
    }

    // Simulate file upload
    const document = {
      id: `doc-${Date.now()}`,
      fileName: file.name,
      fileSize: file.size,
      type,
      url: `/uploads/${Date.now()}-${file.name}`,
      uploadedAt: new Date().toISOString()
    };

    mockDocuments.push(document);

    return NextResponse.json({
      success: true,
      data: document
    }, { status: 201 });
  } catch (error) {
    return NextResponse.json({
      success: false,
      error: 'Failed to upload document'
    }, { status: 400 });
  }
}
