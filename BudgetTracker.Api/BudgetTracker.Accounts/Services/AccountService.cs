namespace BudgetTracker.Accounts.Services
{
	using System;
	using System.Linq;
	using BudgetTracker.Accounts.Interfaces;
	using BudgetTracker.Accounts.Models;
	using BudgetTracker.DataAccess.Context;

	/// <summary>
	/// The account service.
	/// </summary>
	public class AccountService : IAccountService
	{
		/// <summary>
		/// The application database context.
		/// </summary>
		private readonly ApplicationDbContext context;

		/// <summary>
		/// Initialises a new instance of the <see cref="AccountService"/> class.
		/// </summary>
		/// <param name="context"></param>
		public AccountService(ApplicationDbContext context)
		{
			this.context = context;
		}
		
		/// <summary>
		/// Gets the account details for the user with the specified email.
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public AccountViewModel GetAccountFromEmail(string email)
		{
			AccountViewModel? returnValue = this.context.Accounts.Select(x => new AccountViewModel
				{
					Uid = x.Uid,
					Email = x.Email,
					FirstName = x.FirstName,
					LastName = x.LastName,
					ProfilePictureUrl = x.ProfilePictureUrl,
					Balance = x.Balance,
				})
				.FirstOrDefault(x => x.Email == email);

			return returnValue ?? throw new Exception("Account not found");
		}

        /// <summary>Gets total income per month.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The total income per month.</returns>
		public decimal GetTotalIncomePerMonth(int id)
		{
			return this.context.IncomeStreams
				.Where(x => x.AccountUid == id)
				.Select(x => x.Amount)
				.ToList()
				.Sum();
		}
	}
}