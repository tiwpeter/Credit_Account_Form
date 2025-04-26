using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

// ประเภทร้านค้า

[Owned]
public class ShopTypeModel
{

    public int id { get; set; }

    public string shopCode { get; set; }
    public string shopName { get; set; }
    public string shopDes { get; set; }
    public string accGroupName { get; set; }
}


//อถสาหรกรรม
[Owned]
public class IndustryTypeModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ให้ฐานข้อมูลสร้างค่า Id อัตโนมัติ
    public int id { get; set; }
    public string? InduTypeCode { get; set; }  // สามารถรับค่า NULL ได้
    public string? InduTypeName { get; set; }  // สามารถรับค่า NULL ได้
    public string? InduTypeDes { get; set; }   // สามารถรับค่า NULL ได้


}

// หน่วยงานที่รับผิดชอบด้านการขาย
[Owned]
public class SaleOrgModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ให้ฐานข้อมูลสร้างค่า Id อัตโนมัติ
    public int id { get; set; }
    public string saleOrgCode { get; set; }
    public string saleOrgName { get; set; }
    public string saleOrgDes { get; set; }

}

// บริษัท
[Owned]
public class CompanyModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ให้ฐานข้อมูลสร้างค่า Id อัตโนมัติ
    public int company_id { get; set; }
    public string companyCode { get; set; }
    public string companyName { get; set; }
    public string companyAddr { get; set; }


}

//ประเภทกลุ่มลูกค้าในบัญชี
[Owned]
public class accountGroupModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ให้ฐานข้อมูลสร้างค่า Id อัตโนมัติ
    public int id { get; set; }
    public string accGroupCode { get; set; }
    public string accGroupName { get; set; }
    public string accGroupDes { get; set; }
}


[Owned]
//ใช้ ระบุประเภทหรือหมวดหมู่ของบัญชีในระบบขององค์กร เช่น รหัสบัญชีสำหรับสินทรัพย์, หนี้สิน, รายได้, ค่าใช้จ่าย เป็นต้น
public class AccountCodeModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // ให้ฐานข้อมูลสร้างค่า Id อัตโนมัติ
    public int AccountId { get; set; }  // รหัสบัญชี

    public string AccountCode { get; set; }  // รหัสบัญชี
    public string AccountName { get; set; }  // ชื่อบัญชี
    public string AccountType { get; set; }  // ประเภทบัญชี (เช่น สินทรัพย์, หนี้สิน, รายได้, ค่าใช้จ่าย)

    public string Description { get; set; }  // คำอธิบายเพิ่มเติมเกี่ยวกับบัญชี
}
[Owned]
//เป็นคอลัมน์ที่ใช้ในการจัดเรียงข้อมูลจากเล็กไปหามาก (ascending) หรือจากมากไปหาน้อย (descending) ได้ง่ายขึ้น โดยเฉพาะเมื่อข้อมูลนั้นมีการจัดลำดับ (sorting) ในการแสดงผล เช่น ผลิตภัณฑ์, ลูกค้า, หรือรายการที่
public class SortKeyModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // ให้ฐานข้อมูลสร้างค่า Id อัตโนมัติ
    public int Id { get; set; }  // รหัสบัญชี

    public string sortkeyCode { get; set; }  // รหัสบัญชี
    public string sortkeyName { get; set; }  // ชื่อบัญชี
    public string sortkeyDes { get; set; }  // ประเภทบัญชี (เช่น สินทรัพย์, หนี้สิน, รายได้, ค่าใช้จ่าย)

}

//กลุ่มที่ใช้สำหรับการจัดการเงินสด
[Owned]
public class CashGroupModel
{
    [Key]
    public int Id { get; set; }

    public string CashGroupCode { get; set; }  // รหัสของกลุ่มเงินสด
    public string CashGroupName { get; set; }  // ชื่อของกลุ่มเงินสด
    public string Description { get; set; }    // รายละเอียดเพิ่มเติม
}

[Owned]
// วิธีการชำระเงินที่ผู้ใช้เลือกเมื่อทำการซื้อสินค้า
public class PaymentMethodModel
{
    [Key]
    public int Id { get; set; }

    public string PaymentMethodCode { get; set; }  // รหัสของวิธีการชำระเงิน
    public string PaymentMethodName { get; set; }  // ชื่อของวิธีการชำระเงิน เช่น "Credit Card", "Cash", "PayPal"
    public string Description { get; set; }        // รายละเอียดเพิ่มเติมเกี่ยวกับวิธีการชำระเงิน
}
//เงื่อนไขการชำระเงิน ที่ผู้ซื้อจะต้องชำระเงินให้กับผู้ขายภายในระยะเวลาที่กำหนด
[Owned]
public class TermOfPaymentModel
{
    [Key]
    public int Id { get; set; }

    public string TermCode { get; set; }  // เช่น "NET30", "COD"
    public string TermName { get; set; }  // เช่น "Net 30 Days", "Cash on Delivery"
    public string Description { get; set; }  // อธิบายเพิ่มเติม เช่น "ชำระเงินภายใน 30 วันนับจากวันออกบิล"
}

[Owned]
//พื้นที่ที่รับผิดชอบในการขายสินค้า/บริการ ซึ่งใช้ในระบบเพื่อระบุว่าลูกค้าหรือร้านค้าแต่ละรายอยู่ในเขตใด 
public class SaleDistrictModel
{
    [Key]
    public int Id { get; set; }

    public string DistrictCode { get; set; } // เช่น "D001"
    public string DistrictName { get; set; } // เช่น "ภาคกลาง", "กรุงเทพฯ"
    public string Description { get; set; }
}
