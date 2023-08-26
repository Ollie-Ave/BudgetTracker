namespace BudgetTracker.Accounts.Services
{
	using System;
	using System.Linq;
	using BudgetTracker.Accounts.Interfaces;
	using BudgetTracker.Accounts.Models;
	using BudgetTracker.DataAccess.Context;

    /// <summary>A service for accessing accounts information.</summary>
    /// <seealso cref="T:BudgetTracker.Accounts.Interfaces.IAccountService"/>
	public class AccountService : IAccountService
	{
        /// <summary>(Immutable) the context.</summary>
		private readonly ApplicationDbContext context;

        /// <summary>Initialises a new instance of the <see cref="BudgetTracker.Accounts.Services.AccountService"/> class.</summary>
        /// <param name="context">The context.</param>
		public AccountService(ApplicationDbContext context)
		{
			this.context = context;
		}

        /// <summary>Gets account from email.</summary>
        /// <exception cref="Exception">Thrown when an exception error condition occurs.</exception>
        /// <param name="accountUid">The email.</param>
        /// <returns>The account from email.</returns>
        /// <seealso cref="M:IAccountService.GetAccountFromUid(int)"/>
		public AccountViewModel GetAccountFromUid(int accountUid)
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
				.FirstOrDefault(x => x.Uid == accountUid);

			return returnValue ?? throw new Exception("Account not found");
		}

        /// <summary>Gets total income per month.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The total income per month.</returns>
        /// <seealso cref="M:IAccountService.GetTotalIncomePerMonth(int)"/>
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