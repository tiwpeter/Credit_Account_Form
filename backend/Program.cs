using CreditAccountApi.DbContext;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore; // 👈 อย่าลืม include ตัวนี้ถ้าจะใช้ Scalar ด้วย

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

// MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// Database Configuration
var connectionString = builder.Configuration.GetConnectionString("myPostgres");

if (!string.IsNullOrEmpty(connectionString))
{
    builder.Services.AddDbContext<CreditAccountDbContext>(options =>
        options.UseNpgsql(connectionString));
}
else
{
    builder.AddNpgsqlDbContext<CreditAccountDbContext>("myPostgres");
}

builder.Services.AddControllers();

// ============================================================
// ✅ 1. ลงทะเบียน OpenAPI / Swagger Generator ทั้งคู่ไว้ตรงนี้
// ============================================================
builder.Services.AddOpenApi();            // ของ .NET 9 Native
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();         // ของ Swashbuckle (สำหรับ Swagger UI)

var app = builder.Build();

// Database Connection Test & Migration
bool isOpenApiGeneration = Environment.GetCommandLineArgs().Any(arg => arg.Contains("dotnet-getdocument.dll"));

if (!isOpenApiGeneration)
{
    using (var scope = app.Services.CreateScope())
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        var dbContext = scope.ServiceProvider.GetRequiredService<CreditAccountDbContext>();
        try
        {
            await dbContext.Database.MigrateAsync();
            logger.LogInformation("[LOG] Migrate และเชื่อมต่อ PostgreSQL สำเร็จ! ✅");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "[LOG] เกิดข้อผิดพลาดขณะเชื่อมต่อ PostgreSQL ❌: {Message}", ex.Message);
        }
    }
}

app.MapDefaultEndpoints();

// ============================================================
// ✅ 2. เปิดใช้งาน Endpoints และ UI ทั้งหมดใน Development
// ============================================================
if (app.Environment.IsDevelopment())
{
    // 1) เผยแพร่ OpenAPI JSON Document (.NET 9) -> /openapi/v1.json
    app.MapOpenApi();

    // 2) เปิดใช้งาน Swagger UI -> /swagger (ลิงก์ใน Aspire Dashboard จะกดเข้าได้ทันที)
    app.UseSwagger();
    app.UseSwaggerUI();

    // 3) (Optional) เปิดใช้งาน Scalar UI -> /scalar/v1
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();