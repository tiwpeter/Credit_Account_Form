//สำหรับ ดึง "จังหวัด" เฉพาะในประเทศไทย และ กรองตาม ภูมิภาค (Geography)
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using ModelTest.Controllers;

namespace API.Controllers
{
    [ApiController]
    [Route("api/thai-provinces")]
    public class GeographyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GeographyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ThaiProvinces
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThaiProvince>>> GetThaiProvinces()
        {
            return await _context.ThaiProvinces.Include(tp => tp.Country).Include(tp => tp.Geography).ToListAsync();
        }

        // GET: api/ThaiProvinces/5
        // GET: api/thai-provinces/by-geography/1
        [HttpGet("by-geography/{geographyId}")]
        public async Task<ActionResult<IEnumerable<ThaiProvince>>> GetThaiProvincesByGeography(int geographyId)
        {
            // กรองเฉพาะ ThaiProvince ที่มี GeographyId ตรง และอยู่ในประเทศไทย
            // สมมุติว่า CountryId ของประเทศไทยเป็น 1 (คุณสามารถเปลี่ยนให้ dynamic ได้ด้วยถ้าต้องการ)
            var thailandCountryId = 1;

            var provinces = await _context.ThaiProvinces
                .Where(tp => tp.GeographyId == geographyId && tp.CountryId == thailandCountryId)
                .Include(tp => tp.Geography)
                .ToListAsync();

            if (provinces == null || provinces.Count == 0)
            {
                return NotFound("ไม่พบจังหวัดในภูมิภาคที่ระบุ");
            }

            return Ok(provinces);
        }

    }

}
