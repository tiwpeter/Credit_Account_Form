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
            var names = new List<string>(); // List to store names

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                // Query to select names from the Name table
                command.CommandText = "SELECT name FROM Name;";

                using (var reader = command.ExecuteReader())
                {
                    // Read each row and add name to the list
                    while (reader.Read())
                    {
                        names.Add(reader.GetString(0)); // Assuming the column 'name' is of type text
                    }
                }
            }


            return Ok(names); // Return the list of names
        }
    }
}
