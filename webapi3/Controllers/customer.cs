

using Microsoft.AspNetCore.Mvc;

namespace CustomerApi.Controllers
{
    // กำหนด route
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private List<Customer> customers = new List<Customer>
        {
            new Customer {
                CustomerId = 1,
                CountryId = 1,
                ProvinceId = 101,
                Generals = new GeneralsModel {
                    GeneralName = "บริษัท A",
                    GeneralTel = "012-3456789",
                    GeneralFax = "012-3456790",
                    GeneralEmail = "contact@companyA.com",
                    GeneralLine = "@companyA",
                    GeneralTax = "1234567890",
                    GeneralBranch = "สำนักงานใหญ่"
                }
            }
        };

        private List<Country> countries = new List<Country>
        {
            new Country { CountryId = 1, NameTh = "ไทย", IsoAlpha2 = "TH", IsoAlpha3 = "THA", OfficialName = "Thailand", Region = "Asia", SubRegion = "Southeast Asia", CapitalCity = "กรุงเทพมหานคร" },
            new Country { CountryId = 2, NameTh = "สหรัฐอเมริกา", IsoAlpha2 = "US", IsoAlpha3 = "USA", OfficialName = "United States", Region = "Americas", SubRegion = "Northern America", CapitalCity = "วอชิงตัน ดี.ซี." },
        };

        private List<Province> provinces = new List<Province>
        {
            new Province { CountryId = 1, ProvinceId = 101, NameTh = "กรุงเทพมหานคร", NameEn = "Bangkok", ProvinceCode = "BKK" },
            new Province { CountryId = 1, ProvinceId = 102, NameTh = "เชียงใหม่", NameEn = "Chiang Mai", ProvinceCode = "CM" },
            new Province { CountryId = 2, ProvinceId = 201, NameTh = "แคลิฟอร์เนีย", NameEn = "California", ProvinceCode = "CA" },
        };


        // http get cutmoter tabe{//http get id detiall}
        [HttpGet("{customerId}")]
        public IActionResult GetCustomerById(int customerId)
        {
            // ใช้ FirstOrDefault() ค้นหาลูกค้าที่มี CustomerId ตรงกับพารามิเตอร์ที่รับมา
            var customer = customers.FirstOrDefault(c => c.CustomerId == customerId);
            if (customer == null)
            {
                return NotFound(new { message = "Customer not found" });
            }

            // นำ ProvinceId ของที่ได้จากลูกค้าไปค้นหา provincesต่อ
            var province = provinces.FirstOrDefault(p => p.ProvinceId == customer.ProvinceId);

            // สร้าง object สำหรับ response
            var result = new
            {
                Customer = customer,
                Province = province
            };

            return Ok(result);
        }












        // ✅ API คืนค่า List ของ Customers
        [HttpGet]
        public IActionResult GetCustomers()
        {
            return Ok(customers);
        }

        /* ✅ API คืนค่าข้อมูลลูกค้าตาม CustomerId
        [HttpGet("{customerId}")]
        public IActionResult GetCustomerById(int customerId)
        {
            var customer = customers.FirstOrDefault(c => c.CustomerId == customerId);
            if (customer == null)
            {
                return NotFound(new { message = "Customer not found" });
            }
            // คืนค่า อ็อบเจ็กต์ customer ที่ประกอบไปด้วยข้อมูลของลูกค้า
            return Ok(customer);
        }*/

        // ✅ API คืนค่าจังหวัดตาม CountryId

        [HttpGet("provinces/{countryId}")]
        public IActionResult GetProvincesByCountry(int countryId)
        {
            var result = provinces.Where(p => p.CountryId == countryId).ToList();
            return Ok(result);
        }
        //id
        //convet id





    }



}




//id seleck
// กำหนด model
public class Customer
{
    public int CustomerId { get; set; } // เพิ่ม CustomerId

    public int CountryId { get; set; }
    public int ProvinceId { get; set; }
    //GeneralsModel เป็น ชนิดข้อมูล  คลาส ที่คุณสร้างขึ้นมาเพื่อเก็บข้อมูลต่าง ๆ
    //ข้อมูลของ Generals จะถูกเก็บใน GeneralsModel
    public GeneralsModel Generals { get; set; }



}

public class GeneralsModel
{
    public string GeneralName { get; set; }
    public string GeneralName1 { get; set; }
    public string GeneralTel { get; set; }
    public string GeneralFax { get; set; }
    public string GeneralEmail { get; set; }
    public string GeneralLine { get; set; }
    public string GeneralTax { get; set; }
    public string GeneralBranch { get; set; }
}



// ประเทศ
public class Country
{
    public int CountryId { get; set; }
    public string NameTh { get; set; }
    public string IsoAlpha2 { get; set; }
    public string IsoAlpha3 { get; set; }
    public string OfficialName { get; set; }
    public string Region { get; set; }
    public string SubRegion { get; set; }
    public string CapitalCity { get; set; }
}
//ภูมิภาค

public class Province
{
    public int CountryId { get; set; }
    public int ProvinceId { get; set; }
    public string NameTh { get; set; }
    public string NameEn { get; set; }
    public string ProvinceCode { get; set; }
}


/*
thai_geographies

thai_provinces

thai_amphures

thai_tambons

*/

