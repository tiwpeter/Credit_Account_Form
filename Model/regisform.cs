using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelTest.Controllers
{

    //ตัวอย่าง Customer Model แบบมี FK ทั้ง 3
    public class RegisformModel
    {
        [Key] // 👈 เพิ่ม annotation นี้ด้วย
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public int AddressId { get; set; }
        public AddressModel Address { get; set; }
        public int shipping_id { get; set; }
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
        public int ShippingId { get; set; }
        public string ShippingSubDistrict { get; set; }
        public int ShippingProvinceId { get; set; }
        public string ShippingProvinceName { get; set; }
    }


}