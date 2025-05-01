using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelTest.Controllers
{
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

        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public CountryModel Country { get; set; }

    }
    public class ShippingDto
    {
        public int ShippingId { get; set; }
        public string SubDistrict { get; set; }
        public ProvinceDto Province { get; set; }

        public CountryDto Country { get; set; }

    }


}