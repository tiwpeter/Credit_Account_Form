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
                    CountryId = request.ShippingDto.Country.CountryId, // แก้ตรงนี้
                    ProvinceId = request.ShippingDto.Province.ProvinceId, // ← ดึงจาก DTO เลย
                    subDistrict = request.ShippingDto.SubDistrict
                },
                // 🔥 เพิ่มตรงนี้
                ShopType = new ShopTypeModel
                {
                    id = request.ShopType.id,
                    shopCode = request.ShopType.shopCode,
                    shopName = request.ShopType.shopName,
                    shopDes = request.ShopType.shopDes,
                    accGroupName = request.ShopType.accGroupName
                },
                IndustryType = new IndustryTypeModel
                {
                    id = request.IndustryType.id,
                    InduTypeCode = request.IndustryType.InduTypeCode,
                    InduTypeName = request.IndustryType.InduTypeName,
                    InduTypeDes = request.IndustryType.InduTypeDes
                },
                Company = new CompanyModel
                {
                    company_id = request.Company.company_id,
                    companyCode = request.Company.companyCode,
                    companyName = request.Company.companyName,
                    companyAddr = request.Company.companyAddr
                },
                SaleOrg = new SaleOrgModel
                {
                    id = request.SaleOrg.id,
                    saleOrgCode = request.SaleOrg.saleOrgCode,
                    saleOrgName = request.SaleOrg.saleOrgName,
                    saleOrgDes = request.SaleOrg.saleOrgDes
                },
                AccountCode = new AccountCodeModel
                {
                    AccountId = request.AccountCode.AccountId,
                    AccountCode = request.AccountCode.AccountCode,
                    AccountName = request.AccountCode.AccountName,
                    AccountType = request.AccountCode.AccountType,
                    Description = request.AccountCode.Description
                },

                BusinessType = new BusinessTypeModel
                {
                    busiTypeID = request.BusinessType.busiTypeID,
                    busiTypeCode = request.BusinessType.busiTypeCode,
                    busiTypeName = request.BusinessType.busiTypeName,
                    busiTypeDes = request.BusinessType.busiTypeDes,
                    RegistrationDate = request.BusinessType.RegistrationDate,
                    RegisteredCapital = request.BusinessType.RegisteredCapital
                },
                // busi
                accountGroup = new accountGroupModel
                {
                    id = request.accountGroup.id,
                    accGroupCode = request.accountGroup.accGroupCode,
                    accGroupName = request.accountGroup.accGroupName,
                    accGroupDes = request.accountGroup.accGroupDes
                }
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
