using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelTest.Controllers
{

    public class AddressModel
    {
        [Key]
        public int AddressId { get; set; }

        public string Street { get; set; }

        public int CountryId { get; set; }
        public CountryModel Country { get; set; }

        public int? ProvinceId { get; set; }
        public ProvinceModel Province { get; set; }

        public int? ThaiProvinceId { get; set; }
        public ThaiProvince ThaiProvince { get; set; }
    }
    public class CountryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CountryId { get; set; }

        public string CountryName { get; set; }

        // Navigation
        public ICollection<ThaiProvince> ThaiProvinces { get; set; }
        public ICollection<ProvinceModel> Provinces { get; set; }
    }
    public class GeographyModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GeographyId { get; set; }

        public string GeographyName { get; set; } // เช่น "ภาคเหนือ", "ภาคใต้"

        public ICollection<ThaiProvince> ThaiProvinces { get; set; }
        public ICollection<ProvinceModel> Provinces { get; set; }
    }
    public class ThaiProvince
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ThaiProvinceId { get; set; }

        public string ThaiProvinceName { get; set; }

        // FK → Country (ควรเป็นประเทศไทยเสมอ)
        public int CountryId { get; set; }
        [ForeignKey("CountryId")]
        public CountryModel Country { get; set; }

        // FK → Geography
        public int GeographyId { get; set; }
        [ForeignKey("GeographyId")]
        public GeographyModel Geography { get; set; }
    }
    public class ProvinceModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProvinceId { get; set; }

        public string ProvinceName { get; set; }

        // FK → Country
        public int CountryId { get; set; }
        [ForeignKey("CountryId")]
        public CountryModel Country { get; set; }

        // FK → Geography (ถ้ามีการแบ่งเป็นภูมิภาคในประเทศนั้น)
        public int GeographyId { get; set; }
        [ForeignKey("GeographyId")]
        public GeographyModel Geography { get; set; }
    }
    public class ThaiProvinceDto
    {
        public int ThaiProvinceId { get; set; }
        public string ThaiProvinceName { get; set; }
        public string CountryName { get; set; }
        public int GeographyId { get; set; }
        public string GeographyName { get; set; }
    }


}
