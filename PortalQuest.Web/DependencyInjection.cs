using PortalQuest.Web.Middlewares;
using Serilog;

namespace PortalQuest.Web
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration) {
			services.AddControllers();

			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();
			services.AddTransient<CorrelationIdMiddleware>();
			services.AddTransient<ExceptionHandlingMiddleware>();
			services.AddSerilog(options =>
				options.ReadFrom.Configuration(configuration)
			);
			
			
			return services;
		}
		public static IApplicationBuilder UseCustomMiddlewares(this IApplicationBuilder app)
		{
			app.UseMiddleware<CorrelationIdMiddleware>();
			app.UseSerilogRequestLogging();
			app.UseMiddleware<ExceptionHandlingMiddleware>();

			return app;
		}
	}
}
