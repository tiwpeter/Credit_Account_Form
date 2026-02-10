'use client';

import { useState, useCallback } from 'react';
import { DocumentType } from '../types/entities';

interface UseDocumentUploadReturn {
  uploadDocument: (file: File, type: DocumentType) => Promise<string>;
  uploading: boolean;
  progress: number;
  error: string | null;
}

export function useDocumentUpload(): UseDocumentUploadReturn {
  const [uploading, setUploading] = useState(false);
  const [progress, setProgress] = useState(0);
  const [error, setError] = useState<string | null>(null);

  const uploadDocument = useCallback(async (file: File, type: DocumentType): Promise<string> => {
    setUploading(true);
    setProgress(0);
    setError(null);

    try {
      const formData = new FormData();
      formData.append('file', file);
      formData.append('type', type);

      const response = await fetch('/api/v1/documents', {
        method: 'POST',
        body: formData
      });

      if (!response.ok) {
        throw new Error('Upload failed');
      }

      const data = await response.json();
      setProgress(100);
      return data.url;
    } catch (err) {
      const message = err instanceof Error ? err.message : 'Upload failed';
      setError(message);
      throw err;
    } finally {
      setUploading(false);
    }
  }, []);

  return { uploadDocument, uploading, progress, error };
}
