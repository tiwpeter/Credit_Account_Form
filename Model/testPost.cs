using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelTest.Controllers
{



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







    public class CountryDto
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
    }


    // objects  adjust dto


}