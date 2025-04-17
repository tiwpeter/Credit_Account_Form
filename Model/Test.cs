using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelTest.Controllers
{



    // ที่อยู่
    public class AddressModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ให้ฐานข้อมูลสร้างค่า Id อัตโนมัติ
        public int AddressId { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }

        // Foreign Key referencing ProvinceId
        public int ProvinceId { get; set; }
        public ProvinceModel Province { get; set; } // Relationship with Province

    }



    public class ShippingModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ให้ฐานข้อมูลสร้างค่า Id อัตโนมัติ
        public int shipping_id { get; set; }
        public string subDistrict { get; set; }

        // FK ไป Province
        public int ProvinceId { get; set; }
        public ProvinceModel Province { get; set; }
    }








    public class SaleOrgModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ให้ฐานข้อมูลสร้างค่า Id อัตโนมัติ
        public int id { get; set; }
        public string saleOrgCode { get; set; }
        public string saleOrgName { get; set; }
        public string saleOrgDes { get; set; }

    }
    public class accountGroupModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ให้ฐานข้อมูลสร้างค่า Id อัตโนมัติ
        public int id { get; set; }
        public string accGroupCode { get; set; }
        public string accGroupName { get; set; }
        public string accGroupDes { get; set; }
    }
    public class IndustryType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ให้ฐานข้อมูลสร้างค่า Id อัตโนมัติ
        public int id { get; set; }
        public string InduTypeCode { get; set; }
        public string InduTypeName { get; set; }
        public string InduTypeDes { get; set; }


    }

    public class CompanyModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ให้ฐานข้อมูลสร้างค่า Id อัตโนมัติ
        public int company_id { get; set; }
        public string companyCode { get; set; }
        public string companyName { get; set; }
        public string companyAddr { get; set; }


    }


    public class BusinessTypeDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int busiTypeID { get; set; }
        public string busiTypeCode { get; set; }
        public string busiTypeName { get; set; }
        public string busiTypeDes { get; set; }
        public DateTime? RegistrationDate { get; set; }

        // เปลี่ยนเป็น decimal? เพื่อใช้ HasValue
        public decimal? RegisteredCapital { get; set; }
    }

    public class CreditInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ให้ฐานข้อมูลสร้างค่า Id อัตโนมัติ
        public int creditInfo_id { get; set; }
        public string estimatedPurchase { get; set; }
        public string timeRequired { get; set; }
        public string creditLimit { get; set; }

    }

    public class DocumentCredit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ให้ฐานข้อมูลสร้างค่า Id อัตโนมัติ
        public int doccredit_id { get; set; }
        public string CompanyCertificate { get; set; }
        public string CopyOfPP_20 { get; set; }
        public string CopyOfCoRegis { get; set; }
        public string CopyOfIDCard { get; set; }
        public string CompanyLocationMap { get; set; }
        public string OtherSpecify { get; set; }

    }

    public class CustomerSigns
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ให้ฐานข้อมูลสร้างค่า Id อัตโนมัติ
        public int custsign_id { get; set; }
        public string custsignFirstName { get; set; }
        public string custsignLastName { get; set; }
        public string custsignTel { get; set; }
        public string custsignEmail { get; set; }
        public string CompanyLocationMap { get; set; }
        public string custsignLine { get; set; }

    }


}

