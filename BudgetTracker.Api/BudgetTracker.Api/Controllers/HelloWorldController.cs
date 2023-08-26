namespace BudgetTracker.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    
    [ApiController]
    [Route("api/[controller]")]
    public class HelloWorldController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok("Hello World!");
        }
    }
}