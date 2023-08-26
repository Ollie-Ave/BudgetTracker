using NUnit.Framework;
using System.Collections.Generic;
using System;
using BudgetTracker.DataAccess.Models;

namespace BudgetTracker.UnitTests.Transactions.TransactionTotalsService.GetTotalsForTransactionsTests
{
    [TestFixture]
    public class WhenCalledOnValidTransactionList
    {
        [Test]
        public void ThenExpectValidResponse()
        {
            List<Transaction> triggerTransactions = new()
            {
                new()
                {
                    Amount = 333,
                    TimeOfPurchase = DateTime.UtcNow.AddDays(-3)
                },
                new()
                {
                    Amount = 33,
                    TimeOfPurchase = DateTime.UtcNow.AddDays(-3)
                },
                new()
                {
                    Amount = -3,
                    TimeOfPurchase = DateTime.UtcNow.AddDays(-3)
                },
                new()
                {
                    Amount = 22,
                    TimeOfPurchase = DateTime.UtcNow.AddDays(-2)
                },
                new()
                {
                    Amount = -2,
                    TimeOfPurchase = DateTime.UtcNow.AddDays(-2)
                },
                new()
                {
                    Amount = 2,
                    TimeOfPurchase = DateTime.UtcNow.AddDays(-1)
                },
                new()
                {
                    Amount = -2,
                    TimeOfPurchase = DateTime.UtcNow.AddDays(-1)
                },
            };

            DateTime triggerFromDate = DateTime.UtcNow.AddDays(-3);

            List<decimal> expectedResult = new()
            {
                333 + 33 - 3,
                22 - 2,
                0,
                0,
            };

            List<decimal> actualResult = BudgetTracker.Transactions.Services.TransactionTotalsService.GetTotalsForTransactions(triggerTransactions, triggerFromDate);

            Assert.Multiple(() =>
            {
                Assert.That(actualResult, Has.Count.EqualTo(expectedResult.Count));

                for (int i = 0; i < actualResult.Count; i++)
                {
                    Assert.That(expectedResult[i], Is.EqualTo(actualResult[i]), $"Day {i}");
                }
            });
        }
    }
}