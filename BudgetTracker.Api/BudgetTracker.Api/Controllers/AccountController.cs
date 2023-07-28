namespace BudgetTracker.Api.Controllers
{
	using BudgetTracker.Accounts.Interfaces;
    using BudgetTracker.Accounts.Models;
    using BudgetTracker.Authentication.Interfaces;
	using Microsoft.AspNetCore.Mvc;

	/// <summary>
	/// The account controller.
	/// </summary>
	[ApiController]
	[Route("api/[controller]")]
	public class AccountController : ControllerBase
	{
		/// <summary>
		/// The authentication service.
		/// </summary>
        private readonly IAuthenticationService authenticationService;

		/// <summary>
		/// The account service.
		/// </summary>
        private readonly IAccountService accountService;

		/// <summary>
		/// Initialises a new instance of the <see cref="AccountController"/> class.
		/// </summary>
		/// <param name="authenticationService"></param>
		/// <param name="accountService"></param>
        public AccountController(IAuthenticationService authenticationService, IAccountService accountService)
		{
            this.authenticationService = authenticationService;
            this.accountService = accountService;
        }
		
		/// <summary>
		/// Gets the account details for the user with the specified API key.
		/// </summary>
		/// <param name="apiKey"></param>
		/// <returns></returns>
		[HttpGet()]
		public IActionResult Get([FromQuery]string apiKey)
		{
			IActionResult returnValue = this.Unauthorized();

			if (this.authenticationService.ApiKeyIsValid(apiKey))
			{
				int accountUid = this.authenticationService.GetUidFromApiKey(apiKey);

				AccountViewModel account = this.accountService.GetAccountFromUid(accountUid);

                returnValue = this.Ok(account);
			}

			return returnValue;
		}
	}
}