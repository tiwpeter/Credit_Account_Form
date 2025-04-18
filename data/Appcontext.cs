using Microsoft.EntityFrameworkCore;
using ModelTest.Controllers;

namespace API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<RegisformModel> Regisforms { get; set; }

        public DbSet<ThaiProvince> ThaiProvinces { get; set; }
        public DbSet<ProvinceModel> Provinces { get; set; }
        public DbSet<AddressModel> Addresses { get; set; }
        public DbSet<CountryModel> Countries { get; set; }
        public DbSet<ShippingModel> Shippings { get; set; }
        public DbSet<CompanyModel> Company { get; set; }
        public DbSet<IndustryType> IndustryType { get; set; }
        public DbSet<accountGroupModel> accountGroup { get; set; }
        public DbSet<SaleOrgModel> SaleOrg { get; set; }
        public DbSet<shopTypeModel> shopType { get; set; }
        public DbSet<BusinessTypeDto> BusinessType { get; set; }
        public DbSet<RegisterFrom> RegisterForms { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // --- Geography
            modelBuilder.Entity<GeographyModel>().HasData(
                new GeographyModel { GeographyId = 1, GeographyName = "ภาคเหนือ" },
                new GeographyModel { GeographyId = 2, GeographyName = "ภาคกลาง" }
            );

            // --- Country
            modelBuilder.Entity<CountryModel>().HasData(
                new CountryModel { CountryId = 1, CountryName = "ประเทศไทย" },
                new CountryModel { CountryId = 2, CountryName = "สหรัฐอเมริกา" }
            );

            // --- ThaiProvince
            modelBuilder.Entity<ThaiProvince>().HasData(
                new ThaiProvince
                {
                    ThaiProvinceId = 1,
                    ThaiProvinceName = "เชียงใหม่",
                    CountryId = 1,
                    GeographyId = 1
                },
                new ThaiProvince
                {
                    ThaiProvinceId = 2,
                    ThaiProvinceName = "กรุงเทพมหานคร",
                    CountryId = 1,
                    GeographyId = 2
                }
            );

            // --- ProvinceModel (ทั่วไป)
            modelBuilder.Entity<ProvinceModel>().HasData(
                new ProvinceModel
                {
                    ProvinceId = 1,
                    ProvinceName = "California",
                    CountryId = 2,
                    GeographyId = 2
                },
                new ProvinceModel
                {
                    ProvinceId = 2,
                    ProvinceName = "New York",
                    CountryId = 2,
                    GeographyId = 2
                }
            );

            // --- AddressModel
            modelBuilder.Entity<AddressModel>().HasData(
                new AddressModel
                {
                    AddressId = 1,
                    Street = "123 ถนนราชดำเนิน",
                    CountryId = 1,
                    ThaiProvinceId = 2
                },
                new AddressModel
                {
                    AddressId = 2,
                    Street = "456 Sunset Blvd",
                    CountryId = 2,
                    ProvinceId = 1
                }
            );
            // Seed Shipping
            modelBuilder.Entity<ShippingModel>().HasData(
                new ShippingModel { shipping_id = 1, subDistrict = "Wang Thonglang", ProvinceId = 1 },
                new ShippingModel { shipping_id = 2, subDistrict = "Mueang Chiang Mai", ProvinceId = 2 }
            );


            // --- RegisformModel
            // Seed Regisform
            modelBuilder.Entity<RegisformModel>().HasData(
                new RegisformModel { Id = 1, AddressId = 1, shipping_id = 1 },
                new RegisformModel { Id = 2, AddressId = 2, shipping_id = 2 }
            );
        }




    }
}
