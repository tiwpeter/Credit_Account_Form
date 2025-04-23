using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ModelTest.Controllers
{
    public class CreditInfoModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ให้ฐานข้อมูลสร้างค่า Id อัตโนมัติ
        public int CreditInfoId { get; set; } // Primary Key

        public decimal EstimatedPurchase { get; set; } // การซื้อที่คาดการณ์ (แบบทศนิยม)

        public int TimeRequired { get; set; } // เวลาที่ต้องการ (เช่น เป็นจำนวนเดือน หรือปี)

        public decimal CreditLimit { get; set; } // ขีดจำกัดเครดิต (แบบทศนิยม)
                                                 // ForeignKey สำหรับ Customer


    }

    public class CreditInfoDto
    {
        public int CreditInfoId { get; set; }
        [Precision(18, 4)]
        public decimal EstimatedPurchase { get; set; }
        public int TimeRequired { get; set; }
        public decimal CreditLimit { get; set; }
    }

}
