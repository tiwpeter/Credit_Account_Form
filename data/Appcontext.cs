using Microsoft.EntityFrameworkCore;
using ModelTest.Controllers;

namespace API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<AddressModel> Addresses { get; set; }
        public DbSet<CountryModel> Country { get; set; }

        public DbSet<ProvinceModel> Provinces { get; set; }
        public DbSet<CountryModel> Countries { get; set; }
        public DbSet<GeographyModel> Geography { get; set; }
        public DbSet<CustomerModel> Customers { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // แก้ Multiple Cascade Path
            modelBuilder.Entity<AddressModel>()
                .HasOne(a => a.Country)
                .WithMany()
                .HasForeignKey(a => a.CountryId)
                .OnDelete(DeleteBehavior.Restrict); // 👈 สำคัญ

            modelBuilder.Entity<AddressModel>()
                .HasOne(a => a.Province)
                .WithMany()
                .HasForeignKey(a => a.ProvinceId)
                .OnDelete(DeleteBehavior.Restrict); // หรือ SetNull ตามที่คุณต้องการ

            // Index
            modelBuilder.Entity<AddressModel>()
                .HasIndex(a => a.CountryId);

            modelBuilder.Entity<AddressModel>()
                .HasIndex(a => a.ProvinceId);


            // Geography
            modelBuilder.Entity<GeographyModel>().HasData(
                new GeographyModel { GeographyId = 1, GeographyName = "ภาคกลาง" },
                new GeographyModel { GeographyId = 2, GeographyName = "ภาคเหนือ" }
            );

            // Country
            modelBuilder.Entity<CountryModel>().HasData(
                new CountryModel { CountryId = 1, CountryName = "Thailand" }
            );

            // Province
            modelBuilder.Entity<ProvinceModel>().HasData(
                new ProvinceModel { ProvinceId = 1, ProvinceName = "กรุงเทพมหานคร", CountryId = 1, GeographyId = 1 },
                new ProvinceModel { ProvinceId = 2, ProvinceName = "เชียงใหม่", CountryId = 1, GeographyId = 2 }
            );

            // Amphure
            modelBuilder.Entity<AmphureModel>().HasData(
                new AmphureModel { AmphureId = 1, AmphureName = "เขตบางรัก", ProvinceId = 1 },
                new AmphureModel { AmphureId = 2, AmphureName = "อำเภอเมืองเชียงใหม่", ProvinceId = 2 }
            );

            // Tambon
            modelBuilder.Entity<TambonModel>().HasData(
                new TambonModel { TambonId = 1, TambonName = "สีลม", AmphureId = 1 },
                new TambonModel { TambonId = 2, TambonName = "สุเทพ", AmphureId = 2 }
            );

            // Address
            modelBuilder.Entity<AddressModel>().HasData(
                new AddressModel { AddressId = 1, Street = "123 ถนนสีลม", ZipCode = "10500", ProvinceId = 1, CountryId = 1 },
                new AddressModel { AddressId = 2, Street = "456 ถ.สุเทพ", ZipCode = "50200", ProvinceId = 2, CountryId = 1 }
            );

            // General
            modelBuilder.Entity<GeneralModel>().HasData(
                new GeneralModel { general_id = 1, generalName = "นายสมชาย", AddressId = 1 },
                new GeneralModel { general_id = 2, generalName = "นางสาวดารา", AddressId = 2 }
            );

            // Customer
            modelBuilder.Entity<CustomerModel>().HasData(
                new CustomerModel { CustomerId = 1, CustomerName = "ลูกค้า A", GeneralId = 1 },
                new CustomerModel { CustomerId = 2, CustomerName = "ลูกค้า B", GeneralId = 2 }
            );
        }

    }
}