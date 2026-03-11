using PortalQuest.Domain.Enums.Core;

namespace PortalQuest.Application.DTOs.Core
{
	public record EffectDto : BaseCoreEntityDto
	{
		public EffectTypesEnum Type { get; set; }
		public Guid SourceId { get; set; }
		public bool SRD { get; set; }
		public bool BasicRules { get; set; }
		public int SourcePage { get; set; }
	}
}
