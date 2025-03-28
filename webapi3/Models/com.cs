// General Model
namespace RegisteersTable.Models;

public class GeneralModel
{
    public string GeneralName1 { get; set; }
    public string GeneralTel { get; set; }
    public string GeneralFax { get; set; }
    public string GeneralEmail { get; set; }
    public string GeneralLine { get; set; }
    public string GeneralTax { get; set; }
    public string GeneralBranch { get; set; }
}


// Address Model
public class AddressModel
{
    public string AddrType { get; set; }
    public string AddrLine1 { get; set; }
    public string AddrLine2 { get; set; }
    public string SubDistrict { get; set; }
    public string District { get; set; }
    public string Province { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
}

public class ShippingModel
{
    public string DeliveryName { get; set; }
    public string address1 { get; set; }
    public string district { get; set; }
    public string SubDistrict { get; set; }
    public string province { get; set; }
    public string postalCode { get; set; }
}


public class ShopTypeModel
{
    public string shopCode { get; set; }
    public string shopName { get; set; }
    public string shopDes { get; set; }
    public string accGroupName { get; set; }
}

public class CompanyModel
{
    public string companyCode { get; set; }
    public string companyName { get; set; }
    public string companyAddr { get; set; }
}

public class IndustryTypeModel
{
    public string InduTypeCode { get; set; }
    public string InduTypeName { get; set; }
    public string InduTypeDes { get; set; }
}


// Combined RegisterModel for API request
public class RegisterModel
{
    public GeneralModel General { get; set; }
    public AddressModel Address { get; set; }
    public ShippingModel Shipping { get; set; }

    public ShopTypeModel ShopType { get; set; }

    public IndustryTypeModel IndustryType { get; set; }
    public CompanyModel Company { get; set; }


}