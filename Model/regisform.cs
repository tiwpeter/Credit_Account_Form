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

        public int AddressId { get; set; }
        [ForeignKey("AddressId")]
        public AddressModel Address { get; set; }

        public int ShippingId { get; set; }
        [ForeignKey("ShippingId")]
        public ShippingModel Shipping { get; set; }
    }

    public class RegisformDto
    {
        public int Id { get; set; }

        // Address info
        public int AddressId { get; set; }
        public string Street { get; set; }

        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public int? ProvinceId { get; set; }
        public string ProvinceName { get; set; }

        public int? ThaiProvinceId { get; set; }
        public string ThaiProvinceName { get; set; }

        // 👉 Shipping fields
        public int shipping_id { get; set; }
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