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
        public DbSet<Shipping> Shippings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ความสัมพันธ์ระหว่าง Country และ ThaiProvince
            modelBuilder.Entity<ThaiProvince>()
                .HasOne(tp => tp.Country)
                .WithMany()
                .HasForeignKey(tp => tp.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            //ไม่ต้องการเก็บการอ้างอิงย้อนกลับนี้ก็สามารถใช้ WithMany() ได้
            // Shipping โยงกับ Province
            modelBuilder.Entity<Shipping>()
                .HasOne(s => s.Province)
                .WithMany()
                .HasForeignKey(s => s.ProvinceId)
                .OnDelete(DeleteBehavior.Restrict);
            // Shipping โยงกับ ประเทศ
            modelBuilder.Entity<Shipping>()
                .HasOne(s => s.Country)
                .WithMany()
                .HasForeignKey(s => s.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            // General กับ  AddressModel
            modelBuilder.Entity<AddressModel>()
                .HasOne(a => a.General)
                .WithOne(g => g.Address)
                .HasForeignKey<AddressModel>(a => a.general_id)
                .OnDelete(DeleteBehavior.Restrict);

            //ตาราง FK ไป ความสัมพันธ์
            //Shipping Province, Country   Shipping อยู่ในจังหวัดและประเทศไหน
            //AddressModel General ที่อยู่ไหนเป็นของ General คนไหน
            //Customer    General, Shipping ลูกค้าคนนี้โยงกับข้อมูลทั่วไปและที่จัดส่งไหน





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
