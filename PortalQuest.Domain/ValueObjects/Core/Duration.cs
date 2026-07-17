using PortalQuest.Domain.Enums.Core;

namespace PortalQuest.Domain.ValueObjects.Core
{
	public record Duration
	{
		public	DurationTypeEnum Type { get; init; }
		public List<string> Ends { get; init; } // if type is Permanent
											   //dispel
											   //trigger
		public Time? Time { get; init; } // if type is timed
	}
}
