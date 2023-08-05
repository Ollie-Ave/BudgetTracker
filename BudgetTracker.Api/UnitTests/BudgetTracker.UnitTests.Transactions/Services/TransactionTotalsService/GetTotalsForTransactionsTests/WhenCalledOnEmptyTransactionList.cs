using NUnit.Framework;
using System.Collections.Generic;
using System;
using BudgetTracker.DataAccess.Models;

namespace BudgetTracker.UnitTests.Transactions.TransactionTotalsService.GetTotalsForTransactionsTests
{
    [TestFixture]
    public class WhenCalledOnEmptyTransactionList
    {
        [Test]
        public void ThenExpectValidResponse()
        {
            List<Transaction> triggerTransactions = new();

            DateTime triggerFromDate = DateTime.UtcNow.AddDays(-3);

            List<decimal> expectedResult = new()
            {
                0,
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