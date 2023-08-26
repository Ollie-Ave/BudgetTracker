using BudgetTracker.Transactions.Enums;
using System;
using System.Collections.Generic;

namespace BudgetTracker.Transactions.Interfaces
{
    public interface ITransactionTotalsService
    {
        List<decimal> GetTotalsFrom(int accountUid, DateTime fromDate, TotalType totalType);
    }
}