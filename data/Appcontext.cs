using Microsoft.EntityFrameworkCore;
using apiNet8.Models;

namespace API.Data
{
    public class ApplicationDbContext : DbContext  // เปลี่ยนชื่อเป็น ApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<TestnameWWW> TestnameWWW { get; set; }
    }
}
