

namespace ITI_API_Learn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyBindingController : ControllerBase
    {
        //Bind primitive type ==> Route Segment "/d/" ==> Query string "?id =1" ==> default value "0"
        //Bind Complex type ==> Request Body

        //[HttpGet("{id:int}")] ==> Route
        /*[HttpGet]
        public IActionResult Get1(int id)
        {
            return Ok(); 
        }
*/
        /*[HttpPost]
        //Complex will be at the body and the name will be either query or route segment
        //
        public IActionResult Add([FromBody]Department department, string name)
        {
            return Ok();
        }
*/
        /*[HttpGet("{id:int}")]
        //service provider
        public IActionResult Get2(int id,[FromBody]string name)
        {
            return Ok();
        }
        */
        [HttpGet("{Name:alpha}/{Manager:alpha}")]
        //service provider
        public IActionResult Get3([FromBody]int id,[FromRoute]Department department)
        {
            return Ok();
        }

    }
}
