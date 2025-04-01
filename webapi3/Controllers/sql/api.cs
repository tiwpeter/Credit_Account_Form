using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    //เรียกใช้ตัว แปรร DatabaseService จาก DatabaseService.cs
    private readonly DatabaseService _databaseService;

    public PostsController()
    {
        _databaseService = new DatabaseService();
    }

    // GET: api/posts
    [HttpGet]
    public ActionResult<IEnumerable<Post>> GetPosts()
    {
        // เรียกใช้เมธอด GetPosts จาก DatabaseService เพื่อดึงข้อมูลโพสต์จากฐานข้อมูล
        var posts = _databaseService.GetPosts();

        //ถ้าผลลัพธ์ที่ได้ (posts) เป็น null หรือจำนวนโพสต์มีค่าเป็น 0 (ไม่มีโพสต์ในฐานข้อมูล)
        if (posts == null || posts.Count == 0)
        {
            return NotFound("No posts found");
        }

        return Ok(posts);
    }
}
