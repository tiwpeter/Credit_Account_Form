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
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
        {
            var customer = await _context.Customers
                .Include(c => c.General)
                    .ThenInclude(g => g.Address)
                        .ThenInclude(a => a.Province)
                            .ThenInclude(p => p.Geography)
                .Include(c => c.General)
                    .ThenInclude(g => g.Address)
                        .ThenInclude(a => a.Province)
                            .ThenInclude(p => p.Amphures)
                                .ThenInclude(am => am.Tambons)
                .Include(c => c.General)
                    .ThenInclude(g => g.Address)
                        .ThenInclude(a => a.Country)
                .FirstOrDefaultAsync(c => c.CustomerId == id);

            if (customer == null)
            {
                return NotFound();
            }

            var customerDto = CustomerMapper.ToDto(customer);

            return Ok(customerDto);
        }
        [HttpPost]
        public async Task<ActionResult<CustomerDto>> CreateCustomer([FromBody] CustomerDto customerDto)
        {
            if (customerDto == null)
                return BadRequest();

            // ใช้ mapper แปลงจาก DTO -> Entity Model
            var customerModel = CustomerMapper.ToModel(customerDto);

            _context.Customers.Add(customerModel);
            await _context.SaveChangesAsync();

            // แปลงกลับเป็น DTO เพื่อ return
            var createdDto = CustomerMapper.ToDto(customerModel);

            return CreatedAtAction(nameof(GetCustomer), new { id = createdDto.CustomerId }, createdDto);
        }

    }
}