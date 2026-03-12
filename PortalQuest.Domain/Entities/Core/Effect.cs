using PortalQuest.Domain.Enums.Core;

namespace PortalQuest.Domain.Entities.Core;
public class Effect : BaseCoreEntity
{
	public EffectTypesEnum Type { get; set; }
	public Guid SourceId { get; set; }
	public Book Source {  get; set; }
	public int SourcePage { get; set; }
	public bool SRD { get; set; }
	public bool BasicRules { get; set; }
	#region Relations
	public List<Spell> Spells { get; set; } // M2M
	#endregion
}
