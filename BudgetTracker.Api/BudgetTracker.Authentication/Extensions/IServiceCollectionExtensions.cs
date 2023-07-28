namespace Microsoft.Extensions.DependencyInjection
{
	using BudgetTracker.Authentication.Interfaces;
	using BudgetTracker.Authentication.Services;

	public static class IServiceCollectionExtensions
	{
		public static IServiceCollection AddAuthenticationServices(this IServiceCollection services)
		{
			services.AddScoped<IAuthenticationService, AuthenticationService>();
			services.AddMemoryCache();
			
			return services;
		}
	}
}