using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using RegisteersTable.Models; // Use the correct namespace for RegisterModel

namespace TestPost.Controllers
{
    [ApiController]
    [Route("api/testPost")]
    public class RegisterController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=test.db"; // Database connection string

        // POST: api/register
        [HttpPost]
        public IActionResult RegisterName([FromBody] NRegisterModel model)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                // Insert name into the Name table
                command.CommandText = @"
                    INSERT INTO Name (name) 
                    VALUES ($name);
                ";

                // Use parameters to prevent SQL injection
                command.Parameters.AddWithValue("$name", model.Name.Name); // Accessing 'Name' properly

                try
                {
                    command.ExecuteNonQuery();
                    return Ok("Name registered successfully.");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error: {ex.Message}");
                }
            }
        }
    }
}
