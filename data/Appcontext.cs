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

            modelBuilder.Entity<CustomerModel>()
                   .OwnsOne(p => p.SaleDistrict);

            // Seed BusinessType
            modelBuilder.Entity<BusinessTypeModel>().HasData(
               new BusinessTypeModel
               {
                   busiTypeID = 1,
                   busiTypeCode = "BT01",
                   busiTypeName = "ค้าปลีก",
                   busiTypeDes = "ธุรกิจค้าปลีก",
                   RegistrationDate = new DateTime(2025, 1, 1),
                   RegisteredCapital = 1000000m
               }

            );


            modelBuilder.Entity<CustomerModel>().OwnsOne(c => c.IndustryType).HasData(
                new
                {
                    CustomerModelCustomerId = 1,
                    id = 1,
                    InduTypeCode = "AVC2",  // ค่า NULL
                    InduTypeName = "Technology",
                    InduTypeDes = "Industry related to technology"
                }
            );


            // Seed Country
            // ต้อง .OwnsOne แล้วค่อย .HasData
            modelBuilder.Entity<CustomerModel>().OwnsOne(c => c.CustGroupCountry).HasData(
                new
                {
                    CustomerModelCustomerId = 1,  // FK ชื่อ = ParentEntityName + PrimaryKey (EF ตั้งแบบนี้ตาม Convention)
                    Id = 1,
                    CountryCode = "TH",
                    CountryName = "Thailand",
                    CountryDes = "ประเทศในเอเชียตะวันออกเฉียงใต้"
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


            // CustomerModel
            modelBuilder.Entity<CustomerModel>().HasData(
                new CustomerModel
                {
                    CustomerId = 1,
                    CustomerName = "John Doe",
                    GeneralId = 1,
                    shipping_id = 1,
                    busiTypeID = 1,
                    CreditInfoId = 1,
                    CustSignId = 1,// ✅ ใช้ foreign key โดยตรง

                }
            );




        }
    }
}
