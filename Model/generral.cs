using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelTest.Controllers
{
    // ข้อมูลผู้ใช้
    public class General
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ให้ฐานข้อมูลสร้างค่า Id อัตโนมัติ
        public int general_id { get; set; }
        public string generalName1 { get; set; }

        // ความสัมพันธ์กับ Address
        [ForeignKey("AddressId")]
        public int AddressId { get; set; }
        public AddressModel Address { get; set; }
    }

    public class Shipping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ให้ฐานข้อมูลสร้างค่า Id อัตโนมัติ
        public int shipping_id { get; set; }
        public string subDistrict { get; set; }

        // ความสัมพันธ์กับ Address
        //ใช้ ForeignKey เพื่อบ่งบอกว่า AddressId ควรจะอ้างอิงไปที่ AddressModel และให้ระบุความสัมพันธ์ที่ชัดเจน
        [ForeignKey("AddressId")]
        public int AddressId { get; set; }
        public AddressModel Address { get; set; }
    }
}
