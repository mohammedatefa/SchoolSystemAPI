using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystemAPI.Model;

namespace SchoolSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        public Context.SchoolContext Context { get; set; }
        public StudentController(Context.SchoolContext _context)
        {
            Context= _context;
        }

        //get all students
        [HttpGet]
        public IActionResult GettAll()
        {
            List<Student> students = Context.Students.ToList();
            if (students.Count==0)
            {
                return BadRequest();
            }
            else
            {
                return Ok(students);
            }
        }

        //getStudent by uisng Id
        [HttpGet("{id}:int")]
        public IActionResult GettStudentById(int id)
        {
            Student st = Context.Students.Find(id);
            if (st==null)
            {
                return NoContent();
            }
            else
            {
                return Ok(st);
            }
        }

        //getStudent by using Name
        [HttpGet("{name:alpha}")]
        public IActionResult GetStudentByName(string name)
        {
            Student st = Context.Students.FirstOrDefault(s => s.Name == name);
            if (st==null)
            {
                return NoContent();
            }
            else
            {
                return Ok(st);
            }
        }

        //Add New Student
        [HttpPost]
        public IActionResult AddStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Context.Add(student);
                    Context.SaveChanges();
                    return Ok(Context.Students.ToList());
                }
                catch
                {

                    return BadRequest();
                }
            }
            return BadRequest(ModelState);
        }

        //update Student 
        [HttpPut]
        public IActionResult UpdateStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Context.Update(student);
                    Context.SaveChanges();
                    return Ok(Context.Students.ToList());
                }
                catch
                {

                    return BadRequest();
                }
            }
            return BadRequest(ModelState);
        }

        //delete Student 
        [HttpDelete]
        public IActionResult DeleteStudent(int id)
        {
            Student st = Context.Students.Find(id);
            if (st!=null)
            {
                Context.Remove(st);
                Context.SaveChanges();
                return Ok(Context.Students.ToList());
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
