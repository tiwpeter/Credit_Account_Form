using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace MyApp.Controllers
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        // ประกาศ model
        public DbSet<YourEntity> YourEntities { get; set; }
    }

    public class DatabaseController : ControllerBase
    {
        private readonly MyDbContext _context;

        public DatabaseController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet("add-table")]
        public async Task<IActionResult> AddTable()
        {
            try
            {
                // Run the SQL command to create a new table
                _context.Database.ExecuteSqlRaw("CREATE TABLE NewTable (Id INT PRIMARY KEY, Name NVARCHAR(100))");
                await _context.SaveChangesAsync();

                return Ok("Table added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error occurred: {ex.Message}");
            }
        }
    }
}
public class YourEntity
{
    public int Id { get; set; }   // คอลัมน์ที่เป็น Primary Key
    public string Name { get; set; }  // คอลัมน์ที่เก็บข้อมูลชื่อ
}
