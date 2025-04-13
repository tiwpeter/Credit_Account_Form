using Microsoft.EntityFrameworkCore;
using ModelTest.Controllers;

namespace API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ThaiProvince> ThaiProvinces { get; set; }
        public DbSet<ProvinceModel> Provinces { get; set; }
        public DbSet<AddressModel> Addresses { get; set; }
        public DbSet<CountryModel> Countries { get; set; }
        public DbSet<ShippingModel> Shippings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Country
            modelBuilder.Entity<CountryModel>().HasData(
                new CountryModel { CountryId = 1, Name = "Thailand" },
                new CountryModel { CountryId = 2, Name = "Japan" }
            );

            // Seed Province
            modelBuilder.Entity<ProvinceModel>().HasData(
                new ProvinceModel { ProvinceId = 1, ProvinceName = "Bangkok", CountryId = 1 },
                new ProvinceModel { ProvinceId = 2, ProvinceName = "Chiang Mai", CountryId = 1 },
                new ProvinceModel { ProvinceId = 3, ProvinceName = "Tokyo", CountryId = 2 }
            );

            // Seed Address
            modelBuilder.Entity<AddressModel>().HasData(
                new AddressModel { AddressId = 1, Street = "123 Sukhumvit", ZipCode = "10110", ProvinceId = 1 },
                new AddressModel { AddressId = 2, Street = "456 Nimman", ZipCode = "50000", ProvinceId = 2 }
            );

            // Seed Shipping
            modelBuilder.Entity<ShippingModel>().HasData(
                new ShippingModel { shipping_id = 1, subDistrict = "Wattana", ProvinceId = 1, CountryId = 1 },
                new ShippingModel { shipping_id = 2, subDistrict = "Shibuya", ProvinceId = 3, CountryId = 2 }
            );
        }

    }
}
