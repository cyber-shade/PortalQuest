using PortalQuest.Application.DTOs.Core.Common;

namespace PortalQuest.Application.DTOs.Core.Duration
{
	public class DurationDto
	{
		
		public string Type { get; set; }
		public List<AmountDto> Duration { get; set; }
		public bool Concentration { get; set; }
		public int Amount { get; set; }
	}
}
