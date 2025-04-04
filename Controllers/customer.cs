using Microsoft.AspNetCore.Mvc;
using FastReport;
using FastReport.Export.PdfSimple;
using ModelTest.Controllers;


namespace CustomerApi.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private List<Customer> customers = new List<Customer>
        {
            new Customer
            {
                CustomerId = 1,
                Generals = new GeneralsModel
                {
                    GeneralName = "บริษัท A",
                    GeneralTel = "012-3456789",
                    GeneralFax = "012-3456790",
                    GeneralEmail = "contact@companyA.com",
                    GeneralLine = "@companyA",
                    GeneralTax = "1234567890",
                    GeneralBranch = "สำนักงานใหญ่"
                },
                Addresses = new AddressesModel
                {
                    addrType = "ที่อยู่สำนักงาน",
                    addrLine1 = "123 ถนนสุขุมวิท",
                    addrLine2 = "ชั้น 5 อาคาร A",
                    subDistrict = "คลองตัน",
                    district = "เขตวัฒนา",
                    province = "กรุงเทพมหานคร",
                    postalCode = "10110",
                    country = "ไทย",
                    createdDate = "2025-04-01",
                    CountryId = 1,
                    ProvinceId = 101
                },
                Shipping = new ShippingModel
                {
                    shipping_id = 1,
                    addrType = "ที่อยู่จัดส่ง",
                    DeliveryName = "คุณสมชาย",
                    address1 = "456 ถนนสุขุมวิท",
                    district = "คลองเตย",
                    province = "กรุงเทพมหานคร",
                    postalCode = "10110",
                        contact_name = "คุณสมชาย สุวรรณดี",       // ชื่อผู้ติดต่อ
    mobile = "089-123-4567",                 // เบอร์มือถือผู้รับ
    freight = "150.00m",         // ชื่อบริษัทขนส่ง (หากใช้ freight ในความหมายนี้)
   
                },
                ShopType = new ShopTypeModel
                {
                    id = 1,
                    shopCode = "A123",
                    shopName = "ร้าน A",
                    shopDes = "ร้านขายสินค้าครบวงจร",
                    accGroupName = "กลุ่มธุรกิจ A"
                },
                Company = new CompanyModel
                {
                    company_id = 1,
                    companyCode = "CMP001",
                    companyName = "บริษัท A จำกัด",
                    companyAddr = "123 ถนนสุขุมวิท แขวงคลองตัน เขตวัฒนา กรุงเทพมหานคร 10110"
                }
            },
            new Customer
            {
                CustomerId = 2,
                Generals = new GeneralsModel
                {
                    GeneralName = "บริษัท B",
                    GeneralTel = "098-7654321",
                    GeneralFax = "098-7654332",
                    GeneralEmail = "contact@companyB.com",
                    GeneralLine = "@companyB",
                    GeneralTax = "9876543210",
                    GeneralBranch = "สาขาลอสแอนเจลิส"
                },
                Addresses = new AddressesModel
                {
                    addrType = "ที่อยู่สำนักงาน",
                    addrLine1 = "456 ถนนฮอลลีวูด",
                    addrLine2 = "อาคาร B",
                    subDistrict = "Hollywood",
                    district = "Los Angeles",
                    postalCode = "90028",
                    createdDate = "2025-04-02",
                    CountryId = 2,
                    ProvinceId = 201
                },
                Shipping = new ShippingModel
                {
                    shipping_id = 2,
                    addrType = "ที่อยู่จัดส่ง",
                    DeliveryName = "คุณจอห์น",
                    address1 = "789 ถนนเมลโรส",
                    district = "Los Angeles",
                    province = "California",
                    postalCode = "90029",
                    shippingcountry = "us"
                },
                ShopType = new ShopTypeModel
                {
                    id = 2,
                    shopCode = "B456",
                    shopName = "ร้าน B",
                    shopDes = "ร้านจำหน่ายอุปกรณ์ไฮเทค",
                    accGroupName = "กลุ่มธุรกิจ B"
                },
                Company = new CompanyModel
                {
                    company_id = 2,
                    companyCode = "CMP002",
                    companyName = "บริษัท B จำกัด",
                    companyAddr = "456 ถนนฮอลลีวูด เมือง Los Angeles รัฐ California 90028"
                }
            }
        };

        private List<Country> countries = new List<Country>
        {
            new Country { CountryId = 1, NameTh = "ไทย", IsoAlpha2 = "TH", IsoAlpha3 = "THA", OfficialName = "Thailand", Region = "Asia", SubRegion = "Southeast Asia", CapitalCity = "กรุงเทพมหานคร" },
            new Country { CountryId = 2, NameTh = "สหรัฐอเมริกา", IsoAlpha2 = "US", IsoAlpha3 = "USA", OfficialName = "United States", Region = "Americas", SubRegion = "Northern America", CapitalCity = "วอชิงตัน ดี.ซี." }
        };

        private List<Province> provinces = new List<Province>
        {
            new Province { CountryId = 1, ProvinceId = 101, NameTh = "กรุงเทพมหานคร", NameEn = "Bangkok", ProvinceCode = "BKK" },
            new Province { CountryId = 1, ProvinceId = 102, NameTh = "เชียงใหม่", NameEn = "Chiang Mai", ProvinceCode = "CM" },
            new Province { CountryId = 2, ProvinceId = 201, NameTh = "แคลิฟอร์เนีย", NameEn = "California", ProvinceCode = "CA" }
        };

        // http get customer details
        [HttpGet("customer-report/{customerId}")]
        public IActionResult GetCustomerReport(int customerId)
        {
            // 1. โหลดรายงาน
            Report report = new Report();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "1.frx");
            report.Load(filePath);

            // 2. ดึงข้อมูลลูกค้า (แบบจำลองข้อมูล)
            var customer = customers
                .Where(c => c.CustomerId == customerId)
                .Select(c => new
                {
                    c.CustomerId,
                    c.Generals.GeneralName,
                    c.Generals.GeneralTel,
                    c.Generals.GeneralFax,
                    c.Generals.GeneralEmail,
                    c.Generals.GeneralLine,
                    c.Generals.GeneralTax,
                    c.Generals.GeneralBranch,
                    c.Addresses.addrLine1,
                    c.Addresses.addrLine2,
                    c.Addresses.subDistrict,
                    c.Addresses.district,
                    Province = provinces.FirstOrDefault(p => p.ProvinceId == c.Addresses.ProvinceId)?.NameTh,
                    Country = countries.FirstOrDefault(co => co.CountryId == c.Addresses.CountryId)?.NameTh,
                    c.Addresses.postalCode,
                    c.Addresses.createdDate,
                    c.Shipping.DeliveryName,
                    c.Shipping.address1,
                    ShippingDistrict = c.Shipping.district,
                    c.Shipping.shippingcountry,
                    ShippingPostalCode = c.Shipping.postalCode,

                    Freight = c.Shipping.freight,
                    Telephone = c.Shipping.mobile,


                    ShippingProvince = c.Shipping.province,
                    c.ShopType.shopCode,
                    c.ShopType.shopName,
                    c.ShopType.shopDes,
                    c.ShopType.accGroupName,
                    c.Company.companyName,
                    c.Company.companyCode,
                    c.Company.companyAddr
                })
                                .FirstOrDefault();




            // 3. ผูกข้อมูลกับ Report
            // 3. ผูกข้อมูลกับ Report
            report.RegisterData(new List<object> { customer }, "Customer");

            // 4. Prepare และ Export เป็น PDF
            report.Prepare();
            using (MemoryStream pdfStream = new MemoryStream())
            {
                report.Prepare();
                PDFSimpleExport pdfExport = new PDFSimpleExport(); // ใช้ PDFSimpleExport
                report.Export(pdfExport, pdfStream);
                report.Dispose(); // ปิด Report

                // รีเซ็ตตำแหน่งของ MemoryStream ก่อนส่งกลับ
                pdfStream.Position = 0; // รีเซ็ต Stream

                // 5. ส่งกลับ PDF
                return File(pdfStream.ToArray(), "application/pdf", "2.pdf");

            }
        }


    }
}

//id seleck
// กำหนด model