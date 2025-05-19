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
        private readonly GetCustomerService _customerService;

        public RegisformController(GetCustomerService customerService)
        {
            _customerService = customerService;
        }

        //post

        // get all 
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerService.GetCustomersAsync();
            return Ok(customers);
        }

        //get id
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCustomersDTO>> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id); // ✅ ใช้ _getCustomerService

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        //report save
        [HttpGet("update_frx/{id}")]
        public async Task<IActionResult> GetFrx(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();

            using var report = new Report();

            // ✅ เรียกใช้ service ที่สร้างไว้
            var reportService = new ReportService();
            reportService.RegisterCustomerReportData(report, customer);

            // สร้าง path สำหรับบันทึกไฟล์ .frx โดยใช้ id เพื่อไม่ให้ชื่อซ้ำกัน
            string savedFrxPath = Path.Combine(Directory.GetCurrentDirectory(), $"Customer_{id}_DataBoundN2.frx");

            // ✅ บันทึก report template (พร้อม data schema) เป็นไฟล์ .frx
            report.Save(savedFrxPath);

            return Ok(new { message = "FRX file created successfully", path = savedFrxPath });
        }


        //report display

        //report display
        [HttpGet("report/{id}")]
        public async Task<IActionResult> GetReportId(int id)
        {
            // ดึงข้อมูลลูกค้าจาก service/database
            var customer = await _customerService.GetCustomerByIdAsync(id);

            if (customer == null)
                return NotFound("Customer not found");

            // สร้าง Report
            var report = new Report();

            // โหลด template .frx ที่ออกแบบไว้
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Customer_1_DataBoundN.frx");
            report.Load(filePath);

            // นำข้อมูล customer ใส่เข้า Report
            var reportService = new ReportService();
            reportService.RegisterCustomerReportData(report, customer);

            // สร้างรายงานจริง (เตรียม rendering)
            report.Prepare();

            // Export เป็น PDF
            using var ms = new MemoryStream();
            var pdfExport = new PDFSimpleExport();
            report.Export(pdfExport, ms);
            ms.Position = 0;

            // ส่ง PDF กลับไปให้ browser download
            return File(ms.ToArray(), "application/pdf", $"CustomerReport_{id}.pdf");
        }
    }
}