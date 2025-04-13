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

            modelBuilder.Entity<GeographyModel>().HasData(
     new GeographyModel { GeographyId = 1, GeographyName = "ภาคเหนือ" },
     new GeographyModel { GeographyId = 2, GeographyName = "ภาคกลาง" },
     new GeographyModel { GeographyId = 3, GeographyName = "ภาคตะวันออกเฉียงเหนือ" },
     new GeographyModel { GeographyId = 4, GeographyName = "ภาคใต้" },
     new GeographyModel { GeographyId = 5, GeographyName = "ภาคตะวันตก" },
     new GeographyModel { GeographyId = 6, GeographyName = "ภาคตะวันออก" }
 );

            modelBuilder.Entity<ProvinceModel>().HasData(
                new ProvinceModel { ProvinceId = 1, ProvinceName = "Bangkok", CountryId = 1, GeographyId = 2 }, // ภาคกลาง
                new ProvinceModel { ProvinceId = 2, ProvinceName = "Chiang Mai", CountryId = 1, GeographyId = 1 }, // ภาคเหนือ
                new ProvinceModel { ProvinceId = 3, ProvinceName = "Tokyo", CountryId = 2, GeographyId = 2 } // ภาคกลาง (Japan)
            );


        }



    }
}
