using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using ModelTest.Controllers;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShippingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ShippingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/shipping
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShippingModel>>> GetShippings()
        {
            return await _context.Shippings
                .Include(s => s.Province)
                .ThenInclude(p => p.Country) // ดึง Country ด้วย
                .ToListAsync();
        }



        // GET: api/shipping/1
        [HttpGet("{id}")]
        public async Task<ActionResult<ShippingModel>> GetShipping(int id)
        {
            var shipping = await _context.Shippings
                .Include(s => s.Province)
                .ThenInclude(p => p.Country)
                .FirstOrDefaultAsync(s => s.shipping_id == id);

            if (shipping == null)
                return NotFound();

            return shipping;
        }

        // POST: api/shipping
        [HttpPost]
        public async Task<ActionResult<ShippingModel>> CreateShipping(ShippingModel shipping)
        {
            _context.Shippings.Add(shipping);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetShipping), new { id = shipping.shipping_id }, shipping);
        }

        // PUT: api/shipping/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShipping(int id, ShippingModel shipping)
        {
            if (id != shipping.shipping_id)
                return BadRequest();

            _context.Entry(shipping).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Shippings.Any(e => e.shipping_id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/shipping/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShipping(int id)
        {
            var shipping = await _context.Shippings.FindAsync(id);
            if (shipping == null)
                return NotFound();

            _context.Shippings.Remove(shipping);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
