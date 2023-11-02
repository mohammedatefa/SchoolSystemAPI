using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolSystemAPI.Model
{
    public class Student
    {
        public int Id { get; set; }
        [MinLength(3)]
        [RegularExpression("^[A-Za-z]*$", ErrorMessage = "The Name field must contain only letters....")]
        public string? Name { get; set; }

        [Range(11,19)]
        public int Age { get; set; }
        [Required]
        public string? Adress { get; set; }
        [DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
