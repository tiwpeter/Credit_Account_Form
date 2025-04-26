using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

//กลุ่มฝ่ายขาย คือกลุ่มย่อยภายใต้หน่วยงานขาย 
[Owned]
public class SaleGroupModel
{
    [Key]
    public int Id { get; set; }

    public string GroupCode { get; set; }      // เช่น "SG01"
    public string GroupName { get; set; }      // เช่น "กลุ่มขายสินค้าอุปโภค"
    public string Description { get; set; }    // รายละเอียดเพิ่มเติม
}

[Owned]
//ประเภทกลุ่มลูกค้า
public class CustGroupTypeModel
{
    [Key]
    public int Id { get; set; }

    public string GroupCode { get; set; }       // เช่น "CUST001"
    public string GroupName { get; set; }       // เช่น "ลูกค้าทั่วไป"
    public string Description { get; set; }     // รายละเอียดเพิ่มเติม

}

[Owned]
//(สกุลเงิน) ใช้เพื่อระบุว่าสินค้าหรือบริการของลูกค้าคนนั้น ๆ มีการทำธุรกรรมในสกุลเงินใด
public class CurrencyModel
{
    [Key]
    public int Id { get; set; }

    public string CurrencyCode { get; set; }     // เช่น "THB", "USD"
    public string CurrencyName { get; set; }     // เช่น "บาท", "ดอลลาร์สหรัฐ"
    public string Symbol { get; set; }           // เช่น "฿", "$"
}

[Owned]
//อัตราแลกเปลี่ยน (Exchange Rate Type) ใช้ในระบบที่รองรับหลายสกุลเงิน เพื่อระบุว่าอัตราแลกเปลี่ยนที่ใช้เป็นแบบใด
public class ExchRateTypeModel
{
    [Key]
    public int Id { get; set; }

    public string RateTypeCode { get; set; }     // เช่น "M", "B", "C"
    public string RateTypeName { get; set; }     // เช่น "Market Rate", "Bank Rate", "Custom Rate"
    public string Description { get; set; }      // คำอธิบายเพิ่มเติม
}

[Owned]
//คือกระบวนการกำหนดราคาสำหรับลูกค้า ซึ่งมักใช้ในระบบ ERP (เช่น SAP) เพื่อกำหนดว่าลูกค้ารายนั้นใช้ “ขั้นตอน” การคำนวณราคาหรือส่วนลดแบบใด
public class CustPricProcModel
{
    [Key]
    public int Id { get; set; }

    public string PricProcCode { get; set; }       // เช่น "PRC01"
    public string PricProcName { get; set; }       // เช่น "Retail Pricing"
    public string Description { get; set; }        // รายละเอียดขั้นตอนการคิดราคา
}

// รายการราคาสินค้า ใช้เพื่อเก็บข้อมูลราคาสินค้าแต่ละตัวในแต่ละเงื่อนไข เช่น ราคาสำหรับลูกค้าประเภทต่างๆ, เขตการขาย, สกุลเงิน, หรือช่วงเวลาโปรโมชั่น

[Owned]
public class PriceListModel
{
    [Key]
    public int Id { get; set; }

    public string priceListCode { get; set; }
    public string priceListName { get; set; }
    public string priceListDes { get; set; }


}

[Owned]
//กฎที่ใช้ในสัญญาซื้อขายระหว่างประเทศ เพื่อกำหนดความรับผิดชอบของผู้ซื้อและผู้ขายเกี่ยวกับค่าใช้จ่าย
public class IncotermModel
{
    [Key]
    public int Id { get; set; }

    public string incotermCode { get; set; }
    public string incotermName { get; set; }
    public string incotermDes { get; set; }


}
//พนักงานขาย

[Owned]
//ผู้จัดการฝ่ายขาย
public class SaleManagerModel
{
    [Key]
    public int Id { get; set; }  // รหัสผู้จัดการฝ่ายขาย

    // ข้อมูลกลุ่มผู้จัดการขาย
    public string SaleGroupCode { get; set; }   // รหัสกลุ่มผู้จัดการขาย
    public string SaleGroupName { get; set; }   // ชื่อกลุ่มผู้จัดการขาย
    public string SaleGroupDes { get; set; }    // คำอธิบายกลุ่มผู้จัดการขาย

}

[Owned]
// ข้อมูลประเทศที่กลุ่มลูกค้ารับผิดชอบ
public class CustGroupCountryModel
{

    [Key]
    public int Id { get; set; }
    public string CountryCode { get; set; }   // รหัสประเทศ
    public string CountryName { get; set; }   // ชื่อประเทศ
    public string CountryDes { get; set; }    // คำอธิบายประเทศ

}
