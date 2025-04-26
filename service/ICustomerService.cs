using API.Data;
using ModelTest.Controllers;

public class CustomerService
{
    private readonly ApplicationDbContext _context;

    public CustomerService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateCustomerAsync(CreateCustomerRequest request)
    {
        // สร้าง customer ด้วยข้อมูลที่ได้รับจาก request
        var customer = new CustomerModel
        {
            CustomerName = request.CustomerName,
            General = new GeneralModel
            {
                generalName = request.General.GeneralName,
                Address = new AddressModel
                {
                    CountryId = request.General.Address.Country.CountryId, // ใช้ CountryId ที่ถูกต้อง
                    ProvinceId = request.General.Address.Province.ProvinceId // ใช้ ProvinceId ที่ถูกต้อง
                }
            },
            Shipping = new ShippingModel
            {
                ProvinceId = request.ShippingProvinceId
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

        return customer.CustomerId;
    }
}
