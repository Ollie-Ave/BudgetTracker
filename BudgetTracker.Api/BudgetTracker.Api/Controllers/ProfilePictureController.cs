namespace BudgetTracker.Api.Controllers
{
	using BudgetTracker.Accounts.Interfaces;
	using BudgetTracker.Authentication.Interfaces;
	using Microsoft.AspNetCore.Mvc;

	/// <summary>
	/// The profile picture controller.
	/// </summary>
	[ApiController]
	[Route("api/[controller]")]
	public class ProfilePictureController : ControllerBase
	{
		/// <summary>
		/// The profile picture content type.
		/// </summary>
        private const string profilePictureContentType = "image/jpeg";

		/// <summary>
		/// The authentication service.
		/// </summary>
        private readonly IAuthenticationService authenticationService;

		/// <summary>
		/// The profile picture service.
		/// </summary>
		private readonly IProfilePictureService profilePictureService;
		
		/// <summary>
		/// Initialises a new instance of the <see cref="ProfilePictureController"/> class.
		/// </summary>
		/// <param name="authenticationService"></param>
		/// <param name="profilePictureService"></param>
		public ProfilePictureController(IAuthenticationService authenticationService, IProfilePictureService profilePictureService)
		{
			this.authenticationService = authenticationService;
			this.profilePictureService = profilePictureService;
		}
		
		/// <summary>
		/// Gets the profile picture for the user with the specified ID.
		/// </summary>
		/// <param name="apiKey"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("{id}")]
		public IActionResult Get([FromQuery]string apiKey, int id)
		{
			IActionResult returnValue = this.Unauthorized();

			if (this.authenticationService.TryValidateApiKey(apiKey, out string _))
			{
				returnValue = this.File(this.profilePictureService.GetProfilePicture(id), profilePictureContentType);
			}

			return returnValue;
		}
	}
}