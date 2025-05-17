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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();

            var customerList = new List<GetCustomersDTO> { customer };
            var countryList = new List<CustGroupCountryModel> { customer.CustGroupCountries };

            using var report = new Report();
            report.RegisterData(customerList, "CustomerData");
            report.GetDataSource("CustomerData").Enabled = true;

            report.RegisterData(countryList, "CustGroupCountry");
            report.GetDataSource("CustGroupCountry").Enabled = true;

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Customer_1_DataBound009.frx");
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