using CreditAccountApi.DbContext;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

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
// ✅ 1. ลงทะเบียน Swagger Generator
// ============================================================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Database Connection Test & Migration
bool isOpenApiGeneration = Environment.GetCommandLineArgs().Any(arg => arg.Contains("dotnet-getdocument.dll"));

if (!isOpenApiGeneration)
{
    using (var scope = app.Services.CreateScope())
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        var dbContext = scope.ServiceProvider.GetRequiredService<CreditAccountDbContext>();
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
}

app.MapDefaultEndpoints();

// ============================================================
// ✅ 2. เปิดใช้งาน API Reference UI (Scalar)
// ============================================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("Credit Account API")
               .WithOpenApiRoutePattern("/swagger/v1/swagger.json");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();