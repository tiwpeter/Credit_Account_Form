using API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelTest.Controllers;
using System.Threading.Tasks;

namespace ModelTest.ApiControllers
{
    [Route("api/Regisform")]
    [ApiController]
    public class RegisformController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RegisformController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _context.Set<CustomerModel>()
                .Include(c => c.General)
                .ThenInclude(g => g.Address)
                .ThenInclude(a => a.Country)
                .Select(c => new
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    GeneralName = c.General.generalName,
                    AddressCustomerName = c.General.Address.CustomerName,
                    CountryId = c.General.Address.Country.CountryId
                })
                .ToListAsync();

            return Ok(customers);
        }

        // id = sevice


        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request)
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

            return Ok(new { message = "Customer created successfully" });
        }

    }
}