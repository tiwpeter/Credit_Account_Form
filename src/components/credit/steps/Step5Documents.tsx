"use client";

import { UseFormReturn } from "react-hook-form";
import { DocumentsFormData } from "../types/form";
import { DocumentUploader } from "../ui/DocumentUploader";
import { DocumentType } from "../types/entities";
import { FormNavigation } from "../ui/FormNavigation";
import { useDocumentUpload } from "../hooks/use-document-upload";

interface Step5Props {
  form: UseFormReturn<DocumentsFormData>;
  onNext: () => void;
  onPrev: () => void;
}

export function Step5Documents({ form, onNext, onPrev }: Step5Props) {
  const {
    setValue,
    watch,
    formState: { errors },
  } = form;
  const { uploadDocument } = useDocumentUpload();

  const handleDocumentUpload = async (document: DocumentType) => {
    return async (file: File) => {
      const url = await uploadDocument(file, document);
      setValue(document as any, { fileName: file.name, fileUrl: url });
    };
  };

  return (
    <div className="space-y-6">
      <div>
        <h2 className="text-2xl font-bold text-navy-dark mb-2">
          ขั้นตอนที่ 5: เอกสารประกอบ
        </h2>
        <p className="text-gray-600 text-sm">กรุณาอัปโหลดเอกสารให้ครบถ้วน</p>
      </div>

      {/* Required Documents */}
      <div className="space-y-6 bg-blue-50 border border-blue-200 rounded-lg p-6">
        <DocumentUploader
          documentType={DocumentType.ID_CARD}
          onUpload={handleDocumentUpload(DocumentType.ID_CARD)}
          onRemove={() => setValue("idCardDocument" as any, undefined)}
        />

        <DocumentUploader
          documentType={DocumentType.HOUSE_REGISTRATION}
          onUpload={handleDocumentUpload(DocumentType.HOUSE_REGISTRATION)}
          onRemove={() =>
            setValue("houseRegistrationDocument" as any, undefined)
          }
        />

        <DocumentUploader
          documentType={DocumentType.INCOME_PROOF}
          onUpload={handleDocumentUpload(DocumentType.INCOME_PROOF)}
          onRemove={() => setValue("incomeProofDocument" as any, undefined)}
        />

        <DocumentUploader
          documentType={DocumentType.BANK_STATEMENT}
          onUpload={handleDocumentUpload(DocumentType.BANK_STATEMENT)}
          onRemove={() => setValue("bankStatementDocument" as any, undefined)}
        />
      </div>

      {/* Optional Documents */}
      <div className="space-y-6 border border-gray-200 rounded-lg p-6">
        <h3 className="text-lg font-semibold text-gray-900">
          เอกสารเพิ่มเติม (ถ้ามี)
        </h3>

        <DocumentUploader
          documentType={DocumentType.TAX_RETURN}
          onUpload={handleDocumentUpload(DocumentType.TAX_RETURN)}
          onRemove={() => setValue("taxReturnDocument" as any, undefined)}
        />

        <DocumentUploader
          documentType={DocumentType.OTHER}
          onUpload={handleDocumentUpload(DocumentType.OTHER)}
          onRemove={() => setValue("otherDocument" as any, undefined)}
        />
      </div>

      <FormNavigation
        currentStep={5}
        totalSteps={8}
        onNext={onNext}
        onPrev={onPrev}
        canGoNext={true}
        canGoPrev={true}
      />
    </div>
  );
}
