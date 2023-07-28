namespace BudgetTracker.Api.Controllers
{
    using BudgetTracker.Authentication.Interfaces;
    using BudgetTracker.Transactions.Interfaces;
    using BudgetTracker.Transactions.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    /// <summary>A controller for handling transactions.</summary>
    /// <seealso cref="T:Microsoft.AspNetCore.Mvc.ControllerBase"/>
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        /// <summary>(Immutable) the authentication service.</summary>
        private readonly IAuthenticationService authenticationService;

        /// <summary>(Immutable) the transaction service.</summary>
        private readonly ITransactionService transactionService;

        /// <summary>Initialises a new instance of the <see cref="BudgetTracker.Api.Controllers.TransactionsController"/> class.</summary>
        /// <param name="authenticationService">The authentication service.</param>
        /// <param name="context">Context for the application database.</param>
        public TransactionsController(IAuthenticationService authenticationService, ITransactionService transactionService)
        {
            this.authenticationService = authenticationService;
            this.transactionService = transactionService;
        }

        /// <summary>(An Action that handles HTTP GET requests) gets.</summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>An IActionResult.</returns>
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

        /// <summary>(An Action that handles HTTP PUT requests) puts.</summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>An IActionResult.</returns>
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
                decimal newBalance = this.transactionService.AddTransaction(transaction, id);

                returnValue = this.Ok(newBalance);
            }

            return returnValue;
        }

        /// <summary>(An Action that handles HTTP DELETE requests) deletes this object.</summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>An IActionResult.</returns>
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