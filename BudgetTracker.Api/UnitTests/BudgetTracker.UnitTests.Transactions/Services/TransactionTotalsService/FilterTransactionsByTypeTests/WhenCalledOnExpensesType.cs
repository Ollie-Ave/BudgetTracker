using System.Collections.Generic;
using System.Linq;
using BudgetTracker.DataAccess.Models;
using BudgetTracker.Transactions.Enums;
using NUnit.Framework;

namespace BudgetTracker.UnitTests.Transactions.TransactionTotalsService.GetTransactionsByTypeTests
{
    [TestFixture]
    public class WhenCalledOnExpensesType
    {
        [Test]
        public void ThenExpectOnlyPositiveTransactionsReturned()
        {
            TotalType triggerTotalType = TotalType.Expenses;

            List<Transaction> triggerTransactions = new()
            {
                new Transaction
                {
                    Amount = 1,
                },
                new Transaction
                {
                    Amount = -1,
                },
                new Transaction
                {
                    Amount = 2,
                },
                new Transaction
                {
                    Amount = -2,
                },
                new Transaction
                {
                    Amount = 3,
                },
                new Transaction
                {
                    Amount = -3,
                },
            };

            List<Transaction> expectedResult = triggerTransactions.Where(t => t.Amount < 0).ToList();

            List<Transaction> actualResult = BudgetTracker.Transactions.Services.TransactionTotalsService.FilterTransactionsByType(triggerTransactions, triggerTotalType);

            Assert.Multiple(() =>
            {
                Assert.That(actualResult, Has.Count.EqualTo(expectedResult.Count));

                for (int i = 0; i < actualResult.Count; i++)
                {
                    Assert.That(expectedResult[i], Is.EqualTo(actualResult[i]), $"Transaction {i}");
                }
            });
        }
    }
}