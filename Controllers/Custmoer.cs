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
                .AnyAsync(c => c.CountryId == request.General.Address.Country.CountryId);

            Console.WriteLine($"Received CountryId: {request.General.Address.Country.CountryId}");

            if (!countryExists)
            {
                // หาก CountryId ไม่พบในฐานข้อมูล ส่ง NotFound กลับไป
                return NotFound(new { message = "Country not found", countryId = request.General.Address.Country.CountryId });
            }

            // ตรวจสอบว่า CountryId ที่ได้รับจาก ShippingDto มีอยู่ในฐานข้อมูลหรือไม่
            var countryExistsShipping = await _context.Countries
                .AnyAsync(c => c.CountryId == request.ShippingDto.Country.CountryId);

            Console.WriteLine($"Received Shipping CountryId: {request.ShippingDto.Country.CountryId}");

            if (!countryExistsShipping)
            {
                // หาก CountryId จาก ShippingDto ไม่พบในฐานข้อมูล ส่ง NotFound กลับไป
                return NotFound(new { message = "Shipping Country not found", countryId = request.ShippingDto.Country.CountryId });
            }

            // ตรวจสอบว่า ProvinceId ที่ได้รับจาก frontend มีอยู่ในฐานข้อมูลหรือไม่
            var provinceExists = await _context.Provinces
                .AnyAsync(p => p.ProvinceId == request.ShippingDto.Province.ProvinceId);

            Console.WriteLine($"Received ProvinceId: {request.ShippingDto.Province.ProvinceId}");

            if (!provinceExists)
            {
                // หาก ProvinceId ไม่พบในฐานข้อมูล ส่ง NotFound กลับไป
                return NotFound(new { message = "Province not found", provinceId = request.ShippingDto.Province.ProvinceId });
            }

            // ✅ ถ้า countryId เจอแล้ว ค่อยดำเนินการสร้าง Customer
            await _customerService.CreateCustomerAsync(request);

            return Ok(new { message = "Customer created successfully" });
        }

    }
}