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
        [HttpGet]
        public async Task<ActionResult<List<AddressDto>>> GetAll()
        {
            var addresses = await _context.Set<AddressModel>()
                .Include(a => a.Province)
                    .ThenInclude(p => p.Country)
                .ToListAsync();

            return addresses.Select(CustomerMapper.ToDto).ToList();
        }
        [HttpGet("provinces/{countryId}")]
        public async Task<ActionResult<List<ProvinceDto>>> GetProvincesByCountry(int countryId)
        {
            var provinces = await _context.Set<ProvinceModel>()
                .Where(p => p.CountryId == countryId)
                .Include(p => p.Country)
                .ToListAsync();

            if (provinces == null || provinces.Count == 0)
            {
                return NotFound("No provinces found for the given country ID.");
            }

            var result = provinces.Select(ProvinceMapper.ToDto).ToList();

            return Ok(result);
        }



    }
}
