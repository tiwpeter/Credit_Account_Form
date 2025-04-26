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

        // id = service
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request)
        {
            // ตรวจสอบว่า request ข้อมูลถูกต้องหรือไม่
            if (!ModelState.IsValid)
            {
                // หากข้อมูลไม่ถูกต้อง ส่งกลับ BadRequest พร้อมข้อผิดพลาด
                return BadRequest(new { message = "Validation failed", errors = ModelState });
            }


            // ดำเนินการสร้าง customer หากข้อมูลถูกต้อง
            await _customerService.CreateCustomerAsync(request);

            return Ok(new { message = "Customer created successfully" });
        }
    }
}
