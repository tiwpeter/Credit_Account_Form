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

            // Seed Business Types
            modelBuilder.Entity<BusinessTypeModel>().HasData(
                new BusinessTypeModel { busiTypeID = 1, busiTypeName = "Retail" },
                new BusinessTypeModel { busiTypeID = 2, busiTypeName = "Wholesale" },
                new BusinessTypeModel { busiTypeID = 3, busiTypeName = "Service" }
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

            // Seed Customer (รวม FK จาก General และ Shipping)
            modelBuilder.Entity<CustomerModel>().HasData(
                new CustomerModel
                {
                    CustomerId = 1,
                    CustomerName = "John Doe",
                    GeneralId = 1, // Assuming there's an existing General record with Id=1
                    shipping_id = 1, // Assuming there's an existing Shipping record with Id=1
                    BusinessTypeId = 1, // Retail BusinessType
                                        // CreditInfo can be set directly as a navigation property
                    CreditInfo = new CreditInfoModel
                    {
                        CreditInfoId = 1,
                        EstimatedPurchase = 50000.00m,
                        TimeRequired = 12,
                        CreditLimit = 100000.00m
                    }
                },
                new CustomerModel
                {
                    CustomerId = 2,
                    CustomerName = "สมหญิง",
                    GeneralId = 2,
                    shipping_id = 2
                }
            );
        }
    }
}
