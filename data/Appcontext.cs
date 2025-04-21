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
        }

    }
}