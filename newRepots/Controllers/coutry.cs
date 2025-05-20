using API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelTest.Controllers;
using System.Threading.Tasks;
using FastReport;
using FastReport.Export.PdfSimple;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data; // <-- ตัวนี้สำคัญสำหรับ DataTable

namespace ModelTest.ApiControllers
{
    [Route("api/Country")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly GetCustomerService _customerService;
        private readonly CustomerService _postcustomerService;

        private readonly GetByIdCustomerService _getByIdCustomerService;
        private readonly ApplicationDbContext _context;


        public CountryController(GetCustomerService customerService, CustomerService postcustomerService, GetByIdCustomerService getByIdCustomerService, ApplicationDbContext context)
        {
            _customerService = customerService;
            _postcustomerService = postcustomerService;
            _getByIdCustomerService = getByIdCustomerService;
            _context = context;

        }

        // GET: api/country
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetCountries([FromQuery] string? name)

        {
            var query = _context.Countries.Include(c => c.Provinces).AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.CountryName.Contains(name));
            }

            var countries = await query.ToListAsync();

            var result = countries.Select(c => new CountryDto
            {
                CountryId = c.CountryId,
                CountryName = c.CountryName,
                Provinces = c.Provinces.Select(p => new ProvinceDto
                {
                    ProvinceId = p.ProvinceId,
                    ProvinceName = p.ProvinceName
                }).ToList()
            }).ToList();

            return Ok(result);
        }


        // GET: api/country/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetCountryById(int id)
        {
            var country = await _context.Countries
                .Include(c => c.Provinces)
                .FirstOrDefaultAsync(c => c.CountryId == id);

            if (country == null)
                return NotFound();

            var dto = new CountryDto
            {
                CountryId = country.CountryId,
                CountryName = country.CountryName,
                Provinces = country.Provinces.Select(p => new ProvinceDto
                {
                    ProvinceId = p.ProvinceId,
                    ProvinceName = p.ProvinceName
                }).ToList()
            };

            return Ok(dto);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCountry([FromBody] CountryDto dto)
        {
            var country = new CountryModel
            {
                CountryName = dto.CountryName,
                Provinces = dto.Provinces.Select(p => new ProvinceModel
                {
                    ProvinceName = p.ProvinceName
                }).ToList()
            };

            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCountryById), new { id = country.CountryId }, new CountryDto
            {
                CountryId = country.CountryId,
                CountryName = country.CountryName,
                Provinces = country.Provinces.Select(p => new ProvinceDto
                {
                    ProvinceId = p.ProvinceId,
                    ProvinceName = p.ProvinceName
                }).ToList()
            });
        }


    }
}