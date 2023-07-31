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

            List<decimal> expectedResult = new();

            List<decimal> actualResult = BudgetTracker.Transactions.Services.TransactionTotalsService.GetTotalsForTransactions(triggerTransactions, triggerFromDate);

            Assert.That(actualResult, Has.Count.EqualTo(expectedResult.Count));
        }
    }
}