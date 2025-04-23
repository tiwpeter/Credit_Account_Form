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


    public async Task<List<CustomerDto>> GetCustomersAsync()
    {
        var customers = await _context.Set<CustomerModel>()
            .Include(c => c.General)
                .ThenInclude(g => g.Address)
                    .ThenInclude(a => a.Country)
                        .ThenInclude(c => c.Provinces)
            .Include(c => c.Shipping)  // รวมข้อมูล Shipping
                .ThenInclude(s => s.Province)  // รวมข้อมูล Province
            .Include(c => c.BusinessType)  // รวมข้อมูล BusinessType
            .Include(c => c.CreditInfo)  // รวมข้อมูล CreditInfo
                    .Include(c => c.CustomerSigns) // ⬅ เพิ่มการโหลด CustomerSigns
            .Select(c => new CustomerDto
            {
                CustomerId = c.CustomerId,
                CustomerName = c.CustomerName,
                GeneralId = c.GeneralId,
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
                    ProvinceName = c.Shipping.Province.ProvinceName,
                    CountryName = c.Shipping.Province.Country.CountryName

                },
                BusinessTypeId = c.BusinessTypeId,  // ดึง BusinessTypeId
                BusinessTypeName = c.BusinessType.busiTypeName,  // ดึง BusinessTypeName
                CreditInfo = new CreditInfoDto  // รวมข้อมูล CreditInfo
                {
                    EstimatedPurchase = c.CreditInfo.EstimatedPurchase,
                    TimeRequired = c.CreditInfo.TimeRequired,
                    CreditLimit = c.CreditInfo.CreditLimit
                },
                CustomerSign = new CustomerSignDto
                {
                    CustSignId = c.Shipping.shipping_id,
                    CustSignFirstName = c.Shipping.subDistrict
                }
            })
            .ToListAsync();

        return customers;
    }

    public async Task<CustomerDto?> GetCustomerByIdAsync(int id)
    {
        var customer = await _context.Set<CustomerModel>()
            .Include(c => c.General)
                .ThenInclude(g => g.Address)
                    .ThenInclude(a => a.Country)
            .Where(c => c.CustomerId == id)
            .Select(c => new CustomerDto
            {
                CustomerId = c.CustomerId,
                CustomerName = c.CustomerName,
                General = new GeneralDto
                {
                    GeneralName = c.General.generalName,
                    Address = new AddressDto
                    {
                        Country = new CountryDto
                        {
                            CountryId = c.General.Address.Country.CountryId
                        }
                    }
                }
            })
            .FirstOrDefaultAsync();  // ใช้ FirstOrDefault แทน First เพื่อหลีกเลี่ยงข้อผิดพลาด Sequence contains no elements

        return customer;
    }


}
