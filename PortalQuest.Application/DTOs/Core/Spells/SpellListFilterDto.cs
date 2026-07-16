using PortalQuest.Domain.Enums.Common;
using PortalQuest.Domain.Enums.Core;

namespace PortalQuest.Application.DTOs.Core.Spells
{
	public class SpellListFilterDto 
	{
		public string Name { get; set; } = string.Empty;
		public List<Guid> Classes { get; set; } = new();
		public List<Guid> Sources { get; set; } = new();
		public List<MagicSchoolEnum> Schools { get; set; } = new();
		public SpellLevelsEnum? MinLevel { get; set; }
		public SpellLevelsEnum? MaxLevel { get; set; }
		public bool? Ritual { get; set; }
		public bool? Concentration { get; set; }
		public List<DamageTypeEnum> DamageTypes { get; set; } = new();
		public List<Guid> Conditions { get; set; } = new();
		public List<AttackTypeEnum> AttackTypes { get; set; } = new();
		public List<AbilityScoreEnum> SavingThrows { get; set; } = new();
		public List<AbilityScoreEnum> AbilityCheck { get; set; } = new();
		public List<Guid> CastTimes { get; set; } = new();
		public List<Guid> Durations { get; set; } = new();
		public List<DistanceTypeEnum> Ranges { get; set; } = new();
		public List<RangeTypeEnum> AreaStyles { get; set; } = new();
		public List<ComponentsEnum> Components { get; set; } = new();
		public int Skip { get; set; }
		public int Take { get; set; }
		public string Order { get; set; } = string.Empty;
		public LanguageCodeEnum LanguageCode { get; set; }
	}
}
