using Microsoft.EntityFrameworkCore;
using ModelTest.Controllers;

namespace API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<GeneralModel> Generals { get; set; }
        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<CountryModel> Countries { get; set; }
        public DbSet<AddressModel> Addresses { get; set; }
        public DbSet<ProvinceModel> Provinces { get; set; }
        public DbSet<ShippingModel> Shippings { get; set; }
        public DbSet<BusinessTypeModel> BusinessTypes { get; set; }
        public DbSet<CreditInfoModel> CreditInfo { get; set; }
        public DbSet<CustomerSignModel> CustomerSign { get; set; }
        public DbSet<ShopTypeModel> ShopType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // แก้ Multiple Cascade Path
            modelBuilder.Entity<AddressModel>()
                .HasOne(a => a.Country)
                .WithMany()
                .HasForeignKey(a => a.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AddressModel>()
                .HasOne(a => a.Province)
                .WithMany()
                .HasForeignKey(a => a.ProvinceId)
                .OnDelete(DeleteBehavior.Restrict);

            // Index
            modelBuilder.Entity<AddressModel>().HasIndex(a => a.CountryId);
            modelBuilder.Entity<AddressModel>().HasIndex(a => a.ProvinceId);

            modelBuilder.Entity<CustomerModel>()
             .OwnsOne(c => c.ShopType);

            modelBuilder.Entity<CustomerModel>().OwnsOne(c => c.CustGroupCountry).HasData(
    new
    {
        CustomerModelId = 1,
        CountryCode = "TH",
        CountryName = "ประเทศไทย",
        CountryDes = "ลูกค้าในประเทศไทย"
    },
    new
    {
        CustomerModelId = 2,
        CountryCode = "SG",
        CountryName = "สิงคโปร์",
        CountryDes = "ลูกค้าในประเทศสิงคโปร์"
    }
);


            // SaleManager
            modelBuilder.Entity<CustomerModel>().OwnsOne(c => c.SaleManager).HasData(
                new
                {
                    CustomerModelId = 1,
                    SaleGroupCode = "SMG01",
                    SaleGroupName = "ทีมขายภาคเหนือ",
                    SaleGroupDes = "ดูแลพื้นที่ภาคเหนือทั้งหมด"
                },
                new
                {
                    CustomerModelId = 2,
                    SaleGroupCode = "SMG02",
                    SaleGroupName = "ทีมขายกรุงเทพฯ",
                    SaleGroupDes = "ดูแลพื้นที่ในเขตกรุงเทพฯ และปริมณฑล"
                }
            );

            // Incoterm
            modelBuilder.Entity<CustomerModel>().OwnsOne(c => c.Incoterm).HasData(
                new
                {
                    CustomerModelId = 1,
                    incotermCode = "FOB",
                    incotermName = "Free On Board",
                    incotermDes = "ผู้ขายรับผิดชอบค่าใช้จ่ายจนถึงท่าเรือขนส่ง"
                },
                new
                {
                    CustomerModelId = 2,
                    incotermCode = "CIF",
                    incotermName = "Cost, Insurance and Freight",
                    incotermDes = "ผู้ขายรับผิดชอบค่าขนส่งและประกันภัยจนถึงปลายทาง"
                }
            );

            // PriceList
            modelBuilder.Entity<CustomerModel>().OwnsOne(c => c.PriceList).HasData(
                new
                {
                    CustomerModelId = 1,
                    priceListCode = "PL001",
                    priceListName = "รายการราคาทั่วไป",
                    priceListDes = "ราคามาตรฐานที่ใช้กับลูกค้าทุกประเภท"
                },
                new
                {
                    CustomerModelId = 2,
                    priceListCode = "PL002",
                    priceListName = "รายการราคาพิเศษ",
                    priceListDes = "ใช้สำหรับลูกค้าระดับ VIP หรือโปรโมชั่น"
                }
            );


            modelBuilder.Entity<CustomerModel>().OwnsOne(c => c.CustPricProc).HasData(
      new
      {
          CustomerModelId = 1,
          PricProcCode = "PRC01",
          PricProcName = "Retail Pricing",
          Description = "ใช้สำหรับราคาขายปลีกทั่วไป"
      },
      new
      {
          CustomerModelId = 2,
          PricProcCode = "PRC02",
          PricProcName = "Wholesale Pricing",
          Description = "ใช้สำหรับราคาขายส่ง"
      }
  );


            // SEED Exchange Rate Type
            modelBuilder.Entity<CustomerModel>().OwnsOne(c => c.ExchRateType).HasData(
                new
                {
                    CustomerModelId = 1,
                    RateTypeCode = "M",
                    RateTypeName = "Market Rate",
                    Description = "อัตราตลาดทั่วไป"
                },
                new
                {
                    CustomerModelId = 2,
                    RateTypeCode = "B",
                    RateTypeName = "Bank Rate",
                    Description = "อัตราที่ธนาคารใช้"
                },
                new
                {
                    CustomerModelId = 3,
                    RateTypeCode = "C",
                    RateTypeName = "Custom Rate",
                    Description = "อัตราที่กำหนดเอง"
                }
            );

            // SEED Currency
            modelBuilder.Entity<CustomerModel>().OwnsOne(c => c.Currency).HasData(
                new
                {
                    CustomerModelId = 1,
                    CurrencyCode = "THB",
                    CurrencyName = "บาท",
                    Symbol = "฿"
                },
                new
                {
                    CustomerModelId = 2,
                    CurrencyCode = "USD",
                    CurrencyName = "ดอลลาร์สหรัฐ",
                    Symbol = "$"
                },
                new
                {
                    CustomerModelId = 3,
                    CurrencyCode = "EUR",
                    CurrencyName = "ยูโร",
                    Symbol = "€"
                }
            );


            modelBuilder.Entity<CustomerModel>().OwnsOne(c => c.SaleGroup).HasData(
     new
     {
         CustomerModelId = 1, // FK จาก Customer
         GroupCode = "SG01",
         GroupName = "กลุ่มขายสินค้าอุปโภค",
         Description = "สินค้าที่ใช้ในชีวิตประจำวัน เช่น สบู่ แชมพู"
     }
             );


            modelBuilder.Entity<CustomerModel>().OwnsOne(c => c.SaleDistrict).HasData(
                       new
                       {
                           CustomerModelId = 1,
                           DistrictCode = "D001",
                           DistrictName = "กรุงเทพมหานคร",
                           Description = "พื้นที่เขตเมืองหลวงและปริมณฑล"
                       }
            );


            modelBuilder.Entity<SaleDistrictModel>().HasData(
                new SaleDistrictModel
                {
                    Id = 1,
                    DistrictCode = "D001",
                    DistrictName = "กรุงเทพมหานคร",
                    Description = "พื้นที่เขตเมืองหลวงและปริมณฑล"
                },
                new SaleDistrictModel
                {
                    Id = 2,
                    DistrictCode = "D002",
                    DistrictName = "ภาคกลาง",
                    Description = "ครอบคลุมจังหวัดในภาคกลาง เช่น นครปฐม ราชบุรี"
                },
                new SaleDistrictModel
                {
                    Id = 3,
                    DistrictCode = "D003",
                    DistrictName = "ภาคตะวันออกเฉียงเหนือ",
                    Description = "พื้นที่อีสาน เช่น ขอนแก่น อุบลราชธานี"
                },
                new SaleDistrictModel
                {
                    Id = 4,
                    DistrictCode = "D004",
                    DistrictName = "ภาคเหนือ",
                    Description = "เช่น เชียงใหม่ ลำปาง พิษณุโลก"
                },
                new SaleDistrictModel
                {
                    Id = 5,
                    DistrictCode = "D005",
                    DistrictName = "ภาคใต้",
                    Description = "เช่น สุราษฎร์ธานี ภูเก็ต สงขลา"
                }
            );





            modelBuilder.Entity<CustomerModel>().OwnsOne(c => c.TermOfPayment).HasData(
                new
                {
                    CustomerModelCustomerId = 1,
                    Id = 1,
                    TermCode = "NET30",
                    TermName = "เครดิต 30 วัน",
                    Description = "ชำระเงินภายใน 30 วันนับจากวันออกใบแจ้งหนี้"
                },
                new
                {
                    CustomerModelCustomerId = 2,
                    Id = 2,
                    TermCode = "COD",
                    TermName = "ชำระเงินปลายทาง",
                    Description = "ชำระเงินเมื่อได้รับสินค้า"
                }
            );


            modelBuilder.Entity<CustomerModel>().OwnsOne(c => c.PaymentMethod).HasData(
                new
                {
                    CustomerModelCustomerId = 1,
                    Id = 1,
                    PaymentMethodCode = "PM01",
                    PaymentMethodName = "เงินสด",
                    Description = "ชำระด้วยเงินสดเมื่อรับสินค้า"
                },
                new
                {
                    CustomerModelCustomerId = 2,
                    Id = 2,
                    PaymentMethodCode = "PM02",
                    PaymentMethodName = "โอนผ่านธนาคาร",
                    Description = "ชำระผ่านระบบอินเทอร์เน็ตแบงก์กิ้ง"
                }
            );



            modelBuilder.Entity<CustomerModel>().OwnsOne(c => c.CashGroup).HasData(
                new
                {
                    CustomerModelCustomerId = 1,
                    Id = 1,
                    CashGroupCode = "CG01",
                    CashGroupName = "เงินสดทั่วไป",
                    Description = "เงินสดที่ใช้ในแต่ละวัน"
                },
                new
                {
                    CustomerModelCustomerId = 2,
                    Id = 2,
                    CashGroupCode = "CG02",
                    CashGroupName = "เงินสดสำรอง",
                    Description = "เงินสดที่กันไว้สำหรับกรณีฉุกเฉิน"
                }
            );


            modelBuilder.Entity<CustomerModel>().OwnsOne(c => c.SortKey).HasData(
                new
                {
                    CustomerModelCustomerId = 1,
                    Id = 1,
                    sortkeyCode = "SRT01",
                    sortkeyName = "เรียงตามชื่อ",
                    sortkeyDes = "จัดเรียงตามชื่อลูกค้า"
                },
                new
                {
                    CustomerModelCustomerId = 2,
                    Id = 2,
                    sortkeyCode = "SRT02",
                    sortkeyName = "เรียงตามยอดขาย",
                    sortkeyDes = "จัดเรียงตามยอดขายสูงสุด"
                }
            );




            modelBuilder.Entity<CustomerModel>().OwnsOne(c => c.AccountCode).HasData(
                new
                {
                    CustomerModelCustomerId = 1,
                    AccountId = 1,
                    AccountCode = "1001",
                    AccountName = "เงินสด",
                    AccountType = "สินทรัพย์",
                    Description = "เงินสดในมือ"
                },
                new
                {
                    CustomerModelCustomerId = 2,
                    AccountId = 2,
                    AccountCode = "2001",
                    AccountName = "เจ้าหนี้",
                    AccountType = "หนี้สิน",
                    Description = "เจ้าหนี้การค้า"
                }
            );



            modelBuilder.Entity<CustomerModel>().OwnsOne(c => c.accountGroup).HasData(
                new
                {
                    CustomerModelCustomerId = 1,
                    id = 1,
                    accGroupCode = "AG001",
                    accGroupName = "กลุ่มค้าปลีก",
                    accGroupDes = "กลุ่มลูกค้าที่ซื้อสินค้าราคาปลีก"
                },
                new
                {
                    CustomerModelCustomerId = 2,
                    id = 2,
                    accGroupCode = "AG002",
                    accGroupName = "กลุ่มค้าส่ง",
                    accGroupDes = "กลุ่มลูกค้าที่ซื้อสินค้าปริมาณมาก"
                }
            );







            modelBuilder.Entity<CustomerModel>().OwnsOne(c => c.Company).HasData(
                new
                {
                    CustomerModelCustomerId = 1,
                    company_id = 1,
                    companyCode = "COM001",
                    companyName = "บริษัทรุ่งเรืองพาณิชย์ จำกัด",
                    companyAddr = "123 ถนนสุขุมวิท กรุงเทพฯ"
                },
                new
                {
                    CustomerModelCustomerId = 2,
                    company_id = 2,
                    companyCode = "COM002",
                    companyName = "บริษัทไทยรุ่งเรือง",
                    companyAddr = "99 ถนนรัชดา เขตห้วยขวาง กรุงเทพฯ"
                }
            );

            modelBuilder.Entity<CustomerModel>().OwnsOne(c => c.SaleOrg).HasData(
                new
                {
                    CustomerModelCustomerId = 1,
                    id = 1,
                    saleOrgCode = "SORG001",
                    saleOrgName = "ฝ่ายขายกรุงเทพ",
                    saleOrgDes = "ดูแลยอดขายในพื้นที่กรุงเทพฯ"
                },
                new
                {
                    CustomerModelCustomerId = 2,
                    id = 2,
                    saleOrgCode = "SORG002",
                    saleOrgName = "ฝ่ายขายภาคเหนือ",
                    saleOrgDes = "ดูแลยอดขายในภาคเหนือ"
                }
            );



            modelBuilder.Entity<CustomerModel>().OwnsOne(c => c.IndustryType).HasData(
                new
                {
                    CustomerModelCustomerId = 1,
                    id = 1,
                    InduTypeCode = "IND001",
                    InduTypeName = "ยานยนต์",
                    InduTypeDes = "อุตสาหกรรมผลิตชิ้นส่วนยานยนต์"
                },
                new
                {
                    CustomerModelCustomerId = 2,
                    id = 2,
                    InduTypeCode = "IND002",
                    InduTypeName = "เครื่องใช้ไฟฟ้า",
                    InduTypeDes = "อุตสาหกรรมอุปกรณ์อิเล็กทรอนิกส์"
                }
            );



            modelBuilder.Entity<CustomerModel>().OwnsOne(c => c.ShopType).HasData(
                new
                {
                    CustomerModelCustomerId = 1,
                    id = 1,
                    shopCode = "SHOP001",
                    shopName = "ร้านค้าหลัก",
                    shopDes = "ร้านขายปลีกหลักของบริษัท",
                    accGroupName = "กลุ่มค้าปลีก"
                },
                new
                {
                    CustomerModelCustomerId = 2,
                    id = 2,
                    shopCode = "SHOP002",
                    shopName = "ร้านค้าส่ง",
                    shopDes = "ร้านขายส่งสำหรับตัวแทน",
                    accGroupName = "กลุ่มค้าส่ง"
                }
            );


            // Seed Business Types
            modelBuilder.Entity<BusinessTypeModel>().HasData(
                new BusinessTypeModel
                {
                    busiTypeID = 1,
                    busiTypeName = "Retail",
                    busiTypeCode = "RT",
                    busiTypeDes = "Retail Business Type Description"  // เพิ่มค่า busiTypeDes
                }
            );



            // Seed Country
            modelBuilder.Entity<CountryModel>().HasData(
                new CountryModel { CountryId = 1, CountryName = "Thailand" },
                new CountryModel { CountryId = 2, CountryName = "Japan" }
            );

            // Seed Province
            modelBuilder.Entity<ProvinceModel>().HasData(
                new ProvinceModel { ProvinceId = 1, ProvinceName = "Bangkok", CountryId = 1 },
                new ProvinceModel { ProvinceId = 2, ProvinceName = "Tokyo", CountryId = 2 }
            );

            // Seed Address
            modelBuilder.Entity<AddressModel>().HasData(
                new AddressModel { AddressId = 1, CountryId = 1, ProvinceId = 1 },
                new AddressModel { AddressId = 2, CountryId = 2, ProvinceId = 2 }
            );

            // Seed General
            modelBuilder.Entity<GeneralModel>().HasData(
                new GeneralModel { general_id = 1, generalName = "General A", AddressId = 1 },
                new GeneralModel { general_id = 2, generalName = "General B", AddressId = 2 }
            );

            // Seed Shipping
            modelBuilder.Entity<ShippingModel>().HasData(
                new ShippingModel
                {
                    shipping_id = 1,
                    subDistrict = "บางรัก",
                    ProvinceId = 1
                },
                new ShippingModel
                {
                    shipping_id = 2,
                    subDistrict = "ห้วยขวาง",
                    ProvinceId = 2
                }
            );
            // Seed CreditInfo
            modelBuilder.Entity<CreditInfoModel>().HasData(
                new CreditInfoModel
                {
                    CreditInfoId = 1,
                    EstimatedPurchase = 50000.00m,
                    TimeRequired = 12,
                    CreditLimit = 100000.00m,
                }
            );
            // CustomerSignModel
            modelBuilder.Entity<CustomerSignModel>().HasData(
                new CustomerSignModel
                {
                    CustSignId = 1,
                    CustSignFirstName = "John",

                }
            );

            modelBuilder.Entity<CustomerModel>().OwnsOne(c => c.ShopType).HasData(
                    new
                    {
                        CustomerModelCustomerId = 1, // Must match the primary key in CustomerModel
                        id = 1,
                        shopCode = "SHOP001",
                        shopName = "John's Store",
                        shopDes = "A general store",
                        accGroupName = "Retail"
                    }
                );
            // CustomerModel
            modelBuilder.Entity<CustomerModel>().HasData(
                new CustomerModel
                {
                    CustomerId = 1,
                    CustomerName = "John Doe",
                    GeneralId = 1,
                    shipping_id = 1,
                    BusinessTypeId = 1,
                    CreditInfoId = 1,
                    CustSignId = 1,// ✅ ใช้ foreign key โดยตรง

                }
            );




        }
    }
}
