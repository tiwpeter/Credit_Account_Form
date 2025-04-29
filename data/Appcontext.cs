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

            modelBuilder.Entity<ShippingModel>()
                  .HasOne(s => s.Province)
                  .WithMany()
                  .HasForeignKey(s => s.ProvinceId)
                  .OnDelete(DeleteBehavior.Restrict); // หรือ DeleteBehavior.SetNull

            modelBuilder.Entity<ShippingModel>()
                .HasOne(s => s.Country)
                .WithMany()
                .HasForeignKey(s => s.CountryId)
                .OnDelete(DeleteBehavior.Restrict); // หรือ DeleteBehavior.SetNull


            // Index
            modelBuilder.Entity<AddressModel>().HasIndex(a => a.CountryId);
            modelBuilder.Entity<AddressModel>().HasIndex(a => a.ProvinceId);
            modelBuilder.Entity<ShippingModel>().HasIndex(a => a.CountryId);
            modelBuilder.Entity<ShippingModel>().HasIndex(a => a.ProvinceId);

            modelBuilder.Entity<CustomerModel>()
             .OwnsOne(c => c.ShopType);

            modelBuilder.Entity<CustomerModel>()
                   .OwnsOne(p => p.SaleDistrict);



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






        }
    }
}
