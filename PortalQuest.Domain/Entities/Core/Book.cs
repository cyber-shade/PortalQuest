namespace PortalQuest.Domain.Entities.Core;
public class Book : BaseCoreEntity
{
	public string ShortName { get; set; }
	public DateTime PublishedDateTime { get; set; }
	public string Author { get; set; }
	#region Relation
	public List<Spell> spells { get; set; }
	#endregion
}
