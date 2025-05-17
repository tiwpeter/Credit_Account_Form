using API.Data;
using Microsoft.EntityFrameworkCore;
using ModelTest.Controllers;

public class GetCustomerService
{
    private readonly ApplicationDbContext _context;

    public GetCustomerService(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<GetCustomersDTO> GetCustomerByIdAsync(int customerId)
    {

        var customer = await _context.Set<CustomerModel>()
            .Include(c => c.General)
                .ThenInclude(g => g.Address)
                    .ThenInclude(a => a.Country)
            .Include(c => c.General.Address.Province)
            .Include(c => c.Shipping)
                .ThenInclude(s => s.Province)
                    .ThenInclude(p => p.Country)
            .Include(c => c.BusinessType)
            .Include(c => c.CreditInfo)
            .Include(c => c.CustomerSigns)
            .Include(c => c.ShopType)
            .Where(c => c.CustomerId == customerId) // <<== กรองตรงนี้ตาม id
            .Select(c => new GetCustomersDTO
            {
                CustomerId = c.CustomerId,
                CustGroupCountries = new CustGroupCountryModel
                {
                    CountryCode = c.CustGroupCountry.CountryCode,
                    CountryName = c.CustGroupCountry.CountryName,
                    CountryDes = c.CustGroupCountry.CountryDes
                }
            }).FirstOrDefaultAsync(); // <<< เอาแค่ตัวเดียว

        return customer;

    }

    public async Task<List<CountryModel>> GetAllCountriesAsync()
    {
        return await _context.Countries.ToListAsync();
    }

}
