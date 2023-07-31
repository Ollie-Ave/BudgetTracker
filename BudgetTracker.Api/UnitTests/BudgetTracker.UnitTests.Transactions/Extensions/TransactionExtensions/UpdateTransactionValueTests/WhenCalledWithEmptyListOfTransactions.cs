
using System.Collections.Generic;
using System.Linq;
using BudgetTracker.DataAccess.Models;
using BudgetTracker.Transactions.Extensions;
using NUnit.Framework;

namespace BudgetTracker.UnitTests.Transactions.Extensions.UpdateTransactionValue
{
    [TestFixture]
     public class WhenCalledWithEmptyListOfTransactions
    {
        [Test]
        public void ThenReturnValueContainsOnlySourceTransaction()
        {
            Transaction subjectUnderTest = new()
            {
                Amount = 1,
                BalanceBeforePurchase = 10,
                BalanceAfterPurchase = 11,
            };

            List<Transaction> triggerSubsequentTransactions = new();

            decimal triggerNewTransactionAmount = 2;

            List<Transaction> expectedResult = new()
            {
                new()
                {
                    Amount = 2,
                    BalanceBeforePurchase = 10,
                    BalanceAfterPurchase = 12,
                }
            };

            List<Transaction> actualResult = subjectUnderTest.UpdateTransactionValue(triggerSubsequentTransactions, triggerNewTransactionAmount);

            Assert.Multiple(() =>
            {
                Assert.That(actualResult.First().Amount, Is.EqualTo(triggerNewTransactionAmount));

                for (int i = 0; i < actualResult.Count; i++)
                {
                    Assert.That(actualResult[i].BalanceBeforePurchase, Is.EqualTo(expectedResult[i].BalanceBeforePurchase), $"Transaction '{i}', BalanceBeforePurchase is incorrect.");
                    Assert.That(actualResult[i].BalanceAfterPurchase, Is.EqualTo(expectedResult[i].BalanceAfterPurchase), $"Transaction '{i}', BalanceAfterPurchase is incorrect.");
                }
            });
        }
    }
}