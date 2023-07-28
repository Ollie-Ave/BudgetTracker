namespace BudgetTracker.DataAccess.Models
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	
	[Table("Accounts")]
	public partial class Account
	{
		[Key]
		[Column(Order=1)]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Uid { get; set; }
		
		[Key]
		[Required]
		[Column(Order=2)]
		[MaxLength(50)]
		public string Email { get; set; } = string.Empty;
		
		[Required]
		[MaxLength(100)]
		public string Password { get; set; } = string.Empty;
		
		[Required]
		[MaxLength(50)]
		public string FirstName { get; set; } = string.Empty;

		[Required]
		[MaxLength(50)]
		public string LastName { get; set; } = string.Empty;
		
		[Required]
		[MaxLength(50)]
		public string ProfilePictureUrl { get; set; } = string.Empty;

		[Required]
		public decimal Balance { get; set; } = 0;
	}
}