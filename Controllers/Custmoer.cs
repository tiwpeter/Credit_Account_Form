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
        private readonly CustomerService _customerService;

        public RegisformController(ApplicationDbContext context, CustomerService customerService)
        {
            _context = context;
            _customerService = customerService;
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
            await _customerService.CreateCustomerAsync(request);
            return Ok(new { message = "Customer created successfully" });
        }

    }
}