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
        public int ProvinceId { get; set; }
        public ProvinceModel Province { get; set; }
    }
}