namespace BudgetTracker.Authentication.Services
{
	using System;
	using System.Linq;
	using BudgetTracker.Authentication.Extensions;
	using BudgetTracker.Authentication.Interfaces;
	using BudgetTracker.Authentication.Models;
	using BudgetTracker.DataAccess.Context;
	using Microsoft.Extensions.Caching.Memory;

	/// <summary>
	/// The authentication service is used to authorise users and generate API keys.
	/// </summary>
	public class AuthenticationService : IAuthenticationService
	{	
		/// <summary>
		/// The database context is used to retrieve credentials from the database.
		/// </summary>
		private readonly ApplicationDbContext context;
		
		/// <summary>
		/// The memory cache is used to store API keys and their associated email addresses.
		/// </summary>
		private readonly IMemoryCache memoryCache;

		/// <summary>
		/// Initialises a new instance of the <see cref="AuthenticationService"/> class.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="memoryCache"></param>
		public AuthenticationService(ApplicationDbContext context, IMemoryCache memoryCache)
		{
			this.context = context;
			this.memoryCache = memoryCache;
		}

        /// <summary>
        /// Returns true if the API key is valid, false otherwise.
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool TryValidateApiKey(string apiKey, out string email)
        {
            bool returnValue = false;
            email = string.Empty;

            if (this.memoryCache.TryGetValue(apiKey, out string? emailFromCache) && emailFromCache is not null)
            {
                email = emailFromCache;
                returnValue = true;
            }

            return returnValue;
        }

        /// <summary>
        /// Returns an API key for the given credentials, or an empty string if the credentials are invalid.
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public string Authorise(LoginModel credentials)
		{
			string returnValue = string.Empty;
			
			LoginModel? credentialsFromDb = this.GetCredentialsFromDb(credentials.Email);
			
			if (credentialsFromDb is not null && credentialsFromDb.Password == credentials.Password.ToSha256())
			{			
				returnValue = this.GetApiKey(credentials.Email);
			}
			
			return returnValue;
		}
		
		/// <summary>
		/// Returns a new API key for the given email address.
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		/// <exception cref="NullReferenceException"></exception>
		private string GetApiKey(string email)
		{
			string apiKey = Guid.NewGuid().ToString();

			this.memoryCache.CreateEntry(apiKey);
			this.memoryCache.Set(apiKey, email, TimeSpan.FromDays(30));

			return apiKey;
		}
		
		/// <summary>
		/// Returns the credentials from the database for the given email address, or null if the email address is not found.
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		private LoginModel? GetCredentialsFromDb(string email)
		{
			return this.context.Accounts
								.Select(account => new LoginModel 
								{
									Email = account.Email,
									Password = account.Password,
								})
								.FirstOrDefault(account => account.Email == email.Trim());
		}
	}
}