using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System;
using System.Data;
using Microsoft.Extensions.Configuration;


namespace adonet.controllers
{
    //[route("api/[databasetruncatorcontroller]")]
    //[apicontroller]
    public class DatabaseTruncatorController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public DatabaseTruncatorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpDelete("database")]
        public IActionResult TruncateDatabase()
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");



                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();




                    using (var command = new SqlCommand("TRUNCATE TABLE StudentOriginal", connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }



                return Ok("Database truncated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to truncate the database: {ex.Message}");
            }
        }
    }


}


