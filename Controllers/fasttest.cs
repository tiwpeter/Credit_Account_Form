using Microsoft.AspNetCore.Mvc;
using FastReport;
using FastReport.Export.PdfSimple;
using System.IO;
using System.Linq;
using ModelTest.Controllers;

namespace CustomerApi.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerTestController : ControllerBase
    {
        private List<Customer> customers = new List<Customer>
        {
            new Customer
            {
                CustomerId = 1,
                Generals = new GeneralsModel
                {
                    GeneralName = "บริษัท A",
                    GeneralTel = "012-3456789",
                    GeneralFax = "012-3456790",
                    GeneralEmail = "contact@companyA.com",
                    GeneralLine = "@companyA",
                    GeneralTax = "1234567890",
                    GeneralBranch = "สำนักงานใหญ่"
                }
            }
        };

        // http get customer details
        [HttpGet("report/{customerId}")]
        public IActionResult GetCustomerReport(int customerId)
        {
            // 1. โหลดรายงาน
            Report report = new Report();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "1.frx");
            report.Load(filePath);

            // 2. ดึงข้อมูลลูกค้า (แบบจำลองข้อมูล)
            var customer = customers
                .Where(c => c.CustomerId == customerId)
                .Select(c => new
                {
                    c.CustomerId,
                    c.Generals.GeneralName,
                    c.Generals.GeneralTel,
                    c.Generals.GeneralFax,
                    c.Generals.GeneralEmail,
                    c.Generals.GeneralLine,
                    c.Generals.GeneralTax,
                    c.Generals.GeneralBranch,
                })
                .FirstOrDefault();

            // 3. ผูกข้อมูลกับ Report
            report.RegisterData(new List<object> { customer }, "Customer");

            // 4. Prepare และ Export เป็น PDF
            report.Prepare();
            using (MemoryStream pdfStream = new MemoryStream())
            {
                report.Prepare();
                PDFSimpleExport pdfExport = new PDFSimpleExport(); // ใช้ PDFSimpleExport
                report.Export(pdfExport, pdfStream);
                report.Dispose(); // ปิด Report

                // รีเซ็ตตำแหน่งของ MemoryStream ก่อนส่งกลับ
                pdfStream.Position = 0; // รีเซ็ต Stream

                // 5. ส่งกลับ PDF
                return File(pdfStream.ToArray(), "application/pdf", "CustomerReport.pdf");

            }
        }

    }
}
