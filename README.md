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

## 🚀 Getting Started

### ⚙️ การตั้งค่า Environment Variables

ก่อนเริ่มใช้งานระบบ ให้ทำการคัดลอกไฟล์ `.env.example` เพื่อสร้างไฟล์ `.env` สำหรับตั้งค่าคอนฟิกูเรชันต่างๆ เช่น Connection String ของฐานข้อมูล

```bash
# คัดลอกไฟล์ตัวอย่างเพื่อสร้างไฟล์ .env สำหรับใช้งาน
cp .env.example .env
```


Requirements
Node.js

Quick Start
dotnet run --project Aspire/CreditAccountApi.AppHost/
ระบบ จะ โหลดรันทุกอย่าง พร้อม Generate code
Then open your browser at http://localhost:
dashboard

menual
fontend 
backend
