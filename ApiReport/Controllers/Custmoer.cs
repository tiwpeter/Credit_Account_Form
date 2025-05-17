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
        [HttpGet("customer")]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerService.GetCustomersAsync();
            return Ok(customers);
        }

        //get id
        [HttpGet("customer/{id}")]
        public async Task<ActionResult<GetCustomersDTO>> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id); // ✅ ใช้ _getCustomerService

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        //report save
        [HttpGet("update_frx/{id}")]
        // สร้างรายงาน FRX จากข้อมูลลูกค้าตาม ID ที่ระบุ และบันทึกเป็นไฟล์ .frx
        public async Task<IActionResult> GetFrx(int id)
        {
            // ดึงข้อมูลลูกค้าตาม id โดยใช้ service
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound(); // หากไม่พบลูกค้า ส่ง 404 Not Found กลับไป

            // สร้าง List จากข้อมูลลูกค้า เพื่อใช้กับ FastReport
            var customerList = new List<GetCustomersDTO> { customer };

            // สร้าง List สำหรับข้อมูลประเทศ (แม้เก็บเป็น object เดียว ก็ต้องแปลงเป็น List)
            var countryList = new List<CustGroupCountryModel> { customer.CustGroupCountry };

            using var report = new Report();

            // นำข้อมูลลูกค้าเข้าไปใน Report
            report.RegisterData(customerList, "CustomerData");
            report.GetDataSource("CustomerData").Enabled = true;

            // นำข้อมูลประเทศเข้าไปใน Report
            report.RegisterData(countryList, "CustGroupCountry");
            report.GetDataSource("CustGroupCountry").Enabled = true;

            // สร้าง path สำหรับบันทึกไฟล์ .frx โดยใช้ id เพื่อไม่ให้ชื่อซ้ำกัน
            string savedFrxPath = Path.Combine(Directory.GetCurrentDirectory(), $"Customer_{id}_DataBoundN.frx");

            // บันทึกรายงานเป็นไฟล์ .frx
            report.Save(savedFrxPath);

            // ปิดและเคลียร์ทรัพยากรของ report
            report.Dispose();

            // ส่งผลลัพธ์กลับเป็น JSON พร้อม path ของไฟล์ .frx ที่สร้าง
            return Ok(new { message = "FRX file created successfully", path = savedFrxPath });
        }


        //report display
        [HttpGet("report/{id}")]
        public async Task<IActionResult> GetReportId(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();

            var customerList = new List<GetCustomersDTO> { customer };
            var countryList = new List<CustGroupCountryModel> { customer.CustGroupCountry };

            using var report = new Report();
            report.RegisterData(customerList, "CustomerData");
            report.GetDataSource("CustomerData").Enabled = true;

            report.RegisterData(countryList, "CustGroupCountry");
            report.GetDataSource("CustGroupCountry").Enabled = true;

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Customer_1_DataBoundN.frx");
            report.Load(filePath);

            report.Prepare();
            using var stream = new MemoryStream();
            var pdfExport = new PDFSimpleExport();
            pdfExport.Export(report, stream);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", $"Customer_{id}_Report.pdf");
        }
    }
}