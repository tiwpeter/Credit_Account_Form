using Microsoft.EntityFrameworkCore;
using apiNet8.Models;

namespace API.Data
{
    public class ApplicationDbContext : DbContext  // เปลี่ยนชื่อเป็น ApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        //คือการแทนข้อมูลของตารางในฐานข้อมูล โดยที่แต่ละ DbSet จะสอดคล้องกับคลาสโมเดลหนึ่งๆ ซึ่งจะช่วยให้คุณสามารถทำการเข้าถึงและจัดการข้อมูลในฐานข้อมูลได้โดยตรงผ่านโค้ด C#.
        public DbSet<TestnameWWW> TestnameWWW { get; set; }
    }
}
