using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelTest.Controllers
{
    public class CustomerSignModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustSignId { get; set; }

        [Required]
        [MaxLength(100)]
        public string CustSignFirstName { get; set; }

        // Foreign Key
        public int CustomerId { get; set; }


        public string custsignTel { get; set; }
        public string custsignEmail { get; set; }
        public string custsignLine { get; set; }





    }
    public class CustomerSignDto
    {
        public int CustSignId { get; set; }
        public string CustSignFirstName { get; set; }

        public string custsignTel { get; set; }
        public string custsignEmail { get; set; }
        public string custsignLine { get; set; }
    }


}
