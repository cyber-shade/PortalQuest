using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PortalQuest.Application.Interfaces;
using PortalQuest.Persistence.Context;
using PortalQuest.Persistence.Repository;

namespace PortalQuest.Persistence;
public static class DependencyInjection
{
	public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<PortalQuestDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
		#region IOC EFCore
		services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
		#endregion
		return services;
	}
}
