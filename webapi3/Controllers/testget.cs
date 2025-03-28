using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace testget.Controllers
{
    [ApiController]
    [Route("api/testget")]
    public class TestGetController : ControllerBase
    {
        // GET: api/testget
        [HttpGet]
        public IActionResult Get()
        {
            // ข้อมูลจำลองที่คุณต้องการส่งกลับ
            var result = new List<string>
            {
                "Test Data 1",
                "Test Data 2",
                "Test Data 3"
            };

            // ส่งกลับข้อมูลในรูปแบบ JSON
            return Ok(result);
        }
    }
}
