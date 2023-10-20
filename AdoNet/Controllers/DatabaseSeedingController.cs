using AdoNet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Formats.Asn1;
using System.Globalization;
using AdoNet.Controllers;
using CsvHelper;
using CsvHelper.Configuration;
using static CsvHelper.CsvReader;
using static CsvHelper.CsvWriter;
using static CsvHelper.CsvParser;


namespace AdoNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseSeedingController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public DatabaseSeedingController(IConfiguration configuration)
        { 
            _configuration = configuration;
        }

        [Route("ImportCsv")]
        [HttpPost]
        public IActionResult ImportCsv(IFormFile file)
        {
            if (file == null)
            {
                return BadRequest("No file uploaded.");
            }



            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                IEnumerable<Student> csvData = csv.GetRecords<Student>();



                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();



                    foreach (var csvStudent in csvData)
                    {
                        if (!IsDuplicateStudent(connection, csvStudent.StudentID))
                        {
                            InsertCsvStudent(connection, csvStudent);
                        }
                    }



                    connection.Close();
                }
            }



            return Ok("CSV data imported successfully.");
        }
        private bool IsDuplicateStudent(SqlConnection connection, string studentId)
        {
            using (var command = new SqlCommand("SELECT COUNT(*) FROM StudentOriginal WHERE Student_ID = @Student_ID", connection))
            {
                command.Parameters.AddWithValue("@Student_ID", studentId);
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }



        private void InsertCsvStudent(SqlConnection connection, Student csvStudent)
        {
            using (var command = new SqlCommand("INSERT INTO StudentOriginal VALUES (@Student_ID, @Gender, @NationalITy, @PlaceofBirth, @StageID, @GradeID, @SectionID, @Topic, @Semester, @Relation, @raisedhands, @VisITedResources, @AnnouncementsView, @Discussion, @ParentAnsweringSurvey, @ParentschoolSatisfaction, @StudentAbsenceDays, @Student_Marks, @Class)", connection))
            {
                command.Parameters.AddWithValue("@Student_ID", csvStudent.StudentID);
                command.Parameters.AddWithValue("@Gender", csvStudent.Gender);
                command.Parameters.AddWithValue("@NationalITy", csvStudent.Nationality);
                command.Parameters.AddWithValue("@PlaceofBirth", csvStudent.PlaceOfBirth);
                command.Parameters.AddWithValue("@StageID", csvStudent.StageID);
                command.Parameters.AddWithValue("@GradeID", csvStudent.GradeID);
                command.Parameters.AddWithValue("@SectionID", csvStudent.SectionID);
                command.Parameters.AddWithValue("@Topic", csvStudent.Topic);
                command.Parameters.AddWithValue("@Semester", csvStudent.Semester);
                command.Parameters.AddWithValue("@Relation", csvStudent.Relation);
                command.Parameters.AddWithValue("@raisedhands", csvStudent.RaisedHands);
                command.Parameters.AddWithValue("@VisITedResources", csvStudent.VisitedResources);
                command.Parameters.AddWithValue("@AnnouncementsView", csvStudent.AnnouncementsView);
                command.Parameters.AddWithValue("@Discussion", csvStudent.Discussion);
                command.Parameters.AddWithValue("@ParentAnsweringSurvey", csvStudent.ParentAnsweringSurvey);
                command.Parameters.AddWithValue("@ParentschoolSatisfaction", csvStudent.ParentSchoolSatisfaction);
                command.Parameters.AddWithValue("@StudentAbsenceDays", csvStudent.StudentAbsenceDays);
                command.Parameters.AddWithValue("@Student_Marks", csvStudent.StudentMarks);
                command.Parameters.AddWithValue("@Class", csvStudent.Class);
                command.ExecuteNonQuery();
            }
        }
    }
}


