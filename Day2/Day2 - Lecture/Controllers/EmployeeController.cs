

namespace ITI_API_Learn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public EmployeeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpGet]
        public ActionResult GetAllemployees()
        {
            List<Employee> employees = _appDbContext.Employees.Include(emp=> emp.Department).ToList();
            return Ok(employees);
        }
        
        [HttpGet("{id:int}",Name = "OneEmployeeRoute")]//with DTO
        public ActionResult GetEmployee(int id)
        {
            Employee employee = _appDbContext.Employees.Include(emp=> emp.Department).FirstOrDefault(emp=>emp.Id == id);
            EmployeeDataWithDepartmentDTO employeeDataWithDepartmentDTO = new EmployeeDataWithDepartmentDTO()
            {
                Id = id,
                StudentName = employee.Name,
                Address = employee.Address,
                DepartmentName = employee.Department.Name
            };
            return Ok(employeeDataWithDepartmentDTO);
        }
    }
}
