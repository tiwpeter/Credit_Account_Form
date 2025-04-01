/*using FastReport;
using FastReport.Export.PdfSimple;
using Microsoft.AspNetCore.Mvc;
using System.Data;

[Route("api/report")]
[ApiController]
public class ReportController : ControllerBase
{
    [HttpGet("account-opening/{userId}")]
    public IActionResult GetAccountOpeningForm(int userId)
    {
        // ตัวอย่างข้อมูลผู้ใช้ (ปกติจะดึงจาก DB)
        var userData = new
        {
            FullName = "นายสมชาย ใจดี",
            AccountNumber = "123-456-7890",
            AccountType = "ออมทรัพย์",
            Branch = "กรุงเทพฯ",
            UserId = userId
        };

        using var report = new Report();

        // สร้างข้อมูลเป็น DataTable
        var dataSet = new DataSet();
        var table = new DataTable("Account");
        table.Columns.Add("FullName", typeof(string));
        table.Columns.Add("AccountNumber", typeof(string));
        table.Columns.Add("AccountType", typeof(string));
        table.Columns.Add("Branch", typeof(string));
        table.Columns.Add("UserId", typef(int));

        table.Rows.Add(userData.FullName, userData.AccountNumber, userData.AccountType, userData.Branch, userData.UserId);
        dataSet.Tables.Add(table);

        // Load report design
        report.RegisterData(dataSet, "Data");
        report.Load("Reports/AccountOpening.frx"); // ต้องสร้างไฟล์นี้

        report.Prepare();

        // Export to PDF
        using var stream = new MemoryStream();
        var pdfExport = new PDFSimpleExport();
        report.Export(pdfExport, stream);
        stream.Position = 0;

        return File(stream.ToArray(), "application/pdf", "AccountOpening.pdf");
    }
}
*/