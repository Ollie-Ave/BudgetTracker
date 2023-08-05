namespace BudgetTracker.Api.Controllers
{
    using BudgetTracker.Authentication.Interfaces;
    using BudgetTracker.Transactions.Enums;
    using BudgetTracker.Transactions.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;

    [ApiController]
    [Route("api/[controller]")]
    public class DayTotalsController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        private readonly ITransactionTotalsService transactionTotalsService;

        public DayTotalsController(IAuthenticationService authenticationService, ITransactionTotalsService transactionTotalsService)
        {
            this.authenticationService = authenticationService;
            this.transactionTotalsService = transactionTotalsService;
        }

        [HttpGet("{id}")]
        public IActionResult GetTotals(int id, [FromQuery] string api, [FromQuery] int days, [FromQuery] TotalType totalType)
        {
            IActionResult returnValue = this.Unauthorized();

            if (this.authenticationService.ApiKeyIsValid(api))
            {
                DateTime dateToGetFrom = DateTime.Now.AddDays(days * -1);

                returnValue = this.Ok(this.transactionTotalsService.GetTotalsFrom(id, dateToGetFrom, totalType));
            }

            return returnValue;
        }

        [HttpGet("sum/{id}")]
        public IActionResult GetSummedTotals(int id, [FromQuery] string apiKey, [FromQuery] int days, [FromQuery] TotalType totalType)
        {
            IActionResult returnValue = this.Unauthorized();

            if (this.authenticationService.ApiKeyIsValid(apiKey))
            {
                DateTime dateToGetFrom = DateTime.Now.AddDays(days * -1);

                returnValue = this.Ok(this.transactionTotalsService.GetTotalsFrom(id, dateToGetFrom, totalType).Sum());
            }

            return returnValue;
        }
    }
}
