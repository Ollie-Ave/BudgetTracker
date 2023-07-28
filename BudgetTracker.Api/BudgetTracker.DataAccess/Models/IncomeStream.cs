namespace BudgetTracker.DataAccess.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    [Table("IncomeStreams")]
    public class IncomeStream
    {
        [Key]
        public int Uid { get; set; }

        [ForeignKey("Account")]
        public int AccountUid { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public decimal Amount { get; set; } = 0;
    }
}
