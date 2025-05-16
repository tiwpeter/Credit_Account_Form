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

            .Include(c => c.ShopType)

            .Select(c => new GetCustomersDTO
            {
                CustomerId = c.CustomerId,
                General = new GeneralDto
                {
                    GeneralName = c.General.GeneralName,
                    GeneralName1 = c.General.GeneralName1,
                    GeneralTel = c.General.GeneralTel,
                    GeneralFax = c.General.GeneralFax,
                    GeneralEmail = c.General.GeneralEmail,
                    GeneralLine = c.General.GeneralLine,
                    GeneralTax = c.General.GeneralTax,
                    GeneralBranch = c.General.GeneralBranch,
                    AddressId = c.General.AddressId,
                    Address = new AddressDto
                    {
                        addrLine1 = c.General.Address.addrLine1,
                        addrLine2 = c.General.Address.addrLine2,
                        subDistrict = c.General.Address.subDistrict,
                        district = c.General.Address.district,
                        postalCode = c.General.Address.postalCode,
                        createdDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
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
                    DeliveryName = c.Shipping.DeliveryName,
                    address1 = c.Shipping.address1,
                    address2 = c.Shipping.address2,
                    subDistrict = c.Shipping.subDistrict,
                    district = c.Shipping.district,
                    postalCode = c.Shipping.postalCode,
                    contact_name = c.Shipping.contact_name,
                    mobile = c.Shipping.mobile,
                    freight = c.Shipping.freight,
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
                },

                // ใส่ IndustryType
                IndustryType = new IndustryTypeModel
                {
                    id = c.IndustryType.id,
                    InduTypeCode = c.IndustryType.InduTypeCode,
                    InduTypeName = c.IndustryType.InduTypeName,
                    InduTypeDes = c.IndustryType.InduTypeDes
                },
                Company = new CompanyModel
                {
                    company_id = c.Company.company_id,
                    companyCode = c.Company.companyCode,
                    companyName = c.Company.companyName,
                    companyAddr = c.Company.companyAddr
                },
                SaleOrg = new SaleOrgModel
                {
                    id = c.SaleOrg.id,
                    saleOrgCode = c.SaleOrg.saleOrgCode,
                    saleOrgName = c.SaleOrg.saleOrgName,
                    saleOrgDes = c.SaleOrg.saleOrgDes
                },

                accountGroup = new accountGroupModel
                {
                    id = c.accountGroup.id,
                    accGroupCode = c.accountGroup.accGroupCode,
                    accGroupName = c.accountGroup.accGroupName,
                    accGroupDes = c.accountGroup.accGroupDes
                },
                DocCredit = new DocCreditModel
                {
                    CompanyCertificate = c.DocCredit.CompanyCertificate,
                    CopyOfPP_20 = c.DocCredit.CopyOfPP_20,
                    CopyOfCoRegis = c.DocCredit.CopyOfCoRegis,
                    CopyOfIDCard = c.DocCredit.CopyOfIDCard,
                    CompanyLocationMap = c.DocCredit.CompanyLocationMap,
                    OtherSpecify = c.DocCredit.OtherSpecify
                },/*, // 🔥 เพิ่ม CustomerSign ตรงนี้
                CustomerSigns = new CustomerSignModel
                {
                    CustSignFirstName = c.CustomerSign.CustSignFirstName,
                    custsignTel = c.CustomerSign.custsignTel,
                    custsignEmail = c.CustomerSign.custsignEmail,
                    custsignLine = c.CustomerSign.custsignLine,
                },*/
                AccountCode = new AccountCodeModel
                {
                    AccountId = c.AccountCode.AccountId,
                    AccountCode = c.AccountCode.AccountCode,
                    AccountName = c.AccountCode.AccountName,
                    AccountType = c.AccountCode.AccountType,
                    Description = c.AccountCode.Description
                },
                SortKey = new SortKeyModel
                {
                    sortkeyCode = c.SortKey.sortkeyCode,
                    sortkeyName = c.SortKey.sortkeyName,
                    sortkeyDes = c.SortKey.sortkeyDes
                },
                PaymentMethod = new PaymentMethodModel
                {
                    PaymentMethodCode = c.PaymentMethod.PaymentMethodCode,
                    PaymentMethodName = c.PaymentMethod.PaymentMethodName,
                    Description = c.PaymentMethod.Description
                },
                TermOfPayment = new TermOfPaymentModel
                {
                    TermCode = c.TermOfPayment.TermCode,
                    TermName = c.TermOfPayment.TermName,
                    Description = c.TermOfPayment.Description
                },

                SaleDistrict = new SaleDistrictModel
                {
                    DistrictCode = c.SaleDistrict.DistrictCode,
                    DistrictName = c.SaleDistrict.DistrictName,
                    Description = c.SaleDistrict.Description
                },

                SaleGroup = new SaleGroupModel
                {
                    GroupCode = c.SaleGroup.GroupCode,
                    GroupName = c.SaleGroup.GroupName,
                    Description = c.SaleGroup.Description
                },
                CustGroupType = new CustGroupTypeModel
                {
                    GroupCode = c.CustGroupType.GroupCode,
                    GroupName = c.CustGroupType.GroupName,
                    Description = c.CustGroupType.Description
                },

                Currency = new CurrencyModel
                {
                    CurrencyCode = c.Currency.CurrencyCode,
                    CurrencyName = c.Currency.CurrencyName,
                    Symbol = c.Currency.Symbol
                },

                ExchRateType = new ExchRateTypeModel
                {
                    RateTypeCode = c.ExchRateType.RateTypeCode,
                    RateTypeName = c.ExchRateType.RateTypeName,
                    Description = c.ExchRateType.Description
                },
                CustPricProc = new CustPricProcModel
                {
                    PricProcCode = c.CustPricProc.PricProcCode,
                    PricProcName = c.CustPricProc.PricProcName,
                    Description = c.CustPricProc.Description
                },

                PriceList = new PriceListModel
                {
                    priceListCode = c.PriceList.priceListCode,
                    priceListName = c.PriceList.priceListName,
                    priceListDes = c.PriceList.priceListDes
                },

                Incoterm = new IncotermModel
                {
                    incotermCode = c.Incoterm.incotermCode,
                    incotermName = c.Incoterm.incotermName,
                    incotermDes = c.Incoterm.incotermDes
                },
                SaleManager = new SaleManagerModel
                {
                    SaleGroupCode = c.SaleManager.SaleGroupCode,
                    SaleGroupName = c.SaleManager.SaleGroupName,
                    SaleGroupDes = c.SaleManager.SaleGroupDes
                },

                CustGroupCountry = new CustGroupCountryModel
                {
                    CountryCode = c.CustGroupCountry.CountryCode,
                    CountryName = c.CustGroupCountry.CountryName,
                    CountryDes = c.CustGroupCountry.CountryDes
                }

            }).ToListAsync(); // ⬅ เพิ่มตรงนี้
        return customers;
    }

    public async Task<CountryModel> GetCountryByIdAsync(int countryId)
    {
        return await _context.Countries
                             .FirstOrDefaultAsync(c => c.CountryId == countryId);
    }
    public async Task<GetCustomersDTO> GetCustomerByIdAsync(int customerId)
    {
        var customer = await _context.Set<CustomerModel>()
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
            .Include(c => c.ShopType)
            .Where(c => c.CustomerId == customerId) // <<== กรองตรงนี้ตาม id
            .Select(c => new GetCustomersDTO
            {
                CustomerId = c.CustomerId,
                General = new GeneralDto
                {
                    GeneralName = c.General.GeneralName,
                    GeneralName1 = c.General.GeneralName1,
                    GeneralTel = c.General.GeneralTel,
                    GeneralFax = c.General.GeneralFax,
                    GeneralEmail = c.General.GeneralEmail,
                    GeneralLine = c.General.GeneralLine,
                    GeneralTax = c.General.GeneralTax,
                    GeneralBranch = c.General.GeneralBranch,
                    AddressId = c.General.AddressId,
                    Address = new AddressDto
                    {
                        addrLine1 = c.General.Address.addrLine1,
                        addrLine2 = c.General.Address.addrLine2,
                        subDistrict = c.General.Address.subDistrict,
                        district = c.General.Address.district,
                        postalCode = c.General.Address.postalCode,
                        createdDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
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
                // Map แยก property ด้วย
                GeneralName1 = c.General.GeneralName1,

            }).FirstOrDefaultAsync(); // <<< เอาแค่ตัวเดียว

        return customer;
    }

    public async Task<List<CountryModel>> GetAllCountriesAsync()
    {
        return await _context.Countries.ToListAsync();
    }

}
