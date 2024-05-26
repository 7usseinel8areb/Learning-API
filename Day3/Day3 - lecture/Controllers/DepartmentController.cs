using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITI_API_Learn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly AppDbContext _appContext;

        public DepartmentController(AppDbContext appContext)
        {
            _appContext = appContext;
        }
        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            List<Department> departments = _appContext.Departments.Include(dept => dept.employees).ToList();
            List<GetAllDepartmentsWithListOfEmployeesDTO> allDepartments = new List<GetAllDepartmentsWithListOfEmployeesDTO>();
            foreach (Department department in departments)
            {
                GetAllDepartmentsWithListOfEmployeesDTO dept = new GetAllDepartmentsWithListOfEmployeesDTO()
                {
                    DepartmentName = department.Name,
                    ManagerName = department.Manager,
                };
                foreach(Employee employee in department.employees)
                {
                    dept.employees.Add(employee);
                }
                allDepartments.Add(dept);
            }
            return Ok(allDepartments);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetDepartmentById(int id)
        {
            Department department = _appContext.Departments.Include(dept=> dept.employees).FirstOrDefault(dept => dept.Id == id);
            GetAllDepartmentsWithListOfEmployeesDTO dept = new GetAllDepartmentsWithListOfEmployeesDTO()
            {
                DepartmentName = department.Name,
                ManagerName = department.Manager
            };
            foreach(Employee employee in department.employees)
            {
                dept.employees.Add(employee);
            }
            return Ok(dept);
        }
    }
}
