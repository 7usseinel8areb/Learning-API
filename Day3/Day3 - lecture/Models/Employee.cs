using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ITI_API_Learn.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [Phone]
        public string Phone { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        [JsonIgnore]
        public virtual Department Department { get; set; }
    }
}
