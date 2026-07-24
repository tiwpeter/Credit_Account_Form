using CreditAccountApi.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CreditAccountApi.Features.Master.Address;

// ============================================================
// Query — รับ amphureId เข้ามา
// ============================================================
public class GetTambonsQuery : IRequest<IEnumerable<TambonItem>>
{
    public int AmphureId { get; set; }
}

// ============================================================
// Response — รวม ZipCode สำหรับ auto-fill
// ============================================================
public class TambonItem
{
    public int Id { get; set; }
    public string NameTh { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
}

// ============================================================
// Handler
// ============================================================
public class GetTambonsHandler : IRequestHandler<GetTambonsQuery, IEnumerable<TambonItem>>
{
    private readonly CreditAccountDbContext _context;

    public GetTambonsHandler(CreditAccountDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TambonItem>> Handle(
        GetTambonsQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.ThaiTambons
            .Where(x => x.AmphureId == request.AmphureId)
            .Select(x => new TambonItem
            {
                Id = x.Id,
                NameTh = x.NameTh,
                NameEn = x.NameEn ?? string.Empty,
                ZipCode = x.ZipCode ?? string.Empty
            })
            .OrderBy(x => x.NameTh)
            .ToListAsync(cancellationToken);
    }
}