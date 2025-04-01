using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        // ข้อมูลโพสต์สมมุติ (hardcoded data)
        //arrays
        //object
        //ใช้ List<Post> เพื่อเก็บข้อมูลโพสต์หลายๆ รายการในตัวแปร posts
        private List<Post> posts = new List<Post>
        {
            //คลาส เก็บข้อมูล Post และ User เป็น โมเดล ที่ถูกสร้างขึ้นในโค้ดเพื่อเก็บข้อมูลเกี่ยวกับโพสต์และผู้ใช้
            new Post
            {
                PostId = 1,
                Title = "First Post",
                Body = "This is the first post body.",
                User = new User { UserId = 1, FullName = "สมชาย ใจดี", AccountNumber = "123-456-7890", AccountType = "ออมทรัพย์", Branch = "กรุงเทพฯ" }
            },
            new Post
            {
                PostId = 2,
                Title = "Second Post",
                Body = "This is the second post body.",
                User = new User { UserId = 2, FullName = "สมหญิง ใจดี", AccountNumber = "987-654-3210", AccountType = "ออมทรัพย์", Branch = "เชียงใหม่" }
            },
            new Post
            {
                PostId = 3,
                Title = "Third Post",
                Body = "This is the third post body.",
                User = new User { UserId = 3, FullName = "สมพร ใจดี", AccountNumber = "555-123-4567", AccountType = "กระแสรายวัน", Branch = "ภูเก็ต" }
            }
        };

        // GET: api/posts
        [HttpGet]
        public ActionResult<IEnumerable<Post>> GetUser()
        {
            //ค้นหาposts  
            if (posts == null || posts.Count == 0)
            {
                return NotFound("No posts found"); // ส่งกลับ NotFound ถ้าไม่มีโพสต์
            }

            return Ok(posts); // ส่งกลับข้อมูล posts โพสต์ทั้งหมดในรูปแบบ JSON
        }

        // GET: api/posts/{id}
        [HttpGet("{id}")]
        //GetById ตัวแปร หา id จาก post
        public ActionResult<Post> GetById(int id)
        {
            // ค้นหาข้อมูลโพสต์ที่มี id ตรงกับที่ผู้ใช้ร้องขอ
            var post = posts.FirstOrDefault(p => p.PostId == id);

            if (post == null)
            {
                return NotFound($"Post with ID {id} not found"); // ส่งกลับ NotFound ถ้าไม่พบโพสต์
            }

            // ถ้าพบโพสต์ที่มี id ตรงกับที่ร้องขอ, จะส่งกลับโพสต์นั้นในรูปแบบ HTTP 200 OK
            return Ok(post); // ส่งกลับข้อมูลโพสต์ที่มี id ตรงกับที่ร้องขอ
        }
    }

    // โมเดลข้อมูลโพสต์
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        // User เป็นคลาสที่สร้างขึ้นมาเองเพื่อเก็บข้อมูลของผู้ใช้).
        public User User { get; set; } // ข้อมูลผู้ใช้
    }

    // โมเดลข้อมูลผู้ใช้
    public class User
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public string Branch { get; set; }
    }
}
