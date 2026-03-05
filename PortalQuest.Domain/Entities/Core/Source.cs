namespace PortalQuest.Domain.Entities.Core;
public class Source : BaseCoreEntity
{
	public string ShortName { get; set; }
	#region Relation
	public List<Spell> spells { get; set; }
	#endregion
}
