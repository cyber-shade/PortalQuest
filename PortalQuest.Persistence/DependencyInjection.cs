using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PortalQuest.Persistence.Context;

namespace PortalQuest.Persistence;
public static class DependencyInjection
{
	public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<PortalQuestDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
		return services;
	}
}
