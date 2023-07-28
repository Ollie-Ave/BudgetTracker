namespace BudgetTracker
{
	using Microsoft.AspNetCore.Builder;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;

	internal class Program
	{
		private static void Main(string[] args)
		{
			WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers();

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddCors();

			builder.Services.AddApplicationDbContext(builder.Configuration);
			builder.Services.AddAuthenticationServices();
			builder.Services.AddAccountServices(builder.Configuration);
			builder.Services.AddTransactionServices();
			
			WebApplication app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseCors(builder =>
				builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}	
}