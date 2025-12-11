using Microsoft.Extensions.DependencyInjection;
using PortalQuest.Domain.Interfaces;
using PortalQuest.Infrastructure.Services;

namespace PortalQuest.Infrastructure;
public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
	{
		services.AddTransient<IGuidService, GuidService>();
		return services;
	}
}
