using PortalQuest.Domain.Enums.Core;

namespace PortalQuest.Application.DTOs.Core
{
	public record DurationDto
	{
		public DurationTypeEnum Type { get; set; }
		public TimeDto? Time { get; set; }
		public List<string> Ends { get; set; }
	}
}
