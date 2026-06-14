using PortalQuest.Domain.Enums.Core;

namespace PortalQuest.Application.DTOs.Core
{
	public record SpellDto : BaseTranslationDto
	{
		public int SourcePage { get; set; }
		public bool SRD { get; set; }
		public bool BasicRules { get; set; }
		public string NameInSRD { get; set; }
		public int Level { get; set; }
		public bool Concentration { get; set; }
		public bool Ritual { get; set; }
		public MagicSchoolEnum School { get; set; }
		public List<AbilityScoreEnum> AbilityCheck { get; set; } = new List<AbilityScoreEnum>();
		public List<AbilityScoreEnum> SavingThrow { get; set; } = new List<AbilityScoreEnum>();
		public List<DamageTypeEnum> DamageType { get; set; } = new List<DamageTypeEnum>();
		public bool Verbal { get; set; }
		public bool Somatic { get; set; }
		public bool Material { get; set; }
		public int? MaterialCost { get; set; }
		public string? MaterialDescription { get; set; }
		public bool? MaterialConsume { get; set; }
		public List<Guid> DurationIds { get; set; } = new List<Guid>();
		public List<SpellClassDto> ClassIds { get; set; } = new List<SpellClassDto>();
		public Guid RangeId { get; set; }
		public Guid SourceId { get; set; }
		public List<Guid> CastingTimeIds { get; set; } = new List<Guid>();
		public List<Guid> ConditionIds { get; set; } = new List<Guid>();

	}
}
