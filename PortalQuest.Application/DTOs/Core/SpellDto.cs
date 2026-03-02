using System.ComponentModel.DataAnnotations.Schema;
using PortalQuest.Domain.Entities.Core;
using PortalQuest.Domain.Entities.Core.M2M;
using PortalQuest.Domain.Enums.Core;

namespace PortalQuest.Application.DTOs.Core
{
	public class ImportSpellDto
	{
		public int SourcePage { get; set; }
		public bool SRD { get; set; }
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
		public Range Range { get; set; }
		public Source Source { get; set; }
		public List<Time> CastingTime { get; set; }
		public List<SpellClass> SpellClasses { get; set; }
		#endregion
	}
}
