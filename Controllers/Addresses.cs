using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;  // หรือใช้ namespace ที่เหมาะสมสำหรับ DbContext
using ModelTest.Controllers;

namespace API.Controllers
{
    [Route("api/Address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AddressController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all addresses with related Province and Country
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressModel>>> GetAddresses()
        {
            return await _context.Addresses
                .Include(a => a.Province)  // รวมข้อมูลของ Province
                .ThenInclude(p => p.Country)  // รวมข้อมูลของ Country ใน Province
                .ToListAsync();
        }

        // Get a specific address by its ID with related Province and Country
        [HttpGet("{id}")]
        public async Task<ActionResult<AddressModel>> GetAddress(int id)
        {
            var address = await _context.Addresses
                .Include(a => a.Province)
                .ThenInclude(p => p.Country)
                .FirstOrDefaultAsync(a => a.AddressId == id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }
    }
}
