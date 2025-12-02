using PortalQuest.Domain.Entities.Common;

namespace PortalQuest.Domain.Entities.Core;
public class BaseCoreEntity : BaseEntity
{
	public string Name { get; set; }
	public string? Description { get; set; }
}
