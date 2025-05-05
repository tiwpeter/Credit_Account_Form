using API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelTest.Controllers;
using System.Threading.Tasks;
using FastReport;
using FastReport.Export.PdfSimple;
using System.IO;
using System.Data;
using System.Linq; // For LINQ methods like .Any()

namespace ModelTest.ApiControllers
{
    [Route("api")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly CustomerService _customerService;
        private readonly GetCustomerService _getCustomerService;

        public ReportController(ApplicationDbContext context, CustomerService customerService, GetCustomerService getCustomerService)
        {
            _context = context;
            _customerService = customerService;
            _getCustomerService = getCustomerService;
        }

        public DataTable ToDataTable(List<GeneralDto> generalDataList)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("GeneralName", typeof(string));
            dataTable.Columns.Add("GeneralName1", typeof(string));
            dataTable.Columns.Add("GeneralTel", typeof(string));

            foreach (var item in generalDataList)
            {
                dataTable.Rows.Add(item.GeneralName, item.GeneralName1, item.GeneralTel);
            }
            return dataTable;
        }

        [HttpGet("report/{id}")]
public async Task<IActionResult> GenerateCustomerReport(int id)
{
    // ดึงข้อมูลลูกค้าจากบริการ
    var customer = await _getCustomerService.GetCustomerByIdAsync(id);
    if (customer == null)
        return NotFound("Customer not found");

    // ตรวจสอบว่ามีข้อมูล General หรือไม่
    if (customer.General == null)
    {
        return NotFound("General data for the customer not found.");
    }

    // สร้างข้อมูล GeneralDto เพื่อส่งไปยังรายงาน
    var generalDataList = new List<GeneralDto>
    {
        new GeneralDto
        {
            GeneralName = customer.General.GeneralName,
            GeneralName1 = customer.General.GeneralName1,
            GeneralTel = customer.General.GeneralTel
        }
    };

    // แสดงข้อมูลที่ได้จาก customer ใน Console
    foreach (var general in generalDataList)
    {
        Console.WriteLine($"GeneralName: {general.GeneralName}, GeneralName1: {general.GeneralName1}, GeneralTel: {general.GeneralTel}");
    }
    
    // สร้าง Report
    Report report = new Report();
    string reportPath = Path.Combine(Directory.GetCurrentDirectory(), "General.frx");


    report.Load(reportPath);

    // ลงทะเบียนข้อมูลในรูปแบบ List<GeneralDto>
    report.RegisterData(generalDataList, "Generals");

    // แสดงข้อมูลหลังจากลงทะเบียน
    Console.WriteLine("Report data registered successfully.");

    // เตรียมรายงาน
    report.Prepare();

    // สร้าง MemoryStream สำหรับการส่งไฟล์ PDF
    using (var stream = new MemoryStream())
    {
        PDFSimpleExport export = new PDFSimpleExport();
        report.Export(export, stream);
        stream.Position = 0;

        // ส่งไฟล์ PDF กลับไปยังผู้ใช้
        return File(stream.ToArray(), "application/pdf", "CustomerReport.pdf");
    }
}

}
}