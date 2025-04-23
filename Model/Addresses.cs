
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ModelTest.Controllers;

public class AddressModel
{
    [Key]
    public int AddressId { get; set; }


    public int ProvinceId { get; set; }

    [ForeignKey("ProvinceId")]
    public ProvinceModel Province { get; set; }


    public int CountryId { get; set; }

    [ForeignKey("CountryId")]
    public CountryModel Country { get; set; }

}