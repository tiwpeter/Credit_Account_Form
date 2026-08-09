import { CommonModule } from '@angular/common';
import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBar } from '@angular/material/snack-bar';

import { MasterFacadeService } from '../../master/services/master-facade.service';
import { RegisterService } from '../services/register.service';

@Component({
  selector: 'app-register-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatCheckboxModule,
    MatButtonModule,
    MatIconModule,
    MatProgressBarModule
  ],
  templateUrl: './register-form.component.html',
  styleUrl: './register-form.component.scss'
})
export class RegisterFormComponent implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly master = inject(MasterFacadeService);
  private readonly registerService = inject(RegisterService);
  private readonly snackBar = inject(MatSnackBar);

  // ---- master dropdown data (จาก /api/master/all ผ่าน MasterFacadeService) ----
  readonly sales = this.master.sales;
  readonly type = this.master.type;
  readonly finance = this.master.finance;
  readonly address = this.master.address;
  readonly masterLoading = this.master.loading;
  readonly masterError = this.master.error;

  // ---- cascading address dropdown ----
  readonly amphures = this.master.amphures;
  readonly tambons = this.master.tambons;
  readonly amphuresLoading = this.master.amphuresLoading;
  readonly tambonsLoading = this.master.tambonsLoading;

  readonly submitting = this.registerService.submitting;

  // ---- progress: กรอกครบกี่หมวดจาก 8 หมวด ----
  private readonly sectionKeys = [
    'generalName1', 'addrLine1', 'creditLimit', 'companyCertificate',
    'custsignFirstname', 'saleOrg', 'termOfPay', 'busitypeId'
  ] as const;

  readonly progress = computed(() => this._progressTick());
  private readonly _progressTick = signal(0);

  readonly form = this.fb.nonNullable.group({
    // Step 1 — ข้อมูลบริษัท
    generalName1: this.fb.nonNullable.control('', Validators.required),
    generalName2: this.fb.control<string | null>(null),
    generalTel: this.fb.control<string | null>(null),
    generalFax: this.fb.control<string | null>(null),
    generalEmail: this.fb.control<string | null>(null, Validators.email),
    generalLine: this.fb.control<string | null>(null),
    generalTax: this.fb.control<string | null>(null),
    generalBranch: this.fb.control<string | null>(null),

    // Step 2 — ที่อยู่ (cascading: provinceId -> amphureId -> tambonId)
    addrLine1: this.fb.control<string | null>(null),
    addrLine2: this.fb.control<string | null>(null),
    provinceId: this.fb.control<number | null>(null),
    amphureId: this.fb.control<number | null>({ value: null, disabled: true }),
    tambonId: this.fb.control<number | null>({ value: null, disabled: true }),
    postalCode: this.fb.control<string | null>(null),
    countryId: this.fb.control<number | null>(null),

    // Step 3 — วงเงิน
    creditLimit: this.fb.control<number | null>(null),
    estimatedPurchase: this.fb.control<number | null>(null),
    timeRequired: this.fb.control<string | null>(null),

    // Step 4 — เอกสาร
    companyCertificate: this.fb.nonNullable.control(false),
    copyOfPp20: this.fb.nonNullable.control(false),
    copyOfCoRegis: this.fb.nonNullable.control(false),
    copyOfIdCard: this.fb.nonNullable.control(false),
    companyLocationMap: this.fb.nonNullable.control(false),
    otherSpecify: this.fb.control<string | null>(null),

    // Step 5 — ผู้ลงนาม
    custsignFirstname: this.fb.nonNullable.control('', Validators.required),
    custsignLastname: this.fb.nonNullable.control('', Validators.required),
    custsignTel: this.fb.control<string | null>(null),
    custsignEmail: this.fb.control<string | null>(null, Validators.email),
    custsignLine: this.fb.control<string | null>(null),

    // Sales Assignment
    saleOrg: this.fb.control<number | null>(null),
    saleGroup: this.fb.control<number | null>(null),
    saleDistrict: this.fb.control<number | null>(null),
    salePerson: this.fb.control<number | null>(null),
    saleManager: this.fb.control<number | null>(null),

    // Terms
    termOfPay: this.fb.control<number | null>(null),
    paymentMethod: this.fb.control<number | null>(null),
    currency: this.fb.control<number | null>(null),
    incoterms: this.fb.control<number | null>(null),
    priceList: this.fb.control<number | null>(null),

    // Classification
    busitypeId: this.fb.control<number | null>(null),
    industryType: this.fb.control<number | null>(null),
    shopType: this.fb.control<number | null>(null)
  });

  ngOnInit(): void {
    // โหลด Master หลักทั้งหมดครั้งเดียวตอนเปิดหน้า
    this.master.loadAll().subscribe();

    // cascading: เลือกจังหวัด -> โหลดอำเภอ, เคลียร์อำเภอ/ตำบลเดิม
    this.form.controls.provinceId.valueChanges.subscribe((provinceId) => {
      this.form.controls.amphureId.reset(null);
      this.form.controls.tambonId.reset(null);
      this.form.controls.tambonId.disable();

      if (provinceId) {
        this.form.controls.amphureId.enable();
        this.master.loadAmphures(provinceId).subscribe();
      } else {
        this.form.controls.amphureId.disable();
        this.master.clearAmphuresAndTambons();
      }
    });

    // cascading: เลือกอำเภอ -> โหลดตำบล, เคลียร์ตำบลเดิม
    this.form.controls.amphureId.valueChanges.subscribe((amphureId) => {
      this.form.controls.tambonId.reset(null);

      if (amphureId) {
        this.form.controls.tambonId.enable();
        this.master.loadTambons(amphureId).subscribe();
      } else {
        this.form.controls.tambonId.disable();
        this.master.clearTambons();
      }
    });

    // อัปเดต progress bar เวลาค่าฟอร์มเปลี่ยน
    this.form.valueChanges.subscribe(() => this.updateProgress());
    this.updateProgress();
  }

  private updateProgress(): void {
    const raw = this.form.getRawValue();
    const filled = this.sectionKeys.filter((key) => {
      const value = (raw as Record<string, unknown>)[key];
      return value !== null && value !== '' && value !== false && value !== undefined;
    }).length;
    this._progressTick.set(filled);
  }

  onSubmit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      this.snackBar.open('กรุณากรอกข้อมูลที่จำเป็นให้ครบก่อนบันทึก', 'ปิด', { duration: 3000 });
      return;
    }

    this.registerService.create(this.form.getRawValue()).subscribe({
      next: (res) => {
        this.snackBar.open(res.message || 'บันทึกใบสมัครสำเร็จ', 'ปิด', { duration: 3000 });
        this.form.reset();
      },
      error: () => {
        this.snackBar.open('บันทึกไม่สำเร็จ กรุณาลองใหม่อีกครั้ง', 'ปิด', { duration: 3000 });
      }
    });
  }

  onCancel(): void {
    this.form.reset();
  }
}
