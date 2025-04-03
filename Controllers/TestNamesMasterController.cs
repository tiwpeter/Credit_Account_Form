
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;  // ต้องเป็น API.Data ที่มี ApplicationDbContext
using apiNet8.Models;

namespace apiNet8.Controllers
{
    [Route("api/TestNamesMaster")]
    [ApiController]
    public class TestNamesMasterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;  // เปลี่ยนชื่อที่ใช้เป็น ApplicationDbContext

        public TestNamesMasterController(ApplicationDbContext context)  // เปลี่ยนที่ constructor ด้วย
        {
            _context = context;
        }

        // ย้าย [HttpGet] มาที่นี่แทน
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestnameWWW>>> GetTestName()
        {
            // ดึงข้อมูลจากฐานข้อมูลโดยใช้ Entity Framework
            return await _context.TestnameWWW.ToListAsync();  // ตรวจสอบว่า TestnameWWW ถูกประกาศไว้ใน DbContext หรือยัง
        }
    }
}
