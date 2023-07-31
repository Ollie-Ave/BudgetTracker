using System.Collections.Generic;
using BudgetTracker.DataAccess.Models;

namespace BudgetTracker.Transactions.Extensions
{
    public static class TransactionExtensions
    {
        public static List<Transaction> UpdateTransactionValue(this Transaction source, List<Transaction> subsequentTransactions, decimal newTransactionAmount)
        {
            source.Amount = newTransactionAmount;
            source.BalanceAfterPurchase = source.BalanceBeforePurchase + newTransactionAmount;

            List<Transaction> returnValue = new() { source };
            returnValue.AddRange(source.UpdateSubsequentTransactions(subsequentTransactions));

            return returnValue;
        }

        private static List<Transaction> UpdateSubsequentTransactions(this Transaction source, List<Transaction> subsequesntTransactions)
        {
            List<Transaction> returnValue = new();

            Transaction previousTransaction = source;

            foreach(Transaction transaction in subsequesntTransactions)
            {
                transaction.BalanceBeforePurchase = previousTransaction.BalanceAfterPurchase;

                transaction.BalanceAfterPurchase = transaction.BalanceBeforePurchase + transaction.Amount;

                returnValue.Add(transaction);

                previousTransaction = transaction;
            }

            return returnValue;
        }
    }
}