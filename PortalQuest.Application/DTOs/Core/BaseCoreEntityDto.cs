using PortalQuest.Application.DTOs.Common;

namespace PortalQuest.Application.DTOs.Core
{
	public record BaseCoreEntityDto : BaseEntityDto
	{
		public string Name { get; set; }
		public string Content { get; set; }
	}
}
