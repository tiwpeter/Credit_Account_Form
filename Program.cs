using CreditAccountApi.DbContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ============================================================
// MediatR — สแกนทุก class ที่ implement IRequestHandler<,>
// ในนี้จะเจอทั้ง GetRegisterHandler และ GetRegisterReportHandler อัตโนมัติ
// ============================================================
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// ============================================================
// EF Core DbContext — ใช้โดย GetRegisterHandler
// ============================================================
builder.Services.AddDbContext<CreditAccountDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
