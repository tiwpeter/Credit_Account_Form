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
        public IActionResult Get()
        {
            var items = new List<object>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT id, name FROM Name;"; // ✅ เอา id มาด้วย

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        items.Add(new
                        {
                            id = reader.GetInt32(0),    // id
                            name = reader.GetString(1)  // name
                        });
                    }
                }
            }

            return Ok(items); // ✅ return เป็น list ของ object
        }

    }
}
