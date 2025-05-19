using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// ประเภทร้านค้า
public class ShopTypeModel
{
    public int id { get; set; }

    public string shopCode { get; set; }
    public string shopName { get; set; }
    public string shopDes { get; set; }
    public string accGroupName { get; set; }
}






public class SaleOrgModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ให้ฐานข้อมูลสร้างค่า Id อัตโนมัติ
    public int id { get; set; }
    public string saleOrgCode { get; set; }
    public string saleOrgName { get; set; }
    public string saleOrgDes { get; set; }

}
public class accountGroupModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ให้ฐานข้อมูลสร้างค่า Id อัตโนมัติ
    public int id { get; set; }
    public string accGroupCode { get; set; }
    public string accGroupName { get; set; }
    public string accGroupDes { get; set; }
}
public class IndustryType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ให้ฐานข้อมูลสร้างค่า Id อัตโนมัติ
    public int id { get; set; }
    public string InduTypeCode { get; set; }
    public string InduTypeName { get; set; }
    public string InduTypeDes { get; set; }


}

public class CompanyModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ให้ฐานข้อมูลสร้างค่า Id อัตโนมัติ
    public int company_id { get; set; }
    public string companyCode { get; set; }
    public string companyName { get; set; }
    public string companyAddr { get; set; }


}