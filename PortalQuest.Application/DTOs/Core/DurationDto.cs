using PortalQuest.Application.DTOs.Common;
using PortalQuest.Domain.Enums.Core;

namespace PortalQuest.Application.DTOs.Core
{
	public record DurationDto : BaseEntityDto
	{
		public DurationTypeEnum Type { get; set; }
		public Guid? TimeId { get; set; }
		public List<string> Ends { get; set; }
	}
}
