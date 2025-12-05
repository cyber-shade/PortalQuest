using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PortalQuest.Domain.Entities.Core.M2M;
using PortalQuest.Domain.Enums.Core;

namespace PortalQuest.Domain.Entities.Core;
public class Spell : BaseCoreEntity
{
	[Range(0,9)] // 0 means it is cantrip
	public int Level { get; set; }
	public bool Ritual { get; set; }
	public bool Concentration { get; set; }
	public Dictionary<string, int> DamageDices { get; set; }
	public MagicSchoolEnum MagicSchool { get; set; }
	public AbilityScoreEnum? Save { get; set; }
	public AttackTypeEnum? AttackType { get; set; }
	public string CastingTime { get; set; }
	public string Range { get; set; }
	public string Duration { get; set; }
	public string DamageType { get; set; }
	public string Components { get; set; }
	#region Relation
	public Guid? ConditionId { get; set; }
	[ForeignKey(nameof(ConditionId))]
	public Condition? Condition { get; set; }
	public Guid? SourceId { get; set; }
	[ForeignKey(nameof(SourceId))]
	public Source Source { get; set; }	
	#endregion
	#region M2M Relation
	public ICollection<SpellClass> SpellClasses { get; set; }
	public ICollection<SpellTag> SpellTags { get; set; }
	#endregion
}

