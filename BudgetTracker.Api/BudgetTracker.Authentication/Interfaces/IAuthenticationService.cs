using BudgetTracker.Authentication.Models;

namespace BudgetTracker.Authentication.Interfaces
{
    /// <summary>Interface for authentication service.</summary>
	public interface IAuthenticationService
	{
        /// <summary>Authorises the given credentials.</summary>
        /// <param name="credentials">The credentials.</param>
        /// <returns>A string.</returns>
		public string Authorise(LoginModel credentials);

        /// <summary>Gets UID from API key.</summary>
        /// <param name="apiKey">.</param>
        /// <returns>The UID from API key.</returns>
		public int GetUidFromApiKey(string apiKey);

        /// <summary>API key is valid.</summary>
        /// <param name="apiKey">.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public bool ApiKeyIsValid(string apiKey);
	}
}