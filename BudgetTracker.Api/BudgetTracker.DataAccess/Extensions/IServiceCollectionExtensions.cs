namespace Microsoft.Extensions.DependencyInjection
{
	using BudgetTracker.DataAccess.Context;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Configuration;

	public static class IServiceCollectionExtensions
	{
		public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("ApplicationDbContext"));
			});	
				
			return services;		
		}
	}
}