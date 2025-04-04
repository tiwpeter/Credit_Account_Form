
namespace ModelTest.Controllers
{

    public class IndustryTypeModel
    {
        public int id { get; set; }

        public string InduTypeCode { get; set; }
        public string InduTypeName { get; set; }
        public string InduTypeDes { get; set; }
    }


    public class CompanyModel
    {
        public int company_id { get; set; }

        public string companyCode { get; set; }
        public string companyName { get; set; }
        public string companyAddr { get; set; }
    }




    public class ShopTypeModel
    {
        public int id { get; set; }

        public string shopCode { get; set; }
        public string shopName { get; set; }
        public string shopDes { get; set; }
        public string accGroupName { get; set; }
    }



    public class AddressesModel
    {
        // Address-related fields
        public string addrType { get; set; }
        public string addrLine1 { get; set; }
        public string addrLine2 { get; set; }
        public string subDistrict { get; set; }
        public string district { get; set; }
        public string province { get; set; }
        public string postalCode { get; set; }
        public string country { get; set; }
        public string createdDate { get; set; }

        // Country and Province information moved here
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }
    }

    public class GeneralsModel
    {
        public string GeneralName { get; set; }
        public string GeneralName1 { get; set; }
        public string GeneralTel { get; set; }
        public string GeneralFax { get; set; }
        public string GeneralEmail { get; set; }
        public string GeneralLine { get; set; }
        public string GeneralTax { get; set; }
        public string GeneralBranch { get; set; }
    }


    // ประเทศ
    public class Country
    {
        public int CountryId { get; set; }
        public string NameTh { get; set; }
        public string IsoAlpha2 { get; set; }
        public string IsoAlpha3 { get; set; }
        public string OfficialName { get; set; }
        public string Region { get; set; }
        public string SubRegion { get; set; }
        public string CapitalCity { get; set; }
    }




    //ภูมิภาค

    public class Province
    {
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }
        public string NameTh { get; set; }
        public string NameEn { get; set; }
        public string ProvinceCode { get; set; }
    }


    /*
    thai_geographies

    thai_provinces

    thai_amphures

    thai_tambons

    */



    public class ShippingModel
    {
        public int shipping_id { get; set; }

        public string addrType { get; set; }
        public string DeliveryName { get; set; }
        public string address1 { get; set; }
        public string district { get; set; }
        public string province { get; set; }
        public string postalCode { get; set; }

        public string shippingcountry { get; set; }
        public string mobile { get; set; }
        public string contact_name { get; set; }

        public string freight { get; set; }




    }


    public class Customer
    {
        public int CustomerId { get; set; } // เพิ่ม CustomerId

        //GeneralsModel เป็น ชนิดข้อมูล  คลาส ที่คุณสร้างขึ้นมาเพื่อเก็บข้อมูลต่าง ๆ
        //ข้อมูลของ Generals จะถูกเก็บใน GeneralsModel
        public GeneralsModel Generals { get; set; }
        public AddressesModel Addresses { get; set; }
        public ShippingModel Shipping { get; set; }

        public ShopTypeModel ShopType { get; set; }

        public IndustryTypeModel IndustryType { get; set; }
        ///

        public CompanyModel Company { get; set; }
        public SortKeyModel SortKey { get; set; }
        public CashGroupModel CashGroup { get; set; }
        public PaymentMethodModel PaymentMethod { get; set; }
        public TermOfPayModel TermOfPay { get; set; }

        public AccountCodeModel AccountCode { get; set; }

    }

    //รหัสทางบัญชี
    public class AccountCodeModel
    {
        public int id { get; set; }
        public string accCode { get; set; }
        public string accName { get; set; }
        public string accDes { get; set; }
    }






    public class SortKeyModel
    {
        public int id { get; set; }
        public string sortkeyCode { get; set; }
        public string sortkeyName { get; set; }
        public string sortkeyDes { get; set; }
    }

    public class CashGroupModel
    {
        public int id { get; set; }
        public string cashCode { get; set; }
        public string cashName { get; set; }
        public string cashDes { get; set; }

    }

    public class PaymentMethodModel
    {
        public int id { get; set; }
        public string payCode { get; set; }
        public string payName { get; set; }
        public string payDes { get; set; }

    }

    public class TermOfPayModel
    {
        public int id { get; set; }
        public string topCode { get; set; }
        public string topName { get; set; }
        public string topDes { get; set; }

    }







}