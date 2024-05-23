namespace ITI_API_Learn.Controllers
{
    [Route("api/[controller]")]//api/Department ==> "Uniform Name"
    [ApiController]//To Know that the controller is API ==> "Serialization, DeSerialization"
    public class DepartmentController : ControllerBase//response according status Code
    {
        private readonly AppDbContext _appDbContext;
        public DepartmentController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        #region Read
        //action
        [HttpGet]
        public IActionResult GetAllDepartment()
        {
            List<Department> departments = _appDbContext.Departments.ToList();
            return Ok(departments);//response body Json
        }

        /*[HttpGet]
        [Route("{id:int}")]// if i have more than one action with the same verb "Get" so i make difference betweeen them by the parameter*/
        [HttpGet("{id:int}",Name ="GetOneDepartmentRoute")]
        public IActionResult GetDepartment(int id) 
        {
            Department department = _appDbContext.Departments.FirstOrDefault(x => x.Id == id);
            return Ok(department);
        }

        /*[HttpGet]
        [Route("{Name:alpha}")]*/
        [HttpGet("{Name:alpha}")]
        public IActionResult GetByName(string Name)
        {
            Department department = _appDbContext.Departments.FirstOrDefault(dept => dept.Name.Contains(Name));
            return Ok(department);
        }
        #endregion

        #region Create
        [HttpPost]
        public IActionResult PostDepartment(Department department)
        {
            if(ModelState.IsValid)
            {
                _appDbContext.Departments.Add(department);
                _appDbContext.SaveChanges();
                //return Ok("Saved Succefully");
                //return Created($"http://localhost:10633/api/Department/{department.Id}",department);
                //How to get current domain??
                string UrlRoute= Url.Link("GetOneDepartmentRoute",new {id = department.Id });
                return Created(UrlRoute, department);
            }
            //return BadRequest("Department is inValid");
            return BadRequest(ModelState);//open as a dectionary
        }
        #endregion

        #region Update
        [HttpPut("{id:int}")]//All fields
        //[HttpPatch]//some fields
        public IActionResult UpdateDepartment([FromRoute]int id,[FromBody]Department department )
        {
            if(ModelState.IsValid)
            {
                Department oldDepartment = _appDbContext.Departments.FirstOrDefault(dept => dept.Id == id);
                if(oldDepartment != null)
                {                    
                    oldDepartment.Name = department.Name;
                    oldDepartment.Manager = department.Manager;

                    _appDbContext.SaveChanges();
                    //return NoContent();
                    return StatusCode(204, "Data Saved");
                }
                return BadRequest("Id is not valid");
            }
            return BadRequest(ModelState);
        }
        #endregion

        #region Delete
        [HttpDelete("{id:int}")]
        public IActionResult DeleteDepartment(int id)
        {
            Department oldDepartment = _appDbContext.Departments.FirstOrDefault(dept => dept.Id == id);
            if(oldDepartment != null)
            {
                _appDbContext.Departments.Remove(oldDepartment);
                _appDbContext.SaveChanges();

                return StatusCode(204,"Department was deleted succesfully");
            }
            return BadRequest("Id is Invalid");
        }
        #endregion
    }
}
