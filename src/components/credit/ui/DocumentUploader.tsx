"use client";

import { useState } from "react";
import { DocumentType } from "../types/entities";

interface DocumentUploaderProps {
  documentType: DocumentType;
  onUpload: (file: File) => Promise<void>;
  onRemove: () => void;
  maxSize?: number;
  acceptedFormats?: string[];
  uploadedFile?: File;
}

const DOCUMENT_LABELS: Record<DocumentType, string> = {
  ID_CARD: "บัตรประชาชน",
  HOUSE_REGISTRATION: "ทะเบียนบ้าน",
  INCOME_PROOF: "หลักฐานรายได้",
  BANK_STATEMENT: "สำเพจธนาคาร",
  TAX_RETURN: "ใบแนบอากร",
  COMPANY_REGISTRATION: "ใบจดทะเบียนบริษัท",
  FINANCIAL_STATEMENT: "งบการเงิน",
  COLLATERAL_DOCUMENT: "เอกสารประกอบ",
  OTHER: "อื่นๆ",
};

export function DocumentUploader({
  documentType,
  onUpload,
  onRemove,
  maxSize = 5 * 1024 * 1024, // 5MB
  acceptedFormats = ["pdf", "jpg", "jpeg", "png"],
  uploadedFile,
}: DocumentUploaderProps) {
  const [uploading, setUploading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [dragActive, setDragActive] = useState(false);

  const formatFileSize = (bytes: number) => {
    if (bytes === 0) return "0 Bytes";
    const k = 1024;
    const sizes = ["Bytes", "KB", "MB"];
    const i = Math.floor(Math.log(bytes) / Math.log(k));
    return Math.round((bytes / Math.pow(k, i)) * 100) / 100 + " " + sizes[i];
  };

  const validateFile = (file: File): string | null => {
    if (file.size > maxSize) {
      return `ไฟล์ใหญ่เกินไป (สูงสุด ${formatFileSize(maxSize)})`;
    }

    const ext = file.name.split(".").pop()?.toLowerCase();
    if (!ext || !acceptedFormats.includes(ext)) {
      return `รูปแบบไฟล์ไม่ถูกต้อง (อนุญาต: ${acceptedFormats.join(", ")})`;
    }

    return null;
  };

  const handleDrag = (e: React.DragEvent) => {
    e.preventDefault();
    e.stopPropagation();
    if (e.type === "dragenter" || e.type === "dragover") {
      setDragActive(true);
    } else if (e.type === "dragleave") {
      setDragActive(false);
    }
  };

  const handleDrop = async (e: React.DragEvent) => {
    e.preventDefault();
    e.stopPropagation();
    setDragActive(false);
    setError(null);

    const files = e.dataTransfer.files;
    if (files && files[0]) {
      const validationError = validateFile(files[0]);
      if (validationError) {
        setError(validationError);
        return;
      }

      setUploading(true);
      try {
        await onUpload(files[0]);
      } catch (err) {
        setError(err instanceof Error ? err.message : "อัปโหลดล้มเหลว");
      } finally {
        setUploading(false);
      }
    }
  };

  const handleChange = async (e: React.ChangeEvent<HTMLInputElement>) => {
    e.preventDefault();
    setError(null);
    const files = e.target.files;
    if (files && files[0]) {
      const validationError = validateFile(files[0]);
      if (validationError) {
        setError(validationError);
        return;
      }

      setUploading(true);
      try {
        await onUpload(files[0]);
      } catch (err) {
        setError(err instanceof Error ? err.message : "อัปโหลดล้มเหลว");
      } finally {
        setUploading(false);
      }
    }
  };

  return (
    <div className="space-y-4">
      <label className="block text-sm font-medium text-gray-700">
        {DOCUMENT_LABELS[documentType]} <span className="text-red-500">*</span>
      </label>

      {uploadedFile ? (
        <div className="border-2 border-green-200 rounded-lg p-4 bg-green-50">
          <div className="flex items-center justify-between">
            <div className="text-sm">
              <p className="font-medium text-green-800">{uploadedFile.name}</p>
              <p className="text-green-600">
                {formatFileSize(uploadedFile.size)}
              </p>
            </div>
            <button
              onClick={onRemove}
              disabled={uploading}
              className="px-3 py-1 bg-red-500 text-white rounded hover:bg-red-600 disabled:opacity-50"
            >
              ลบ
            </button>
          </div>
        </div>
      ) : (
        <div
          onDragEnter={handleDrag}
          onDragLeave={handleDrag}
          onDragOver={handleDrag}
          onDrop={handleDrop}
          className={`border-2 border-dashed rounded-lg p-8 text-center transition-colors ${
            dragActive ? "border-gold bg-gold/10" : "border-gray-300"
          } ${uploading ? "opacity-50 cursor-not-allowed" : "cursor-pointer"}`}
        >
          <input
            type="file"
            accept={acceptedFormats.map((f) => `.${f}`).join(",")}
            onChange={handleChange}
            disabled={uploading}
            className="hidden"
            id={`file-upload-${documentType}`}
          />
          <label
            htmlFor={`file-upload-${documentType}`}
            className="cursor-pointer block"
          >
            <div className="text-4xl mb-2">📄</div>
            <p className="font-medium text-gray-700">
              {uploading
                ? "กำลังอัปโหลด..."
                : "ลากไฟล์ที่นี่หรือคลิกเพื่อเลือก"}
            </p>
            <p className="text-sm text-gray-500 mt-1">
              รูปแบบ: {acceptedFormats.join(", ")} | สูงสุด:{" "}
              {formatFileSize(maxSize)}
            </p>
          </label>
        </div>
      )}

      {error && <p className="text-sm text-red-500">{error}</p>}
    </div>
  );
}
