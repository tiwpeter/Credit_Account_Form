using API.Data;
using Microsoft.AspNetCore.Mvc;

namespace ModelTest.ApiControllers
{
    [Route("api/countries")] // เปลี่ยน Route ใหม่
    [ApiController]
    public class CountryController : ControllerBase // แก้ชื่อ Class ให้ถูกต้องด้วย
    {
        private readonly ApplicationDbContext _context;
        public CountryController(ApplicationDbContext context)
        {
            _context = context;
        }


    }
}
