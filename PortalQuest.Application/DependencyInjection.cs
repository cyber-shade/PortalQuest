using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PortalQuest.Application.Features.Common.Pipeline;


namespace PortalQuest.Application;
public static class DependencyInjection
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		services.AddAutoMapper(cfg => cfg.AddMaps(Assembly.GetExecutingAssembly()));
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));

		return services;
	}
}

