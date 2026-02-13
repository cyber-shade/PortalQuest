using PortalQuest.Application.DTOs.Core.Common;

namespace PortalQuest.Application.DTOs.Core.Range
{
	public class UpsertRangeDto
	{
		public string Type { get; set; }
		public AmountDto Distance { get; set; }
	}
}
