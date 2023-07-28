using BudgetTracker.Accounts.Interfaces;
using BudgetTracker.Accounts.Services;
using BudgetTracker.Api.Models;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class IServiceCollectionExtensions
	{
		public static IServiceCollection AddAccountServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<IAccountService, AccountService>();
			services.AddScoped<IProfilePictureService, ProfilePictureService>();
			services.Configure<ImageOptions>(configuration.GetSection(nameof(ImageOptions)));

			return services;
		}
	}
}