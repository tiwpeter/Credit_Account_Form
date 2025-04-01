using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.IO;
using FastReport;
using FastReport.Export.PdfSimple;

namespace demoapi.Controller
{
    [ApiController]
    [Route("api/Downloadtest")] // URL API
    public class TestController : ControllerBase
    {
        [HttpGet("report/{id}")]
        public IActionResult DownloadReport(int id)
        {
            // 🔹 ข้อมูลตัวอย่างทั้งหมด
            var usersData = new List<AccountData>
            {
                new AccountData
                {
                    FullName = "นายสมชาย ใจดี",
                    AccountNumber = "123-456-7890",
                    AccountType = "ออมทรัพย์",
                    Branch = "กรุงเทพฯ",
                    UserId = 1
                },
                new AccountData
                {
                    FullName = "นายสมศักดิ์ รักดี",
                    AccountNumber = "987-654-3210",
                    AccountType = "กระแสรายวัน",
                    Branch = "เชียงใหม่",
                    UserId = 2
                }
            };

            // 🔹 ค้นหาข้อมูลจาก usersData ที่ตรงกับ id ที่รับเข้ามา
            var userData = usersData.Find(u => u.UserId == id);

            if (userData == null)
            {
                return NotFound("ไม่พบข้อมูลผู้ใช้ที่มี ID นี้");
            }

            // 🔹 สร้าง DataTable เพื่อเก็บข้อมูลบัญชี
            DataTable table = new DataTable("AccountData");
            table.Columns.Add("FullName", typeof(string));
            table.Columns.Add("AccountNumber", typeof(string));
            table.Columns.Add("AccountType", typeof(string));
            table.Columns.Add("Branch", typeof(string));
            table.Columns.Add("UserId", typeof(int));

            // 🔹 เพิ่มข้อมูลที่ตรงกับ userData
            table.Rows.Add(userData.FullName, userData.AccountNumber, userData.AccountType, userData.Branch, userData.UserId);

            using (Report report = new Report())
            {
                string reportPath = Path.Combine(Directory.GetCurrentDirectory(), "BankAccountReport.frx");
                if (!System.IO.File.Exists(reportPath))
                {
                    return NotFound("ไม่พบไฟล์รายงาน");
                }

                report.Load(reportPath);
                report.RegisterData(table, "AccountData");
                report.Prepare();

                // 🔹 ส่งออกเป็น PDF
                using (MemoryStream stream = new MemoryStream())
                {
                    PDFSimpleExport pdfExport = new PDFSimpleExport();
                    report.Export(pdfExport, stream);
                    stream.Position = 0;

                    return File(stream.ToArray(), "application/pdf", "BankAccountReport.pdf");
                }
            }
        }
    }

    // 🔹 คลาสข้อมูล
    public class AccountData
    {
        public string FullName { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public string Branch { get; set; }
        public int UserId { get; set; }
    }
}
