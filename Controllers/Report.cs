using Microsoft.AspNetCore.Mvc;
using FastReport;
using FastReport.Export.PdfSimple;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;
using YourNamespace;

namespace CustomerApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ReportController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // ดึงข้อมูลลูกค้าเป็น List<CustomerReport>
        private List<CustomerReport> GetReportData(int customerId)
        {
            List<CustomerReport> reportData = new List<CustomerReport>();

            string connectionString = _configuration.GetConnectionString("TestCon");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT ID, FullName, ReportDetail FROM Customers WHERE ID = @CustomerId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reportData.Add(new CustomerReport
                            {
                                CustomerId = reader.GetInt32(0),
                                FullName = reader.GetString(1),
                                ReportDetail = reader.GetString(2)
                            });
                        }
                    }
                }
            }

            return reportData;
        }

        // API สร้างรายงาน PDF
        [HttpGet("report/{customerId}")]
        public IActionResult GetCustomerReport(int customerId)
        {
            var reportData = GetReportData(customerId);
            if (reportData.Count == 0)
                return NotFound("Customer not found");

            var customer = reportData[0];

            using (Report report = new Report())
            {
                // โหลด template (.frx)
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "reports", "Jacop.frx");
                report.Load(filePath);

                // ลงทะเบียนข้อมูล
                report.RegisterData(reportData, "ReportData");

                // ตั้งค่าพารามิเตอร์ (หากใช้ในรายงาน)
                report.SetParameterValue("CustomerId", customer.CustomerId);
                report.SetParameterValue("FullName", customer.FullName);
                report.SetParameterValue("ReportDetail", customer.ReportDetail);

                // สร้าง PDF
                report.Prepare();
                using MemoryStream pdfStream = new MemoryStream();
                PDFSimpleExport pdfExport = new PDFSimpleExport();
                report.Export(pdfExport, pdfStream);
                pdfStream.Position = 0;

                return File(pdfStream.ToArray(), "application/pdf", "Jacop.pdf");
            }
        }
    }


}
