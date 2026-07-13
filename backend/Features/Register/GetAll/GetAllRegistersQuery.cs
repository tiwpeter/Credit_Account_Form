using CreditAccountApi.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CreditAccountApi.Features.Register.GetAll;

// ============================================================
// Query — ดึงรายการ register ทั้งหมด (รองรับ paging + filter คร่าวๆ)
// ============================================================
public class GetAllRegistersQuery : IRequest<GetAllRegistersResponse>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;

    // optional filter: ค้นหาจากชื่อบริษัท
    public string? Search { get; set; }
}

// ============================================================
// Item ย่อในแต่ละแถว (ไม่เอาข้อมูลครบทุก field แบบ GetRegisterResponse
// เพราะหน้า list ปกติโชว์แค่สรุป)
// ============================================================
public class RegisterListItem
{
    public int RegisterId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? GeneralName1 { get; set; }
    public string? GeneralTax { get; set; }
    public string? Province { get; set; }
    public decimal? CreditLimit { get; set; }
}

// ============================================================
// Response — list + ข้อมูล paging
// ============================================================
public class GetAllRegistersResponse
{
    public List<RegisterListItem> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => PageSize == 0 ? 0 : (int)Math.Ceiling(TotalCount / (double)PageSize);
}

// ============================================================
// Handler
// ============================================================
public class GetAllRegistersHandler : IRequestHandler<GetAllRegistersQuery, GetAllRegistersResponse>
{
    private readonly CreditAccountDbContext _context;

    public GetAllRegistersHandler(CreditAccountDbContext context)
    {
        _context = context;
    }

    public async Task<GetAllRegistersResponse> Handle(
        GetAllRegistersQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.RegisterFroms
            .Include(r => r.General)
                .ThenInclude(g => g!.Addresses)
            .Include(r => r.Creditinfo)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            query = query.Where(r =>
                r.General != null &&
                r.General.GeneralName1 != null &&
                r.General.GeneralName1.Contains(request.Search));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var page = request.Page < 1 ? 1 : request.Page;
        var pageSize = request.PageSize < 1 ? 20 : request.PageSize;

        var items = await query
            .OrderByDescending(r => r.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(r => new RegisterListItem
            {
                RegisterId = r.RegisterId,
                CreatedAt = r.CreatedAt,
                GeneralName1 = r.General != null ? r.General.GeneralName1 : null,
                GeneralTax = r.General != null ? r.General.GeneralTax : null,
                Province = r.General != null && r.General.Addresses.Any()
                    ? r.General.Addresses.First().Province
                    : null,
                CreditLimit = r.Creditinfo != null ? r.Creditinfo.CreditLimit : null,
            })
            .ToListAsync(cancellationToken);

        return new GetAllRegistersResponse
        {
            Items = items,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize,
        };
    }
}
