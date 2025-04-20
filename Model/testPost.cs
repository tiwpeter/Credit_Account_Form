using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelTest.Controllers
{




    public class CustomerModel
    {
        [Key]
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public int GeneralId { get; set; }

    }


    public class ProvinceModel
    {
        [Key]
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }

        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public CountryModel Country { get; set; }
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
    }

    public class CountryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CountryId { get; set; }

        public string CountryName { get; set; }


    }
    public class AddressDto
    {
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string ProvinceName { get; set; }
        public string CountryName { get; set; }
    }


}