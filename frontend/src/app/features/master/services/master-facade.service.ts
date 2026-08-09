import { Injectable, computed, inject, signal } from '@angular/core';
import { Observable, tap } from 'rxjs';

// ⚠️ TODO: แก้ path นี้ให้ตรงกับตำแหน่งจริงของ generated client ในโปรเจกต์
// (โฟลเดอร์ที่ OpenAPI Generator สร้างให้ เช่น src/app/api-client/services/master.service.ts)
import { MasterService as GeneratedMasterService } from '../../../api-client/services/master.service';

import {
  AddressMaster,
  FinanceMaster,
  MasterOption,
  RawMasterAllResponse,
  RawOption,
  RegisterMasterData,
  SalesMaster,
  TypeMaster
} from '../models/master.model';

@Injectable({ providedIn: 'root' })
export class MasterFacadeService {
  // generated client ตัวจริงที่ยิง HTTP (มาจาก OpenAPI Generator, ห้ามแก้ไฟล์นั้นเอง)
  private readonly api = inject(GeneratedMasterService);

  // ---- state: ข้อมูลหลักที่โหลดครั้งเดียวจาก /api/master/all ----
  private readonly _data = signal<RegisterMasterData | null>(null);
  private readonly _loading = signal(false);
  private readonly _error = signal<string | null>(null);

  readonly data = this._data.asReadonly();
  readonly loading = this._loading.asReadonly();
  readonly error = this._error.asReadonly();

  readonly sales = computed<SalesMaster | null>(() => this._data()?.sales ?? null);
  readonly type = computed<TypeMaster | null>(() => this._data()?.type ?? null);
  readonly finance = computed<FinanceMaster | null>(() => this._data()?.finance ?? null);
  readonly address = computed<AddressMaster | null>(() => this._data()?.address ?? null);

  // ---- state: cascading address (จังหวัด -> อำเภอ -> ตำบล) ----
  private readonly _amphures = signal<MasterOption[]>([]);
  private readonly _tambons = signal<MasterOption[]>([]);
  private readonly _amphuresLoading = signal(false);
  private readonly _tambonsLoading = signal(false);

  readonly amphures = this._amphures.asReadonly();
  readonly tambons = this._tambons.asReadonly();
  readonly amphuresLoading = this._amphuresLoading.asReadonly();
  readonly tambonsLoading = this._tambonsLoading.asReadonly();

  /**
   * โหลด Master หลักทั้งหมด (Sales/Type/Finance/Address) จาก /api/master/all
   * เรียกครั้งเดียวตอน ngOnInit ของฟอร์ม
   */
  loadAll(): Observable<RawMasterAllResponse> {
    this._loading.set(true);
    this._error.set(null);

    return this.api.apiMasterAllGet().pipe(
      tap({
        next: (raw: RawMasterAllResponse) => {
          this._data.set(this.normalizeMasterAll(raw));
          this._loading.set(false);
        },
        error: () => {
          this._error.set('โหลดข้อมูล Master ไม่สำเร็จ กรุณาลองใหม่อีกครั้ง');
          this._loading.set(false);
        }
      })
    );
  }

  /**
   * โหลดอำเภอ/เขต ตามจังหวัดที่เลือก — เรียกตอน province control เปลี่ยนค่า
   * เคลียร์ tambons ทิ้งด้วย เพราะต้องเลือกอำเภอใหม่ก่อน
   */
  loadAmphures(provinceId: number): Observable<RawOption[]> {
    this._amphuresLoading.set(true);
    this._tambons.set([]);

    return this.api.apiMasterAmphuresGet(provinceId).pipe(
      tap({
        next: (raw: RawOption[]) => {
          this._amphures.set(this.normalizeOptionList(raw));
          this._amphuresLoading.set(false);
        },
        error: () => {
          this._amphuresLoading.set(false);
        }
      })
    );
  }

  /**
   * โหลดตำบล/แขวง ตามอำเภอที่เลือก — เรียกตอน district control เปลี่ยนค่า
   */
  loadTambons(amphureId: number): Observable<RawOption[]> {
    this._tambonsLoading.set(true);

    return this.api.apiMasterTambonsGet(amphureId).pipe(
      tap({
        next: (raw: RawOption[]) => {
          this._tambons.set(this.normalizeOptionList(raw));
          this._tambonsLoading.set(false);
        },
        error: () => {
          this._tambonsLoading.set(false);
        }
      })
    );
  }

  clearAmphuresAndTambons(): void {
    this._amphures.set([]);
    this._tambons.set([]);
  }

  clearTambons(): void {
    this._tambons.set([]);
  }

  // ============================================================
  // Normalize: แปลง response ดิบจาก backend ให้เป็น { id, name }[]
  // เสมอ ไม่ว่า backend จะตั้งชื่อ field ว่าอะไร
  //
  // ⚠️ TODO: ถ้ารู้ชื่อ field จริงแล้ว (เช่น backend ใช้ `code`/`description`
  // แทน `id`/`name`) ให้แก้ลำดับความสำคัญใน normalizeOption() บรรทัดล่างนี้
  // ============================================================
  private normalizeOption(raw: RawOption): MasterOption {
    const id = raw.id ?? raw.code ?? raw.value ?? 0;
    const name = raw.name ?? raw.label ?? raw.description ?? '';
    return { id: Number(id), name: String(name) };
  }

  private normalizeOptionList(raw: RawOption[] | undefined | null): MasterOption[] {
    return (raw ?? []).map((item) => this.normalizeOption(item));
  }

  private normalizeMasterAll(raw: RawMasterAllResponse): RegisterMasterData {
    return {
      sales: {
        saleOrgs: this.normalizeOptionList(raw.saleOrgs),
        saleGroups: this.normalizeOptionList(raw.saleGroups),
        saleDistricts: this.normalizeOptionList(raw.saleDistricts),
        salePersons: this.normalizeOptionList(raw.salePersons),
        saleManagers: this.normalizeOptionList(raw.saleManagers)
      },
      type: {
        businessTypes: this.normalizeOptionList(raw.businessTypes),
        industryTypes: this.normalizeOptionList(raw.industryTypes),
        shopTypes: this.normalizeOptionList(raw.shopTypes)
      },
      finance: {
        termOfPays: this.normalizeOptionList(raw.termOfPays),
        paymentMethods: this.normalizeOptionList(raw.paymentMethods),
        currencies: this.normalizeOptionList(raw.currencies),
        incoterms: this.normalizeOptionList(raw.incoterms),
        priceLists: this.normalizeOptionList(raw.priceLists)
      },
      address: {
        provinces: this.normalizeOptionList(raw.provinces),
        countries: this.normalizeOptionList(raw.countries)
      }
    };
  }
}
