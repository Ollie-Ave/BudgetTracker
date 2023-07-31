namespace BudgetTracker.Transactions.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using BudgetTracker.DataAccess.Context;
    using BudgetTracker.DataAccess.Models;
    using BudgetTracker.Transactions.Interfaces;
    using BudgetTracker.Transactions.Models;

    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext context;

        public TransactionService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public decimal CreateTransaction(TransactionUploadModel transaction, int accountId)
        {
            Account account = this.context.Accounts.First(a => a.Uid == accountId);

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

        private List<Transaction> UpdateBalanceValuesForTransactions(Transaction changedTransaction, List<Transaction> subsequentTransactions, decimal initialBalance)
        {
            List<Transaction> returnValue = new();

            if (subsequentTransactions.Count > 0 )
            {
                Transaction previousTransaction = subsequentTransactions.First();
                previousTransaction.BalanceBeforePurchase = initialBalance;

                foreach(Transaction transaction in subsequentTransactions.Skip(1))
                {
                    transaction.BalanceBeforePurchase = previousTransaction.BalanceAfterPurchase;

                    transaction.BalanceAfterPurchase = transaction.BalanceBeforePurchase + transaction.Amount;
                }
            }

            return returnValue;
        }
    }
}
