namespace BudgetTracker.Api.Controllers
{
    using BudgetTracker.Authentication.Interfaces;
    using BudgetTracker.Transactions.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;

    /// <summary>A controller for handling expense totals.</summary>
    /// <seealso cref="T:Microsoft.AspNetCore.Mvc.ControllerBase"/>
    [ApiController]
	[Route("api/[controller]")]
    public class TotalExpensesController : ControllerBase
    {
        /// <summary>(Immutable) the transaction service.</summary>
        private readonly ITransactionService transactionService;

        /// <summary>(Immutable) the authentication service.</summary>
        private readonly IAuthenticationService authenticationService;

        /// <summary>Initialises a new instance of the <see cref="BudgetTracker.Api.Controllers.TotalExpensesController"/> class.</summary>
        /// <param name="transactionService">(Immutable) the transaction service.</param>
        /// <param name="authorizationService">The authorization service.</param>
        public TotalExpensesController(ITransactionService transactionService, IAuthenticationService authenticationService)
        {
            this.transactionService = transactionService;
            this.authenticationService = authenticationService;
        }

        /// <summary>(An Action that handles HTTP GET requests) gets the get.</summary>
        /// <returns>An IActionResult.</returns>
        [HttpGet("{id}")]
        public IActionResult Get([FromQuery] string apikey, int id)
        {
            IActionResult returnValue = this.Unauthorized();

            if (this.authenticationService.TryValidateApiKey(apikey, out string _))
            {
                decimal totalExpenses = this.transactionService.GetTotalExpensesForAccountFrom(id, DateTime.Now.AddMonths(-1));

                returnValue = this.Ok(totalExpenses);
            }

            return returnValue;
        }
    }
}