using PortalQuest.Domain.Enums.Core;

namespace PortalQuest.Domain.ValueObjects.Core
{
	public record Range
	{
		public RangeTypeEnum Type { get; init; }
		public DistanceTypeEnum DistanceType { get; init; }
		public int Amount { get; init; }
	}
}
