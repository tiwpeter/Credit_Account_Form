using Microsoft.AspNetCore.Mvc;
using System.IO;
using FastReport; // เพิ่มการใช้งาน FastReport
using Test.Models;  // เชื่อมต่อกับคลาส Employee ที่อยู่ใน Test.Models
using FastReport.Export.PdfSimple; // ใช้ PDFSimpleExport

namespace demoapi.Controller
{
    [ApiController]
    [Route("api/Download")] // แก้ไขชื่อเส้นทาง URL
    public class DownloadController : ControllerBase
    {
        // GET: api/Download
        [HttpGet]
        public IActionResult Get()
        {
            // 🔹 สร้าง List ของ Employee
            List<Employee> employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "Alice", Position = "Manager", Salary = 50000 },
                new Employee { Id = 2, Name = "Bob", Position = "Developer", Salary = 40000 },
                new Employee { Id = 3, Name = "Charlie", Position = "Designer", Salary = 35000 }
            };

            // 🔹 โหลด FastReport
            Report report = new Report();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "EmployeeReport.frx");
            report.Load(filePath);

            // 🔹 เชื่อมต่อ List<Employee> กับ FastReport
            report.RegisterData(employees, "Employee");

            // Debug: พิมพ์ตำแหน่งไฟล์ออกมา
            Console.WriteLine($"Looking for file at: {filePath}");

            // ตรวจสอบว่าไฟล์มีอยู่ไหม
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found.");
            }

            // 🔹 ส่งออกเป็น PDF (ใช้ PDFSimpleExport)
            using (MemoryStream pdfStream = new MemoryStream())
            {
                // เตรียมการและส่งออกไฟล์ PDF
                report.Prepare();
                PDFSimpleExport pdfExport = new PDFSimpleExport(); // ใช้ PDFSimpleExport
                report.Export(pdfExport, pdfStream);
                report.Dispose(); // ปิด Report

                pdfStream.Position = 0; // รีเซ็ต Stream

                // 🔹 ส่งไฟล์ PDF ให้ผู้ใช้
                return File(pdfStream.ToArray(), "application/pdf", "EmployeeReport.pdf");
            }
        }
    }
}
