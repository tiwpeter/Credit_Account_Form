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

        // GET: api/Regisform
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regisforms = await _context.Regisforms
                .Include(r => r.General)
                    .ThenInclude(g => g.Address)
                        .ThenInclude(a => a.Country)
                .Include(r => r.General)
                    .ThenInclude(g => g.Address)
                        .ThenInclude(a => a.Province)
                .Include(r => r.General)
                    .ThenInclude(g => g.Address)
                        .ThenInclude(a => a.ThaiProvince)
                .Include(r => r.Shipping)
                    .ThenInclude(s => s.Province)
                .ToListAsync();

            var dtoList = regisforms.Select(RegisformMapper.ToDto).ToList();
            return Ok(dtoList);
        }

        [HttpPost]
        public IActionResult Post([FromBody] RegisformDto dto)
        {
            if (dto == null)
                return BadRequest();

            var model = RegisformMapper.ToModel(dto);

            // บันทึกลง DB หรือ Logic อื่นๆ
            _context.Regisforms.Add(model);
            _context.SaveChanges();

            return Ok(new { message = "Data saved successfully." });
        }


    }
}
