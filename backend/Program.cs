using CreditAccountApi.DbContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

// ============================================================
// MediatR — สแกนทุก class ที่ implement IRequestHandler<,>
// ============================================================
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// ดึง ConnectionString เผื่อไว้กรณีรันแบบ Standalone หรือ Build โดยไม่ผ่าน AppHost
var connectionString = builder.Configuration.GetConnectionString("myPostgres");

if (!string.IsNullOrEmpty(connectionString))
{
    // ถ้ามีค่า (เช่น อ่านจาก appsettings.json) ให้ต่อด้วยวิธีนี้
    builder.Services.AddDbContext<CreditAccountDbContext>(options =>
        options.UseNpgsql(connectionString));
}
else
{
    // ถ้าไม่มี ให้ใช้ Aspire Service Discovery ตามปกติ
    builder.AddNpgsqlDbContext<CreditAccountDbContext>("myPostgres");
}

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// ✅ ทดสอบการเชื่อมต่อ Postgres เฉพาะตอนรันแอปจริง (ข้ามตอน Build เจน OpenAPI)
bool isOpenApiGeneration = Environment.GetCommandLineArgs().Any(arg => arg.Contains("dotnet-getdocument.dll"));

if (!isOpenApiGeneration)
{
    using (var scope = app.Services.CreateScope())
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        var dbContext = scope.ServiceProvider.GetRequiredService<CreditAccountDbContext>();
        try
        {
            var canConnect = await dbContext.Database.CanConnectAsync();
            if (canConnect)
            {
                logger.LogInformation("[LOG] เชื่อมต่อ PostgreSQL สำเร็จ! ✅");
            }
            else
            {
                logger.LogWarning("[LOG] ไม่สามารถเชื่อมต่อ PostgreSQL ได้ ⚠️");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "[LOG] เกิดข้อผิดพลาดขณะเชื่อมต่อ PostgreSQL ❌");
        }
    }
}

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();