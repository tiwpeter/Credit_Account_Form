/**
 * BANK CREDIT SYSTEM - DATABASE SEED
 * Seeds: roles, admin user, bank officers, approvers, customers, and example applications
 */

import { PrismaClient } from '@prisma/client';
import * as bcrypt from 'bcrypt';

const prisma = new PrismaClient();

const SALT_ROUNDS = 12;

async function hashPassword(password: string): Promise<string> {
  return bcrypt.hash(password, SALT_ROUNDS);
}

async function main() {
  console.log('🌱 Starting database seed...\n');

  // ============================================================
  // 1. SEED ROLES
  // ============================================================
  console.log('📋 Seeding roles...');

  const roles = await Promise.all([
    prisma.role.upsert({
      where: { code: 'CUSTOMER' },
      update: {},
      create: {
        code: 'CUSTOMER',
        name: 'Customer',
        description: 'Bank customer applying for credit',
      },
    }),
    prisma.role.upsert({
      where: { code: 'BANK_OFFICER' },
      update: {},
      create: {
        code: 'BANK_OFFICER',
        name: 'Bank Officer',
        description: 'Bank officer responsible for application review and document verification',
      },
    }),
    prisma.role.upsert({
      where: { code: 'APPROVER' },
      update: {},
      create: {
        code: 'APPROVER',
        name: 'Approver',
        description: 'Senior staff with authority to approve or reject credit applications',
      },
    }),
    prisma.role.upsert({
      where: { code: 'ADMIN' },
      update: {},
      create: {
        code: 'ADMIN',
        name: 'System Administrator',
        description: 'Full system access including master data management',
      },
    }),
  ]);

  console.log(`   ✓ ${roles.length} roles created/updated`);

  const [customerRole, officerRole, approverRole, adminRole] = roles;

  // ============================================================
  // 2. SEED ADMIN USER
  // ============================================================
  console.log('👤 Seeding admin user...');

  const adminUser = await prisma.user.upsert({
    where: { email: 'admin@bank.th' },
    update: {},
    create: {
      email: 'admin@bank.th',
      password: await hashPassword('Admin@1234!'),
      firstName: 'System',
      lastName: 'Administrator',
      phoneNumber: '0200000001',
      userRoles: {
        create: { roleId: adminRole.id },
      },
    },
  });

  console.log(`   ✓ Admin: ${adminUser.email} (password: Admin@1234!)`);

  // ============================================================
  // 3. SEED BANK OFFICERS
  // ============================================================
  console.log('👔 Seeding bank officers...');

  const officer1 = await prisma.user.upsert({
    where: { email: 'officer.somchai@bank.th' },
    update: {},
    create: {
      email: 'officer.somchai@bank.th',
      password: await hashPassword('Officer@1234!'),
      firstName: 'Somchai',
      lastName: 'Prachan',
      phoneNumber: '0812340001',
      userRoles: {
        create: { roleId: officerRole.id },
      },
    },
  });

  const officer2 = await prisma.user.upsert({
    where: { email: 'officer.malee@bank.th' },
    update: {},
    create: {
      email: 'officer.malee@bank.th',
      password: await hashPassword('Officer@1234!'),
      firstName: 'Malee',
      lastName: 'Siriwan',
      phoneNumber: '0812340002',
      userRoles: {
        create: { roleId: officerRole.id },
      },
    },
  });

  console.log(`   ✓ Officers: ${officer1.email}, ${officer2.email} (password: Officer@1234!)`);

  // ============================================================
  // 4. SEED APPROVERS
  // ============================================================
  console.log('✅ Seeding approvers...');

  const approver1 = await prisma.user.upsert({
    where: { email: 'approver.wanchai@bank.th' },
    update: {},
    create: {
      email: 'approver.wanchai@bank.th',
      password: await hashPassword('Approver@1234!'),
      firstName: 'Wanchai',
      lastName: 'Thongchai',
      phoneNumber: '0812350001',
      userRoles: {
        create: { roleId: approverRole.id },
      },
    },
  });

  console.log(`   ✓ Approver: ${approver1.email} (password: Approver@1234!)`);

  // ============================================================
  // 5. SEED CUSTOMERS
  // ============================================================
  console.log('🙋 Seeding customers...');

  const customer1 = await prisma.user.upsert({
  where: { email: 'customer.napat@email.com' },
    update: {},
    create: {
      email: 'customer.napat@email.com',
      password: await hashPassword('Customer@1234!'),
      firstName: 'Napat',
      lastName: 'Kasem',
      phoneNumber: '0812360001',
      userRoles: {
        create: { roleId: customerRole.id },
      },
    },
  });

  const customer2 = await prisma.user.upsert({
    where: { email: 'customer.suda@email.com' },
    update: {},
    create: {
      email: 'customer.suda@email.com',
      password: await hashPassword('Customer@1234!'),
      firstName: 'Suda',
      lastName: 'Pornpan',
      phoneNumber: '0812360002',
      userRoles: {
        create: { roleId: customerRole.id },
      },
    },
  });

  console.log(`   ✓ Customers: ${customer1.email}, ${customer2.email} (password: Customer@1234!)`);

  // ============================================================
  // 6. SEED INTEREST RATE CONFIG
  // ============================================================
  console.log('💰 Seeding interest rate configurations...');

  const rateConfigs = [
    // Personal Loan rates
    { riskGrade: 'A_PLUS', loanType: 'PERSONAL_LOAN', minRate: 0.04, maxRate: 0.06, baseRate: 0.045 },
    { riskGrade: 'A',      loanType: 'PERSONAL_LOAN', minRate: 0.06, maxRate: 0.08, baseRate: 0.065 },
    { riskGrade: 'B_PLUS', loanType: 'PERSONAL_LOAN', minRate: 0.08, maxRate: 0.10, baseRate: 0.085 },
    { riskGrade: 'B',      loanType: 'PERSONAL_LOAN', minRate: 0.10, maxRate: 0.13, baseRate: 0.110 },
    { riskGrade: 'C',      loanType: 'PERSONAL_LOAN', minRate: 0.13, maxRate: 0.18, baseRate: 0.150 },
    // Corporate Loan rates
    { riskGrade: 'A_PLUS', loanType: 'CORPORATE_LOAN', minRate: 0.03, maxRate: 0.05, baseRate: 0.035 },
    { riskGrade: 'A',      loanType: 'CORPORATE_LOAN', minRate: 0.05, maxRate: 0.07, baseRate: 0.055 },
    { riskGrade: 'B_PLUS', loanType: 'CORPORATE_LOAN', minRate: 0.07, maxRate: 0.09, baseRate: 0.075 },
    { riskGrade: 'B',      loanType: 'CORPORATE_LOAN', minRate: 0.09, maxRate: 0.12, baseRate: 0.095 },
    { riskGrade: 'C',      loanType: 'CORPORATE_LOAN', minRate: 0.12, maxRate: 0.16, baseRate: 0.130 },
  ];

  for (const config of rateConfigs) {
    await prisma.interestRateConfig.upsert({
      where: {
        riskGrade_loanType_effectiveFrom: {
          riskGrade: config.riskGrade as any,
          loanType: config.loanType as any,
          effectiveFrom: new Date('2024-01-01'),
        },
      },
      update: {},
      create: {
        riskGrade: config.riskGrade as any,
        loanType: config.loanType as any,
        minRate: config.minRate,
        maxRate: config.maxRate,
        baseRate: config.baseRate,
        effectiveFrom: new Date('2024-01-01'),
      },
    });
  }

  console.log(`   ✓ ${rateConfigs.length} interest rate configurations seeded`);

  // ============================================================
  // 7. SEED EXAMPLE APPLICATION (Personal Loan - In Progress)
  // ============================================================
  console.log('📄 Seeding example applications...');

  const existingApp = await prisma.creditApplication.findFirst({
    where: { applicationNumber: 'APP-2024-000001' },
  });

  if (!existingApp) {
    const exampleApp = await prisma.creditApplication.create({
      data: {
        applicationNumber: 'APP-2024-000001',
        type: 'PERSONAL_LOAN',
        status: 'SUBMITTED',
        customerId: customer1.id,
        assignedOfficerId: officer1.id,
        requestedAmount: 250000,
        requestedTenureMonths: 36,
        loanPurpose: 'Home renovation',
        submittedAt: new Date('2024-01-15T10:30:00Z'),
        personalApplicant: {
          create: {
            nationalId: '1234567890123',
            firstName: 'Napat',
            lastName: 'Kasem',
            firstNameTh: 'นภัส',
            lastNameTh: 'เกษม',
            dateOfBirth: new Date('1990-03-20'),
            gender: 'MALE',
            maritalStatus: 'SINGLE',
            phoneNumber: '0812360001',
            email: 'customer.napat@email.com',
            employmentType: 'EMPLOYED',
            employerName: 'Bangkok Software Co., Ltd.',
            jobTitle: 'Senior Developer',
            yearsEmployed: 4.5,
            monthlyIncome: 75000,
            otherIncome: 5000,
            monthlyExpenses: 20000,
            existingDebtPayment: 8000,
            addresses: {
              createMany: {
                data: [
                  {
                    addressType: 'HOME',
                    isPrimary: true,
                    addressLine1: '88/5 Moo 3, Soi Lat Phrao 101',
                    subDistrict: 'Khlong Chan',
                    district: 'Bang Kapi',
                    province: 'Bangkok',
                    postalCode: '10240',
                  },
                ],
              },
            },
          },
        },
      },
    });

    // Status history
    await prisma.applicationStatusHistory.createMany({
      data: [
        {
          applicationId: exampleApp.id,
          fromStatus: null,
          toStatus: 'DRAFT',
          changedBy: customer1.id,
          changedByName: 'Napat Kasem',
          remark: 'Application created',
          createdAt: new Date('2024-01-14T14:00:00Z'),
        },
        {
          applicationId: exampleApp.id,
          fromStatus: 'DRAFT',
          toStatus: 'SUBMITTED',
          changedBy: customer1.id,
          changedByName: 'Napat Kasem',
          remark: 'Application submitted by customer',
          createdAt: new Date('2024-01-15T10:30:00Z'),
        },
      ],
    });

    console.log(`   ✓ Example application APP-2024-000001 created (SUBMITTED)`);
  }

  // ============================================================
  // SUMMARY
  // ============================================================
  console.log('\n🎉 Seed completed successfully!\n');
  console.log('═══════════════════════════════════════════════════════');
  console.log('                  SEED CREDENTIALS                      ');
  console.log('═══════════════════════════════════════════════════════');
  console.log('ADMIN:         admin@bank.th            / Admin@1234!   ');
  console.log('OFFICER:       officer.somchai@bank.th  / Officer@1234! ');
  console.log('OFFICER:       officer.malee@bank.th    / Officer@1234! ');
  console.log('APPROVER:      approver.wanchai@bank.th / Approver@1234!');
  console.log('CUSTOMER:      customer.napat@email.com / Customer@1234!');
  console.log('CUSTOMER:      customer.suda@email.com  / Customer@1234!');
  console.log('═══════════════════════════════════════════════════════\n');
}

main()
  .catch((e) => {
    console.error('❌ Seed failed:', e);
    process.exit(1);
  })
  .finally(async () => {
    await prisma.$disconnect();
  });
