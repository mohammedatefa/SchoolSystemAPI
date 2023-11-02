using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolSystemAPI.Context;
using SchoolSystemAPI.DTO;
using SchoolSystemAPI.Model;

namespace SchoolSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentWithStudentsController : ControllerBase
    {

        public SchoolContext Context;
        public DepartmentWithStudentsController(SchoolContext _context)
        {
            Context = _context;
        }
        [HttpGet]
        public IActionResult GetAllDptsWithStudent()
        {
            List<Department> departments = Context.Departments.Include(d => d.Students).ToList();
            List<DepartmentWithStudents> dto = new List<DepartmentWithStudents>();
            foreach (var item in departments)
            {
                DepartmentWithStudents dtoobj = new DepartmentWithStudents();
                dtoobj.DepartmentName =item.Name;
                List<string> students = new List<string>();
                foreach (Student s in item.Students)
                {
                    students.Add(s.Name);
                    //dtoobj.StudentNames.Add();
                }
                dtoobj.StudentNames = students;
                dto.Add(dtoobj);
            }
            return Ok(dto);
        }
    }
}
