using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelTest.Controllers
{
    public class ProvinceModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProvinceId { get; set; }

        public string ProvinceName { get; set; }

        // Foreign Key → Country
        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public CountryModel Country { get; set; }
    }
    public class ProvinceDto
    {
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }
    }

    public class CountryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public List<ProvinceModel> Provinces { get; set; }
    }
}
