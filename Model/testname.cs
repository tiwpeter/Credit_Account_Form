using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiNet8.Models  // เปลี่ยนให้ตรงกับที่ใช้ใน Appcontext
{
    [Table("New", Schema = "dbo")]
    public class TestnameWWW
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }  // ใช้ required แทน
    }
}
