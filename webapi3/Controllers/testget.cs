using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace testget.Controllers
{
    [ApiController]
    [Route("api/testget")]
    public class TestGetController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=test.db"; // Your database file path

        // GET: api/testget
        [HttpGet]
        public IActionResult GetAll()
        {
            var items = new List<object>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT name_id, name FROM Name;"; // Query ดึงข้อมูลทั้งหมด

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        items.Add(new
                        {
                            id = reader.GetInt32(0),        // name_id
                            name = reader.GetString(1)
                        });
                    }
                }
            }

            return Ok(items); // ส่งกลับเป็น List ของ Object
        }

        // GET: api/testget/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT name_id, name FROM Name WHERE name_id = @id;"; // Query ดึงข้อมูลจาก id ที่ระบุ
                command.Parameters.AddWithValue("@id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var item = new
                        {
                            id = reader.GetInt32(0),        // name_id
                            name = reader.GetString(1)
                        };
                        return Ok(item); // ส่งข้อมูลที่เจอ
                    }
                    else
                    {
                        return NotFound(); // ถ้าไม่พบข้อมูล
                    }
                }
            }
        }
    }
}
