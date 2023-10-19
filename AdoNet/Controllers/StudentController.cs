using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using AdoNet.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace AdoNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IConfiguration _configuration;



        public StudentController(IConfiguration configuration)

        {

            _configuration = configuration;

        }
        SqlConnection connString;
        SqlCommand cmd;
        SqlDataAdapter adap;
        DataTable dtb;



        [Route("GetAllStudents")]

        [HttpGet]

        public async Task<IActionResult> GetAllStudents()

        {

            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            SqlCommand cmd = new SqlCommand("SELECT * FROM StudentOriginal", con);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            adapter.Fill(dt);



            List<Student> lstprofiles = new List<Student>();

            foreach (DataRow dr in dt.Rows)

            {

                lstprofiles.Add(new Student

                {

                    StudentID = dr["Student_ID"].ToString(),

                    Gender = dr["gender"].ToString(),

                    Nationality = dr["NationalITy"].ToString(),

                    PlaceOfBirth = dr["PlaceOfBirth"].ToString(),

                    StageID = dr["StageID"].ToString(),

                    GradeID = dr["GradeID"].ToString(),

                    SectionID = dr["SectionID"].ToString(),

                    Topic = dr["Topic"].ToString(),

                    Semester = dr["Semester"].ToString(),

                    Relation = dr["Relation"].ToString(),

                    RaisedHands =Convert.ToInt32( dr["raisedhands"]),

                    VisitedResources =Convert.ToInt32( dr["VisITedResources"]),

                    AnnouncementsView =Convert.ToInt32( dr["AnnouncementsView"]),

                    Discussion =Convert.ToInt32( dr["Discussion"]),

                    ParentAnsweringSurvey = dr["ParentAnsweringSurvey"].ToString(),

                    ParentSchoolSatisfaction = dr["ParentschoolSatisfaction"].ToString(),

                    StudentAbsenceDays = dr["StudentAbsenceDays"].ToString(),
                       
                    StudentMarks =Convert.ToInt32( dr["Student_Marks"]),

                    Class = dr["Class"].ToString()




                });

            }

            return Ok(lstprofiles);

        }



        [Route("CreateStudent")]

        [HttpPost]

        public async Task<IActionResult> CreateStudent(Student obj)

        {

            try

            {

                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                SqlCommand cmd = new SqlCommand("insert into StudentOriginal values ('" + obj.StudentID + "','" + obj.Gender + "','" + obj.Nationality + "','" + obj.PlaceOfBirth + "','" + obj.StageID + "', '" + obj.GradeID + "','" + obj.SectionID + "' ,'" + obj.Topic + "' ,'" + obj.Semester + "' , '" + obj.Relation + "' , '" + obj.RaisedHands + "','" + obj.VisitedResources + "','" + obj.AnnouncementsView + "','" + obj.Discussion + "', '" + obj.ParentAnsweringSurvey + "', '" + obj.ParentSchoolSatisfaction + "', '" + obj.StudentAbsenceDays + "', '" + obj.StudentMarks + "', '" + obj.Class + "' )", con);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();

                return Ok("Success");

            }

            catch (Exception ex)

            {

                throw ex;

            }

        }

        [Route("FindById")] //GET
        [HttpGet]
        public async Task<IActionResult> FindById(string id)
        {
            connString = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            dtb = new DataTable();
            cmd = new SqlCommand("select * from StudentOriginal where Student_ID=@StudentID", connString);
            cmd.Parameters.AddWithValue("@StudentID ", id);
            try
            {
                connString.Open();
                adap = new SqlDataAdapter(cmd);
                adap.Fill(dtb);
                DataRow dr = dtb.Rows[0];
                Student lstprofile = new Student

                {

                    StudentID = dr["Student_ID"].ToString(),

                    Gender = dr["gender"].ToString(),

                    Nationality = dr["NationalITy"].ToString(),

                    PlaceOfBirth = dr["PlaceOfBirth"].ToString(),

                    StageID = dr["StageID"].ToString(),

                    GradeID = dr["GradeID"].ToString(),

                    SectionID = dr["SectionID"].ToString(),

                    Topic = dr["Topic"].ToString(),

                    Semester = dr["Semester"].ToString(),

                    Relation = dr["Relation"].ToString(),

                    RaisedHands = Convert.ToInt32(dr["raisedhands"]),

                    VisitedResources = Convert.ToInt32(dr["VisITedResources"]),

                    AnnouncementsView = Convert.ToInt32(dr["AnnouncementsView"]),

                    Discussion = Convert.ToInt32(dr["Discussion"]),

                    ParentAnsweringSurvey = dr["ParentAnsweringSurvey"].ToString(),

                    ParentSchoolSatisfaction = dr["ParentschoolSatisfaction"].ToString(),

                    StudentAbsenceDays = dr["StudentAbsenceDays"].ToString(),

                    StudentMarks = Convert.ToInt32(dr["Student_Marks"]),

                    Class = dr["Class"].ToString()

                };

                if (dtb.Rows.Count > 0)
                {
                    return Ok(lstprofile);
                }
                else
                    return Ok("Not Found!");
            }
            catch (Exception ef)
            {
                return Ok(ef.Message);
            }
            finally { connString.Close(); }
        }


        [Route("Delete/{id}")]
        [HttpDelete] //DELETE
        public ActionResult Delete(string id)
        {
            try
            {
                connString = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                cmd = new SqlCommand("delete from StudentOriginal where Student_ID=@StudentID" + "", connString);
                cmd.Parameters.AddWithValue("@StudentID", id);
                connString.Open();
                int x = cmd.ExecuteNonQuery();
                if (x > 0)
                {
                    return Ok(new { Message = "Record Deleted!" });
                }
                return BadRequest(new { Message = "Record Not found!" });
            }
            catch (Exception ef)
            {
                return BadRequest(ef.Message);
            }
            finally { connString.Close(); }
        }


        [HttpPatch]
        [Route("UpdateStudent/{id}")]
        public async Task<IActionResult> UpdateStudent(string id, [FromBody] JsonPatchDocument<Student> patchDoc)
        {
            if (patchDoc == null)
            { 
                return BadRequest("Invalid patch document.");
            }

            connString = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            try
            {
                connString.Open();

                // Assuming patchDoc.Operations[0].path contains the column name to update
                string columnNameToUpdate = patchDoc.Operations[0].path;
                object newValue = patchDoc.Operations[0].value;

                // Using parameterized query to avoid SQL injection
                string query = "UPDATE StudentOriginal SET " + columnNameToUpdate + " = @NewValue WHERE Student_ID = @StudentID";
                SqlCommand cmd = new SqlCommand(query, connString);
                cmd.Parameters.AddWithValue("@NewValue", newValue);
                cmd.Parameters.AddWithValue("@StudentID", id);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return Ok("Student updated successfully.");
                }
                else
                {
                    return NotFound("Student not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
            finally
            {
                connString.Close();
            }
        }

    }
}




 

