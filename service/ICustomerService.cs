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

        //ฟอร์ม(Angular)->ส่ง JSON->CreateCustomerRequest(รับใน API)->CustomerModel(Entity)->Database
        var customer = new CustomerModel
        {
            CustomerName = request.CustomerName,
            // 
            General = new GeneralModel
            {
                Address = new AddressModel
                {
                    CountryId = request.CountryId,
                    ProvinceId = request.ProvinceId
                }
            },
            Shipping = new ShippingModel
            {
                ProvinceId = request.ShippingProvinceId
            },
            BusinessTypeId = request.BusinessTypeId,
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
