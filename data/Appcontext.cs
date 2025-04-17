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
        public DbSet<CompanyModel> Company { get; set; }
        public DbSet<IndustryType> IndustryType { get; set; }
        public DbSet<accountGroupModel> accountGroup { get; set; }
        public DbSet<SaleOrgModel> SaleOrg { get; set; }
        public DbSet<shopTypeModel> shopType { get; set; }
        public DbSet<BusinessTypeDto> BusinessType { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Country
            modelBuilder.Entity<CountryModel>().HasData(
                new CountryModel { CountryId = 1, Name = "Thailand" },
                new CountryModel { CountryId = 2, Name = "Japan" },
                new CountryModel { CountryId = 3, Name = "United States" }
            );

            // Seed Geography (ภูมิภาคต่างๆ)
            modelBuilder.Entity<GeographyModel>().HasData(
                new GeographyModel { GeographyId = 1, GeographyName = "ภาคเหนือ" },
                new GeographyModel { GeographyId = 2, GeographyName = "ภาคกลาง" },
                new GeographyModel { GeographyId = 3, GeographyName = "ภาคตะวันออกเฉียงเหนือ" },
                new GeographyModel { GeographyId = 4, GeographyName = "ภาคใต้" },
                new GeographyModel { GeographyId = 5, GeographyName = "ภาคตะวันตก" },
                new GeographyModel { GeographyId = 6, GeographyName = "ภาคตะวันออก" }
            );

            // Seed ThaiProvince (จังหวัดในประเทศไทย)
            modelBuilder.Entity<ThaiProvince>().HasData(
                new ThaiProvince { ThaiProvinceId = 1, ThaiProvinceName = "Bangkok", CountryId = 1, GeographyId = 2 }, // ภาคกลาง
                new ThaiProvince { ThaiProvinceId = 2, ThaiProvinceName = "Chiang Mai", CountryId = 1, GeographyId = 1 }, // ภาคเหนือ
                new ThaiProvince { ThaiProvinceId = 3, ThaiProvinceName = "Chiang Rai", CountryId = 1, GeographyId = 1 }, // ภาคเหนือ
                new ThaiProvince { ThaiProvinceId = 4, ThaiProvinceName = "Phuket", CountryId = 1, GeographyId = 4 }, // ภาคใต้
                new ThaiProvince { ThaiProvinceId = 5, ThaiProvinceName = "Khon Kaen", CountryId = 1, GeographyId = 3 }, // ภาคตะวันออกเฉียงเหนือ
                new ThaiProvince { ThaiProvinceId = 6, ThaiProvinceName = "Nakhon Ratchasima", CountryId = 1, GeographyId = 3 } // ภาคตะวันออกเฉียงเหนือ
            );

            // Seed ProvinceModel (จังหวัดทั่วโลก)
            modelBuilder.Entity<ProvinceModel>().HasData(
                new ProvinceModel { ProvinceId = 1, ProvinceName = "Bangkok", CountryId = 1, GeographyId = 2 }, // Thailand
                new ProvinceModel { ProvinceId = 2, ProvinceName = "Chiang Mai", CountryId = 1, GeographyId = 1 }, // Thailand
                new ProvinceModel { ProvinceId = 3, ProvinceName = "Tokyo", CountryId = 2, GeographyId = 2 }, // Japan
                new ProvinceModel { ProvinceId = 4, ProvinceName = "Osaka", CountryId = 2, GeographyId = 2 }, // Japan
                new ProvinceModel { ProvinceId = 5, ProvinceName = "California", CountryId = 3, GeographyId = 2 }, // United States
                new ProvinceModel { ProvinceId = 6, ProvinceName = "New York", CountryId = 3, GeographyId = 2 } // United States
            );

            // Seed Address (ที่อยู่)
            modelBuilder.Entity<AddressModel>().HasData(
                new AddressModel { AddressId = 1, Street = "123 Sukhumvit", ZipCode = "10110", ProvinceId = 1 },
                new AddressModel { AddressId = 2, Street = "456 Nimman", ZipCode = "50000", ProvinceId = 2 },
                new AddressModel { AddressId = 3, Street = "789 Shibuya", ZipCode = "150-0002", ProvinceId = 3 }, // Japan
                new AddressModel { AddressId = 4, Street = "101 Manhattan", ZipCode = "10001", ProvinceId = 5 } // United States
            );

            // Seed Shipping (ลบ CountryId)
            modelBuilder.Entity<ShippingModel>().HasData(
                new ShippingModel { shipping_id = 1, subDistrict = "Wattana", ProvinceId = 1 },
                new ShippingModel { shipping_id = 2, subDistrict = "Shibuya", ProvinceId = 3 },
                new ShippingModel { shipping_id = 3, subDistrict = "Brooklyn", ProvinceId = 5 }
            );

            modelBuilder.Entity<BusinessTypeDto>().HasData(
            new BusinessTypeDto
            {
                busiTypeID = 1,
                busiTypeCode = "RET",
                busiTypeName = "ค้าปลีก",
                busiTypeDes = "ธุรกิจค้าปลีกทั่วไป",
                RegistrationDate = new DateTime(2010, 1, 1),
                RegisteredCapital = 1000000m
            },
            new BusinessTypeDto
            {
                busiTypeID = 2,
                busiTypeCode = "WHL",
                busiTypeName = "ค้าส่ง",
                busiTypeDes = "ธุรกิจค้าส่งสินค้า",
                RegistrationDate = new DateTime(2011, 5, 20),
                RegisteredCapital = 5000000.00m
            },
            new BusinessTypeDto
            {
                busiTypeID = 3,
                busiTypeCode = "MFG",
                busiTypeName = "การผลิต",
                busiTypeDes = "โรงงานผลิตสินค้าทั่วไป",
                RegistrationDate = new DateTime(2015, 3, 15),
                RegisteredCapital = 15000000.00m
            },
            new BusinessTypeDto
            {
                busiTypeID = 4,
                busiTypeCode = "SRV",
                busiTypeName = "บริการ",
                busiTypeDes = "บริษัทให้บริการทั่วไป",
                RegistrationDate = new DateTime(2018, 7, 10),
                RegisteredCapital = 2000000.00m
            },
            new BusinessTypeDto
            {
                busiTypeID = 5,
                busiTypeCode = "GOV",
                busiTypeName = "หน่วยงานราชการ",
                busiTypeDes = "องค์กรภาครัฐ",
                RegistrationDate = new DateTime(2005, 11, 1),
                RegisteredCapital = null
            }
        );
        }




    }
}
