namespace BudgetTracker.DataAccess.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Transactions")]
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Uid { get; set; }

        [Required]
        [ForeignKey("Account")]
        public int AccountUid { get; set; }

        [Required]
        [MaxLength(50)]
        public string PlaceOfPurchase { get; set; } = string.Empty;

        [Required]
        public decimal Amount { get; set; } = 0;

        [Required]
        public decimal BalanceBeforePurchase { get; set; } = 0;

        [Required]
        public decimal BalanceAfterPurchase { get; set; } = 0;

        [Required]
        public DateTime TimeOfPurchase { get; set; } = DateTime.Now;
    }
}
