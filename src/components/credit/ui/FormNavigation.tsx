"use client";

interface FormNavigationProps {
  currentStep: number;
  totalSteps: number;
  onNext: () => void;
  onPrev: () => void;
  canGoNext: boolean;
  canGoPrev: boolean;
  isSubmitting?: boolean;
}

export function FormNavigation({
  currentStep,
  totalSteps,
  onNext,
  onPrev,
  canGoNext,
  canGoPrev,
  isSubmitting = false,
}: FormNavigationProps) {
  const isLastStep = currentStep === totalSteps;

  return (
    <div className="flex justify-between pt-8 border-t border-gray-200 mt-8">
      {canGoPrev && currentStep > 1 ? (
        <button
          onClick={onPrev}
          disabled={isSubmitting}
          className="px-6 py-3 bg-white text-navy-dark font-semibold rounded-lg border-2 border-navy-dark
                   hover:bg-gray-50 transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
        >
          ← ย้อนกลับ
        </button>
      ) : (
        <div />
      )}

      <button
        onClick={onNext}
        disabled={!canGoNext || isSubmitting}
        className="px-6 py-3 bg-navy-dark text-white font-semibold rounded-lg 
                 hover:bg-blue-900 transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
      >
        {isSubmitting
          ? "กำลังบันทึก..."
          : isLastStep
            ? "ส่งใบสมัคร"
            : "ต่อไป →"}
      </button>
    </div>
  );
}
