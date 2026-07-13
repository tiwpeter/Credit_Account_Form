using CreditAccountApi.Common.Exceptions;
using CreditAccountApi.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CreditAccountApi.Entities;

namespace CreditAccountApi.Features.Register.Get;

// ============================================================
// Query — รับ id เข้ามา
// ============================================================
public class GetRegisterQuery : IRequest<GetRegisterResponse>
{
    public int Id { get; set; }
}

// ============================================================
// Response — ข้อมูลครบทุกอย่างที่ลูกค้ากรอก
// ============================================================
public class GetRegisterResponse
{
    public int RegisterId { get; set; }
    public DateTime? CreatedAt { get; set; }

    // Step 1 — ข้อมูลบริษัท
    public string? GeneralName1  { get; set; }
    public string? GeneralName2  { get; set; }
    public string? GeneralTax    { get; set; }
    public string? GeneralBranch { get; set; }
    public string? GeneralTel    { get; set; }
    public string? GeneralFax    { get; set; }
    public string? GeneralEmail  { get; set; }
    public string? GeneralLine   { get; set; }

    // Step 2 — ที่อยู่
    public string? AddrLine1    { get; set; }
    public string? AddrLine2    { get; set; }
    public string? SubDistrict  { get; set; }
    public string? District     { get; set; }
    public string? Province     { get; set; }
    public string? PostalCode   { get; set; }
    public string? Country      { get; set; }

    // Step 3 — วงเงิน
    public decimal? CreditLimit       { get; set; }
    public decimal? EstimatedPurchase { get; set; }
    public string?  TimeRequired      { get; set; }

    // Step 4 — เอกสาร
    public bool    CompanyCertificate { get; set; }
    public bool    CopyOfPp20        { get; set; }
    public bool    CopyOfCoRegis     { get; set; }
    public bool    CopyOfIdCard      { get; set; }
    public bool    CompanyLocationMap { get; set; }
    public string? OtherSpecify      { get; set; }

    // Step 5 — ผู้ลงนาม
    public string? CustsignFirstname { get; set; }
    public string? CustsignLastname  { get; set; }
    public string? CustsignTel       { get; set; }
    public string? CustsignEmail     { get; set; }
    public string? CustsignLine      { get; set; }

    // ประเภท
    public string? BusinessType  { get; set; }
    public string? IndustryType  { get; set; }
    public string? ShopType      { get; set; }

    // เงื่อนไขการค้า
    public string? TermOfPay      { get; set; }
    public string? PaymentMethod  { get; set; }
    public string? Currency       { get; set; }
    public string? Incoterms      { get; set; }
}

// ============================================================
// Handler — Query DB JOIN ทุกตารางที่ลูกค้ากรอก
// ============================================================
public class GetRegisterHandler : IRequestHandler<GetRegisterQuery, GetRegisterResponse>
{
    private readonly CreditAccountDbContext _context;

    public GetRegisterHandler(CreditAccountDbContext context)
    {
        _context = context;
    }

    public async Task<GetRegisterResponse> Handle(
        GetRegisterQuery request,
        CancellationToken cancellationToken)
    {
        var register = await _context.RegisterFroms
            .Include(r => r.General)
                .ThenInclude(g => g!.Addresses)
            .Include(r => r.Creditinfo)
            .Include(r => r.Doccredit)
            .Include(r => r.Custsign)
            .Include(r => r.Busitype)
            .Include(r => r.IndustryTypeNavigation)
            .Include(r => r.ShopTypeNavigation)
            .Include(r => r.TermOfPayNavigation)
            .Include(r => r.PaymentMethodNavigation)
            .Include(r => r.CurrencyNavigation)
            .Include(r => r.IncotermsNavigation)
            .FirstOrDefaultAsync(r => r.RegisterId == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(RegisterFrom), request.Id);

        var address = register.General?.Addresses.FirstOrDefault();

        return new GetRegisterResponse
        {
            RegisterId = register.RegisterId,
            CreatedAt  = register.CreatedAt,

            // Step 1
            GeneralName1  = register.General?.GeneralName1,
            GeneralName2  = register.General?.GeneralName2,
            GeneralTax    = register.General?.GeneralTax,
            GeneralBranch = register.General?.GeneralBranch,
            GeneralTel    = register.General?.GeneralTel,
            GeneralFax    = register.General?.GeneralFax,
            GeneralEmail  = register.General?.GeneralEmail,
            GeneralLine   = register.General?.GeneralLine,

            // Step 2
            AddrLine1   = address?.AddrLine1,
            AddrLine2   = address?.AddrLine2,
            SubDistrict = address?.SubDistrict,
            District    = address?.District,
            Province    = address?.Province,
            PostalCode  = address?.PostalCode,
            Country     = address?.Country,

            // Step 3
            CreditLimit       = register.Creditinfo?.CreditLimit,
            EstimatedPurchase = register.Creditinfo?.EstimatedPurchase,
            TimeRequired      = register.Creditinfo?.TimeRequired,

            // Step 4
            CompanyCertificate = register.Doccredit?.CompanyCertificate ?? false,
            CopyOfPp20        = register.Doccredit?.CopyOfPp20 ?? false,
            CopyOfCoRegis     = register.Doccredit?.CopyOfCoRegis ?? false,
            CopyOfIdCard      = register.Doccredit?.CopyOfIdCard ?? false,
            CompanyLocationMap = register.Doccredit?.CompanyLocationMap ?? false,
            OtherSpecify      = register.Doccredit?.OtherSpecify,

            // Step 5
            CustsignFirstname = register.Custsign?.CustsignFirstname,
            CustsignLastname  = register.Custsign?.CustsignLastname,
            CustsignTel       = register.Custsign?.CustsignTel,
            CustsignEmail     = register.Custsign?.CustsignEmail,
            CustsignLine      = register.Custsign?.CustsignLine,

            // ประเภท
            BusinessType = register.Busitype?.BusiTypeName,
            IndustryType = register.IndustryTypeNavigation?.InduTypeName,
            ShopType     = register.ShopTypeNavigation?.ShopName,

            // เงื่อนไขการค้า
            TermOfPay     = register.TermOfPayNavigation?.TopName,
            PaymentMethod = register.PaymentMethodNavigation?.PayName,
            Currency      = register.CurrencyNavigation?.CurrencyCode,
            Incoterms     = register.IncotermsNavigation?.IncotermCode,
        };
    }
}
