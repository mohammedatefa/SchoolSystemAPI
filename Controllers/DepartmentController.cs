using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystemAPI.Context;
using SchoolSystemAPI.Model;

namespace SchoolSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        public SchoolContext Context;
        public DepartmentController(SchoolContext _context)
        {
            Context = _context;
        }
        [HttpGet]
        public IActionResult GettAllDepartments()
        {
            List<Department> departments = Context.Departments.ToList();
            if (departments.Count != 0)
            {
                return Ok(departments);
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpGet("{id:int}")]
        public IActionResult GetDepartmentById(int Id)
        {
            Department found = Context.Departments.Find(Id);
            if (found == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(found);
            }
        }
        [HttpGet("{name:alpha}")]
        public IActionResult GetDepartmentByName(string name)
        {
            Department found = Context.Departments.FirstOrDefault(d => d.Name == name);
            if (found == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(found);
            }
        }
        [HttpPost]
        public IActionResult AddNewDepartment(Department dept)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Context.Departments.Add(dept);
                    Context.SaveChanges();
                    return Ok(Context.Departments.ToList());
                }
                catch 
                {
                    return BadRequest();
                    
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult UbdateDepartment(Department dept)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Context.Departments.Update(dept);
                    Context.SaveChanges();
                    return Ok(Context.Departments.ToList());
                }
                catch (Exception)
                {

                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public IActionResult DeleteDepartmentById(int Id)
        {
            Department found = Context.Departments.Find(Id);
            if (found!=null)
            {
                try
                {
                    Context.Departments.Remove(found);
                    Context.SaveChanges();
                    return Ok(Context.Departments.ToList());
                }
                catch (Exception)
                {

                    return BadRequest();
                }
            }else
            {
                return BadRequest();
            }
        }
    }
}
