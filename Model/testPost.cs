using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelTest.Controllers
{


    public class GeneralModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ให้ฐานข้อมูลสร้างค่า Id อัตโนมัติ
        public int general_id { get; set; }
        public string generalName { get; set; }


        //จะส่งข้อมูลจาก General โดยอ้างอิงไปที่ AddressModel ของ General
        [ForeignKey("AddressId")]

        // หากชื่อเป็น AddressId1 ก็ต้องระบุเป็น AddressId1
        public int AddressId { get; set; }  // กำหนด FK ที่ใช้ใน General
        public AddressModel Address { get; set; }
    }

    public class CustomerModel
    {
        [Key]
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public int GeneralId { get; set; }

        [ForeignKey("GeneralId")]
        public GeneralModel General { get; set; }

    }


    public class CountryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CountryId { get; set; }

        public string CountryName { get; set; }

    }

    public class AddressModel
    {
        [Key]
        public int AddressId { get; set; }

        public string CustomerName { get; set; }



        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public CountryModel Country { get; set; }

    }

    public class GeneralDto
    {
        public int GeneralId { get; set; }
        public string GeneralName { get; set; }
        public int AddressId { get; set; }
        public AddressDto Address { get; set; }
    }

    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int GeneralId { get; set; }
        public GeneralDto General { get; set; }
    }

    public class AddressDto
    {
        public int AddressId { get; set; }
        public string CustomerName { get; set; }
        public int CountryId { get; set; }
        public CountryDto Country { get; set; }
    }

    public class CountryDto
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
    }
    public class CreateCustomerRequest
    {
        public string CustomerName { get; set; }
        public string GeneralName { get; set; }
        public string AddressCustomerName { get; set; }
        public int CountryId { get; set; }  // เปลี่ยนจาก CountryName เป็น CountryId

    }
    // objects  adjust dto


}