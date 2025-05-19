using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelTest.Controllers
{

    //ตัวอย่าง Customer Model แบบมี FK ทั้ง 3
    public class RegisformModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int GeneralId { get; set; }

        [ForeignKey("GeneralId")]
        public GeneralModel General { get; set; }

        public int ShippingId { get; set; }
        [ForeignKey("ShippingId")]
        public ShippingModel Shipping { get; set; }
    }

    public class RegisformDto
    {
        public int Id { get; set; }
        public GeneralModel General { get; set; }
        public ShippingDto Shipping { get; set; }
    }
    public class GeneralDto
    {
        public int GeneralId { get; set; }
        public string GeneralName { get; set; }
        public AddressDto Address { get; set; }
    }
    public class AddressDto
    {
        public int AddressId { get; set; }
        public string Street { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int? ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public int? ThaiProvinceId { get; set; }
        public string ThaiProvinceName { get; set; }
    }

    public class ShippingDto
    {
        public int ShippingId { get; set; }
        public string ShippingSubDistrict { get; set; }
        public int ShippingProvinceId { get; set; }
        public string ShippingProvinceName { get; set; }
    }


    public class ShippingModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ให้ฐานข้อมูลสร้างค่า Id อัตโนมัติ
        public int shipping_id { get; set; }
        public string subDistrict { get; set; }

        // FK ไป Province
        [ForeignKey("ProvinceId")]
        public int ProvinceId { get; set; }
        public ProvinceModel Province { get; set; }
    }

}
/*
RegisformModel
 ├─ GeneralModel
 │    └─ AddressModel
 │         ├─ Country
 │         ├─ Province
 │         └─ ThaiProvince
 └─ ShippingModel
      └─ Province
markdawn
*/