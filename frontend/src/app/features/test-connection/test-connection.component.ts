import { CommonModule } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

import { TestService } from '../../api-client/services/test.service';

type CallState = 'idle' | 'loading' | 'success' | 'error';

@Component({
  selector: 'app-test-connection',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatProgressSpinnerModule
  ],
  template: `
    <div class="wrap">
      <h1>ทดสอบเชื่อมต่อ API</h1>
      <p class="sub">เรียก TestService (generated จาก OpenAPI) เพื่อเช็คว่า Angular ยิงไป backend ได้จริง</p>

      <!-- GET /api/Test/hello -->
      <mat-card class="box">
        <h2>1. GET /api/Test/hello</h2>
        <button mat-flat-button color="primary" (click)="callHello()" [disabled]="helloState() === 'loading'">
          @if (helloState() === 'loading') {
            <mat-spinner diameter="18" style="display:inline-block; margin-right:8px;"></mat-spinner>
          }
          เรียก Hello
        </button>

        @if (helloState() === 'success') {
          <pre class="result ok">{{ helloResult() | json }}</pre>
        }
        @if (helloState() === 'error') {
          <pre class="result err">{{ helloError() }}</pre>
        }
      </mat-card>

      <!-- POST /api/Test/echo -->
      <mat-card class="box">
        <h2>2. POST /api/Test/echo</h2>
        <mat-form-field appearance="outline" class="full">
          <mat-label>ข้อความที่จะส่งไป (message)</mat-label>
          <input matInput [(ngModel)]="echoMessage" placeholder="สวัสดี Backend">
        </mat-form-field>

        <button mat-flat-button color="primary" (click)="callEcho()" [disabled]="echoState() === 'loading'">
          @if (echoState() === 'loading') {
            <mat-spinner diameter="18" style="display:inline-block; margin-right:8px;"></mat-spinner>
          }
          ส่ง Echo
        </button>

        @if (echoState() === 'success') {
          <pre class="result ok">{{ echoResult() | json }}</pre>
        }
        @if (echoState() === 'error') {
          <pre class="result err">{{ echoError() }}</pre>
        }
      </mat-card>

      <p class="hint">
        ⚠️ ถ้าเจอ error CORS หรือ "Failed to fetch" ให้เช็ค <code>environment.apiUrl</code>
        และเช็คว่า backend เปิด CORS ให้ origin ของ Angular dev server แล้ว
      </p>
    </div>
  `,
  styles: [`
    .wrap { max-width: 640px; margin: 32px auto; padding: 0 20px; font-family: 'Sarabun', sans-serif; }
    h1 { font-size: 20px; margin: 0 0 4px; }
    .sub { color: #6b7684; font-size: 13px; margin: 0 0 20px; }
    .box { padding: 18px 20px; margin-bottom: 18px; }
    .box h2 { font-size: 15px; margin: 0 0 12px; }
    .full { width: 100%; }
    .result { margin-top: 14px; padding: 12px 14px; border-radius: 8px; font-size: 12.5px; overflow-x: auto; }
    .result.ok { background: #eafaf3; color: #1b6e4a; border: 1px solid #b9e8cf; }
    .result.err { background: #fdecea; color: #b3261e; border: 1px solid #f5c2c0; }
    .hint { font-size: 12px; color: #6b7684; }
    code { background: #eef1f4; padding: 1px 5px; border-radius: 4px; }
  `]
})
export class TestConnectionComponent {
  private readonly testService = inject(TestService);

  // ---- Hello ----
  readonly helloState = signal<CallState>('idle');
  readonly helloResult = signal<unknown>(null);
  readonly helloError = signal<string>('');

  callHello(): void {
    this.helloState.set('loading');
    this.testService.apiTestHelloGet().subscribe({
      next: (res) => {
        this.helloResult.set(res);
        this.helloState.set('success');
      },
      error: (err) => {
        this.helloError.set(this.formatError(err));
        this.helloState.set('error');
      }
    });
  }

  // ---- Echo ----
  echoMessage = 'สวัสดี Backend';
  readonly echoState = signal<CallState>('idle');
  readonly echoResult = signal<unknown>(null);
  readonly echoError = signal<string>('');

  callEcho(): void {
    this.echoState.set('loading');
    this.testService.apiTestEchoPost({ message: this.echoMessage }).subscribe({
      next: (res) => {
        this.echoResult.set(res);
        this.echoState.set('success');
      },
      error: (err) => {
        this.echoError.set(this.formatError(err));
        this.echoState.set('error');
      }
    });
  }

  private formatError(err: unknown): string {
    if (err && typeof err === 'object' && 'message' in err) {
      return String((err as { message: unknown }).message);
    }
    return JSON.stringify(err);
  }
}
