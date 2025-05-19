//GET /api/country → ดูรายชื่อประเทศทั้งหมด

//GET / api / thaiprovince → ดูจังหวัดไทยทั้งหมด

//GET /api/province/country/2 → ดูจังหวัดในประเทศที่มี CountryId = 2 (เช่น USA)
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using ModelTest.Controllers;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ThaiProvinceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ThaiProvinceController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThaiProvince>>> GetThaiProvinces()
        {
            return await _context.ThaiProvinces
                .Include(p => p.Country)
                .ToListAsync();
        }

        //ค้นหา ThaiProvinces ตาม countryId
        [HttpGet("country/{countryId}")]
        public async Task<ActionResult<IEnumerable<ThaiProvince>>> GetByCountry(int countryId)
        {
            return await _context.ThaiProvinces
                .Where(p => p.CountryId == countryId)
                .ToListAsync();
        }
    }
}
