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
                    var countryList = new List<CountryDto> { customer.General.Address.Country };
                    report.RegisterData(countryList, "AddressCountry");
                    report.GetDataSource("AddressCountry").Enabled = true;
                }

                // Register Address.Province
                if (customer.General.Address.Province != null)
                {
                    var provinceList = new List<ProvinceDto> { customer.General.Address.Province };
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
    }
}