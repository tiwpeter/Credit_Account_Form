using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelTest.Controllers
{

    //ตัวอย่าง Customer Model แบบมี FK ทั้ง 3
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        // FK1 → General
        public int GeneralId { get; set; }
        public General General { get; set; }

        // FK2 → AddressModel
        // ถ้า General อ้างอิงถึง Address แล้ว อาจไม่จำเป็นต้องมี AddressId ใน Customer
        public int AddressId { get; set; }  // เพิ่มฟิลด์นี้

        public AddressModel Address { get; set; }

        // FK3 → Shipping
        public int ShippingId { get; set; }
        public ShippingModel Shipping { get; set; }
    }

}