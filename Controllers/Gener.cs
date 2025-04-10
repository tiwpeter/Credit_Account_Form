using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using ModelTest.Controllers;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GeneralController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/General
        [HttpGet]
        public async Task<ActionResult<IEnumerable<General>>> GetGenerals()
        {
            var generals = await _context.Generals.Include(g => g.Address).ThenInclude(a => a.Province).ToListAsync();
            return Ok(generals);
        }

        // GET: api/General/5
        [HttpGet("{id}")]
        public async Task<ActionResult<General>> GetGeneral(int id)
        {
            var general = await _context.Generals
                                        .Include(g => g.Address)
                                        .ThenInclude(a => a.Province)
                                        .FirstOrDefaultAsync(g => g.general_id == id);

            if (general == null)
            {
                return NotFound();
            }

            return Ok(general);
        }

        // POST: api/General
        [HttpPost]
        public async Task<ActionResult<General>> PostGeneral(General general)
        {
            // Validate if the AddressId exists
            var address = await _context.Addresses.FindAsync(general.AddressId);
            if (address == null)
            {
                return BadRequest("Invalid AddressId.");
            }

            _context.Generals.Add(general);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGeneral", new { id = general.general_id }, general);
        }

        // PUT: api/General/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGeneral(int id, General general)
        {
            if (id != general.general_id)
            {
                return BadRequest();
            }

            // Validate if the AddressId exists
            var address = await _context.Addresses.FindAsync(general.AddressId);
            if (address == null)
            {
                return BadRequest("Invalid AddressId.");
            }

            _context.Entry(general).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/General/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGeneral(int id)
        {
            var general = await _context.Generals.FindAsync(id);
            if (general == null)
            {
                return NotFound();
            }

            _context.Generals.Remove(general);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
