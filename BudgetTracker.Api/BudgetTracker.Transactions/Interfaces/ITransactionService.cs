using BudgetTracker.Transactions.Models;
using System;
using System.Collections.Generic;

namespace BudgetTracker.Transactions.Interfaces
{
    /// <summary>Interface for transaction service.</summary>
    public interface ITransactionService
    {
        /// <summary>Gets total expenses for account this week.</summary>
        /// <param name="accountId">Identifier for the account.</param>
        /// <returns>The total expenses for account this week.</returns>
        decimal GetTotalExpensesForAccountFrom(int accountId, DateTime from);

        /// <summary>Gets all transactions for account.</summary>
        /// <param name="accountId">Identifier for the account.</param>
        /// <param name="page">The page.</param>
        /// <returns>all transactions for account.</returns>
        public List<TransactionViewModel> GetTransactionsForAccount(int accountId, int page);

        /// <summary>Deletes the transaction described by transactionId.</summary>
        /// <param name="transactionId">Identifier for the transaction.</param>
        public decimal DeleteTransaction(int transactionId);

        /// <summary>Updates the transaction.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="transaction">The transaction.</param>
        public decimal UpdateTransaction(TransactionViewModel transaction);

        /// <summary>Adds a transaction.</summary>
        /// <param name="transaction">The transaction.</param>
        /// <returns>A decimal.</returns>
        public decimal AddTransaction(TransactionUploadModel transaction, int accountId);

        /// <summary>Gets day totals.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="days">The days.</param>
        /// <returns>The day totals.</returns>
        List<decimal> GetDayIncomeTotals(int id, int days);

        /// <summary>Gets day expense totals.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="days">The days.</param>
        /// <returns>The day expense totals.</returns>
        List<decimal> GetDayExpenseTotals(int id, int days);

        /// <summary>Gets day difference totals.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="days">The days.</param>
        /// <returns>The day difference totals.</returns>
        List<decimal> GetDayDifferenceTotals(int id, int days);
    }
}
