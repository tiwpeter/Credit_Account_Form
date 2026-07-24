
<h1 align="center">Credit Account Form System</h1>

<p align="center">
  <b>ระบบจัดการแบบฟอร์มขออนุมัติวงเงินเครดิต (Full-Stack)</b><br />
  พัฒนาด้วย .NET Aspire, ASP.NET Core (CQRS + MediatR), PostgreSQL และ Angular
</p>

<p align="center">
  <a href="#-สถานะโปรเจกต์-status">สถานะโปรเจกต์</a> •
  <a href="#-tech-stack">Tech Stack</a> •
  <a href="#-project-structure">Project Structure</a> •
  <a href="#-api-endpoints">API Endpoints</a> •
  <a href="#-getting-started">Getting Started</a> •
  <a href="#-roadmap">Roadmap</a>
</p>

---

## 📸 Preview

<p align="center">
  <img src="https://github.com/user-attachments/assets/40237ae9-2992-43f8-80e5-f8c674afb6d7" alt="Credit Account Form Preview" width="700" />
</p>

<p align="center">
  <i>ตัวอย่างเอกสารรายงานแบบฟอร์มขออนุมัติวงเงินเครดิต (PDF Report) ที่สร้างจากระบบ</i>
</p>

---

## 🚧 สถานะโปรเจกต์ (Status)

> **โปรเจกต์นี้ยังอยู่ระหว่างการพัฒนา (Work in Progress)** ส่วนที่ทำงานได้จริงตอนนี้คือฝั่ง Backend (อ่านข้อมูล + ออกรายงาน PDF) ส่วน Frontend ยังเป็นแค่โครง Angular เริ่มต้น ยังไม่มีหน้าฟอร์มจริง

| ส่วน | สถานะ | รายละเอียด |
|---|---|---|
| Backend API (Read) | ✅ ใช้งานได้ | `GET /api/register`, `GET /api/register/{id}`, `GET /api/register/{id}/report` |
| Backend API (Create/Update/Delete) | ❌ ยังไม่ทำ | ยังไม่มี endpoint สำหรับบันทึก/แก้ไข/ลบข้อมูลฟอร์ม มีแต่การอ่านข้อมูลที่มีอยู่แล้วในฐานข้อมูล |
| Database schema | ⚠️ ต้องเตรียมเอง | `CreditAccountDbContext` ถูก scaffold มาจากฐานข้อมูล PostgreSQL ที่มีอยู่แล้ว (ไม่มีโฟลเดอร์ EF Core Migrations ในโปรเจกต์) จึงต้องมี schema/ตารางพร้อมอยู่ก่อนจึงจะรันเชื่อมต่อได้ ปัจจุบันยังไม่มี migration script แนบมาด้วย |
| Frontend (Angular) | ❌ ยังไม่ทำ | `app.routes.ts` ยังว่างเปล่า และหน้า `app.component.html` ยังเป็น placeholder เริ่มต้นของ Angular CLI มีเพียง API client ที่ generate อัตโนมัติจาก Swagger ด้วย [Orval](https://orval.dev/) ไว้ในโฟลเดอร์ `frontend/src/api/generated` |
| PDF Report generation | ✅ ใช้งานได้ | ใช้ [FastReport.OpenSource](https://github.com/FastReports/FastReport) และ template `backend/Reports/RegisterReport.frx` |
| Service orchestration | ✅ ใช้งานได้ | จัดการรัน backend + frontend พร้อมกันผ่าน .NET Aspire AppHost |

---

## 🛠 Tech Stack

* **Orchestration:** [.NET Aspire](https://learn.microsoft.com/en-us/dotnet/aspire/) — รัน/เชื่อม backend, frontend และ connection string ของฐานข้อมูลให้พร้อมกันจากที่เดียว
* **Backend:** C# / ASP.NET Core 8, สถาปัตยกรรมแบบ CQRS ด้วย [MediatR](https://github.com/jbogard/MediatR), Entity Framework Core (Npgsql)
* **PDF Report:** FastReport.OpenSource + FastReport.OpenSource.Export.PdfSimple
* **Frontend:** Angular 19, TypeScript, SCSS, API client ที่ gen อัตโนมัติด้วย Orval
* **Database:** PostgreSQL (database-first — schema มีอยู่ก่อนแล้ว ไม่ได้สร้างผ่าน EF Migrations)
* **API Docs:** Swagger / Swashbuckle

---

## 🚀 Features

* 📋 **Register listing:** ดึงรายการคำขออนุมัติวงเงินเครดิตแบบมี paging และค้นหาจากชื่อบริษัท (`GET /api/register`)
* 🔍 **Register detail:** ดึงข้อมูลฟอร์มแบบเต็ม รวมข้อมูลบริษัท ที่อยู่ วงเงิน เอกสารแนบ ผู้ลงนาม และเงื่อนไขการค้า จากหลายตารางที่เกี่ยวข้อง (`GET /api/register/{id}`)
* 📄 **PDF Report export:** สร้างรายงานสรุปแบบฟอร์มเป็นไฟล์ PDF จาก template FastReport (`GET /api/register/{id}/report`)
* 🔗 **Service orchestration:** บริหารจัดการ backend, frontend และ PostgreSQL connection ผ่าน .NET Aspire Dashboard
* 🔒 **Environment configuration:** ตั้งค่า connection string และค่าคอนฟิกอื่น ๆ ผ่านไฟล์ `.env`

---

## 📂 Project Structure

```text
Credit_Account_Form/
├── Aspire/
│   ├── AppHost/              # .NET Aspire orchestrator — จุดเริ่มรันทั้งระบบ (backend + frontend)
│   └── ServiceDefaults/      # ค่า default ร่วม (health checks, telemetry, resilience)
├── backend/
│   ├── Controllers/          # RegisterController (ใช้งานจริง), TodoItemsController & WeatherForecastController (ตัวอย่าง/boilerplate จาก template)
│   ├── Features/Register/    # CQRS: Get, GetAll, Report (query + handler ของแต่ละ use case)
│   ├── Entities/              # EF Core entity classes ที่ scaffold มาจากฐานข้อมูล PostgreSQL ที่มีอยู่
│   ├── DbContext/             # CreditAccountDbContext
│   ├── Reports/                # RegisterReport.frx (FastReport template สำหรับออก PDF)
│   └── Program.cs
├── frontend/                  # Angular app (ยังเป็นโครงเริ่มต้น ยังไม่มีหน้าฟอร์มจริง)
│   └── src/api/generated/    # API client ที่ gen อัตโนมัติจาก Swagger ด้วย Orval
├── Program.cs                 # Entry point เดิม (root) — โปรเจกต์หลักที่ใช้รันจริงคือ Aspire/AppHost
└── CreditAccountApi.sln
```

---

## 🔌 API Endpoints

| Method | Endpoint | คำอธิบาย |
|---|---|---|
| GET | `/api/register?page=1&pageSize=20&search=` | รายการคำขออนุมัติวงเงินเครดิตแบบมี paging + ค้นหาจากชื่อบริษัท |
| GET | `/api/register/{id}` | ข้อมูลฟอร์มแบบเต็มของรายการนั้น ๆ (JSON) |
| GET | `/api/register/{id}/report` | ออกรายงานเป็นไฟล์ PDF |

> `TodoItemsController` และ `WeatherForecastController` เป็นตัวอย่าง/boilerplate ที่มากับ template ตอนสร้างโปรเจกต์ ไม่ได้เกี่ยวข้องกับฟีเจอร์หลักของระบบ

ดู schema แบบเต็มและทดลองยิง API ได้ที่ Swagger UI (`/swagger`) หลังรันระบบแล้ว

---

## ⚙️ Requirements

* [.NET SDK 8.0](https://dotnet.microsoft.com/download)
* [Node.js](https://nodejs.org/) 18+ และ npm (สำหรับ Angular frontend)
* PostgreSQL ที่มี **schema/ตารางพร้อมอยู่แล้ว** ตรงกับ entity ในโฟลเดอร์ `backend/Entities` — โปรเจกต์นี้ยังไม่มี EF Migrations หรือ SQL script สำหรับสร้างตารางให้ ต้องเตรียมฐานข้อมูลเองก่อน
* (แนะนำ) [.NET Aspire workload](https://learn.microsoft.com/en-us/dotnet/aspire/fundamentals/setup-tooling) สำหรับรันผ่าน AppHost

---

## 🚀 Getting Started

### 1) Clone repository

> ⚠️ Repo นี้มีขนาด commit history ค่อนข้างใหญ่ (~57 MB) เนื่องจากมีไฟล์ขนาดใหญ่หลงเหลืออยู่ใน commit เก่า แนะนำให้ใช้ **shallow clone** (`--depth 1`) เพื่อดึงเฉพาะ commit ล่าสุด จะได้ไม่ต้องโหลดประวัติทั้งหมด
 
```bash
git clone --depth 1 https://github.com/tiwpeter/Credit_Account_Form.git
cd Credit_Account_Form
```

หรือจะ clone แบบเต็มตั้งแต่แรกก็ได้ด้วยคำสั่งปกติ:
 
```bash
git clone https://github.com/tiwpeter/Credit_Account_Form.git
```

### 2) ตั้งค่า Environment Variables

คัดลอกไฟล์ตัวอย่างในโฟลเดอร์ `Aspire/AppHost` แล้วกรอกค่าให้ตรงกับฐานข้อมูลของคุณ:

```bash
cp Aspire/AppHost/.env.example Aspire/AppHost/.env
```

แก้ไขค่าในไฟล์ `.env`:

```env
ConnectionStrings__myPostgres=Host=localhost;Port=5432;Database=creditaccount;Username=postgres;Password=changeme
MY_API_KEY=your-api-key-here
```

### 3) เตรียม Database Schema + Seed ข้อมูล Master (จังหวัด/อำเภอ/ตำบล)

โปรเจกต์นี้เป็น database-first (ดูหัวข้อ Tech Stack) — ต้องมี schema/ตารางพร้อมอยู่ก่อน
ตรงกับ entity ใน `backend/Entities` (ดูหัวข้อ Requirements) ถึงจะรัน backend เชื่อมต่อได้
หลังจากสร้างตารางแล้ว ให้ seed ข้อมูลที่อยู่มาตรฐานไทย (จังหวัด/อำเภอ/ตำบล) ด้วยสคริปต์ที่เตรียมไว้ให้:

```bash
psql -U your_user -d creditaccount -f database/seed_thai_geography.sql
```

ข้อมูลนี้แปลงมาจาก open source [`kongvut/thai-province-data`](https://github.com/kongvut/thai-province-data)
(จังหวัด 77 / อำเภอ 930 / ตำบล 7,452 รายการ) รันเป็น transaction เดียวและจัดลำดับ FK ให้แล้ว
(geographies → provinces → amphures → tambons) ไม่ต้องแก้อะไรเพิ่ม

### 4) รันระบบทั้งหมดผ่าน .NET Aspire

```bash
dotnet run --project Aspire/AppHost/CreditAccountApi.AppHost.csproj
```
...

คำสั่งนี้จะ:
- รัน backend API (ASP.NET Core)
- รัน frontend (Angular) พร้อม generate API client จาก Swagger อัตโนมัติ (ผ่าน `npm run prestart` / Orval)
- เปิด .NET Aspire Dashboard ให้ดูสถานะ service, log และ trace ของทั้งระบบ

จากนั้นเปิดเบราว์เซอร์ไปที่ URL ที่ Aspire Dashboard แสดง (ปกติจะเปิดอัตโนมัติ) เพื่อดู resource ทั้งหมดและลิงก์ไปยัง backend/frontend

### รันแยกเฉพาะ backend (ไม่ผ่าน Aspire)

```bash
dotnet run --project backend/CreditAccountApi.csproj
```

จากนั้นเปิด `http://localhost:5054/swagger` เพื่อทดสอบ API

### รันแยกเฉพาะ frontend

```bash
cd frontend
npm install
npm start
```

---

### วิธีรัน generator
 
**รันเอง (manual):**
 
```bash
cd frontend
npm run generate:api
```
 
**รันอัตโนมัติทุกครั้งก่อน `npm start`:**
 
ใน `package.json` มี hook `prestart` ที่เรียก `generate:api` ให้อยู่แล้ว ดังนั้นเวลารัน `npm start` ตามปกติ (หรือรันผ่าน .NET Aspire AppHost) ระบบจะ generate API client ให้ใหม่โดยอัตโนมัติทุกครั้ง **แต่ backend ต้องรันอยู่ก่อนแล้ว** เพื่อให้ดึง `swagger.json` ได้สำเร็จ


## 🗺 Roadmap

รายการสิ่งที่ยังไม่ได้ทำ / ควรทำต่อ:

- [ ] เพิ่ม endpoint สำหรับสร้าง/แก้ไข/ลบข้อมูลฟอร์ม (`POST` / `PUT` / `DELETE` ใน `RegisterController`)
- [ ] จัดเตรียม SQL script หรือ EF Core Migrations สำหรับสร้าง schema ฐานข้อมูลตั้งแต่ต้น (ปัจจุบันต้องมี schema อยู่ก่อนเอง)
- [ ] พัฒนาหน้า UI จริงฝั่ง Angular สำหรับกรอกฟอร์มขออนุมัติวงเงินเครดิต (ปัจจุบันมีแค่ placeholder ของ Angular CLI)
- [ ] เพิ่มระบบ Authentication / Authorization (ปัจจุบันยังไม่มี)
- [ ] ลบ/ทำความสะอาด `TodoItemsController` และ `WeatherForecastController` ที่เป็น boilerplate ตัวอย่าง หากไม่ได้ใช้งานจริง

---

## 🤝 Contributing

ยินดีรับ Pull Request และ Issue — โปรดเปิด Issue เพื่ออธิบายแนวทางก่อนทำการเปลี่ยนแปลงใหญ่

## 📄 License

ยังไม่ได้ระบุ License — โปรดเพิ่มไฟล์ `LICENSE` หากต้องการเผยแพร่แบบ open source
