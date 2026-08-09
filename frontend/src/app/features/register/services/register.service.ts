import { HttpClient } from '@angular/common/http';
import { Injectable, inject, signal } from '@angular/core';
import { tap } from 'rxjs/operators';
import { environment } from '../../../../environments/environment';
import { CreateRegisterCommand, CreateRegisterResponse } from '../models/register.model';

// ⚠️ TODO: ถ้ามี generated RegisterService (เหมือน MasterService ที่ให้มา)
// ให้ลบไฟล์นี้ทิ้ง แล้ว inject generated service แทน เหมือนที่ทำใน
// master-facade.service.ts — จะได้ base URL / interceptor ตรงกับ config เดียวกัน
@Injectable({ providedIn: 'root' })
export class RegisterService {
  private readonly http = inject(HttpClient);

  // TODO: แก้ path ให้ตรงกับ RegisterController จริง เช่น `/api/register`
  private readonly endpoint = `${environment.apiUrl}/api/register`;

  private readonly _submitting = signal(false);
  readonly submitting = this._submitting.asReadonly();

  create(command: CreateRegisterCommand) {
    this._submitting.set(true);

    return this.http.post<CreateRegisterResponse>(this.endpoint, command).pipe(
      tap({
        next: () => this._submitting.set(false),
        error: () => this._submitting.set(false)
      })
    );
  }
}
