using API.Data;
using ModelTest.Controllers;

public class CustomerService
{
    private readonly ApplicationDbContext _context;

    public CustomerService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<string>> CreateCustomerAsync(CreateCustomerRequest request)
    {

        try
        {
            // สร้าง customer ด้วยข้อมูลที่ได้รับจาก request
            var customer = new CustomerModel
            {
                CustomerName = request.CustomerName,
                General = new GeneralModel
                {
                    generalName = request.General?.GeneralName,
                    Address = new AddressModel
                    {
                        CountryId = request.General.Address.Country.CountryId, // ใช้ CountryId ที่ถูกต้อง
                        ProvinceId = request.General.Address.Province.ProvinceId // ใช้ ProvinceId ที่ถูกต้อง
                    }
                },
                Shipping = new ShippingModel
                {
                    ProvinceId = request.ShippingProvinceId,
                    subDistrict = request.ShippingDto.SubDistrict,
                    CountryId = request.ShippingDto.Country.CountryId, // <-- แบบนี้ถูกต้อง

                },
                busiTypeID = request.BusinessTypeId,
                CreditInfo = new CreditInfoModel
                {
                    EstimatedPurchase = request.EstimatedPurchase,
                    TimeRequired = request.TimeRequired,
                    CreditLimit = request.CreditLimit
                },
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return new List<string>(); // คืนค่ารายการที่ว่างหมายความว่าไม่มีฟิลด์ขาดหาย
        }
        catch (Exception ex)
        {
            // จับ exception และบันทึกข้อผิดพลาด
            Console.WriteLine($"Error occurred while creating customer: {ex.Message}");
            return new List<string> { "An error occurred while processing your request." };
        }
    }


}
