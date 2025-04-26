using API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelTest.Controllers;
using System.Threading.Tasks;

namespace ModelTest.ApiControllers
{
    [Route("api/coutry/{countryId}")]
    [ApiController]
    public class Coutry : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly CustomerService _customerService;
        private readonly GetCustomerService _getcustomerService;

        public Coutry(ApplicationDbContext context, CustomerService customerService, GetCustomerService getcustomerService)
        {
            _context = context;
            _customerService = customerService;
            _getcustomerService = getcustomerService;
        }
        [HttpGet]
        public async Task<IActionResult> CheckCountryExist(int countryId)
        {
            var country = await _getcustomerService.GetCountryByIdAsync(countryId);
            if (country != null)
            {
                return Ok(new { message = "Country exists", country });
            }
            else
            {
                return NotFound(new { message = "Country not found" });
            }
        }

    }
}
