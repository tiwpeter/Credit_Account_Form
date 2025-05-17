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
            // 1. ดึงข้อมูลลูกค้าจากฐานข้อมูล
            var customer = await _context.Customers
                .Include(c => c.CustGroupCountry)
                .Where(c => c.CustomerId == id)
                .Select(c => new GetCustomersDTO
                {
                    CustomerId = c.CustomerId,
                    CustGroupCountries = new CustGroupCountryModel
                    {
                        CountryCode = c.CustGroupCountry.CountryCode,
                        CountryName = c.CustGroupCountry.CountryName,
                        CountryDes = c.CustGroupCountry.CountryDes
                    }
                })
                .FirstOrDefaultAsync();

            if (customer == null)
                return NotFound();

            // 2. แปลงให้อยู่ใน List (FastReport ต้องการ IEnumerable)
            var customerList = new List<GetCustomersDTO> { customer };
            var countryList = new List<CustGroupCountryModel> { customer.CustGroupCountries };

            // 3. โหลดรายงาน
            Report report = new Report();
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Jacop.frx");
            report.Load(filePath);

            // 4. ลงทะเบียนข้อมูล
            report.RegisterData(customerList, "CustomerData");
            report.GetDataSource("CustomerData").Enabled = true;

            report.RegisterData(countryList, "CustGroupCountry");
            report.GetDataSource("CustGroupCountry").Enabled = true;

            string savedFrxPath = Path.Combine(Directory.GetCurrentDirectory(), $"Customer_{id}_DataBound.frx");
            report.Save(savedFrxPath);
            report.Dispose();

            return Ok(new { message = "FRX file created successfully", path = savedFrxPath });
        }




    }
}