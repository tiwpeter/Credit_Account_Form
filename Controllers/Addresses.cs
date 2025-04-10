using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using ModelTest.Controllers;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/Addresses")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AddressController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/address
        [HttpGet]
        public async Task<ActionResult> GetAddresses()
        {
            var addresses = await _context.Addresses
                .Include(a => a.Province) // Include related Province data
                .ThenInclude(p => p.Country) // Include related Country data
                .ToListAsync();

            return Ok(addresses);
        }

        // GET: api/address/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAddress(int id)
        {
            var address = await _context.Addresses
                .Include(a => a.Province)
                .ThenInclude(p => p.Country)
                .FirstOrDefaultAsync(a => a.AddressId == id);

            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }
    }
}
