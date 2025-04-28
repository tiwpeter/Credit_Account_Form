using API.Data;
using Microsoft.EntityFrameworkCore;
using ModelTest.Controllers;

public class GetCustomerService
{
    private readonly ApplicationDbContext _context;

    public GetCustomerService(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<List<GetCustomersDTO>> GetCustomersAsync()
    {
        var customers = await _context.Set<CustomerModel>()
            .Include(c => c.General)
                .ThenInclude(g => g.Address)
                    .ThenInclude(a => a.Country)
            .Include(c => c.General.Address.Province)
            .Include(c => c.Shipping)
                .ThenInclude(s => s.Province)
                    .ThenInclude(p => p.Country)
            .Include(c => c.BusinessType)
            .Include(c => c.CreditInfo)
            .Include(c => c.CustomerSigns)
            .Select(c => new GetCustomersDTO
            {
                CustomerId = c.CustomerId,
                General = new GeneralDto
                {
                    GeneralName = c.General.generalName,
                    Address = new AddressDto
                    {
                        Country = new CountryDto
                        {
                            CountryId = c.General.Address.Country.CountryId,
                            CountryName = c.General.Address.Country.CountryName
                        },
                        Province = new ProvinceDto
                        {
                            ProvinceId = c.General.Address.Province.ProvinceId,
                            ProvinceName = c.General.Address.Province.ProvinceName
                        }
                    }
                },
                Shipping = new ShippingDto
                {
                    ShippingId = c.Shipping.shipping_id,
                    SubDistrict = c.Shipping.subDistrict,
                    Country = new CountryDto
                    {
                        CountryId = c.General.Address.Country.CountryId,
                        CountryName = c.General.Address.Country.CountryName
                    },
                    Province = new ProvinceDto
                    {
                        ProvinceId = c.Shipping.Province.ProvinceId,
                        ProvinceName = c.Shipping.Province.ProvinceName
                    },
                },
                BusinessType = new BusinessTypeDTO
                {
                    busiTypeName = c.BusinessType.busiTypeName,
                },
                CreditInfo = new CreditInfoDto
                {
                    EstimatedPurchase = c.CreditInfo.EstimatedPurchase,
                    TimeRequired = c.CreditInfo.TimeRequired,
                    CreditLimit = c.CreditInfo.CreditLimit
                },
                CustomerSign = new CustomerSignDto
                {
                    CustSignId = c.Shipping.shipping_id,
                    CustSignFirstName = c.Shipping.subDistrict
                },
                ShopType = new ShopTypeModel
                {
                    id = c.ShopType.id,
                    shopCode = c.ShopType.shopCode,
                    shopName = c.ShopType.shopName,
                    shopDes = c.ShopType.shopDes,
                    accGroupName = c.ShopType.accGroupName
                }

            }).ToListAsync(); // ⬅ เพิ่มตรงนี้
        return customers;
    }

    public async Task<CountryModel> GetCountryByIdAsync(int countryId)
    {
        return await _context.Countries
                             .FirstOrDefaultAsync(c => c.CountryId == countryId);
    }


}
