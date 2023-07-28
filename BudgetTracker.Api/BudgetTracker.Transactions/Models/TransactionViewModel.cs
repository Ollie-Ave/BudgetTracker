namespace BudgetTracker.Transactions.Models
{
    using System;

    /// <summary>A ViewModel for the transaction.</summary>
    public class TransactionViewModel
    {
        /// <summary>Gets or sets the UID.</summary>
        /// <value>The UID.</value>
        public int Uid { get; set; }

        /// <summary>Gets or sets the place of purchase.</summary>
        /// <value>The place of purchase.</value>
        public string PlaceOfPurchase { get; set; } = string.Empty;

        /// <summary>Gets or sets the amount.</summary>
        /// <value>The amount.</value>
        public decimal Amount { get; set; } = 0;

        /// <summary>Gets or sets the balance before purchase.</summary>
        /// <value>The balance before purchase.</value>
        public decimal BalanceBeforePurchase { get; set; } = 0;

        /// <summary>Gets or sets the balance after purchase.</summary>
        /// <value>The balance after purchase.</value>
        public decimal BalanceAfterPurchase { get; set; } = 0;

        /// <summary>Gets or sets the time of purchase.</summary>
        /// <value>The time of purchase.</value>
        public DateTime TimeOfPurchase { get; set; } = DateTime.Now;
    }
}