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

    public async Task<List<CustomerDto>> GetCustomersAsync()
    {
        var customers = await _context.Set<CustomerModel>()
            .Include(c => c.General)
            .ThenInclude(g => g.Address)
            .ThenInclude(a => a.Country)
            .Select(c => new CustomerDto
            {
                CustomerId = c.CustomerId,
                CustomerName = c.CustomerName,
                General = new GeneralDto
                {
                    GeneralName = c.General.generalName,
                    Address = new AddressDto
                    {
                        CustomerName = c.General.Address.CustomerName,
                        Country = new CountryDto
                        {
                            CountryId = c.General.Address.Country.CountryId
                        }
                    }
                }
            })
            .ToListAsync();

        return customers;
    }
}
