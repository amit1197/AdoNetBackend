using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;

namespace AdoNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportDbController : ControllerBase
    {
       

        [HttpGet]
        [Route("ExportToExcel")]
        public IActionResult ExportToExcel()
        {
            // Create a new Excel package
            using (var package = new ExcelPackage())
            {
                // Add a worksheet
                var worksheet = package.Workbook.Worksheets.Add("Students");

                // Retrieve data from the database (you can use your existing method)
                var students = GetStudentsFromDatabase();

                // Define column headers
                var headers = new string[] { "StudentID", "Gender", "Nationality", /* ... other headers ... */ };
                for (var col = 1; col <= headers.Length; col++)
                {
                    worksheet.Cells[1, col].Value = headers[col - 1];
                    worksheet.Cells[1, col].Style.Font.Bold = true;
                }

                // Populate data
                for (var row = 2; row <= students.Count + 1; row++)
                {
                    var student = students[row - 2]; // Adjust the index as needed
                    worksheet.Cells[row, 1].Value = student.StudentID;
                    worksheet.Cells[row, 2].Value = student.Gender;
                    // ... populate other columns ...
                }

                // Auto-fit columns
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Prepare the stream for Excel file download
                var stream = new MemoryStream(package.GetAsByteArray());
                stream.Seek(0, SeekOrigin.Begin);

                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Students.xlsx");
            }
        }

    }
}
