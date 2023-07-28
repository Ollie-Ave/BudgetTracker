namespace BudgetTracker.Authentication.Services
{
    using System;
    using System.Linq;
    using BudgetTracker.Authentication.Extensions;
    using BudgetTracker.Authentication.Interfaces;
    using BudgetTracker.Authentication.Models;
    using BudgetTracker.DataAccess.Context;
    using Microsoft.Extensions.Caching.Memory;

    /// <summary>A service for accessing authentications information.</summary>
    /// <seealso cref="T:BudgetTracker.Authentication.Interfaces.IAuthenticationService"/>
    public class AuthenticationService : IAuthenticationService
    {	
        /// <summary>(Immutable) the context.</summary>
        private readonly ApplicationDbContext context;

        /// <summary>(Immutable) the memory cache.</summary>
        private readonly IMemoryCache memoryCache;

        /// <summary>Initialises a new instance of the <see cref="BudgetTracker.Authentication.Services.AuthenticationService"/> class.</summary>
        /// <param name="context">The context.</param>
        /// <param name="memoryCache">The memory cache.</param>
        public AuthenticationService(ApplicationDbContext context, IMemoryCache memoryCache)
        {
            this.context = context;
            this.memoryCache = memoryCache;
        }

        /// <summary>API key is valid.</summary>
        /// <param name="apiKey">The API key.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        /// <seealso cref="M:IAuthenticationService.ApiKeyIsValid(string)"/>
        public bool ApiKeyIsValid(string apiKey)
        {
            bool returnValue = false;

            if (this.memoryCache.TryGetValue(apiKey, out _))
            {
                returnValue = true;
            }

            return returnValue;
        }

        /// <summary>Gets email from API key.</summary>
        /// <exception cref="UnauthorizedAccessException">Thrown when an Unauthorized Access error condition occurs.</exception>
        /// <param name="apiKey">The API key.</param>
        /// <returns>The email from API key.</returns>
        /// <seealso cref="M:IAuthenticationService.GetEmailFromApiKey(string)"/>
        public int GetUidFromApiKey(string apiKey)
        {
            this.memoryCache.TryGetValue(apiKey, out int emailFromCache);

            return emailFromCache;
        }

        /// <summary>Authorises the given credentials.</summary>
        /// <param name="credentials">The credentials.</param>
        /// <returns>A string.</returns>
        /// <seealso cref="M:IAuthenticationService.Authorise(LoginModel)"/>
        public string Authorise(LoginModel credentials)
        {
            string returnValue = string.Empty;
            
            LoginModel? credentialsFromDb = this.context.Accounts
                                                    .Select(account => new LoginModel
                                                    {
                                                        Email = account.Email,
                                                        Password = account.Password,
                                                    })
                                                    .FirstOrDefault(account => account.Email == credentials.Email.Trim());

            string inputtedPasswordHash = credentials.Password.ToSha256();

            if (credentialsFromDb is not null && credentialsFromDb.Password == inputtedPasswordHash)
            {
                int accountUid = this.context.Accounts
                                            .Select(a => new
                                            {
                                                a.Email,
                                                a.Uid,
                                            })
                                            .ToList()
                                            .First(a => a.Email == credentials.Email).Uid;

                returnValue = this.GetApiKey(accountUid);
            }

            return returnValue;
        }

        /// <summary>Gets API key.</summary>
        /// <param name="accountUId">Identifier for the account u.</param>
        /// <returns>The API key.</returns>
        private string GetApiKey(int accountUId)
        {
            string apiKey = Guid.NewGuid().ToString();

            this.memoryCache.CreateEntry(apiKey);
            this.memoryCache.Set(apiKey, accountUId, TimeSpan.FromDays(30));

            return apiKey;
        }
    }
}