namespace BudgetTracker.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    
    /// <summary>A controller for handling hello worlds.</summary>
    /// <seealso cref="T:ControllerBase"/>
    [ApiController]
    [Route("api/[controller]")]
    public class HelloWorldController : ControllerBase
    {
        /// <summary>(An Action that handles HTTP GET requests) gets the get.</summary>
        /// <returns>An IActionResult.</returns>
        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok("Hello World!");
        }
    }
}