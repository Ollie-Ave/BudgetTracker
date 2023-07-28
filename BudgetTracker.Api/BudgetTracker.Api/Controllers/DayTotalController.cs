namespace BudgetTracker.Api.Controllers
{
    using BudgetTracker.Authentication.Interfaces;
    using BudgetTracker.Transactions.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    [ApiController]
    [Route("api/[controller]")]
    public class DayTotalController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;
        private readonly ITransactionService transactionService;

        public DayTotalController(IAuthenticationService authenticationService, ITransactionService transactionService)
        {
            this.authenticationService = authenticationService;
            this.transactionService = transactionService;
        }

        /// <summary>(An Action that handles HTTP GET requests) gets the get.</summary>
        /// <param name="apikey">The apikey.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>An IActionResult.</returns>
        [HttpGet("expense/{id}")]
        public IActionResult GetDayExpenseTotals([FromQuery] string apikey, int id)
        {
            IActionResult returnValue = this.Unauthorized();

            if (this.authenticationService.ApiKeyIsValid(apikey))
            {
                List<decimal> totalExpenses = this.transactionService.GetDayExpenseTotals(id, 7);

                returnValue = this.Ok(totalExpenses);
            }

            return returnValue;
        }

        /// <summary>(An Action that handles HTTP GET requests) gets the get.</summary>
        /// <param name="apikey">The apikey.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>An IActionResult.</returns>
        [HttpGet("income/{id}")]
        public IActionResult GetDayIncomeTotals([FromQuery] string apikey, int id)
        {
            IActionResult returnValue = this.Unauthorized();

            if (this.authenticationService.ApiKeyIsValid(apikey))
            {
                List<decimal> totalIncome = this.transactionService.GetDayIncomeTotals(id, 7);

                returnValue = this.Ok(totalIncome);
            }

            return returnValue;
        }

        /// <summary>(An Action that handles HTTP GET requests) gets the get.</summary>
        /// <param name="apikey">The apikey.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>An IActionResult.</returns>
        [HttpGet("difference/{id}")]
        public IActionResult GetDayDifferenceTotals([FromQuery] string apikey, int id)
        {
            IActionResult returnValue = this.Unauthorized();

            if (this.authenticationService.ApiKeyIsValid(apikey))
            {
                List<decimal> totalDifferences = this.transactionService.GetDayDifferenceTotals(id, 7);

                returnValue = this.Ok(totalDifferences);
            }

            return returnValue;
        }
    }
}
