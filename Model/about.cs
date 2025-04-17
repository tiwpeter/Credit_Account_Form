using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

// table for serach
namespace ModelTest.Controllers
{
    public class RegisterFrom
    {
        public int Id { get; set; }
        public shopTypeModel shopType { get; set; }
    }

    [Owned]
    public class shopTypeModel
    {

        public int id { get; set; }
        public string InduTypeCode { get; set; }

    }


    public class CreditInfoModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int creditInfo_id { get; set; }
        public string estimatedPurchase { get; set; }
        public string timeRequired { get; set; }
        public string creditLimit { get; set; }
    }
    public class DocumentCredit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int doccredit_id { get; set; }

        public bool CompanyCertificate { get; set; }
        public bool CopyOfPP_20 { get; set; }
        public bool CopyOfCoRegis { get; set; }
        public bool CopyOfIDCard { get; set; }
        public bool CompanyLocationMap { get; set; }
    }
    public class CustomerSign
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int custsign_id { get; set; }

        [Required]
        [MaxLength(255)]
        public string custsignFirstName { get; set; }

        [Required]
        [MaxLength(255)]
        public string custsignLastName { get; set; }

        [Required]
        [MaxLength(255)]
        public string custsignTel { get; set; }

        [Required]
        [MaxLength(255)]
        public string custsignEmail { get; set; }

        [Required]
        [MaxLength(255)]
        public string custsignLine { get; set; }
    }
}
