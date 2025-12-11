using PortalQuest.Domain.Interfaces;

namespace PortalQuest.Infrastructure.Services;
public class GuidService : IGuidService
{
	public Guid Generate() => Guid.NewGuid();
} 
