using API.Data;
using Microsoft.EntityFrameworkCore;
using ModelTest.Controllers;

public interface ICustomerService
{
    Task<string> CreateCustomerAsync(CreateCustomerRequest request);
}
public class CustomerService : ICustomerService
{
    private readonly ApplicationDbContext _context;

    public CustomerService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> CreateCustomerAsync(CreateCustomerRequest request)
    {
        // 1. ตรวจสอบหรือสร้าง Country
        var country = await _context.Set<CountryModel>()
            .FirstOrDefaultAsync(c => c.CountryId == request.CountryId);

        if (country == null)
        {
            country = new CountryModel { CountryId = request.CountryId };
            _context.Add(country);
            await _context.SaveChangesAsync();
        }

        // 2. ตรวจสอบหรือสร้าง Address
        var address = await _context.Set<AddressModel>()
            .FirstOrDefaultAsync(a =>
                a.CustomerName == request.AddressCustomerName &&
                a.CountryId == country.CountryId);

        if (address == null)
        {
            address = new AddressModel
            {
                CustomerName = request.AddressCustomerName,
                CountryId = country.CountryId
            };
            _context.Add(address);
            await _context.SaveChangesAsync();
        }

        // 3. ตรวจสอบหรือสร้าง General
        var general = await _context.Set<GeneralModel>()
            .FirstOrDefaultAsync(g =>
                g.generalName == request.GeneralName &&
                g.AddressId == address.AddressId);

        if (general == null)
        {
            general = new GeneralModel
            {
                generalName = request.GeneralName,
                AddressId = address.AddressId
            };
            _context.Add(general);
            await _context.SaveChangesAsync();
        }

        // 4. สร้าง Customer
        var customer = new CustomerModel
        {
            CustomerName = request.CustomerName,
            GeneralId = general.general_id
        };

        _context.Add(customer);
        await _context.SaveChangesAsync();

        return "Customer created successfully";
    }
}
