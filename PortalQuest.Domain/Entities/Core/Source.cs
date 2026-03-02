namespace PortalQuest.Domain.Entities.Core;
public class Source : BaseCoreEntity
{

	#region Relation
	public List<Spell> spells { get; set; }
	#endregion
}
