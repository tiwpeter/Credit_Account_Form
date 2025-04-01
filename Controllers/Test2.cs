using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace demoapi.Controller
{
    [ApiController]
    [Route("api/Downloadtest")]
    public class Test2Controller : ControllerBase
    {
        [HttpGet("User/{id}")]
        public IActionResult GetUser(int id)
        {
            // ข้อมูลตัวอย่างทั้งหมด
            var usersData = new List<AccountData2>
            {
                new AccountData2
                {
                    FullName = "นายสมชาย ใจดี",
                    AccountNumber = "123-456-7890",
                    AccountType = "ออมทรัพย์",
                    Branch = "กรุงเทพฯ",
                    UserId = 1
                },
                new AccountData2
                {
                    FullName = "นายสมศักดิ์ รักดี",
                    AccountNumber = "987-654-3210",
                    AccountType = "กระแสรายวัน",
                    Branch = "เชียงใหม่",
                    UserId = 2
                }
            };

            // ค้นหาข้อมูลจาก usersData ที่ตรงกับ id ที่รับเข้ามา
            var userData = usersData.Find(u => u.UserId == id);

            // ถ้าข้อมูลไม่พบ
            if (userData == null)
            {
                return NotFound("ไม่พบข้อมูลผู้ใช้ที่มี ID นี้");
            }

            // ส่งข้อมูลผู้ใช้ที่พบกลับไปเป็น JSON
            return Ok(userData);
        }
    }

    // คลาสข้อมูล
    public class AccountData2
    {
        public string FullName { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public string Branch { get; set; }
        public int UserId { get; set; }
    }
}
