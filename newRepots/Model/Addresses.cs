
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ModelTest.Controllers;

public class AddressModel
{
    [Key]
    public int AddressId { get; set; }

    public string addrLine1 { get; set; }
    public string addrLine2 { get; set; }
    public string subDistrict { get; set; }
    public string district { get; set; }
    public string postalCode { get; set; }
    public string createdDate { get; set; }

    public int ProvinceId { get; set; }

    [ForeignKey("ProvinceId")]
    public ProvinceModel Province { get; set; }


    public int CountryId { get; set; }

    [ForeignKey("CountryId")]
    public CountryModel Country { get; set; }

}


public class AddressRequest
{
    public int AddressId { get; set; }
    public string addrLine1 { get; set; }
    public string addrLine2 { get; set; }
    public string subDistrict { get; set; }
    public string district { get; set; }
    public string postalCode { get; set; }

    // กำหนดให้มีค่าเริ่มต้นเป็นเวลาปัจจุบันตอนสร้างอ็อบเจกต์
    public string createdDate { get; set; }

    public CountryReferenceDto Country { get; set; }

    public ProvinceReferenceDto Province { get; set; }
}