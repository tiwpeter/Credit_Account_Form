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

        private readonly GetCustomerService _getcustomerService;


        public RegisformController(ApplicationDbContext context, CustomerService customerService, GetCustomerService getcustomerService)
        {
            _context = context;
            _customerService = customerService;
            _getcustomerService = getcustomerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _getcustomerService.GetCustomersAsync();
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