using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class BusinessTypeDto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int busiTypeID { get; set; }
    public string busiTypeCode { get; set; }
    public string busiTypeName { get; set; }
    public string busiTypeDes { get; set; }
    public DateTime? RegistrationDate { get; set; }

    // เปลี่ยนเป็น decimal? เพื่อใช้ HasValue
    public decimal? RegisteredCapital { get; set; }
}