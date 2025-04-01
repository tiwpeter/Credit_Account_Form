using System;

namespace demoapi.Models
{
    public class UserModel
    {
        public int Id { get; set; } // รหัสผู้ใช้ (Primary Key)
        public string Name { get; set; } = string.Empty; // ชื่อ
        public string Email { get; set; } = string.Empty; // อีเมล
        public string Password { get; set; } = string.Empty; // รหัสผ่าน (เก็บแบบ raw เฉพาะตัวอย่าง)
    }
}
