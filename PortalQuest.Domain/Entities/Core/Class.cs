using PortalQuest.Domain.Entities.Common;
using PortalQuest.Domain.Entities.Core.M2M;

namespace PortalQuest.Domain.Entities.Core;
public class Class : BaseCoreEntity
{
	#region M2M Relation
	public ICollection<SpellClass> SpellClasses { get; set; }
	#endregion
}

