namespace BudgetTracker.Transactions.Services
{
    using BudgetTracker.DataAccess.Context;
    using BudgetTracker.DataAccess.Models;
    using BudgetTracker.Transactions.Interfaces;
    using BudgetTracker.Transactions.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>A service for accessing transactions information.</summary>
    /// <seealso cref="T:BudgetTracker.Transactions.Interfaces.ITransactionService"/>
    public class TransactionService : ITransactionService
    {
        /// <summary>(Immutable) the context.</summary>
        private readonly ApplicationDbContext context;

        /// <summary>Initialises a new instance of the <see cref="BudgetTracker.Transactions.Services.TransactionService"/> class.</summary>
        /// <param name="context">The context.</param>
        public TransactionService(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>Deletes the transaction described by transactionId.</summary>
        /// <param name="transactionId">Identifier for the transaction.</param>
        /// <returns>A decimal.</returns>
        /// <seealso cref="M:ITransactionService.DeleteTransaction(int)"/>
        public decimal DeleteTransaction(int transactionId)
        {
            decimal newAccountBalance;

            Transaction transactionToRemove = this.context.Transactions.First(t => t.Uid == transactionId);
            transactionToRemove.BalanceAfterPurchase = transactionToRemove.BalanceBeforePurchase;

            newAccountBalance = this.UpdateBalanceValuesForTransactionsAfter(transactionToRemove);

            this.context.Accounts.First(a => a.Uid == transactionToRemove.AccountUid).Balance = newAccountBalance;

            this.context.Transactions.Remove(transactionToRemove);

            this.context.SaveChanges();

            return newAccountBalance;
        }

        /// <summary>Gets total expenses for account this week.</summary>
        /// <param name="accountId">Identifier for the account.</param>
        /// <param name="from">from Date/Time.</param>
        /// <returns>The total expenses for account this week.</returns>
        /// <seealso cref="M:ITransactionService.GetTotalExpensesForAccountFrom(int,DateTime)"/>
        public decimal GetTotalExpensesForAccountFrom(int accountId, DateTime from)
        {
            List<decimal> allTransactionTotals = this.context.Transactions
                .Where(t => t.AccountUid == accountId && t.TimeOfPurchase > from)
                .Select(t => t.Amount)
                .ToList();

            return allTransactionTotals.Sum();
        }

        /// <summary>Gets all transactions for account.</summary>
        /// <param name="accountId">Identifier for the account.</param>
        /// <param name="page">The page.</param>
        /// <returns>all transactions for account.</returns>
        public List<TransactionViewModel> GetTransactionsForAccount(int accountId, int page)
        {
            return this.context.Transactions
                .Where(t => t.AccountUid == accountId)
                .Select(t => new TransactionViewModel()
                {
                    Uid = t.Uid,
                    Amount = t.Amount,
                    BalanceAfterPurchase = t.BalanceAfterPurchase,
                    BalanceBeforePurchase = t.BalanceBeforePurchase,
                    PlaceOfPurchase = t.PlaceOfPurchase,
                    TimeOfPurchase = t.TimeOfPurchase,
                })
                .ToList()
                .OrderByDescending(t => t.TimeOfPurchase)
                .Skip(page)
                .Take(10)
                .ToList();
        }

        /// <summary>Updates the transaction.</summary>
        /// <param name="transaction">The transaction.</param>
        /// <returns>A decimal.</returns>
        /// <seealso cref="M:ITransactionService.UpdateTransaction(TransactionViewModel)"/>
        public decimal UpdateTransaction(TransactionViewModel transaction)
        {
            Transaction existingTransaction = this.context.Transactions.First(t => t.Uid == transaction.Uid);

            decimal newAccountBalance = this.context.Accounts.First(a => a.Uid == existingTransaction.AccountUid).Balance;

            existingTransaction.PlaceOfPurchase = transaction.PlaceOfPurchase;
            existingTransaction.TimeOfPurchase = transaction.TimeOfPurchase;

            if (existingTransaction.Amount != transaction.Amount)
            {
                existingTransaction.BalanceAfterPurchase = existingTransaction.BalanceBeforePurchase + transaction.Amount;
                existingTransaction.Amount = transaction.Amount;

                newAccountBalance = this.UpdateBalanceValuesForTransactionsAfter(existingTransaction);
            }

            this.context.SaveChanges();

            return newAccountBalance;
        }

        /// <summary>Updates the balance values for transactions after described by updatedTransaction.</summary>
        /// <param name="updatedTransaction">The updated transaction.</param>
        /// <returns>A decimal.</returns>
        private decimal UpdateBalanceValuesForTransactionsAfter(Transaction updatedTransaction)
        {
            decimal newAccountBalance;
            List<Transaction> transactionsAfterThis = this.context.Transactions.Where(t => t.TimeOfPurchase > updatedTransaction.TimeOfPurchase).ToList();

            if (transactionsAfterThis.Count > 0)
            {
                Transaction previousTransaction = updatedTransaction;

                foreach (Transaction transaction in transactionsAfterThis)
                {
                    transaction.BalanceBeforePurchase = previousTransaction.BalanceAfterPurchase;
                    transaction.BalanceAfterPurchase = transaction.BalanceBeforePurchase + transaction.Amount;

                    previousTransaction = transaction;
                }

                newAccountBalance = this.context.Accounts.First(a => a.Uid == updatedTransaction.AccountUid).Balance = transactionsAfterThis.Last().BalanceAfterPurchase;
            }
            else
            {
                newAccountBalance = this.context.Accounts.First(a => a.Uid == updatedTransaction.AccountUid).Balance = updatedTransaction.BalanceAfterPurchase;
            }

            return newAccountBalance;
        }

        /// <summary>Adds a transaction.</summary>
        /// <param name="transaction">The transaction.</param>
        /// <param name="accountId">Identifier for the account.</param>
        /// <returns>A decimal.</returns>
        /// <seealso cref="M:ITransactionService.AddTransaction(TransactionUploadModel,int)"/>
        public decimal AddTransaction(TransactionUploadModel transaction, int accountId)
        {
            Account? account = this.context.Accounts.FirstOrDefault(a => a.Uid == accountId);

            Transaction transactionToAdd = new()
            {
                AccountUid = accountId,
                Amount = transaction.Amount,
                BalanceBeforePurchase = account.Balance,
                BalanceAfterPurchase = account.Balance + transaction.Amount,
                PlaceOfPurchase = transaction.PlaceOfPurchase,
                TimeOfPurchase = transaction.TimeOfPurchase,
            };

            account.Balance = transactionToAdd.BalanceAfterPurchase;

            this.context.Add(transactionToAdd);

            this.context.SaveChanges();

            return transactionToAdd.BalanceAfterPurchase;
        }

        /// <summary>Gets day totals.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="days">The days.</param>
        /// <returns>The day totals.</returns>
        /// <seealso cref="M:ITransactionService.GetDayIncomeTotals(int,int)"/>
        public List<decimal> GetDayIncomeTotals(int id, int days)
        {
            List<Transaction> allTransactions = this.context.Transactions
                                                                .Where(t => t.AccountUid == id &&
                                                                                                    t.TimeOfPurchase > DateTime.Now.AddDays(-days) &&
                                                                                                    t.Amount > 0)
                                                                .ToList();

            return GetDayTotalsForTransactions(days, allTransactions);
        }

        /// <summary>Gets day expense totals.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="days">The days.</param>
        /// <returns>The day expense totals.</returns>
        /// <seealso cref="M:ITransactionService.GetDayExpenseTotals(int,int)"/>
        public List<decimal> GetDayExpenseTotals(int id, int days)
        {
            List<Transaction> allTransactions = this.context.Transactions
                                                                .Where(t => t.AccountUid == id &&
                                                                                                    t.TimeOfPurchase > DateTime.Now.AddDays(-days) &&
                                                                                                    t.Amount < 0)
                                                                .ToList();

            return GetDayTotalsForTransactions(days, allTransactions);
        }

        /// <summary>Gets day difference totals.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="days">The days.</param>
        /// <returns>The day difference totals.</returns>
        /// <seealso cref="M:ITransactionService.GetDayDifferenceTotals(int,int)"/>
        public List<decimal> GetDayDifferenceTotals(int id, int days)
        {
            List<Transaction> allTransactions = this.context.Transactions
                                                                .Where(t => t.AccountUid == id &&
                                                                                                    t.TimeOfPurchase > DateTime.Now.AddDays(-days))
                                                                .ToList();

            return GetDayTotalsForTransactions(days, allTransactions);
        }

        /// <summary>Gets day totals for transactions.</summary>
        /// <param name="days">The days.</param>
        /// <param name="allTransactions">all transactions.</param>
        /// <returns>The day totals for transactions.</returns>
        private static List<decimal> GetDayTotalsForTransactions(int days, List<Transaction> allTransactions)
        {
            List<decimal> dayTotals = new();

            for (int i = 0; i < days; i++)
            {
                IEnumerable<Transaction> transactionsOnThisDay = allTransactions.Where(t => t.TimeOfPurchase.Date == DateTime.Now.AddDays(-i).Date);

                dayTotals.Add(transactionsOnThisDay.Sum(t => t.Amount));
            }

            return dayTotals;
        }
    }
}
