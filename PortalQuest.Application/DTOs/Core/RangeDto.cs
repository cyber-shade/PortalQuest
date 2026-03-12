using PortalQuest.Application.DTOs.Common;
using PortalQuest.Domain.Enums.Core;

namespace PortalQuest.Application.DTOs.Core
{
	public record RangeDto : BaseEntityDto
	{
		public RangeTypeEnum Type { get; set; }
		public DistanceTypeEnum DistanceType { get; set; }
		public int Amount { get; set; }
	}
}
