using FastReport;
using FastReport.Web.Services;
using ModelTest.Controllers;

public class ReportService
{
    public void RegisterCustomerReportData(Report report, GetCustomersDTO customer)
    {
        if (report == null || customer == null)
            throw new ArgumentNullException();

        // Register CustomerData
        var customerList = new List<GetCustomersDTO> { customer };
        report.RegisterData(customerList, "CustomerData");
        report.GetDataSource("CustomerData").Enabled = true;

        // Register GeneralData
        if (customer.General != null)
        {
            var generalList = new List<GeneralDto> { customer.General };
            report.RegisterData(generalList, "GeneralData");
            report.GetDataSource("GeneralData").Enabled = true;

            // Register AddressData
            if (customer.General.Address != null)
            {
                var addressList = new List<AddressDto> { customer.General.Address };
                report.RegisterData(addressList, "AddressData");
                report.GetDataSource("AddressData").Enabled = true;

                // Register Address.Country
                if (customer.General.Address.Country != null)
                {
                    var countryList = new List<CountryModel> { customer.General.Address.Country };
                    report.RegisterData(countryList, "AddressCountry");
                    report.GetDataSource("AddressCountry").Enabled = true;
                }

                // Register Address.Province
                if (customer.General.Address.Province != null)
                {
                    var provinceList = new List<ProvinceModel> { customer.General.Address.Province };
                    report.RegisterData(provinceList, "AddressProvince");
                    report.GetDataSource("AddressProvince").Enabled = true;
                }
            }
        }

        // Register ShippingData
        if (customer.Shipping != null)
        {
            var shippingList = new List<ShippingDto> { customer.Shipping };
            report.RegisterData(shippingList, "ShippingData");
            report.GetDataSource("ShippingData").Enabled = true;

            if (customer.Shipping.Country != null)
            {
                var shipCountryList = new List<CountryDto> { customer.Shipping.Country };
                report.RegisterData(shipCountryList, "ShippingCountry");
                report.GetDataSource("ShippingCountry").Enabled = true;
            }

            if (customer.Shipping.Province != null)
            {
                var shipProvinceList = new List<ProvinceDto> { customer.Shipping.Province };
                report.RegisterData(shipProvinceList, "ShippingProvince");
                report.GetDataSource("ShippingProvince").Enabled = true;
            }
        }

        // Register CustGroupCountry if exists
        if (customer.CustGroupCountry != null)
        {
            var custGroupList = new List<CustGroupCountryModel> { customer.CustGroupCountry };
            report.RegisterData(custGroupList, "CustGroupCountry");
            report.GetDataSource("CustGroupCountry").Enabled = true;
        }

        // TODO: Register additional data like BusinessType, CreditInfo, CustomerSigns, ShopType if needed
        // BusinessType
        if (customer.BusinessType != null)
        {
            report.RegisterData(new List<BusinessTypeDTO> { customer.BusinessType }, "BusinessTypeData");
            report.GetDataSource("BusinessTypeData").Enabled = true;
        }

        // CreditInfo
        if (customer.CreditInfo != null)
        {
            report.RegisterData(new List<CreditInfoDto> { customer.CreditInfo }, "CreditInfoData");
            report.GetDataSource("CreditInfoData").Enabled = true;
        }

        // CustomerSign
        if (customer.CustomerSign != null)
        {
            report.RegisterData(new List<CustomerSignDto> { customer.CustomerSign }, "CustomerSignData");
            report.GetDataSource("CustomerSignData").Enabled = true;
        }

        // ShopType
        if (customer.ShopType != null)
        {
            report.RegisterData(new List<ShopTypeModel> { customer.ShopType }, "ShopTypeData");
            report.GetDataSource("ShopTypeData").Enabled = true;
        }

        // IndustryType
        if (customer.IndustryType != null)
        {
            report.RegisterData(new List<IndustryTypeModel> { customer.IndustryType }, "IndustryTypeData");
            report.GetDataSource("IndustryTypeData").Enabled = true;
        }
        // Company
        if (customer.Company != null)
        {
            report.RegisterData(new List<CompanyModel> { customer.Company }, "CompanyData");
            report.GetDataSource("CompanyData").Enabled = true;
        }

        // SaleOrg
        if (customer.SaleOrg != null)
        {
            report.RegisterData(new List<SaleOrgModel> { customer.SaleOrg }, "SaleOrgData");
            report.GetDataSource("SaleOrgData").Enabled = true;
        }

        // accountGroup
        if (customer.accountGroup != null)
        {
            report.RegisterData(new List<accountGroupModel> { customer.accountGroup }, "AccountGroupData");
            report.GetDataSource("AccountGroupData").Enabled = true;
        }

        // DocCredit
        if (customer.DocCredit != null)
        {
            report.RegisterData(new List<DocCreditModel> { customer.DocCredit }, "DocCreditData");
            report.GetDataSource("DocCreditData").Enabled = true;
        }

        // AccountCode
        if (customer.AccountCode != null)
        {
            report.RegisterData(new List<AccountCodeModel> { customer.AccountCode }, "AccountCodeData");
            report.GetDataSource("AccountCodeData").Enabled = true;
        }

        // SortKey
        if (customer.SortKey != null)
        {
            report.RegisterData(new List<SortKeyModel> { customer.SortKey }, "SortKeyData");
            report.GetDataSource("SortKeyData").Enabled = true;
        }

        // PaymentMethod
        if (customer.PaymentMethod != null)
        {
            report.RegisterData(new List<PaymentMethodModel> { customer.PaymentMethod }, "PaymentMethodData");
            report.GetDataSource("PaymentMethodData").Enabled = true;
        }

        // TermOfPayment
        if (customer.TermOfPayment != null)
        {
            report.RegisterData(new List<TermOfPaymentModel> { customer.TermOfPayment }, "TermOfPaymentData");
            report.GetDataSource("TermOfPaymentData").Enabled = true;
        }

        // SaleDistrict
        if (customer.SaleDistrict != null)
        {
            report.RegisterData(new List<SaleDistrictModel> { customer.SaleDistrict }, "SaleDistrictData");
            report.GetDataSource("SaleDistrictData").Enabled = true;
        }

        // SaleGroup
        if (customer.SaleGroup != null)
        {
            report.RegisterData(new List<SaleGroupModel> { customer.SaleGroup }, "SaleGroupData");
            report.GetDataSource("SaleGroupData").Enabled = true;
        }

        // CustGroupType
        if (customer.CustGroupType != null)
        {
            report.RegisterData(new List<CustGroupTypeModel> { customer.CustGroupType }, "CustGroupTypeData");
            report.GetDataSource("CustGroupTypeData").Enabled = true;
        }

        // Currency
        if (customer.Currency != null)
        {
            report.RegisterData(new List<CurrencyModel> { customer.Currency }, "CurrencyData");
            report.GetDataSource("CurrencyData").Enabled = true;
        }

        // ExchRateType
        if (customer.ExchRateType != null)
        {
            report.RegisterData(new List<ExchRateTypeModel> { customer.ExchRateType }, "ExchRateTypeData");
            report.GetDataSource("ExchRateTypeData").Enabled = true;
        }

        // CustPricProc
        if (customer.CustPricProc != null)
        {
            report.RegisterData(new List<CustPricProcModel> { customer.CustPricProc }, "CustPricProcData");
            report.GetDataSource("CustPricProcData").Enabled = true;
        }

        // PriceList
        if (customer.PriceList != null)
        {
            report.RegisterData(new List<PriceListModel> { customer.PriceList }, "PriceListData");
            report.GetDataSource("PriceListData").Enabled = true;
        }

        // Incoterm
        if (customer.Incoterm != null)
        {
            report.RegisterData(new List<IncotermModel> { customer.Incoterm }, "IncotermData");
            report.GetDataSource("IncotermData").Enabled = true;
        }

        if (customer.CustGroupCountry != null)
        {
            var custGroupList = new List<CustGroupCountryModel> { customer.CustGroupCountry };
            report.RegisterData(custGroupList, "CustGroupCountry");
            report.GetDataSource("CustGroupCountry").Enabled = true;
        }

        // SaleManager
        if (customer.SaleManager != null)
        {
            report.RegisterData(new List<SaleManagerModel> { customer.SaleManager }, "SaleManagerData");
            report.GetDataSource("SaleManagerData").Enabled = true;
        }

    }
}