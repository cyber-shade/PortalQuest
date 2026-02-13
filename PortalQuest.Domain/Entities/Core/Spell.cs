using PortalQuest.Domain.Entities.Core.M2M;
using PortalQuest.Domain.Enums.Core;

namespace PortalQuest.Domain.Entities.Core
{
	public class Spell : BaseCoreEntity
	{
		public int SourcePage { get; set; }
		public bool SRD { get; set; }
		public string NameInSRD { get; set; }
		public int Level { get; set; }
		public bool Concentration { get; set; }
		public bool Ritual { get; set; }
		public MagicSchoolEnum School { get; set; }
		public Component Components { get; set; }
		public List<AbilityScoreEnum> AbilityCheck { get; set; }
		public List<AbilityScoreEnum> SavingThrow { get; set; }
		public List<DamageTypeEnum> DamageType { get; set; }
		#region Realation
		public List<Duration> Duration { get; set; }
		public Range Range { get; set; }
		public Source Source { get; set; }
		public List<Time> CastingTime { get; set; }
		#endregion
		#region M2M Realation
		public List<SpellClass> SpellClasses { get; set; }
		#endregion
	}
}
