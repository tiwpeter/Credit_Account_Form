using API.Data;
using Microsoft.AspNetCore.Mvc;

namespace ModelTest.ApiControllers
{
    [Route("api/countries")] // เปลี่ยน Route ใหม่
    [ApiController]
    public class CountryController : ControllerBase // แก้ชื่อ Class ให้ถูกต้องด้วย
    {
        private readonly ApplicationDbContext _context;
        private readonly CustomerService _customerService;
        private readonly GetCustomerService _getcustomerService;

        public CountryController(ApplicationDbContext context, CustomerService customerService, GetCustomerService getcustomerService)
        {
            _context = context;
            _customerService = customerService;
            _getcustomerService = getcustomerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            var countries = await _getcustomerService.GetAllCountriesAsync();
            return Ok(countries);
        }
    }
}
