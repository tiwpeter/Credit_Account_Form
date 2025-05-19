using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelTest.Controllers
{











    public class CreditInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ให้ฐานข้อมูลสร้างค่า Id อัตโนมัติ
        public int creditInfo_id { get; set; }
        public string estimatedPurchase { get; set; }
        public string timeRequired { get; set; }
        public string creditLimit { get; set; }

    }



    public class CustomerSigns
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ให้ฐานข้อมูลสร้างค่า Id อัตโนมัติ
        public int custsign_id { get; set; }
        public string custsignFirstName { get; set; }
        public string custsignLastName { get; set; }
        public string custsignTel { get; set; }
        public string custsignEmail { get; set; }
        public string CompanyLocationMap { get; set; }
        public string custsignLine { get; set; }

    }


}

