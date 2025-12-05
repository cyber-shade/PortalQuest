using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PortalQuest.Application.Interfaces.Repository;
using PortalQuest.Application.Interfaces.Repository.Core;
using PortalQuest.Persistence.Context;
using PortalQuest.Persistence.Repository;
using PortalQuest.Persistence.Repository.Core;

namespace PortalQuest.Persistence;
public static class DependencyInjection
{
	public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<PortalQuestDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
		#region IOC EFCore
		services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
		services.AddScoped<IClassRepository, ClassRepository>();
		services.AddScoped<ISourceRepository, SourceRepository>();
		services.AddScoped<ISpellRepository, SpellRepository>();
		#endregion
		return services;
	}
}
