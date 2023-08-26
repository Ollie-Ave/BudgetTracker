using BudgetTracker.Transactions.Interfaces;
using BudgetTracker.Transactions.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddTransactionServices(this IServiceCollection services)
        {
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ITransactionTotalsService, TransactionTotalsService>();

            return services;
        }
    }
}
