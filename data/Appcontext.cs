using Microsoft.EntityFrameworkCore;
using ModelTest.Controllers;

namespace API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<GeneralModel> Generals { get; set; }
        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<CountryModel> Countries { get; set; }
        public DbSet<AddressModel> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Country seed
            modelBuilder.Entity<CountryModel>().HasData(
                new CountryModel { CountryId = 1, CountryName = "Thailand" },
                new CountryModel { CountryId = 2, CountryName = "Japan" }
            );

            // Address seed
            modelBuilder.Entity<AddressModel>().HasData(
                new AddressModel { AddressId = 1, CustomerName = "John Doe", CountryId = 1 },
                new AddressModel { AddressId = 2, CustomerName = "Sakura Tanaka", CountryId = 2 }
            );

            // General seed
            modelBuilder.Entity<GeneralModel>().HasData(
                new GeneralModel { general_id = 1, generalName = "General A", AddressId = 1 },
                new GeneralModel { general_id = 2, generalName = "General B", AddressId = 2 }
            );

            // Customer seed
            modelBuilder.Entity<CustomerModel>().HasData(
                new CustomerModel { CustomerId = 1, CustomerName = "Customer A", GeneralId = 1 },
                new CustomerModel { CustomerId = 2, CustomerName = "Customer B", GeneralId = 2 }
            );
        }
    }
}
