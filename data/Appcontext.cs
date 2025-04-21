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
        public DbSet<CountryModel> Countries { get; set; }
        public DbSet<GeographyModel> Geography { get; set; }
        public DbSet<AmphureModel> Amphures { get; set; }
        public DbSet<Tambon> Tambons { get; set; }


        public static class SeedData
        {
            public static void Initialize(ApplicationDbContext context)
            {
                // --- Seed Geography ---
                if (!context.Geography.Any())
                {
                    var geographies = new List<GeographyModel>
            {
                new GeographyModel { GeographyName = "ภาคกลาง" }, // ID 1
                new GeographyModel { GeographyName = "ภาคเหนือ" }  // ID 2
            };
                    context.Geography.AddRange(geographies);
                    context.SaveChanges();
                }

                // --- Seed Countries ---
                if (!context.Countries.Any())
                {
                    var countries = new List<CountryModel>
            {
                new CountryModel { CountryName = "ประเทศไทย" } // ID 1
            };
                    context.Countries.AddRange(countries);
                    context.SaveChanges();
                }

                // --- Seed Provinces ---
                if (!context.Provinces.Any())
                {
                    var provinces = new List<ProvinceModel>
            {
                new ProvinceModel
                {
                    ProvinceName = "กรุงเทพมหานคร",
                    CountryId = 1,
                    GeographyId = 1,
                    Amphures = new List<AmphureModel>
                    {
                        new AmphureModel { AmphureName = "เขตพระนคร" },
                        new AmphureModel { AmphureName = "เขตดุสิต" }
                    }
                },
                new ProvinceModel
                {
                    ProvinceName = "เชียงใหม่",
                    CountryId = 1,
                    GeographyId = 2,
                    Amphures = new List<AmphureModel>
                    {
                        new AmphureModel { AmphureName = "เมืองเชียงใหม่" },
                        new AmphureModel { AmphureName = "แม่ริม" }
                    }
                }
            };

                    context.Provinces.AddRange(provinces);
                    context.SaveChanges();
                }

                // --- Seed Tambons ---
                if (!context.Tambons.Any())
                {
                    var amphure = context.Amphures.ToList();

                    var tambons = new List<Tambon>
            {
                new Tambon { TambonName = "วัดสามพระยา", AmphureId = amphure.First(a => a.AmphureName == "เขตพระนคร").AmphureId },
                new Tambon { TambonName = "ดุสิต", AmphureId = amphure.First(a => a.AmphureName == "เขตดุสิต").AmphureId },
                new Tambon { TambonName = "ศรีวิชัย", AmphureId = amphure.First(a => a.AmphureName == "เมืองเชียงใหม่").AmphureId },
                new Tambon { TambonName = "ริมใต้", AmphureId = amphure.First(a => a.AmphureName == "แม่ริม").AmphureId }
            };

                    context.Tambons.AddRange(tambons);
                    context.SaveChanges();
                }

                // --- Seed Addresses ---
                if (!context.Addresses.Any())
                {
                    var provinces = context.Provinces.ToList();

                    var addresses = new List<AddressModel>
            {
                new AddressModel { Street = "ถนนราชดำเนิน", ZipCode = "10200", ProvinceId = provinces.First(p => p.ProvinceName == "กรุงเทพมหานคร").ProvinceId },
                new AddressModel { Street = "ถนนสุเทพ", ZipCode = "50200", ProvinceId = provinces.First(p => p.ProvinceName == "เชียงใหม่").ProvinceId }
            };

                    context.Addresses.AddRange(addresses);
                    context.SaveChanges();
                }
            }
        }

    }

}