// Workflow state machine and transitions

export const WORKFLOW_STATES = {
  DRAFT: {
    label: 'Draft',
    color: '#6B7280',
    description: 'Application is being filled out',
    allowedActions: ['SUBMIT', 'SAVE_DRAFT'],
  },
  SUBMITTED: {
    label: 'Submitted',
    color: '#3B82F6',
    description: 'Application has been submitted',
    allowedActions: ['DOCUMENT_CHECK', 'REJECT'],
  },
  DOCUMENT_CHECK: {
    label: 'Document Verification',
    color: '#F59E0B',
    description: 'Documents are being verified',
    allowedActions: ['PASS_DOCUMENT_CHECK', 'REQUEST_MORE_DOCUMENTS', 'REJECT'],
  },
  CREDIT_ANALYSIS: {
    label: 'Credit Analysis',
    color: '#9333EA',
    description: 'Credit analysis is in progress',
    allowedActions: ['APPROVE', 'CONDITIONAL_APPROVE', 'REJECT'],
  },
  APPROVAL: {
    label: 'Approval',
    color: '#EC4899',
    description: 'Awaiting final approval',
    allowedActions: ['APPROVE', 'REJECT'],
  },
  APPROVED: {
    label: 'Approved',
    color: '#10B981',
    description: 'Application has been approved',
    allowedActions: ['SIGN_CONTRACT', 'CANCEL'],
  },
  REJECTED: {
    label: 'Rejected',
    color: '#EF4444',
    description: 'Application has been rejected',
    allowedActions: [],
  },
  CONTRACT_SIGNED: {
    label: 'Contract Signed',
    color: '#06B6D4',
    description: 'Contract has been signed',
    allowedActions: ['DISBURSE', 'CANCEL'],
  },
  DISBURSED: {
    label: 'Disbursed',
    color: '#14B8A6',
    description: 'Loan has been disbursed',
    allowedActions: [],
  },
};

// Workflow Transitions
export const WORKFLOW_TRANSITIONS = {
  DRAFT: ['SUBMITTED', 'SAVE_DRAFT'],
  SUBMITTED: ['DOCUMENT_CHECK', 'REJECTED'],
  DOCUMENT_CHECK: ['CREDIT_ANALYSIS', 'SUBMITTED', 'REJECTED'],
  CREDIT_ANALYSIS: ['APPROVAL', 'REJECTED'],
  APPROVAL: ['APPROVED', 'REJECTED'],
  APPROVED: ['CONTRACT_SIGNED', 'REJECTED'],
  REJECTED: [],
  CONTRACT_SIGNED: ['DISBURSED', 'REJECTED'],
  DISBURSED: [],
};

// Role-based Permissions
export const ROLE_PERMISSIONS = {
  CUSTOMER: [
    'view_own_application',
    'create_application',
    'edit_draft_application',
    'submit_application',
    'view_application_status',
    'upload_documents',
  ],
  BANK_OFFICER: [
    'view_all_applications',
    'verify_documents',
    'request_more_documents',
    'view_credit_analysis',
    'add_notes',
    'create_credit_analysis',
  ],
  APPROVER: [
    'view_all_applications',
    'approve_application',
    'reject_application',
    'view_credit_analysis',
    'view_documents',
    'add_approval_notes',
  ],
  CREDIT_COMMITTEE: [
    'view_all_applications',
    'approve_application',
    'reject_application',
    'override_decision',
    'view_credit_analysis',
  ],
  ADMIN: [
    'view_all_applications',
    'edit_application',
    'delete_application',
    'manage_users',
    'view_reports',
    'system_settings',
  ],
};

// Step Definitions for Form Wizard
export const FORM_STEPS = [
  {
    number: 1,
    name: 'Personal Information',
    thai: 'ข้อมูลส่วนตัว',
    description: 'Basic personal information',
    fields: [
      'title',
      'firstName',
      'lastName',
      'gender',
      'dateOfBirth',
      'nationality',
      'idCardNumber',
      'maritalStatus',
      'dependents',
      'mobilePhone',
      'email',
    ],
    estimatedTime: '5 minutes',
  },
  {
    number: 2,
    name: 'Address Information',
    thai: 'ที่อยู่',
    description: 'Current and permanent address',
    fields: [
      'currentAddress',
      'permanentAddress',
      'yearsAtCurrentAddress',
      'housingType',
    ],
    estimatedTime: '3 minutes',
  },
  {
    number: 3,
    name: 'Income & Employment',
    thai: 'รายได้และการจ้างงาน',
    description: 'Employment and income details',
    fields: [
      'employmentType',
      'company',
      'position',
      'industry',
      'monthlyIncome',
      'annualIncome',
      'otherIncome',
      'employmentStartDate',
    ],
    estimatedTime: '5 minutes',
  },
  {
    number: 4,
    name: 'Credit Details',
    thai: 'รายละเอียดเงินกู้',
    description: 'Loan amount and existing debts',
    fields: [
      'loanType',
      'loanAmount',
      'loanPurpose',
      'tenorMonths',
      'existingLoans',
    ],
    estimatedTime: '7 minutes',
  },
  {
    number: 5,
    name: 'Documents',
    thai: 'เอกสาร',
    description: 'Upload required documents',
    fields: ['documents'],
    estimatedTime: '10 minutes',
  },
  {
    number: 6,
    name: 'Guarantors',
    thai: 'ผู้ค้ำประกัน',
    description: 'Guarantor information (if applicable)',
    fields: ['guarantors'],
    estimatedTime: '5 minutes',
    optional: true,
  },
  {
    number: 7,
    name: 'Company Information',
    thai: 'ข้อมูลบริษัท',
    description: 'Company details for business loans',
    fields: [
      'companyName',
      'businessType',
      'yearsInBusiness',
      'annualRevenue',
    ],
    estimatedTime: '5 minutes',
    conditional: 'loanType === SME || loanType === CORPORATE',
  },
  {
    number: 8,
    name: 'Review & Submit',
    thai: 'ตรวจสอบและส่ง',
    description: 'Review application and submit',
    fields: [
      'acceptTerms',
      'acceptPrivacy',
      'authorizedSigner',
    ],
    estimatedTime: '2 minutes',
  },
];

// Application Status Timeline
export const STATUS_TIMELINE = [
  {
    status: 'DRAFT',
    step: 1,
    label: 'Application Started',
    description: 'You\'ve started the application',
  },
  {
    status: 'SUBMITTED',
    step: 2,
    label: 'Application Submitted',
    description: 'Your application has been submitted to the bank',
  },
  {
    status: 'DOCUMENT_CHECK',
    step: 3,
    label: 'Documents Verification',
    description: 'Our team is verifying your documents',
  },
  {
    status: 'CREDIT_ANALYSIS',
    step: 4,
    label: 'Credit Analysis',
    description: 'We are analyzing your creditworthiness',
  },
  {
    status: 'APPROVAL',
    step: 5,
    label: 'Final Approval',
    description: 'Your application is under final review',
  },
  {
    status: 'APPROVED',
    step: 6,
    label: 'Application Approved',
    description: 'Congratulations! Your application has been approved',
  },
  {
    status: 'CONTRACT_SIGNED',
    step: 7,
    label: 'Contract Signed',
    description: 'The contract has been signed',
  },
  {
    status: 'DISBURSED',
    step: 8,
    label: 'Funds Disbursed',
    description: 'The loan amount has been transferred to your account',
  },
];

// Rejection Reasons
export const REJECTION_REASONS = [
  'Income too low',
  'DTI ratio too high',
  'Insufficient employment history',
  'Poor credit history',
  'Invalid documents',
  'Loan amount too high',
  'Missing required documents',
  'Identity verification failed',
  'Incomplete application',
  'Other',
];
