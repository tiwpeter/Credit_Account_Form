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
        private readonly CustomerService _postcustomerService;

        private readonly GetByIdCustomerService _getByIdCustomerService;


        public RegisformController(GetCustomerService customerService, CustomerService postcustomerService, GetByIdCustomerService getByIdCustomerService)
        {
            _customerService = customerService;
            _postcustomerService = postcustomerService;
            _getByIdCustomerService = getByIdCustomerService;
        }

        //post

        // get all 
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerService.GetCustomersAsync();
            return Ok(customers);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request)
        {
            var errors = await _postcustomerService.CreateCustomerAsync(request);

            if (errors.Any())
            {
                return BadRequest(new { message = "Validation errors", errors });
            }

            return Ok(new { message = "Customer created successfully" });
        }

        //get id
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCustomersDTO>> GetCustomerById(int id)
        {
            var customer = await _getByIdCustomerService.GetCustomerByIdAsync(id); // ✅ ใช้ _getCustomerService

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

    }
}