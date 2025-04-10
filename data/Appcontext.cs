using Microsoft.EntityFrameworkCore;
using ModelTest.Controllers;

namespace API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ProvinceModel> Provinces { get; set; }
        public DbSet<AddressModel> Addresses { get; set; }
        public DbSet<CountryModel> Countries { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Address to Province relationship
            modelBuilder.Entity<AddressModel>()
                .HasOne(a => a.Province) // Each Address has one Province
                .WithMany()  // One Province can have many Addresses
                .HasForeignKey(a => a.ProvinceId) // ProvinceId is the foreign key in Address
                .OnDelete(DeleteBehavior.Restrict);  // Don't delete Address when Province is deleted

            // Configure Province to Country relationship
            modelBuilder.Entity<ProvinceModel>()
                .HasOne(p => p.Country) // Each Province has one Country
                .WithMany() // One Country can have many Provinces
                .HasForeignKey(p => p.CountryId) // CountryId is the foreign key in Province
                .OnDelete(DeleteBehavior.Restrict); // Don't delete Province when Country is deleted

            // Seed data
            modelBuilder.Entity<CountryModel>().HasData(
                new CountryModel { CountryId = 1, Name = "Thailand" },
                new CountryModel { CountryId = 2, Name = "USA" }
            );

            modelBuilder.Entity<ProvinceModel>().HasData(
                new ProvinceModel { ProvinceId = 1, ProvinceName = "Bangkok", CountryId = 1 },
                new ProvinceModel { ProvinceId = 2, ProvinceName = "California", CountryId = 2 }
            );

            modelBuilder.Entity<AddressModel>().HasData(
                new AddressModel { AddressId = 1, Street = "123 Sukhumvit Road", ZipCode = "10110", ProvinceId = 1 },
                new AddressModel { AddressId = 2, Street = "456 Market Street", ZipCode = "94105", ProvinceId = 2 }
            );
        }


    }
}
