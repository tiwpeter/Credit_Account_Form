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
    [Route("api/Regisform")]
    [ApiController]
    public class RegisformController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly CustomerService _customerService;

        private readonly GetCustomerService _getcustomerService;


        public RegisformController(ApplicationDbContext context, CustomerService customerService, GetCustomerService getcustomerService)
        {
            _context = context;
            _customerService = customerService;
            _getcustomerService = getcustomerService;
        }



        // id = sevice
        // id = service
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            // 1. เรียกใช้ Service
            var customer = await _getcustomerService.GetCustomerByIdAsync(id);

            if (customer == null)
                return NotFound();

            // ✅ FastReport ต้องการรายการ => ใส่ลงใน List
            var customerList = new List<GetCustomersDTO> { customer };

            // 2. สร้าง Report
            Report report = new Report();

            // 3. โหลด Template
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Jacop.frx");
            report.Load(filePath);

            DataTable dt = new DataTable("CustomerData");
            dt.Columns.Add("CustomerId", typeof(int));
            dt.Columns.Add("GeneralName", typeof(string));

            foreach (var c in customerList)
            {
                dt.Rows.Add(c.CustomerId, c.General?.GeneralName ?? string.Empty);
            }
            // 4. ลงทะเบียนข้อมูล
            report.RegisterData(customerList, "CustomerData");
            report.GetDataSource("CustomerData").Enabled = true;  // ✅ ต้องเปิดใช้แบบนี้



            // 6. Prepare และ Export
            report.Prepare();
            using var stream = new MemoryStream();
            var pdfExport = new PDFSimpleExport();
            pdfExport.Export(report, stream);
            stream.Position = 0;

            // 7. Dispose Report
            report.Dispose();

            // 8. ส่งกลับเป็น PDF
            return File(stream.ToArray(), "application/pdf", $"Customer_{id}_Report.pdf");
        }



    }
}