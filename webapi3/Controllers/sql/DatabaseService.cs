using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

public class DatabaseService
{
    // _connectionString กำหนดการเชื่อมต่อฐานข้อมูล
    private string _connectionString = "Data Source=mydatabase.db;Version=3;";

    // List<Post> การกำหนดรูปแบบข้อมูล(model)ที่เราต้องการดึงจากฐานข้อมูล ไว้ใน GetPosts
    //
    public List<Post> GetPosts()
    {
        //สร้างตัวแปร posts เพื่อใช้เก็บข้อมูลโพสต์ที่ดึงออกมาจากฐานข้อมูล.
        var posts = new List<Post>();

        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Posts";

            //command ใช้ ตัวแปร query ค้นหา และ connection เชื่อมฐานข้อมูล
            //reader = command.ExecuteReader ตัวแปรดำเนินการ เพื่อดึงข้อมูลจากฐานข้อมูลผ่านคำสั่ง SQL
            using (var command = new SQLiteCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                //เรียกใช้reader ด้วย Read
                //ลูปเพื่ออ่านข้อมูลจาก SQLiteDataReader ที่ได้จากคำสั่ง ExecuteReader()
                while (reader.Read())
                {
                    // reader.Read() จะอ่านแถว
                    // 0 คือแถว แรก PostId 
                    posts.Add(new Post
                    {
                        PostId = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Body = reader.GetString(2)
                    });
                }
            }
        }

        return posts;


    }

    // ค้นหาโพสต์โดยใช้ PostId ที่ได้รับเป็นพารามิเตอร์ ถ้ามีโพสต์ที่ตรงกับ PostId จะสร้างอ็อบเจ็กต์ Post และคืนค่าโพสต์นั้น ถ้าไม่มีโพสต์ที่ตรงกันจะคืนค่า null.

    //รับค่า postId หรือ id เพื่อใช้ในการค้นฐานข้อมูล
    public Post GetPostById(int postId)
    {
        Post post = null;

        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Posts WHERE PostId = @PostId";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@PostId", postId); // เพิ่มพารามิเตอร์เพื่อป้องกัน SQL Injection

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read()) // ตรวจสอบว่าอ่านข้อมูลได้
                    {
                        post = new Post
                        {
                            PostId = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Body = reader.GetString(2)
                        };
                    }
                }
            }
        }

        return post; // ถ้าไม่มีข้อมูลคืนค่า null
    }

    //เมธอด เพิ่ม Title และ Body ลงในตาราง Posts
    public bool CreatePost(Post newPost)
    {
        // สร้างตัวแปรเพื่อเก็บผลลัพธ์ว่าโพสต์ถูกเพิ่มหรือไม่
        bool isSuccess = false;

        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();

            // สร้างคำสั่ง SQL เพื่อเพิ่มโพสต์ใหม่
            string query = "INSERT INTO Posts (Title, Body) VALUES (@Title, @Body)";

            using (var command = new SQLiteCommand(query, connection))
            {
                // ป้องกัน SQL Injection โดยการใช้พารามิเตอร์
                //ใช้พารามิเตอร์ชื่อ @Title, @Body เพื่อแทนที่ค่าที่ผู้ใช้ป้อนเข้าสู่ฐานข้อมูล
                //AddWithValue() เป็นวิธีการเพิ่มพารามิเตอร์ลงในคำสั่ง SQL เพื่อป้องกัน SQL Injection
                //ใช้ Add() แทนถ้าต้องการกำหนดประเภทข้อมูลเอง
                command.Parameters.AddWithValue("@Title", newPost.Title);
                command.Parameters.AddWithValue("@Body", newPost.Body);

                //@ParameterName → ชื่อพารามิเตอร์ในคำสั่ง SQL ที่ต้องการแทนที่ค่า
                //value → ค่าที่ต้องการใส่ลงในพารามิเตอร์นั้น
                command.Parameters.AddWithValue("@ParameterName", value);


                try
                {
                    // ดำเนินการคำสั่ง SQL
                    command.ExecuteNonQuery();
                    isSuccess = true;  // ถ้าคำสั่งทำงานสำเร็จ
                }
                catch (Exception ex)
                {
                    // ถ้ามีข้อผิดพลาดเกิดขึ้น
                    Console.WriteLine(ex.Message);
                    isSuccess = false;
                }
            }
        }

        return isSuccess;  // คืนค่าผลลัพธ์การเพิ่มโพสต์
    }


}
public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
}
