using CreditAccountApi.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CreditAccountApi.Features.Master.All;

// ============================================================
// Query — ไม่ต้องส่งอะไรเข้ามา
// ============================================================
public class GetAllMasterQuery : IRequest<GetAllMasterResponse> { }

// ============================================================
// Response — ทุก Dropdown ในครั้งเดียว
// ============================================================
public class MasterItem
{
    public int    Id   { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}

public class ProvinceItem
{
    public int    Id     { get; set; }
    public string NameTh { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
}

public class GetAllMasterResponse
{
    // ประเภท
    public IEnumerable<MasterItem> BusinessTypes  { get; set; } = [];
    public IEnumerable<MasterItem> IndustryTypes  { get; set; } = [];
    public IEnumerable<MasterItem> ShopTypes      { get; set; } = [];

    // Sales
    public IEnumerable<MasterItem> SaleOrgs       { get; set; } = [];
    public IEnumerable<MasterItem> SaleGroups     { get; set; } = [];
    public IEnumerable<MasterItem> SaleDistricts  { get; set; } = [];
    public IEnumerable<MasterItem> SalePersons    { get; set; } = [];

    // การเงิน
    public IEnumerable<MasterItem> TermOfPays     { get; set; } = [];
    public IEnumerable<MasterItem> PaymentMethods { get; set; } = [];
    public IEnumerable<MasterItem> Currencies     { get; set; } = [];
    public IEnumerable<MasterItem> Incoterms      { get; set; } = [];

    // ที่อยู่ (แค่จังหวัด อำเภอ/ตำบล Cascade แยก)
    public IEnumerable<ProvinceItem> Provinces    { get; set; } = [];
}

// ============================================================
// Handler — ดึงทุกตารางพร้อมกัน
// ============================================================
public class GetAllMasterHandler : IRequestHandler<GetAllMasterQuery, GetAllMasterResponse>
{
    private readonly CreditAccountDbContext _context;

    public GetAllMasterHandler(CreditAccountDbContext context)
    {
        _context = context;
    }

    public async Task<GetAllMasterResponse> Handle(
        GetAllMasterQuery request,
        CancellationToken cancellationToken)
    {
        // ดึงทุกตารางพร้อมกัน (Parallel)
        var businessTypesTask  = _context.BusinessTypes
            .Select(x => new MasterItem { Id = x.BusitypeId, Code = x.BusiTypeCode, Name = x.BusiTypeName })
            .OrderBy(x => x.Name).ToListAsync(cancellationToken);

        var industryTypesTask  = _context.IndustryTypes
            .Select(x => new MasterItem { Id = x.Id, Code = x.InduTypeCode, Name = x.InduTypeName })
            .OrderBy(x => x.Name).ToListAsync(cancellationToken);

        var shopTypesTask      = _context.ShopTypes
            .Select(x => new MasterItem { Id = x.Id, Code = x.ShopCode, Name = x.ShopName })
            .OrderBy(x => x.Name).ToListAsync(cancellationToken);

        var saleOrgsTask       = _context.SaleOrgs
            .Select(x => new MasterItem { Id = x.Id, Code = x.SaleOrgCode, Name = x.SaleOrgName })
            .OrderBy(x => x.Name).ToListAsync(cancellationToken);

        var saleGroupsTask     = _context.SaleGroups
            .Select(x => new MasterItem { Id = x.Id, Code = x.SaleGroCode, Name = x.SaleGroName })
            .OrderBy(x => x.Name).ToListAsync(cancellationToken);

        var saleDistrictsTask  = _context.SaleDistricts
            .Select(x => new MasterItem { Id = x.Id, Code = x.SaledisCode, Name = x.SaledisName })
            .OrderBy(x => x.Name).ToListAsync(cancellationToken);

        var salePersonsTask    = _context.SalePeople
            .Select(x => new MasterItem { Id = x.Id, Code = x.SalePersonCode, Name = x.SalePersonName })
            .OrderBy(x => x.Name).ToListAsync(cancellationToken);

        var termOfPaysTask     = _context.TermOfPays
            .Select(x => new MasterItem { Id = x.Id, Code = x.TopCode, Name = x.TopName })
            .OrderBy(x => x.Name).ToListAsync(cancellationToken);

        var paymentMethodsTask = _context.PaymentMethods
            .Select(x => new MasterItem { Id = x.Id, Code = x.PayCode, Name = x.PayName })
            .OrderBy(x => x.Name).ToListAsync(cancellationToken);

        var currenciesTask     = _context.Currencies
            .Select(x => new MasterItem { Id = x.Id, Code = x.CurrencyCode, Name = x.CurrencyName })
            .OrderBy(x => x.Code).ToListAsync(cancellationToken);

        var incotermsTask      = _context.Incoterms
            .Select(x => new MasterItem { Id = x.Id, Code = x.IncotermCode, Name = x.IncotermName })
            .OrderBy(x => x.Code).ToListAsync(cancellationToken);

        var provincesTask      = _context.ThaiProvinces
            .Select(x => new ProvinceItem { Id = x.Id, NameTh = x.NameTh, NameEn = x.NameEn ?? string.Empty })
            .OrderBy(x => x.NameTh).ToListAsync(cancellationToken);

        // รอทุก Task พร้อมกัน
        await Task.WhenAll(
            businessTypesTask, industryTypesTask, shopTypesTask,
            saleOrgsTask, saleGroupsTask, saleDistrictsTask, salePersonsTask,
            termOfPaysTask, paymentMethodsTask, currenciesTask,
            incotermsTask, provincesTask);

        return new GetAllMasterResponse
        {
            BusinessTypes  = businessTypesTask.Result,
            IndustryTypes  = industryTypesTask.Result,
            ShopTypes      = shopTypesTask.Result,
            SaleOrgs       = saleOrgsTask.Result,
            SaleGroups     = saleGroupsTask.Result,
            SaleDistricts  = saleDistrictsTask.Result,
            SalePersons    = salePersonsTask.Result,
            TermOfPays     = termOfPaysTask.Result,
            PaymentMethods = paymentMethodsTask.Result,
            Currencies     = currenciesTask.Result,
            Incoterms      = incotermsTask.Result,
            Provinces      = provincesTask.Result
        };
    }
}
