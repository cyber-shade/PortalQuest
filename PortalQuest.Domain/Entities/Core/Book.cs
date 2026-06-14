using PortalQuest.Domain.Entities.Common;
using PortalQuest.Domain.Entities.Core.Translations;
using PortalQuest.Domain.Interfaces;

namespace PortalQuest.Domain.Entities.Core;
public class Book : BaseEntity, ITranslatable<BookTranslation>
{
	public string ShortName { get; set; }
	public DateTime PublishedDateTime { get; set; }
	public string Author { get; set; }
	#region Relation
	public List<Spell> Spells { get; set; }
	public List<BookTranslation> Translations { get; set; }
	#endregion
}
