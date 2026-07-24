using CreditAccountApi.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CreditAccountApi.Features.Master.Address;

// ============================================================
// Query — รับ provinceId เข้ามา
// ============================================================
public class GetAmphuresQuery : IRequest<IEnumerable<AmphureItem>>
{
    public int ProvinceId { get; set; }
}

// ============================================================
// Response
// ============================================================
public class AmphureItem
{
    public int Id { get; set; }
    public string NameTh { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
}

// ============================================================
// Handler
// ============================================================
public class GetAmphuresHandler : IRequestHandler<GetAmphuresQuery, IEnumerable<AmphureItem>>
{
    private readonly CreditAccountDbContext _context;

    public GetAmphuresHandler(CreditAccountDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AmphureItem>> Handle(
        GetAmphuresQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.ThaiAmphures
            .Where(x => x.ProvinceId == request.ProvinceId)
            .Select(x => new AmphureItem
            {
                Id = x.Id,
                NameTh = x.NameTh,
                NameEn = x.NameEn ?? string.Empty
            })
            .OrderBy(x => x.NameTh)
            .ToListAsync(cancellationToken);
    }
}