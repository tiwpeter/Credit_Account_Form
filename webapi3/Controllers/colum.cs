using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace Table.Controllers
{
    [ApiController]
    [Route("api/table")]
    public class TestColum : ControllerBase
    {
        private readonly string connectionString = "Data Source=test.db";

        // API Endpoint เพื่อดึงชื่อของตารางทั้งหมด
        [HttpGet("tables")]
        public IActionResult GetAllTables()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                
                var command = connection.CreateCommand();
                command.CommandText = "SELECT name FROM sqlite_master WHERE type='table';";

                using (var reader = command.ExecuteReader())
                {
                    var tableNames = new List<string>();
                    while (reader.Read())
                    {
                        tableNames.Add(reader.GetString(0)); // ชื่อของตาราง
                    }

                    return Ok(tableNames); // ส่งคืนข้อมูลเป็น JSON
                }
            }
        }
    }
}
