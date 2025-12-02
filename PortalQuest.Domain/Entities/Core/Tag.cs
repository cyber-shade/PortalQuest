using PortalQuest.Domain.Entities.Core.M2M;

namespace PortalQuest.Domain.Entities.Core;
public class Tag : BaseCoreEntity
{
	public ICollection<SpellTag> SpellTags { get; set; }
}
