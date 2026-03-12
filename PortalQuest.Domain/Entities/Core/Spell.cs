using System.ComponentModel.DataAnnotations.Schema;
using PortalQuest.Domain.Entities.Core.M2M;
using PortalQuest.Domain.Enums.Core;

namespace PortalQuest.Domain.Entities.Core
{
	public class Spell : BaseCoreEntity
	{
		public int SourcePage { get; set; }
		public bool SRD { get; set; }
		public bool BasicRules { get; set; }
		public string NameInSRD { get; set; }
		public int Level { get; set; }
		public bool Concentration { get; set; }
		public bool Ritual { get; set; }
		public MagicSchoolEnum School { get; set; }
		public List<AbilityScoreEnum> AbilityCheck { get; set; }
		public List<AbilityScoreEnum> SavingThrow { get; set; }
		public List<DamageTypeEnum> DamageType { get; set; }
		public bool Verbal { get; set; }
		public bool Somatic { get; set; }
		public bool Material { get; set; }
		public int? MaterialCost { get; set; }
		public string? MaterialDescription { get; set; }
		public bool? MaterialConsume { get; set; }
		#region Realation
		public List<Duration> Duration { get; set; }
		public Guid RangeId { get; set; }
		[ForeignKey(nameof(RangeId))]
		public Range Range { get; set; }
		public Guid SourceId { get; set; }
		[ForeignKey(nameof(SourceId))]
		public Book Source { get; set; }
		public List<Time> CastingTime { get; set; } // O2M
		public List<SpellClass> SpellClasses { get; set; } // M2M
		public List<Effect> Conditions { get; set; } // M2M
		#endregion
	}
}
