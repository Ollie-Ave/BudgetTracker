namespace BudgetTracker.Transactions.Models
{
    using System;

    /// <summary>A data Model for the transaction upload.</summary>
    public class TransactionUploadModel
    {
        /// <summary>Gets or sets the place of purchase.</summary>
        /// <value>The place of purchase.</value>
        public string PlaceOfPurchase { get; set; } = string.Empty;

        /// <summary>Gets or sets the amount.</summary>
        /// <value>The amount.</value>
        public decimal Amount { get; set; } = 0;

        /// <summary>Gets or sets the time of purchase.</summary>
        /// <value>The time of purchase.</value>
        public DateTime TimeOfPurchase { get; set; } = DateTime.Now;
    }
}
