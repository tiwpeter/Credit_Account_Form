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

    public class GeneralDto
    {
        public int GeneralId { get; set; }
        public string FullName { get; set; }
        public AddressDto Address { get; set; }

    }

    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public GeneralDto General { get; set; }
    }



    public class AddressDto
    {
        public int? CountryId { get; set; } // เพิ่มตรงนี้
        public string CountryName { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string TambonName { get; set; }
        public string AmphureName { get; set; }
        public string ProvinceName { get; set; }
        public string GeographyName { get; set; }
    }


    public class ProvinceDto
    {
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public string CountryName { get; set; }
    }
    //(ภูมิภาค)

    public class AmphureModel
    {
        [Key]
        public int AmphureId { get; set; }
        public string AmphureName { get; set; }

        public int ProvinceId { get; set; }
        [ForeignKey("ProvinceId")]
        public ProvinceModel Province { get; set; }

        public ICollection<TambonModel> Tambons { get; set; }


    }

    public class TambonModel
    {
        [Key]
        public int TambonId { get; set; }
        public string TambonName { get; set; }

        public int AmphureId { get; set; }

        [ForeignKey("AmphureId")]
        public AmphureModel Amphure { get; set; }

    }
    public class ProvinceModel
    {
        [Key]
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }

        public int CountryId { get; set; }

        [ForeignKey("CountryId")]

        public CountryModel Country { get; set; }

        [ForeignKey("GeographyId")]

        public int GeographyId { get; set; }

        public GeographyModel Geography { get; set; }

        // หนึ่งจังหวัด(Province) มี หลายอำเภอ(Amphure)
        //หนึ่งอำเภอ(Amphure) มี หลายตำบล(Tambon)
        // ✅ ความสัมพันธ์แบบ One-to-Many: Province ➝ Amphure
        public ICollection<AmphureModel> Amphures { get; set; }

    }
    public class GeographyModel
    {
        [Key]
        public int GeographyId { get; set; }
        public string GeographyName { get; set; }


    }

    public class CountryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CountryId { get; set; }

        public string CountryName { get; set; }

        // เพิ่ม ICollection สำหรับความสัมพันธ์ One-to-Many
        public ICollection<AddressModel> Addresses { get; set; }
        public ICollection<ProvinceModel> Provinces { get; set; }
    }

    public class AddressModel
    {
        [Key]
        public int AddressId { get; set; }

        public string Street { get; set; }
        public string ZipCode { get; set; }



        // เพิ่ม Province
        public int ProvinceId { get; set; }

        [ForeignKey("ProvinceId")]
        public ProvinceModel Province { get; set; }

        public int CountryId { get; set; }

        [ForeignKey("CountryId")]

        public CountryModel Country { get; set; }


    }


}