namespace BudgetTracker.Accounts.Models
{
    /// <summary>An account.</summary>
	public class AccountViewModel
	{
        /// <summary>Gets or sets the UID.</summary>
        /// <value>The UID.</value>
		public int Uid { get; set; }

        /// <summary>Gets or sets the email.</summary>
        /// <value>The email.</value>
		public string Email { get; set; } = string.Empty;

        /// <summary>Gets or sets the person's first name.</summary>
        /// <value>The name of the first.</value>
		public string FirstName { get; set; } = string.Empty;

        /// <summary>Gets or sets the person's last name.</summary>
        /// <value>The name of the last.</value>
		public string LastName { get; set; } = string.Empty;

        /// <summary>Gets or sets URL of the profile picture.</summary>
        /// <value>The profile picture URL.</value>
		public string ProfilePictureUrl { get; set; } = string.Empty;

        /// <summary>Gets or sets the balance.</summary>
        /// <value>The balance.</value>
		public decimal Balance { get; set; } = 0;
	}
}