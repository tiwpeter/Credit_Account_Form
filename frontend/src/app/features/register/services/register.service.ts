import { HttpClient } from '@angular/common/http';
import { Injectable, inject, signal } from '@angular/core';
import { tap } from 'rxjs/operators';
import { CreateRegisterCommand, CreateRegisterResponse } from '../models/register.model';

@Injectable({ providedIn: 'root' })
export class RegisterService {
  private readonly http = inject(HttpClient);

  // ใช้ relative path เพื่อให้ proxy.conf.js จัดการ forward ไปยัง backend
  private readonly endpoint = `/api/register`;

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