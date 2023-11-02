using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SchoolSystemAPI.Model
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        public string?  Name { get; set; }
        [MaxLength(120)]
        public string Description { get; set; }
        [JsonIgnore]
        public ICollection<Student> Students { get; set; } = new HashSet<Student>();
    }
}
