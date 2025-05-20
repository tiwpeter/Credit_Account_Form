// dto get post
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelTest.Controllers
{
    // ข้อมูลผู้ใช้


    public class GeneralModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ให้ฐานข้อมูลสร้างค่า Id อัตโนมัติ
        public int general_id { get; set; }
        public string GeneralName { get; set; }
        public string GeneralName1 { get; set; }
        public string GeneralTel { get; set; }
        public string GeneralFax { get; set; }
        public string GeneralEmail { get; set; }
        public string GeneralLine { get; set; }
        public string GeneralTax { get; set; }
        public string GeneralBranch { get; set; }

        //จะส่งข้อมูลจาก General โดยอ้างอิงไปที่ AddressModel ของ General
        [ForeignKey("AddressId")]

        // หากชื่อเป็น AddressId1 ก็ต้องระบุเป็น AddressId1
        public int AddressId { get; set; }  // กำหนด FK ที่ใช้ใน General
        public AddressModel Address { get; set; }
    }

    //for get

    //for post
    public class GeneralRequest
    {
        public string GeneralName { get; set; }
        public string GeneralName1 { get; set; }
        public string GeneralTel { get; set; }
        public string GeneralFax { get; set; }
        public string GeneralEmail { get; set; }
        public string GeneralLine { get; set; }
        public string GeneralTax { get; set; }
        public string GeneralBranch { get; set; }
        public AddressRequest Address { get; set; }
    }
}
