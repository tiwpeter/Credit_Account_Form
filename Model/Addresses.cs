using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelTest.Controllers
{

    //จังหวัดในไทย
    //ในกรณีที่ Country เป็น "ประเทศไทย": คุณจะสามารถค้นหาข้อมูลจาก thai_provinces ได้ 
    // โดยใช้ CountryId ที่ตรงกับ "ประเทศไทย"
    // ตาราง ThaiProvinces (เก็บจังหวัดของประเทศไทย)
    public class ThaiProvince
    {
        [Key]
        public int ThaiProvinceId { get; set; }
        public string ThaiProvinceName { get; set; } // ชื่อจังหวัดในประเทศไทย

        public int CountryId { get; set; } // Foreign Key ไปที่ Country
        public CountryModel Country { get; set; } // ความสัมพันธ์กับ Country

        // เพิ่ม geography_id และความสัมพันธ์
        public int GeographyId { get; set; }
        public GeographyModel Geography { get; set; }
    }

    //ดึง "จังหวัด" เฉพาะในประเทศไทย และ กรองตาม ภูมิภาค(Geography)
    public class GeographyModel
    {
        [Key]
        public int GeographyId { get; set; }

        public string GeographyName { get; set; } // เช่น "ภาคเหนือ", "ภาคใต้"

        public ICollection<ThaiProvince> ThaiProvinces { get; set; }
    }




}
