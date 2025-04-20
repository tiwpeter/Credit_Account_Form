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



    }
}
