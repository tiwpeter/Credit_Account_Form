using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// เก็บ model
namespace ModelTest.Controllers
{
    public class CustomerModel
    {
        [Key]
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        //ข้อมูลประจำตัว
        public int GeneralId { get; set; }

        [ForeignKey("GeneralId")]
        public GeneralModel General { get; set; }

        //ที่จัดส่ง
        public int shipping_id { get; set; }

        [ForeignKey("shipping_id")]
        public ShippingModel Shipping { get; set; }

        //ลักษณะของร้านค้า
        public ShopTypeModel ShopType { get; set; }
        //ประเภทอุตสาหกรรม
        public IndustryTypeModel IndustryType { get; set; }
        //บริษัท
        public CompanyModel Company { get; set; }
        // หน่วยงานที่รับผิดชอบด้านการขาย
        public SaleOrgModel SaleOrg { get; set; }


        //ใช้ ระบุประเภทหรือหมวดหมู่ของบัญชีในระบบขององค์กร เช่น รหัสบัญชีสำหรับสินทรัพย์, หนี้สิน, รายได้, ค่าใช้จ่าย เป็นต้น
        public AccountCodeModel AccountCode { get; set; }




        //ประเภทของธุรกิจที่พวกเขาทำอยู่ครับ เช่นว่า ธุรกิจนั้นคือ ค้าปลีก, ขายส่ง, โรงงาน, ซอฟต์แวร์, บริการ ฯลฯ
        public int busiTypeID { get; set; }  // เพิ่มคอลัมน์ BusinessTypeId

        [ForeignKey("busiTypeID ")]
        public BusinessTypeModel BusinessType { get; set; }

        // ข้อมูลการชำระเงิน, สถานะเครดิต, ประวัติการกู้ยืม หรือข้อมูลที่เกี่ยวข้องกับการให้เครดิตทางการเงิน
        public int? CreditInfoId { get; set; }
        [ForeignKey("CreditInfoId")]
        public CreditInfoModel CreditInfo { get; set; }


        //สินเชื่อหรือการตรวจสอบเอกสารที่เกี่ยวข้องกับการกู้ยืมเงินในทางการเงิน
        //ใช้เก็บข้อมูลว่าเอกสารเครดิตนั้นมีใบรับรองบริษัทหรือไม่
        public int? DocCreditId { get; set; }
        [ForeignKey("DocCreditId")]
        public DocCreditModel DocCredit { get; set; }



        //เก็บข้อมูลเกี่ยวกับลายเซ็นของลูกค้า
        public int? CustSignId { get; set; }
        [ForeignKey("CustSignId")]
        public CustomerSignModel CustomerSigns { get; set; }

        ////ประเภทกลุ่มลูกค้าในบัญชี
        public accountGroupModel accountGroup { get; set; }


        //ใช้ เรียงลำดับ
        public SortKeyModel SortKey { get; set; }

        //กลุ่มที่ใช้สำหรับการจัดการเงินสด
        public CashGroupModel CashGroup { get; set; }

        // วิธีการชำระเงินที่ผู้ใช้เลือกเมื่อทำการซื้อสินค้า
        public PaymentMethodModel PaymentMethod { get; set; }

        //เงื่อนไขการชำระเงิน ที่ผู้ซื้อจะต้องชำระเงินให้กับผู้ขายภายในระยะเวลาที่กำหนด
        public TermOfPaymentModel TermOfPayment { get; set; }


        //พื้นที่ที่รับผิดชอบในการขายสินค้า/บริการ ซึ่งใช้ในระบบเพื่อระบุว่าลูกค้าหรือร้านค้าแต่ละรายอยู่ในเขตใด 
        public SaleDistrictModel SaleDistrict { get; set; }

        //กลุ่มฝ่ายขาย คือกลุ่มย่อยภายใต้หน่วยงานขาย 
        public SaleGroupModel SaleGroup { get; set; }


        //ประเภทกลุ่มลูกค้า
        public CustGroupTypeModel CustGroupType { get; set; }

        //(สกุลเงิน) ใช้เพื่อระบุว่าสินค้าหรือบริการของลูกค้าคนนั้น ๆ มีการทำธุรกรรมในสกุลเงินใด
        public CurrencyModel Currency { get; set; }

        //อัตราแลกเปลี่ยน (Exchange Rate Type) ใช้ในระบบที่รองรับหลายสกุลเงิน เพื่อระบุว่าอัตราแลกเปลี่ยนที่ใช้เป็นแบบใด
        public ExchRateTypeModel ExchRateType { get; set; }


        //คือกระบวนการกำหนดราคาสำหรับลูกค้า ซึ่งมักใช้ในระบบ ERP (เช่น SAP) เพื่อกำหนดว่าลูกค้ารายนั้นใช้ “ขั้นตอน” การคำนวณราคาหรือส่วนลดแบบใด
        public CustPricProcModel CustPricProc { get; set; }

        // รายการราคาสินค้า ใช้เพื่อเก็บข้อมูลราคาสินค้าแต่ละตัวในแต่ละเงื่อนไข เช่น ราคาสำหรับลูกค้าประเภทต่างๆ, เขตการขาย, สกุลเงิน, หรือช่วงเวลาโปรโมชั่น
        public PriceListModel PriceList { get; set; }

        //กฎที่ใช้ในสัญญาซื้อขายระหว่างประเทศ เพื่อกำหนดความรับผิดชอบของผู้ซื้อและผู้ขายเกี่ยวกับค่าใช้จ่าย
        public IncotermModel Incoterm { get; set; }

        //ผู้จัดการฝ่ายขาย
        public SaleManagerModel SaleManager { get; set; }

        // ข้อมูลประเทศที่กลุ่มลูกค้ารับผิดชอบ
        //กลุ่มประเทศที่ลูกค้ารับผิดชอบ
        public CustGroupCountryModel CustGroupCountry { get; set; }





    }




    //ตัวอย่าง Customer Model แบบมี FK ทั้ง 3


}
/*
RegisformModel
 ├─ GeneralModel
 │    └─ AddressModel
 │         ├─ Country
 │         ├─ Province
 │         └─ ThaiProvince
 └─ ShippingModel
      └─ Province
markdawn
*/