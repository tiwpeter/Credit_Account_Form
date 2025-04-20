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

        // GET: api/Regisform
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAllCustomers()
        {
            var customers = await _context.Customer
                .Include(c => c.General)
                    .ThenInclude(g => g.Address)
                        .ThenInclude(a => a.Country)
                .ToListAsync();

            var customerDtos = customers.Select(CustomerMapper.ToDto).ToList();
            return Ok(customerDtos);
        }




    }
}
