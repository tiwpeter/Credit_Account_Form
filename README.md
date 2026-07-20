<p align="center">
  <a href="#">
    <img src="https://github.com/user-attachments/assets/40237ae9-2992-43f8-80e5-f8c674afb6d7" alt="Credit Account Form Logo" width="600" />
  </a>
</p>

<h1 align="center">Credit Account Form System</h1>

<p align="center">
  <b>ระบบจัดการแบบฟอร์มขออนุมัติบัญชีสินเชื่อแบบ Full-Stack</b><br />
  พัฒนาด้วย .NET Aspire, ASP.NET Core, PostgreSQL และ Angular
</p>

<p align="center">
  <a href="#-tech-stack">Tech Stack</a> •
  <a href="#-features">Features</a> •
  <a href="#-getting-started">Getting Started</a> •
  <a href="#-project-structure">Project Structure</a> •
  <a href="#-contributors">Contributors</a>
</p>

---

## 📸 Preview

<p align="center">
  <img src="https://github.com/user-attachments/assets/40237ae9-2992-43f8-80e5-f8c674afb6d7" alt="Credit Account Form Preview" width="700" />
</p>

<p align="center">
  <i>ตัวอย่างเอกสารรายงานแบบฟอร์มขออนุมัติสินเชื่อ (PDF Report) ที่สร้างจากระบบ</i>
</p>

---

## 🛠 Tech Stack

* **Orchestration:** [.NET Aspire](https://learn.microsoft.com/en-us/dotnet/aspire/)
* **Backend:** C# / ASP.NET Core (RESTful API, Dapper / EF Core)
* **Frontend:** Angular, TypeScript, SCSS
* **Database:** PostgreSQL

---

## 🚀 Features

* 📝 **Credit Form Management:** สร้าง อ่าน แก้ไข และลบข้อมูล (CRUD) สำหรับการลงทะเบียนขออนุมัติสินเชื่อ
* 📄 **PDF Report Generation:** ส่งออกรายงานสรุปแบบฟอร์มเป็นไฟล์ PDF พร้อม Register ID และ Timestamp
* 🔗 **Service Orchestration:** บริหารจัดการ Microservices และฐานข้อมูล PostgreSQL ผ่าน .NET Aspire Dashboard
* 🔒 **Environment Configuration:** รองรับการตั้งค่า Connection String ผ่านไฟล์ `.env`

---

## 📂 Project Structure

```text
Credit_Account_Form/
├── Aspire/           # Aspire AppHost และ Service Defaults สำหรับ Orchestration
├── backend/          # Backend API (CRUD operations & PDF Report Export)
├── frontend/         # Frontend Web Application (Angular)
├── Program.cs        # Entry point หลักของระบบ
└── CreditAccountApi.sln

 ``` 
---

## Requirements (ข้อกำหนด)
- .NET SDK 6.0+ หรือเวอร์ชันที่โปรเจกต์ใช้ (ตรวจสอบไฟล์ global.json ถ้ามี)
- Node.js 16.x/18.x+ และ npm (สำหรับส่วน frontend/TypeScript)
- ตัวจัดการแพ็กเกจ (optional): yarn (ถ้าใช้)
- เครื่องมือฐานข้อมูล (ถ้ามี): PostgreSQL / SQL Server / SQLite ตามที่โปรเจกต์กำหนด
- (ถ้าใช้ EF Core) dotnet-ef CLI: ติดตั้งด้วย `dotnet tool install --global dotnet-ef`

ไฟล์/การตั้งค่า (ตัวอย่าง)
- ไฟล์ environment: `.env` หรือ `appsettings.Development.json` — ให้สร้างจาก `.env.example` หรือ `appsettings.example.json` และกำหนดค่าเช่น:
  - DATABASE_URL / ConnectionStrings: connection string ของฐานข้อมูล
  - ASPNETCORE_ENVIRONMENT=Development
  - PORT (ถ้ามี)
  - API keys / secrets (เก็บนอก repo หรือใน secret manager)

---


## Quick Start (เริ่มต้นอย่างรวดเร็ว)

1. Clone repository
```bash
git clone https://github.com/tiwpeter/Credit_Account_Form.git
cd Credit_Account_Form



-------


## 🚀 Getting Started

### ⚙️ การตั้งค่า Environment Variables

ก่อนเริ่มใช้งานระบบ ให้ทำการคัดลอกไฟล์ `.env.example` เพื่อสร้างไฟล์ `.env` สำหรับตั้งค่าคอนฟิกูเรชันต่างๆ เช่น Connection String ของฐานข้อมูล

```bash
# คัดลอกไฟล์ตัวอย่างเพื่อสร้างไฟล์ .env สำหรับใช้งาน
cp .env.example .env
```

การตั้งค่าแบบปรับแต่งเอง (Manual Setup)
หากไม่ต้องการใช้ตัวช่วยสำเร็จรูป จะต้องทำตามขั้นตอนนี้:
คัดลอก Environment Variables: ก็อปปี้ไฟล์ .env.example ไปเป็น .env
สร้างคีย์ความปลอดภัย (สำคัญมาก): ต้องรันคำสั่งเพื่อสร้างคีย์เข้ารหัสไปใส่ในไฟล์ .env
เจนคีย์เซสชัน: openssl rand -base64 32 (ใส่ที่ NEXTAUTH_SECRET)
เจนคีย์เข้ารหัสข้อมูล: openssl rand -base64 24 (ใส่ที่ CALENDSO_ENCRYPTION_KEY)
จัดการฐานข้อมูล: ตั้งค่า DATABASE_URL ชี้ไปยัง Postgres (รันเองในเครื่อง หรือใช้บริการภายนอกเช่น Railway, Render) จากนั้นสั่ง Migrate โครงสร้างตารางด้วยคำสั่ง yarn workspace @calcom/prisma db-migrate


## npm 



Requirements
Node.js

##Quick Start
dotnet run --project Aspire/CreditAccountApi.AppHost/
ระบบ จะ โหลดรันทุกอย่าง พร้อม Generate code
Then open your browser at http://localhost:
dashboard

menual
fontend 
backend
