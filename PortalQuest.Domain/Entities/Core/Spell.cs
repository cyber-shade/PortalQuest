using System.ComponentModel.DataAnnotations.Schema;
using PortalQuest.Domain.Entities.Core.M2M;
using PortalQuest.Domain.Entities.Core.Translations;
using PortalQuest.Domain.Enums.Core;
using PortalQuest.Domain.Interfaces;

namespace PortalQuest.Domain.Entities.Core
{
	public class Spell : BaseRuleEntity, ITranslatable<SpellTranslation>
	{
		public SpellLevelsEnum Level { get; set; }
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
		public bool? MaterialConsume { get; set; }
		#region Realation
		public List<Duration> Duration { get; set; }
		public Guid RangeId { get; set; }
		[ForeignKey(nameof(RangeId))]
		public Range Range { get; set; }
		public List<Time> CastingTime { get; set; } // O2M
		public List<SpellClass> SpellClasses { get; set; } // M2M
		public List<Effect> Conditions { get; set; } // M2M
		public List<SpellTranslation> Translations { get; set; }
		#endregion
	}
}
