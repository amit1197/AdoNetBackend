using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using ClosedXML.Excel;

namespace AdoNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportDbToExcelController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public ExportDbToExcelController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("export")]
        public IActionResult ExportToExcel()
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();



                    using (var command = new SqlCommand("SELECT Student_ID, gender, NationalITy, PlaceOfBirth, StageID, GradeID, SectionID, Topic, Semester, Relation, raisedhands, VisITedResources, AnnouncementsView, Discussion, ParentAnsweringSurvey, ParentschoolSatisfaction, StudentAbsenceDays, Student_Marks, Class FROM StudentOriginal", connection))
                    using (var reader = command.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);



                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("StudentOriginal");
                            worksheet.Cell(1, 1).InsertTable(dataTable);
                            worksheet.Columns().AdjustToContents();



                            using (var stream = new System.IO.MemoryStream())
                            {
                                workbook.SaveAs(stream);
                                var content = stream.ToArray();
                                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Students.xlsx");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error exporting data to Excel: " + ex.Message);
            }
        }


    }
}
