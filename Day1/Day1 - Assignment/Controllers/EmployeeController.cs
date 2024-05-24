using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAllEmployees()
        {
            List<Employee> employees = _appDbContext.Employees.ToList();
            return Ok(employees);
        }

        [HttpGet("{id:int}",Name ="GetOneEmployeeRoute")]
        public IActionResult GetEmployee(int id)
        {
            Employee emp = _appDbContext.Employees.FirstOrDefault(emp =>  emp.Id == id);
            if (emp == null)
            {
                return NotFound();
            }
            return Ok(emp);
        }
        
        [HttpPost]
        public IActionResult CreateEmployee(Employee employee)
        {
            if(ModelState.IsValid)
            {
                _appDbContext.Employees.Add(employee);
                _appDbContext.SaveChanges();

                string url = Url.Link("GetOneEmployeeRoute", new { id = employee.Id });
                return Created(url,"Added Succesfully");
            }
            return BadRequest(ModelState);
        }


        [HttpPut("{id:int}")]
        public IActionResult UpdateEmployee([FromRoute]int id,[FromBody]Employee employee)
        {
            if (ModelState.IsValid)
            {
                Employee oldEmployee = _appDbContext.Employees.FirstOrDefault(employee => employee.Id == id);
                if (oldEmployee == null)
                {
                    return BadRequest("Id is Invalid");
                }
                oldEmployee.Email = employee.Email;
                oldEmployee.Name = employee.Name;
                oldEmployee.Phone = employee.Phone;
                _appDbContext.SaveChanges();

                return StatusCode(204,"Data saved succesfully");
            }
            return BadRequest(ModelState);
        }


        [HttpDelete("{id:int}")]
        public IActionResult DeleteEmployee(int id)
        {
            Employee employee = _appDbContext.Employees.FirstOrDefault(e => e.Id == id);
            if(employee == null)
            {
                return BadRequest("Id is Invalid");
            }
            _appDbContext.Employees.Remove(employee);
            _appDbContext.SaveChanges();

            return StatusCode(204,"Employee deleted succesfully");
        }

    }
}
