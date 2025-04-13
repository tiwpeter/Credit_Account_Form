//สำหรับ ดึง "จังหวัด" เฉพาะในประเทศไทย และ กรองตาม ภูมิภาค (Geography)
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using ModelTest.Controllers;

namespace API.Controllers
{
    [ApiController]
    [Route("api/provinces")]
    public class ProvincesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;


        public ProvincesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("thai")]
        public ActionResult<IEnumerable<ThaiProvinceDto>> GetThaiProvinces()
        {
            var provinces = _context.ThaiProvinces
                .Include(p => p.Country)
                .Include(p => p.Geography)
                .ToList();

            var dtos = provinces.Select(p => ProvinceMapper.ToDto(p)).ToList();

            return Ok(dtos);
        }

        [HttpGet("global")]
        public ActionResult<IEnumerable<ProvinceDto>> GetAllProvinces()
        {
            var provinces = _context.Provinces
                .Include(p => p.Country)
                .Include(p => p.Geography)
                .ToList();

            var dtos = provinces.Select(p => ProvinceMapper.ToDto(p)).ToList();

            return Ok(dtos);
        }
    }


}
