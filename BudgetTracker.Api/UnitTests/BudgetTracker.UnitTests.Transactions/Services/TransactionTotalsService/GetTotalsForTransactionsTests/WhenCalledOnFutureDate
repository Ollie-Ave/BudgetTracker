using NUnit.Framework;
using System.Collections.Generic;
using System;
using BudgetTracker.DataAccess.Models;

namespace BudgetTracker.UnitTests.Transactions.TransactionTotalsService.GetTotalsForTransactionsTests
{
    [TestFixture]
    public class WhenCalledOnFutureDate
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

            DateTime triggerFromDate = DateTime.UtcNow.AddDays(1);

            List<decimal> expectedResult = new();

            List<decimal> actualResult = BudgetTracker.Transactions.Services.TransactionTotalsService.GetTotalsForTransactions(triggerTransactions, triggerFromDate);

            Assert.That(actualResult, Has.Count.EqualTo(expectedResult.Count));
        }
    }
}