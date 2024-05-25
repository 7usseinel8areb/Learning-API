using System.Diagnostics.Contracts;

namespace ITI_API_Learn.DTO
{
    public class GetAllDepartmentsWithListOfEmployeesDTO
    {
        public string DepartmentName { get; set; }
        public string ManagerName { get; set; }

        public List<Employee> employees { get; set; } = new List<Employee>();

    }
}
