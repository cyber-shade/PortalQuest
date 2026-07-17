using PortalQuest.Domain.Enums.Core;

namespace PortalQuest.Domain.ValueObjects.Core
{
	public record Time
	{
		public int Amount { get; init; }
		public TimeTypeEnum Type {get; init; }
		public string Condition { get; init; }
	}
}
