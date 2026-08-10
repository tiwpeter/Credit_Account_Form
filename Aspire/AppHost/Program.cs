using DotNetEnv;
using Microsoft.Extensions.Configuration;

// 1. โหลดไฟล์ .env เข้าสู่ Environment Variables ของระบบ
Env.Load();

var builder = DistributedApplication.CreateBuilder(args);

// --- [ลองเขียน Log แสดงค่าออกมาดู] ---
var rawConnString = builder.Configuration.GetConnectionString("myPostgres");
Console.WriteLine($"[LOG] Connection String จาก .env คือ: {rawConnString}");

var apiKeyFromEnv = builder.Configuration.GetValue<string>("MY_API_KEY") ?? "not-set";
//Console.WriteLine($"[LOG] API Key จาก .env คือ: {apiKeyFromEnv}");
// ------------------------------------

// 2. นำ Connection String ที่ดึงได้ ลงทะเบียนเข้าสู่ระบบ .NET Aspire
var postgres = builder.AddConnectionString("myPostgres");

// 3. นำค่าความลับทั่วไปมาลงทะเบียนเป็น Parameter ใน Aspire
//var apiKeyParam = builder.AddParameter("my-api-key", apiKeyFromEnv);

// 4. ส่งต่อฐานข้อมูล และ API Key ไปให้โปรเจกต์หลังบ้านใช้งาน // 1. ลงทะเบียนโปรเจกต์ Backend API (.NET)
var apiService = builder.AddProject<Projects.CreditAccountApi>("creditaccountapi")
       .WithReference(postgres)
       .WithUrlForEndpoint("https", url => url.Url = "/swagger");
// .WithEnvironment("MY_API_KEY", apiKeyParam);


// 4. เพิ่ม resource สำหรับรัน npm install ก่อน serve
// npm install เป็น one-shot task ที่รันจบแล้วปิดตัวเอง
var npmInstall = builder.AddExecutable(
        "angular-npm-install",
        "npm",
        "../../frontend",
        "install");

var angular = builder.AddNpmApp("angular", "../../frontend", "start")
    .WithReference(apiService)
    .WaitForCompletion(npmInstall)   // <-- เปลี่ยนจาก WaitFor เป็น WaitForCompletion
    .WaitFor(apiService)             // apiService ยัง WaitFor ปกติ เพราะเป็น long-running service
    .WithHttpEndpoint(env: "PORT")
    .WithEnvironment("BROWSER", "none")
    .WithExternalHttpEndpoints();

builder.Build().Run();