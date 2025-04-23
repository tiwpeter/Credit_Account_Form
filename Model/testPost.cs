using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelTest.Controllers
{


    public class GeneralModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ให้ฐานข้อมูลสร้างค่า Id อัตโนมัติ
        public int general_id { get; set; }
        public string generalName { get; set; }


        //จะส่งข้อมูลจาก General โดยอ้างอิงไปที่ AddressModel ของ General
        [ForeignKey("AddressId")]

        // หากชื่อเป็น AddressId1 ก็ต้องระบุเป็น AddressId1
        public int AddressId { get; set; }  // กำหนด FK ที่ใช้ใน General
        public AddressModel Address { get; set; }
    }

    public class CustomerModel
    {
        [Key]
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public int GeneralId { get; set; }

        [ForeignKey("GeneralId")]
        public GeneralModel General { get; set; }


        public int shipping_id { get; set; }

        [ForeignKey("shipping_id")]
        public ShippingModel Shipping { get; set; }

        public int BusinessTypeId { get; set; }  // เพิ่มคอลัมน์ BusinessTypeId

        [ForeignKey("BusinessTypeId")]
        public BusinessTypeModel BusinessType { get; set; }

        // การเพิ่ม CreditInfo ที่เกี่ยวข้องกับ Customer
        public int? CreditInfoId { get; set; }  // Make it nullable if not all customers have credit info
        [ForeignKey("CreditInfoId")]
        public CreditInfoModel CreditInfo { get; set; } // ชี้ไปที่ CreditInfo


        public int? CustSignId { get; set; }  // Make it nullable if not all customers have credit info
        [ForeignKey("CustSignId")]
        public CustomerSignModel CustomerSigns { get; set; }


    }



    public class GeneralDto
    {
        public int GeneralId { get; set; }
        public string GeneralName { get; set; }
        public int AddressId { get; set; }
        public AddressDto Address { get; set; }
    }

    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int GeneralId { get; set; }
        public GeneralDto General { get; set; }
        public ShippingDto Shipping { get; set; }
        public int BusinessTypeId { get; set; }  // เพิ่ม BusinessTypeId
        public string BusinessTypeName { get; set; }  // เพิ่ม BusinessTypeName

        // เพิ่ม CreditInfo
        public CreditInfoDto CreditInfo { get; set; }  // ชี้ไปที่ CreditInfoDto

        public CustomerSignDto CustomerSign { get; set; }



    }




    public class AddressDto
    {
        public int AddressId { get; set; }
        public string CustomerName { get; set; }


        public CountryDto Country { get; set; }


        public ProvinceDto Province { get; set; }


    }

    public class CountryDto
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
    }
    public class CreateCustomerRequest
    {
        // Customer
        public string CustomerName { get; set; }

        // General
        public string GeneralName { get; set; }

        // Address
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }

        // Shipping
        public string SubDistrict { get; set; }
        public int ShippingProvinceId { get; set; }

        // BusinessType
        public int BusinessTypeId { get; set; }

        // CreditInfo
        public decimal EstimatedPurchase { get; set; }
        public int TimeRequired { get; set; }
        public decimal CreditLimit { get; set; }

        // CustomerSign
        public string CustSignFirstName { get; set; }
    }

    // objects  adjust dto


}