using PortalQuest.Domain.Entities.Common;
using PortalQuest.Domain.Entities.Core.Translations;
using PortalQuest.Domain.Enums.Core;
using PortalQuest.Domain.Interfaces;

namespace PortalQuest.Domain.Entities.Core;
public class Effect : BaseEntity, ITranslatable<EffectTranslation>
{
	public EffectTypesEnum Type { get; set; }
	public Guid SourceId { get; set; }
	public Book Source {  get; set; }
	public int SourcePage { get; set; }
	public bool SRD { get; set; }
	public bool BasicRules { get; set; }
	#region Relations
	public List<Spell> Spells { get; set; } // M2M
	public List<EffectTranslation> Translations { get; set;  }
	#endregion
}
