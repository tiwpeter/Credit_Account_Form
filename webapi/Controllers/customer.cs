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
    GeneralName = "บริษัท เอ ไอ เทคโนโลยี จำกัด",
    GeneralName1 = "A.I. Technology Co., Ltd.",
    GeneralTel = "02-123-4567",
    GeneralFax = "02-123-4568",
    GeneralEmail = "contact@aitech.co.th",
    GeneralLine = "@aitech",
    GeneralTax = "1234567890123",
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
    shopDes = "ร้านจำหน่ายสินค้าครบวงจร รวมทั้งอุปกรณ์ต่าง ๆ ที่หลากหลาย",
    accGroupName = "กลุ่มธุรกิจค้าปลีก"
},
                Company = new CompanyModel
                {
                    company_id = 1,
                    companyCode = "CMP001",
                    companyName = "บริษัท A จำกัด",
                    companyAddr = "123 ถนนสุขุมวิท แขวงคลองตัน เขตวัฒนา กรุงเทพมหานคร 10110"
                },
                IndustryType = new IndustryTypeModel
{
    id = 2,
    InduTypeCode = "IT001",  // รหัสประเภทอุตสาหกรรม
    InduTypeName = "เทคโนโลยีสารสนเทศ",  // ชื่อประเภทอุตสาหกรรม
    InduTypeDes = "อุตสาหกรรมที่เกี่ยวข้องกับการพัฒนาเทคโนโลยีสารสนเทศ เช่น ซอฟต์แวร์, ฮาร์ดแวร์, และบริการด้านไอที"  // คำอธิบาย
}
            },

            new Customer
           {
     CustomerId = 2,
    Generals = new GeneralsModel
{
    GeneralName = "บริษัท สมาร์ทเทค โซลูชั่น จำกัด",
    GeneralName1 = "SmartTech Solution Co., Ltd.",
    GeneralTel = "02-987-6543",
    GeneralFax = "02-987-6544",
    GeneralEmail = "info@smarttech.co.th",
    GeneralLine = "@smarttech",
    GeneralTax = "9876543210987",
    GeneralBranch = "สาขากรุงเทพฯ"
},
    Addresses = new AddressesModel
{
    addrType = "ที่อยู่สำนักงาน",
    addrLine1 = "99/1 ถนนสุขุมวิท",
    addrLine2 = "ชั้น 10 อาคารไทยพาณิชย์",
    subDistrict = "คลองเตย",
    district = "คลองเตย",
    postalCode = "10110",
    createdDate = "2025-04-02",
    CountryId = 1,         // ประเทศไทย
    ProvinceId = 101        // กรุงเทพมหานคร
}
,
    Shipping = new ShippingModel
{
    shipping_id = 2,
    addrType = "ที่อยู่จัดส่ง",
    DeliveryName = "คุณสมชาย ใจดี",
    address1 = "88 ซอยสุขุมวิท 22",
    district = "คลองเตย",
    province = "กรุงเทพมหานคร",
    postalCode = "10110",
    shippingcountry = "TH",     // รหัสประเทศไทย
    freight = "150.00m",
    mobile = "081-2345678",
    contact_name = "คุณสมชาย ใจดี"
}
,
    IndustryType = new IndustryTypeModel
    {
        id = 2,
        InduTypeCode = "TECH",
        InduTypeName = "เทคโนโลยีและอิเล็กทรอนิกส์",
        InduTypeDes = "กลุ่มธุรกิจที่เกี่ยวข้องกับอุปกรณ์อิเล็กทรอนิกส์ ซอฟต์แวร์ และเทคโนโลยีขั้นสูง"
    },
    ShopType = new ShopTypeModel
    {
    id = 2,
    shopCode = "B456",
    shopName = "ร้าน B",
    shopDes = "ร้านจำหน่ายอุปกรณ์ไฮเทคและเทคโนโลยีล้ำสมัยสำหรับผู้ที่สนใจ",
    accGroupName = "กลุ่มธุรกิจเทคโนโลยี"
},
    Company = new CompanyModel
    {
        company_id = 2,
        companyCode = "CMP002",
        companyName = "บริษัท B จำกัด",
        companyAddr = "456 ถนนฮอลลีวูด เมือง Los Angeles รัฐ California 90028"
    },
    SortKey = new SortKeyModel
    {
        id = 1,
        sortkeyCode = "SK01",
        sortkeyName = "เรียงตามตัวอักษร",
        sortkeyDes = "เรียงตามชื่อบริษัท"
    },
    CashGroup = new CashGroupModel
    {
        id = 1,
        cashCode = "CG01",
        cashName = "กลุ่มเงินสด A",
        cashDes = "ลูกค้าที่ชำระเงินสด"
    },
    PaymentMethod = new PaymentMethodModel
    {
        id = 1,
        payCode = "PM01",
        payName = "โอนผ่านธนาคาร",
        payDes = "การชำระเงินโดยการโอนเงินเข้าบัญชีธนาคาร"
    },
    TermOfPay = new TermOfPayModel
    {
        id = 1,
        topCode = "TOP01",
        topName = "ชำระภายใน 30 วัน",
        topDes = "เงื่อนไขการชำระเงินหลังได้รับสินค้า 30 วัน"
    },
                AccountCode = new AccountCodeModel
{
    id = 1,
    accCode = "AC001",
    accName = "บัญชีรายรับ",
    accDes = "บัญชีสำหรับบันทึกรายรับของบริษัท"
},
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

        [HttpPut("customer/{customerId}")]
        public IActionResult UpdateCustomer(int customerId, [FromBody] Customer updatedCustomer)
        {
            // ตรวจสอบข้อมูลที่ได้รับ
            if (updatedCustomer == null)
            {
                return BadRequest("ข้อมูลลูกค้าผิดรูปแบบ");
            }

            // แสดงข้อมูลที่ได้รับใน console
            Console.WriteLine("ข้อมูลที่ได้รับจาก client:");
            Console.WriteLine($"GeneralName: {updatedCustomer.Generals?.GeneralName}");

            var customer = customers.FirstOrDefault(c => c.CustomerId == customerId);

            if (customer == null)
            {
                return NotFound("ไม่พบข้อมูลลูกค้า");
            }

            // อัปเดตข้อมูลลูกค้า
            customer.Generals.GeneralName = updatedCustomer.Generals?.GeneralName ?? customer.Generals.GeneralName;

            return Ok("อัปเดตข้อมูลลูกค้าสำเร็จ");
        }



        [HttpGet("customer")]
        public IActionResult GetAllCustomers()
        {
            var allCustomers = customers.Select(customer => new
            {
                id = customer.CustomerId, // ✅ เพิ่ม id ที่นี่
                GeneralName = customer.Generals?.GeneralName ?? "ไม่ระบุ",
                addrLine1 = customer.Addresses?.addrLine1 ?? "ไม่ระบุ",
                accGroupName = customer.ShopType?.accGroupName ?? "ไม่ระบุ",
                InduTypeName = customer.IndustryType?.InduTypeName ?? "ไม่ระบุ",
                DeliveryName = customer.Shipping?.DeliveryName ?? "ไม่ระบุ",
                payName = customer.PaymentMethod?.payName ?? "ไม่ระบุ"
            }).ToList();

            return Ok(allCustomers);
        }



        [HttpGet("customer/{customerId}")]
        public IActionResult GetDetailCustomers(int customerId)
        {
            var customer = customers.FirstOrDefault(c => c.CustomerId == customerId);

            if (customer == null)
            {
                return NotFound("ไม่พบข้อมูลลูกค้า");
            }

            var detailCustomer = new
            {
                General = customer.Generals,
                Addresses = customer.Addresses,
                Shipping = customer.Shipping,
                IndustryType = customer.IndustryType,
                ShopType = customer.ShopType,
                Company = customer.Company,
                SortKey = customer.SortKey,
                CashGroup = customer.CashGroup,
                PaymentMethod = customer.PaymentMethod,
                TermOfPay = customer.TermOfPay,
                AccountCode = customer.AccountCode,
            };

            return Ok(detailCustomer); // ส่ง object เดียว ไม่ใช่ list
        }






        // PDF
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
                    c.Generals.GeneralName1,
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

                    //Contact Person/บุคคลที่ติดต่อ			
                    c.Shipping.contact_name,
                    // 🏭 IndustryType: ประเภทอุตสาหกรรม
                    IndustryTypeId = c.IndustryType.id,
                    IndustryTypeCode = c.IndustryType.InduTypeCode,
                    IndustryTypeName = c.IndustryType.InduTypeName,
                    IndustryTypeDescription = c.IndustryType.InduTypeDes,

                    // company
                    c.Company.companyName,
                    c.Company.companyCode,
                    c.Company.companyAddr,

                    //SortKey
                    c.SortKey.sortkeyCode,
                    c.SortKey.sortkeyName,
                    c.SortKey.sortkeyDes,

                    c.CashGroup.cashCode,
                    c.CashGroup.cashName,
                    c.CashGroup.cashDes,

                    c.PaymentMethod.payCode,
                    c.PaymentMethod.payName,
                    c.PaymentMethod.payDes,

                    c.TermOfPay.topCode,
                    c.TermOfPay.topName,
                    c.TermOfPay.topDes,


                    c.AccountCode.accCode,
                    c.AccountCode.accName,
                    c.AccountCode.accDes,




                    ShippingProvince = c.Shipping.province,
                    c.ShopType.shopCode,
                    c.ShopType.shopName,
                    c.ShopType.shopDes,
                    c.ShopType.accGroupName,

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