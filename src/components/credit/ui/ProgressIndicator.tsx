"use client";

const STEP_LABELS = [
  "ข้อมูลส่วนตัว",
  "ที่อยู่",
  "รายได้ & การจ้างงาน",
  "รายละเอียดการกู้",
  "เอกสาร",
  "ผู้ค้ำประกัน",
  "ข้อมูลบริษัท",
  "ตรวจสอบ",
];

interface ProgressIndicatorProps {
  currentStep: number;
  totalSteps: number;
  onStepClick?: (step: number) => void;
}

export function ProgressIndicator({
  currentStep,
  totalSteps,
  onStepClick,
}: ProgressIndicatorProps) {
  return (
    <div className="w-full">
      {/* Progress Bar */}
      <div className="mb-8">
        <div className="h-2 bg-gray-200 rounded-full overflow-hidden">
          <div
            className="h-full bg-gold transition-all duration-300"
            style={{ width: `${(currentStep / totalSteps) * 100}%` }}
          />
        </div>
        <p className="mt-2 text-sm text-gray-600 text-center">
          ขั้นตอนที่ {currentStep}/{totalSteps}
        </p>
      </div>

      {/* Step Indicators */}
      <div className="flex justify-between">
        {Array.from({ length: totalSteps }, (_, i) => i + 1).map((step) => (
          <div key={step} className="flex flex-col items-center flex-1">
            <button
              onClick={() => onStepClick?.(step)}
              disabled={!onStepClick}
              className={`w-10 h-10 rounded-full font-bold transition-all mb-2 ${
                step < currentStep
                  ? "bg-gold text-white"
                  : step === currentStep
                    ? "bg-navy-dark text-white ring-2 ring-gold"
                    : "bg-gray-200 text-gray-600"
              } ${onStepClick ? "cursor-pointer hover:scale-110" : "cursor-default"}`}
            >
              {step < currentStep ? "✓" : step}
            </button>
            <span className="text-xs text-center text-gray-600 leading-tight px-1">
              {STEP_LABELS[step - 1]}
            </span>
          </div>
        ))}
      </div>
    </div>
  );
}
