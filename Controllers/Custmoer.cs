using API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelTest.Controllers;
using System.Threading.Tasks;

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

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _getcustomerService.GetCustomersAsync();
            return Ok(customers);
        }

        // id = sevice


        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request)
        {
            // ตรวจสอบว่า CountryId ที่ได้รับจาก frontend มีอยู่ในฐานข้อมูลหรือไม่
            var countryExists = await _context.Countries
                .AnyAsync(c => c.CountryId == request.General.Address.Country.CountryId);  // เปลี่ยนเป็น request.General.Address.Country.CountryId

            Console.WriteLine($"Received CountryId: {request.General.Address.Country.CountryId}");  // แก้ไขเป็น request.General.Address.Country.CountryId

            if (!countryExists)
            {
                // หาก CountryId ไม่พบในฐานข้อมูล ส่งผลลัพธ์ NotFound
                return NotFound(new { message = "Country not found", countryId = request.General.Address.Country.CountryId });
            }

            // หากพบ CountryId ในฐานข้อมูล
            else
            {
                // ส่งผลลัพธ์ที่แสดงว่า CountryId มีอยู่ในฐานข้อมูล
                return Ok(new { message = "Country found", countryId = request.General.Address.Country.CountryId });
            }

            // ดำเนินการสร้าง customer ถ้าผ่านการตรวจสอบ
            await _customerService.CreateCustomerAsync(request);

            return Ok(new { message = "Customer created successfully" });
        }





    }
}