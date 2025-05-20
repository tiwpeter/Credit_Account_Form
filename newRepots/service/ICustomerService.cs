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
            // ดึง Country และ Province ที่มีอยู่จากฐานข้อมูล
            var country = await _context.Countries.FindAsync(request.General.Address.Country.CountryId);
            var province = await _context.Provinces.FindAsync(request.General.Address.Province.ProvinceId);

            // สร้าง customer ด้วยข้อมูลที่ได้รับจาก request
            var customer = new CustomerModel
            {
                CustomerName = request.CustomerName,

                General = new GeneralModel
                {
                    GeneralName = request.General.GeneralName,
                    GeneralName1 = request.General.GeneralName1,
                    GeneralTel = request.General.GeneralTel,
                    GeneralFax = request.General.GeneralFax,
                    GeneralEmail = request.General.GeneralEmail,
                    GeneralLine = request.General.GeneralLine,
                    GeneralTax = request.General.GeneralTax,
                    GeneralBranch = request.General.GeneralBranch,
                    Address = new AddressModel
                    {
                        addrLine1 = request.General.Address.addrLine1,
                        addrLine2 = request.General.Address.addrLine2,
                        subDistrict = request.General.Address.subDistrict,
                        district = request.General.Address.district,
                        postalCode = request.General.Address.postalCode,
                        createdDate = request.General.Address.createdDate,
                        Country = country,
                        Province = province
                    }
                },
                Shipping = new ShippingModel
                {
                    DeliveryName = request.Shipping.DeliveryName,
                    address1 = request.Shipping.address1,
                    address2 = request.Shipping.address2,
                    subDistrict = request.Shipping.subDistrict,
                    district = request.Shipping.district,
                    postalCode = request.Shipping.postalCode,
                    contact_name = request.Shipping.contact_name,
                    mobile = request.Shipping.mobile,
                    freight = request.Shipping.freight,
                    Country = country,
                    Province = province
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

                BusinessType = new BusinessTypeModel
                {
                    busiTypeCode = request.BusinessType.busiTypeCode,
                    busiTypeName = request.BusinessType.busiTypeName,
                    busiTypeDes = request.BusinessType.busiTypeDes,
                    RegistrationDate = DateTime.Now, // <<< ให้ระบบกำหนดวันเองตอนนี้เลย
                    RegisteredCapital = request.BusinessType.RegisteredCapital
                },

                accountGroup = new accountGroupModel
                {
                    id = request.accountGroup.id,
                    accGroupCode = request.accountGroup.accGroupCode,
                    accGroupName = request.accountGroup.accGroupName,
                    accGroupDes = request.accountGroup.accGroupDes
                },
                CreditInfo = new CreditInfoModel
                {
                    EstimatedPurchase = request.CreditInfo.EstimatedPurchase,
                    TimeRequired = request.CreditInfo.TimeRequired,
                    CreditLimit = request.CreditInfo.CreditLimit
                },
                DocCredit = new DocCreditModel
                {
                    CompanyCertificate = request.DocCredit.CompanyCertificate,
                    CopyOfPP_20 = request.DocCredit.CopyOfPP_20,
                    CopyOfCoRegis = request.DocCredit.CopyOfCoRegis,
                    CopyOfIDCard = request.DocCredit.CopyOfIDCard,
                    CompanyLocationMap = request.DocCredit.CompanyLocationMap,
                    OtherSpecify = request.DocCredit.OtherSpecify
                }, // 🔥 เพิ่ม CustomerSign ตรงนี้
                CustomerSign = new CustomerSignModel
                {
                    CustSignFirstName = request.CustomerSign.CustSignFirstName,
                    custsignTel = request.CustomerSign.custsignTel,
                    custsignEmail = request.CustomerSign.custsignEmail,
                    custsignLine = request.CustomerSign.custsignLine,
                },
                AccountCode = new AccountCodeModel
                {
                    AccountId = request.AccountCode.AccountId,
                    AccountCode = request.AccountCode.AccountCode,
                    AccountName = request.AccountCode.AccountName,
                    AccountType = request.AccountCode.AccountType,
                    Description = request.AccountCode.Description
                },
                SortKey = new SortKeyModel
                {
                    sortkeyCode = request.SortKey.sortkeyCode,
                    sortkeyName = request.SortKey.sortkeyName,
                    sortkeyDes = request.SortKey.sortkeyDes
                },
                PaymentMethod = new PaymentMethodModel
                {
                    PaymentMethodCode = request.PaymentMethod.PaymentMethodCode,
                    PaymentMethodName = request.PaymentMethod.PaymentMethodName,
                    Description = request.PaymentMethod.Description
                },
                TermOfPayment = new TermOfPaymentModel
                {
                    TermCode = request.TermOfPayment.TermCode,
                    TermName = request.TermOfPayment.TermName,
                    Description = request.TermOfPayment.Description
                },

                SaleDistrict = new SaleDistrictModel
                {
                    DistrictCode = request.SaleDistrict.DistrictCode,
                    DistrictName = request.SaleDistrict.DistrictName,
                    Description = request.SaleDistrict.Description
                },

                SaleGroup = new SaleGroupModel
                {
                    GroupCode = request.SaleGroup.GroupCode,
                    GroupName = request.SaleGroup.GroupName,
                    Description = request.SaleGroup.Description
                },
                CustGroupType = new CustGroupTypeModel
                {
                    GroupCode = request.CustGroupType.GroupCode,
                    GroupName = request.CustGroupType.GroupName,
                    Description = request.CustGroupType.Description
                },

                Currency = new CurrencyModel
                {
                    CurrencyCode = request.Currency.CurrencyCode,
                    CurrencyName = request.Currency.CurrencyName,
                    Symbol = request.Currency.Symbol
                },

                ExchRateType = new ExchRateTypeModel
                {
                    RateTypeCode = request.ExchRateType.RateTypeCode,
                    RateTypeName = request.ExchRateType.RateTypeName,
                    Description = request.ExchRateType.Description
                },
                CustPricProc = new CustPricProcModel
                {
                    PricProcCode = request.CustPricProc.PricProcCode,
                    PricProcName = request.CustPricProc.PricProcName,
                    Description = request.CustPricProc.Description
                },

                PriceList = new PriceListModel
                {
                    priceListCode = request.PriceList.priceListCode,
                    priceListName = request.PriceList.priceListName,
                    priceListDes = request.PriceList.priceListDes
                },

                Incoterm = new IncotermModel
                {
                    incotermCode = request.Incoterm.incotermCode,
                    incotermName = request.Incoterm.incotermName,
                    incotermDes = request.Incoterm.incotermDes
                },
                SaleManager = new SaleManagerModel
                {
                    SaleGroupCode = request.SaleManager.SaleGroupCode,
                    SaleGroupName = request.SaleManager.SaleGroupName,
                    SaleGroupDes = request.SaleManager.SaleGroupDes
                },

                CustGroupCountry = new CustGroupCountryModel
                {
                    CountryCode = request.CustGroupCountry.CountryCode,
                    CountryName = request.CustGroupCountry.CountryName,
                    CountryDes = request.CustGroupCountry.CountryDes
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