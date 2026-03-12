using PortalQuest.Application.DTOs.Common;
using PortalQuest.Domain.Enums.Core;

namespace PortalQuest.Application.DTOs.Core
{
	public record TimeDto : BaseEntityDto
	{
		public int Amount { get; set; }
		public TimeTypeEnum Type { get; set; }
		public string Condition { get; set; }
	}
}
