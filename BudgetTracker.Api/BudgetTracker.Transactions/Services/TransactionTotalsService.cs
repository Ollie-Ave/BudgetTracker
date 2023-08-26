using System;
using System.Collections.Generic;
using System.Linq;
using BudgetTracker.DataAccess.Context;
using BudgetTracker.DataAccess.Models;
using BudgetTracker.Transactions.Enums;
using BudgetTracker.Transactions.Interfaces;

namespace BudgetTracker.Transactions.Services
{
    public class TransactionTotalsService : ITransactionTotalsService
    {
        private readonly ApplicationDbContext context;

        public TransactionTotalsService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<decimal> GetTotalsFrom(int accountUid, DateTime fromDate, TotalType totalType)
        {
            List<decimal> returnValue = new();

            List<Transaction> allTransactionsSinceFromDate = this.context.Transactions
                    .Where(t => t.AccountUid == accountUid && t.TimeOfPurchase >= fromDate)
                    .ToList();

            List<Transaction> filteredTransactions = FilterTransactionsByType(allTransactionsSinceFromDate, totalType);

            return GetTotalsForTransactions(filteredTransactions, fromDate);
        }

        internal static List<Transaction> FilterTransactionsByType(List<Transaction> transactions, TotalType type)
        {
            List<Transaction> returnValue;

            if (type == TotalType.Income)
            {
                returnValue = transactions
                                .Where(t => t.Amount > 0)
                                .ToList();
            }
            else if (type == TotalType.Expenses)
            {
                returnValue = transactions
                                .Where(t => t.Amount < 0)
                                .ToList();
            }
            else if (type == TotalType.Difference)
            {
                returnValue = transactions;
            }
            else
            {
                throw new NotImplementedException($"TotalType '{type}' not implemented");
            }

            return returnValue;
        }

        internal static List<decimal> GetTotalsForTransactions(List<Transaction> allTransactions, DateTime fromDate)
        {
            List<decimal> returnValue = new();

            DateTime dateToProcess = fromDate.Date;

            while (dateToProcess.Date <= DateTime.UtcNow.Date)
            {
                returnValue.Add(allTransactions
                    .Where(t => t.TimeOfPurchase.Date == dateToProcess.Date)
                    .Select(t => t.Amount)
                    .ToList()
                    .Sum());

                dateToProcess = dateToProcess.AddDays(1);
            }

            return returnValue;
        }
    }
}