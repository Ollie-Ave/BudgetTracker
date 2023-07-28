namespace BudgetTracker.Api.Controllers
{
    using BudgetTracker.Authentication.Interfaces;
    using BudgetTracker.Authentication.Models;
    using Microsoft.AspNetCore.Mvc;

	/// <summary>
	/// The login controller.
	/// </summary>
    [ApiController]
	[Route("api/[controller]")]
	public class LoginController : ControllerBase
	{
		/// <summary>
		/// The authentication service.
		/// </summary>
		private readonly IAuthenticationService authenticationService;

		/// <summary>
		/// Initialises a new instance of the <see cref="LoginController"/> class.
		/// </summary>
		/// <param name="authenticationService"></param>
		public LoginController(IAuthenticationService authenticationService)
		{
			this.authenticationService = authenticationService;
		}

		/// <summary>
		/// Returns an API key for the given credentials, or an empty string if the credentials are invalid.
		/// </summary>
		/// <param name="credentials"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult Get([FromBody]LoginModel credentials)
		{
			string apiKey = this.authenticationService.Authorise(credentials);
			
			return string.IsNullOrWhiteSpace(apiKey)
				? Unauthorized()
				: Ok(apiKey);
		}
	}
}