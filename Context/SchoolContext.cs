using Microsoft.EntityFrameworkCore;
using SchoolSystemAPI.Model;

namespace SchoolSystemAPI.Context
{
    public class SchoolContext:DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
       // updated
    }
}
