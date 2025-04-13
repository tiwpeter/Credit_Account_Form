using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelTest.Controllers
{
    // จังหวัด
    public class ProvinceModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }



        // เก็บชื่อประเทศ
        public int CountryId { get; set; }
        public CountryModel Country { get; set; } // ความสัมพันธ์กับ Province

        // ✅ เพิ่ม geography
        public int GeographyId { get; set; }
        public GeographyModel Geography { get; set; }

    }
    // ประเทศ
    public class CountryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ให้ฐานข้อมูลสร้างค่า Id อัตโนมัติ
        public int CountryId { get; set; }
        public string Name { get; set; }
        public ICollection<AddressModel> Address { get; set; }
        public ICollection<ShippingModel> Shipping { get; set; }

    }

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
}
