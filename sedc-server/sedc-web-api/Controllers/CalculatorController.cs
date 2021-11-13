using Microsoft.AspNetCore.Mvc;

namespace Sedc.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        [HttpGet("{first}/{second}")]
        public int Add([FromRoute] int first, [FromRoute] int second)
        {

            return first + second;

        }
    }
}
