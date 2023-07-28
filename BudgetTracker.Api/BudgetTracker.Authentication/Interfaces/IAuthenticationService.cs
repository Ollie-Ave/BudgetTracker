using BudgetTracker.Authentication.Models;

namespace BudgetTracker.Authentication.Interfaces
{
	/// <summary>
	/// The authentication service is used to authorise users and generate API keys.
	/// </summary>
	public interface IAuthenticationService
	{
		/// <summary>
		/// Returns an API key for the given credentials, or an empty string if the credentials are invalid.
		/// </summary>
		/// <param name="credentials"></param>
		/// <returns></returns>
		public string Authorise(LoginModel credentials);
		
		/// <summary>
		/// Returns a new API key for the given email address.
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		public string GetApiKey(string email);

		/// <summary>
		/// Returns the credentials for the given email address, or null if the email address is not found.
		/// </summary>
		/// <param name="apiKey"></param>
		/// <param name="email"></param>
		/// <returns></returns>
		public bool TryValidateApiKey(string apiKey, out string email);
	}
}