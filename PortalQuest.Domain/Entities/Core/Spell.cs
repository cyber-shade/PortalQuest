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
	#region Relation
	public Guid CastingTimeId { get; set; }
	[ForeignKey(nameof(CastingTimeId))]
	public CastingTime CastingTime { get; set; }
	public Guid DurationId { get; set; }
	[ForeignKey(nameof(DurationId))]
	public Duration Duration { get; set; }
	public Guid? DamageTypeId { get; set; }
	[ForeignKey(nameof(DamageTypeId))]
	public DamageType? DamageType { get; set; }
	public Guid? ConditionId { get; set; }
	[ForeignKey(nameof(ConditionId))]
	public Condition? Condition { get; set; }
	public Guid RangeId { get; set; }
	[ForeignKey(nameof(RangeId))]
	public Range Range { get; set; }
	#endregion
	#region M2M Relation
	public ICollection<SpellClass> SpellClasses { get; set; }
	public ICollection<SpellTag> SpellTags { get; set; }
	#endregion
}

