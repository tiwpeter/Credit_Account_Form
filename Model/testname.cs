using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiNet8.Models
{
    // กำหนดว่า TestnameWWW จะเป็นตาราง "TestnameWWW" ใน schema "dbo"
    public class TestnameWWW
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ให้ฐานข้อมูลสร้างค่า Id อัตโนมัติ
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        // Foreign Key ที่เชื่อมโยงกับ User
        public int UserId { get; set; }  // Foreign Key

        // Navigation Property เพื่อเชื่อมโยงกับ User
        [ForeignKey("UserId")]
        public User User { get; set; }  // ทำให้ TestnameWWW เชื่อมโยงกับ User
    }

    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ให้ฐานข้อมูลสร้างค่า UserId อัตโนมัติ
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        // Navigation Property เพื่อแสดงความสัมพันธ์กับ TestnameWWW
        public ICollection<TestnameWWW> TestnameWWWs { get; set; }  // การเชื่อมโยง One-to-Many
    }
}
