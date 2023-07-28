namespace BudgetTracker.Accounts.Interfaces
{
    using BudgetTracker.Accounts.Models;

    /// <summary>Interface for account service.</summary>
	public interface IAccountService
	{
        /// <summary>Gets account from email.</summary>
        /// <param name="email">The email.</param>
        /// <returns>The account from email.</returns>
		public AccountViewModel GetAccountFromEmail(string email);

        /// <summary>Gets total income per month.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The total income per month.</returns>
        public decimal GetTotalIncomePerMonth(int id);
    }
}