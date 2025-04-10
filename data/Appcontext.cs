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

        public DbSet<General> Generals { get; set; }
        public DbSet<ProvinceModel> Provinces { get; set; }
        public DbSet<AddressModel> Addresses { get; set; }
        public DbSet<CountryModel> Countries { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ความสัมพันธ์ระหว่าง Country และ ThaiProvince
            modelBuilder.Entity<ThaiProvince>()
                .HasOne(tp => tp.Country)
                .WithMany()
                .HasForeignKey(tp => tp.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            //ไม่ต้องการเก็บการอ้างอิงย้อนกลับนี้ก็สามารถใช้ WithMany() ได้
            // Define the relationship between Address and General (one-to-many)
            modelBuilder.Entity<General>()
                .HasOne(g => g.Address)
                .WithMany()  // No need for a collection in AddressModel, as it's one-to-many
                .HasForeignKey(g => g.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Shipping>()
                   .HasOne(s => s.Address)  // One Shipping can have one Address
                   .WithMany()  // One Address can have many Shipping entries
                   .HasForeignKey(s => s.AddressId)
                   .OnDelete(DeleteBehavior.Restrict);

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

            //ค้น หาจังหวัดใน ไทยด้วย id
            modelBuilder.Entity<ProvinceModel>().HasData(
                new ProvinceModel { ProvinceId = 1, ProvinceName = "Bangkok", CountryId = 1 },
                new ProvinceModel { ProvinceId = 2, ProvinceName = "California", CountryId = 2 }
            );

            //สร้างตาราง thai_provinces สำหรับจังหวัดในประเทศไทยโดยเฉพาะ
            // Seed Data สำหรับ ThaiProvince
            modelBuilder.Entity<ThaiProvince>().HasData(
                new ThaiProvince { ThaiProvinceId = 1, ThaiProvinceName = "Bangkok", CountryId = 1 },
                new ThaiProvince { ThaiProvinceId = 2, ThaiProvinceName = "Chiang Mai", CountryId = 1 },
                new ThaiProvince { ThaiProvinceId = 3, ThaiProvinceName = "Phuket", CountryId = 1 }
            );

            modelBuilder.Entity<AddressModel>().HasData(
                new AddressModel { AddressId = 1, Street = "123 Sukhumvit Road", ZipCode = "10110", ProvinceId = 1 },
                new AddressModel { AddressId = 2, Street = "456 Market Street", ZipCode = "94105", ProvinceId = 2 }
            );

            // Seed data for General (optional, add if necessary)
            modelBuilder.Entity<General>().HasData(
                new General { general_id = 1, generalName1 = "General 1", AddressId = 1 },
                new General { general_id = 2, generalName1 = "General 2", AddressId = 2 }
            );

            // Seed data for Shipping (optional, add if necessary)
            modelBuilder.Entity<Shipping>().HasData(
                new Shipping { shipping_id = 1, subDistrict = "Sukhumvit", AddressId = 1 },
                new Shipping { shipping_id = 2, subDistrict = "Market", AddressId = 2 }
            );
        }


    }
}
