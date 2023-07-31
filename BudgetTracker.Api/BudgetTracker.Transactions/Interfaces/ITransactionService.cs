
namespace BudgetTracker.Transactions.Interfaces
{
    using BudgetTracker.Transactions.Models;
    using System.Collections.Generic;

    public interface ITransactionService
    {
        public decimal CreateTransaction(TransactionUploadModel transaction, int accountId);

        public List<TransactionViewModel> GetTransactionsForAccount(int accountId, int page);

        public decimal UpdateTransaction(TransactionViewModel transaction);

        public decimal DeleteTransaction(int transactionId);
    }
}