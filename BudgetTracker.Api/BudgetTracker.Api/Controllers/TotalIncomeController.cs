namespace BudgetTracker.Api.Controllers
{
    using BudgetTracker.Accounts.Interfaces;
    using BudgetTracker.Authentication.Interfaces;
    using BudgetTracker.Transactions.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    /// <summary>A controller for handling total incomes.</summary>
    /// <seealso cref="T:Microsoft.AspNetCore.Mvc.ControllerBase"/>
    [ApiController]
    [Route("api/[controller]")]
    public class TotalIncomeController : ControllerBase
    {
        /// <summary>(Immutable) the authentication service.</summary>
        private readonly IAuthenticationService authenticationService;

        /// <summary>(Immutable) the account service.</summary>
        private readonly IAccountService accountService;

        /// <summary>Initialises a new instance of the <see cref="BudgetTracker.Api.Controllers.TotalIncomeController"/> class.</summary>
        /// <param name="authenticationService">(Immutable) the authentication service.</param>
        /// <param name="accountService">(Immutable) the account service.</param>
        /// <param name="transactionService">(Immutable) the transaction service.</param>
        public TotalIncomeController(IAuthenticationService authenticationService, IAccountService accountService)
        {
            this.authenticationService = authenticationService;
            this.accountService = accountService;
        }

        /// <summary>(An Action that handles HTTP GET requests) gets the get.</summary>
        /// <returns>An IActionResult.</returns>
        [HttpGet("{id}")]
        public IActionResult Get([FromQuery] string apiKey, int id)
        {
            IActionResult returnValue = this.Unauthorized();

            if (this.authenticationService.TryValidateApiKey(apiKey, out string _))
            {
                decimal totalIncome = this.accountService.GetTotalIncomePerMonth(id);

                returnValue = this.Ok(totalIncome);
            }

            return returnValue;
        }
    }
}