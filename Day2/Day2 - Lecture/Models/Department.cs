

namespace ITI_API_Learn.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Manager { get; set; }

        //[JsonIgnore]
        //Another solution DTO it looks like ViewModel at MVC
        public virtual List<Employee> Employee { get; set; }
    }
}
