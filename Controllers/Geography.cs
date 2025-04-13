//สำหรับ ดึง "จังหวัด" เฉพาะในประเทศไทย และ กรองตาม ภูมิภาค (Geography)
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using ModelTest.Controllers;

namespace API.Controllers
{
    [ApiController]
    [Route("api/thai-provinces/by-geography")]
    public class GeographyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GeographyController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProvinceModel>>> GetProvincesByGeography([FromQuery] int geographyId)
        {
            // สมมติว่า CountryId ของ "ประเทศไทย" = 1
            int thailandCountryId = 1;

            var provinces = await _context.Provinces
                .Include(p => p.Geography)
                .Include(p => p.Country)
                .Where(p => p.CountryId == thailandCountryId && p.GeographyId == geographyId)
                .ToListAsync();

            return provinces;
        }
    }

}
