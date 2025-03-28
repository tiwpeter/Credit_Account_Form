using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace CreateTable.Controllers
{
    [ApiController]
    [Route("api/createtable")]
    public class TestCreateTable : ControllerBase
    {
        private readonly string connectionString = "Data Source=test.db";

        // POST: api/table/createTable
        [HttpPost("test")]
        public IActionResult CreateTable()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = @"
PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS Name (
    name_id INTEGER PRIMARY KEY,
    name TEXT
);
";

                command.ExecuteNonQuery();
            }

            return Ok("Tables created successfully.");
        }
    }
}