using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PortalQuest.Domain.Entities.Common;
using PortalQuest.Domain.Entities.Core.M2M;

namespace PortalQuest.Domain.Entities.Core;
public class Spell : BaseCoreEntity
{
	[Range(0,9)] // 0 means it is cantrip
	public int Level { get; set; }
	public bool Ritual { get; set; }
	public bool Concentration { get; set; }
	public Dictionary<string,int> DamageDices { get; set; }
	#region Relation
	public int MagicSchoolId { get; set; }
	[ForeignKey(nameof(MagicSchoolId))]
	public MagicSchool MagicSchool { get; set; }
	public int? SaveId { get; set; }
	[ForeignKey(nameof(SaveId))]
	public AbilityScore? Save {  get; set; }
	public int CastingTimeId { get; set; }
	[ForeignKey(nameof(CastingTimeId))]
	public CastingTime CastingTime { get; set; }
	public int DurationId { get; set; }
	[ForeignKey(nameof(DurationId))]
	public Duration Duration { get; set; }
	public int? DamageTypeId { get; set; }
	[ForeignKey(nameof(DamageTypeId))]
	public DamageType? DamageType { get; set; }
	public int? ConditionId { get; set; }
	[ForeignKey(nameof(ConditionId))]
	public Condition? Condition { get; set; }
	public int RangeId { get; set; }
	[ForeignKey(nameof(RangeId))]
	public Range Range { get; set; }
	public int? AttackTypeId { get; set; }
	[ForeignKey(nameof(AttackTypeId))]
	public AttackType? AttackType { get; set; }
	#endregion
	#region M2M Relation
	public ICollection<SpellClass> SpellClasses { get; set; }
	public ICollection<SpellTag> SpellTags { get; set; }
	#endregion
}

