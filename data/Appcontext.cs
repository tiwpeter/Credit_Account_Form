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
        public DbSet<CustomerModel> Customer { get; set; }

        public DbSet<ProvinceModel> Provinces { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Country
            modelBuilder.Entity<CountryModel>().HasData(
                new CountryModel { CountryId = 1, CountryName = "Thailand" },
                new CountryModel { CountryId = 2, CountryName = "Japan" }
            );

            // Seed Province
            modelBuilder.Entity<ProvinceModel>().HasData(
                new ProvinceModel { ProvinceId = 1, ProvinceName = "Bangkok", CountryId = 1 },
                new ProvinceModel { ProvinceId = 2, ProvinceName = "Chiang Mai", CountryId = 1 },
                new ProvinceModel { ProvinceId = 3, ProvinceName = "Tokyo", CountryId = 2 }
            );

            // Seed Address
            modelBuilder.Entity<AddressModel>().HasData(
                new AddressModel
                {
                    AddressId = 1,
                    Street = "123 Sukhumvit Rd",
                    ZipCode = "10110",
                    ProvinceId = 1
                },
                new AddressModel
                {
                    AddressId = 2,
                    Street = "456 Nimmanhaemin Rd",
                    ZipCode = "50200",
                    ProvinceId = 2
                },
                new AddressModel
                {
                    AddressId = 3,
                    Street = "789 Shibuya",
                    ZipCode = "150-0002",
                    ProvinceId = 3
                }
            );
        }



    }
}