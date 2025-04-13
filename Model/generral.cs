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


        //จะส่งข้อมูลจาก General โดยอ้างอิงไปที่ AddressModel ของ General

        // หากชื่อเป็น AddressId1 ก็ต้องระบุเป็น AddressId1
        public int AddressId { get; set; }  // กำหนด FK ที่ใช้ใน General
        public AddressModel Address { get; set; }
    }


}
