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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressModel>>> GetAddresses()
        {
            return await _context.Addresses.Include(a => a.Province).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AddressModel>> GetAddress(int id)
        {
            var address = await _context.Addresses.Include(a => a.Province).FirstOrDefaultAsync(a => a.AddressId == id);
            if (address == null) return NotFound();
            return address;
        }

        [HttpPost]
        public async Task<ActionResult<AddressModel>> PostAddress(AddressModel address)
        {
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAddress), new { id = address.AddressId }, address);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, AddressModel address)
        {
            if (id != address.AddressId) return BadRequest();
            _context.Entry(address).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null) return NotFound();
            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
