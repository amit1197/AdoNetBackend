using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System;
using System.Data;


namespace AdoNet.Controllers
{
    //[Route("api/[databasetruncatorcontroller]")]
    //[ApiController]
    public class DatabaseTruncatorController : ControllerBase
    {
        private readonly string connectionString;

        public DatabaseTruncatorController(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void TruncateTable(string tableName)
        {
            string query = "TRUNCATE TABLE " + tableName;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Table '{0}' truncated successfully.", tableName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                }
            }
        }
    }


}


