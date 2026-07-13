using CreditAccountApi.DbContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

// ============================================================
// MediatR — สแกนทุก class ที่ implement IRequestHandler<,>
// ในนี้จะเจอทั้ง GetRegisterHandler และ GetRegisterReportHandler อัตโนมัติ
// ============================================================
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.AddNpgsqlDbContext<CreditAccountDbContext>("myPostgres");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ ทดสอบการเชื่อมต่อ Postgres จริง ๆ ตอน startup
using (var scope = app.Services.CreateScope())
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
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

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();