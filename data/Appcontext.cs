using Microsoft.EntityFrameworkCore;
using ModelTest.Controllers;

namespace API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<AddressModel> Address { get; set; }
        public DbSet<CountryModel> Country { get; set; }
        public DbSet<CustomerModel> Customer { get; set; }
        public DbSet<GeneralModel> General { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Country
            modelBuilder.Entity<CountryModel>().HasData(
                new CountryModel
                {
                    CountryId = 1,
                    CountryName = "ไทย"
                }
            );

            // Seed Address (ต้องมี CountryId)
            modelBuilder.Entity<AddressModel>().HasData(
                new AddressModel
                {
                    AddressId = 1,
                    Street = "ถนนพระรามที่ 2",
                    ZipCode = "10150",
                    CountryId = 1
                }
            );

            // Seed General
            modelBuilder.Entity<GeneralModel>().HasData(
                new GeneralModel
                {
                    general_id = 1,
                    generalName = "สมชาย ใจดี",
                    AddressId = 1
                }
            );

            // Seed Customer
            modelBuilder.Entity<CustomerModel>().HasData(
                new CustomerModel
                {
                    CustomerId = 1,
                    CustomerName = "ชานนท์ เทพทวี",
                    GeneralId = 1
                }
            );
        }


    }
}