using API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelTest.Controllers;

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
                .Include(r => r.Address)
                    .ThenInclude(a => a.Country)
                .Include(r => r.Address)
                    .ThenInclude(a => a.Province)
                .Include(r => r.Address)
                    .ThenInclude(a => a.ThaiProvince)
                .Include(r => r.Shipping) // 👉 เพิ่ม Shipping ด้วย
                    .ThenInclude(s => s.Province)
                .ToListAsync();

            var dtoList = regisforms.Select(RegisformMapper.ToDto);
            return Ok(dtoList);
        }


        // GET: api/Regisform/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var regisform = await _context.Regisforms
                .Include(r => r.Address)
                    .ThenInclude(a => a.Country)
                .Include(r => r.Address)
                    .ThenInclude(a => a.Province)
                .Include(r => r.Address)
                    .ThenInclude(a => a.ThaiProvince)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (regisform == null)
                return NotFound();

            return Ok(regisform);
        }

        // POST: api/Regisform
        [HttpPost]
        public async Task<IActionResult> Create(RegisformModel model)
        {
            _context.Regisforms.Add(model);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        // PUT: api/Regisform/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RegisformModel model)
        {
            if (id != model.Id)
                return BadRequest();

            _context.Entry(model).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Regisform/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var regisform = await _context.Regisforms.FindAsync(id);
            if (regisform == null)
                return NotFound();

            _context.Regisforms.Remove(regisform);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
