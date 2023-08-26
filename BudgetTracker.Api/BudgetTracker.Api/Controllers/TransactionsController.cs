namespace BudgetTracker.Api.Controllers
{
    using BudgetTracker.Authentication.Interfaces;
    using BudgetTracker.Transactions.Interfaces;
    using BudgetTracker.Transactions.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        private readonly ITransactionService transactionService;

        public TransactionsController(IAuthenticationService authenticationService, ITransactionService transactionService)
        {
            this.authenticationService = authenticationService;
            this.transactionService = transactionService;
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromQuery] string apiKey, int id, [FromQuery] int page = 0)
        {
            IActionResult returnValue = this.Unauthorized();

            if (this.authenticationService.ApiKeyIsValid(apiKey))
            {
                List<TransactionViewModel> transactions = this.transactionService.GetTransactionsForAccount(id, page);

                returnValue = this.Ok(transactions);
            }

            return returnValue;
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromQuery] string apiKey, int id, [FromBody] TransactionViewModel transaction)
        {
            IActionResult returnValue = this.Unauthorized();

            if (this.authenticationService.ApiKeyIsValid(apiKey))
            {
                decimal newBalance = this.transactionService.UpdateTransaction(transaction);

                returnValue = this.Ok(newBalance);
            }

            return returnValue;
        }

        [HttpPost("{id}")]
        public IActionResult Post([FromQuery] string apiKey, int id, [FromBody] TransactionUploadModel transaction)
        {
            IActionResult returnValue = this.Unauthorized();

            if (this.authenticationService.ApiKeyIsValid(apiKey))
            {
                decimal newBalance = this.transactionService.CreateTransaction(transaction, id);

                returnValue = this.Ok(newBalance);
            }

            return returnValue;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromQuery] string apiKey, int id)
        {
            IActionResult returnValue = this.Unauthorized();

            if (this.authenticationService.ApiKeyIsValid(apiKey))
            {
                decimal newBalance = this.transactionService.DeleteTransaction(id);

                returnValue = this.Ok(newBalance);
            }

            return returnValue;
        }

    }
}