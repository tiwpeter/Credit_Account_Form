using FastReport;
using FastReport.Web.Services;
using ModelTest.Controllers;

public class ReportService
{
    public void RegisterCustomerReportData(Report report, GetCustomersDTO customer)
    {
        if (report == null || customer == null)
            throw new ArgumentNullException();

        var customerList = new List<GetCustomersDTO> { customer };
        report.RegisterData(customerList, "CustomerData");
        report.GetDataSource("CustomerData").Enabled = true;

        if (customer.General != null)
        {
            var generalList = new List<GeneralDto> { customer.General };
            report.RegisterData(generalList, "GeneralData");
            report.GetDataSource("GeneralData").Enabled = true;

            if (customer.General.Address != null)
            {
                var addressList = new List<AddressDto> { customer.General.Address };
                report.RegisterData(addressList, "AddressData");
                report.GetDataSource("AddressData").Enabled = true;
            }
        }

        if (customer.CustGroupCountry != null)
        {
            var countryList = new List<CustGroupCountryModel> { customer.CustGroupCountry };
            report.RegisterData(countryList, "CustGroupCountry");
            report.GetDataSource("CustGroupCountry").Enabled = true;
        }
    }
}
