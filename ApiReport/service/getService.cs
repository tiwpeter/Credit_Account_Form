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


    public async Task<List<GetCustomersDTO>> GetCustomersAsync()
    {
        var customers = await _context.Set<CustomerModel>()
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

            .Select(c => new GetCustomersDTO
            {
                CustomerId = c.CustomerId,


            }).ToListAsync(); // ⬅ เพิ่มตรงนี้
        return customers;
    }

    public async Task<CountryModel> GetCountryByIdAsync(int countryId)
    {
        return await _context.Countries
                             .FirstOrDefaultAsync(c => c.CountryId == countryId);
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
                CustGroupCountries = new List<CustGroupCountryModel>  // 👈 เปลี่ยนตรงนี้
        {
            new CustGroupCountryModel
            {
                CountryCode = c.CustGroupCountry.CountryCode,
                CountryName = c.CustGroupCountry.CountryName,
                CountryDes = c.CustGroupCountry.CountryDes
            }
        }

            }).FirstOrDefaultAsync(); // <<< เอาแค่ตัวเดียว

        return customer;

    }

    public async Task<List<CountryModel>> GetAllCountriesAsync()
    {
        return await _context.Countries.ToListAsync();
    }

}
