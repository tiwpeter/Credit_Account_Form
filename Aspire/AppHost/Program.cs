using DotNetEnv;
using Microsoft.Extensions.Configuration;

// 1. โหลดไฟล์ .env เข้าสู่ Environment Variables ของระบบ
Env.Load();

var builder = DistributedApplication.CreateBuilder(args);

// --- [ลองเขียน Log แสดงค่าออกมาดู] ---
var rawConnString = builder.Configuration.GetConnectionString("myPostgres");
//Console.WriteLine($"[LOG] Connection String จาก .env คือ: {rawConnString}");

var apiKeyFromEnv = builder.Configuration.GetValue<string>("MY_API_KEY") ?? "not-set";
//Console.WriteLine($"[LOG] API Key จาก .env คือ: {apiKeyFromEnv}");
// ------------------------------------

// 2. นำ Connection String ที่ดึงได้ ลงทะเบียนเข้าสู่ระบบ .NET Aspire
var postgres = builder.AddConnectionString("myPostgres");

// 3. นำค่าความลับทั่วไปมาลงทะเบียนเป็น Parameter ใน Aspire
//var apiKeyParam = builder.AddParameter("my-api-key", apiKeyFromEnv);

// 4. ส่งต่อฐานข้อมูล และ API Key ไปให้โปรเจกต์หลังบ้านใช้งาน // 1. ลงทะเบียนโปรเจกต์ Backend API (.NET)
var apiService = builder.AddProject<Projects.CreditAccountApi>("creditaccountapi")
       .WithReference(postgres);
// .WithEnvironment("MY_API_KEY", apiKeyParam);


var angular = builder.AddNpmApp("angular", "../../frontend", "start")
    .WithReference(apiService)
    .WaitFor(apiService)
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .WithEnvironment("API_URL", apiService.GetEndpoint("http"));   // ← ไม่มี ; คั่นกลาง + ใช้ apiService

builder.Build().Run();