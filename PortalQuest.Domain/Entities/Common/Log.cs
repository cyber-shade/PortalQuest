using PortalQuest.Domain.Enums.Common;

namespace PortalQuest.Domain.Entities.Common
{
	public class Log :BaseEntity
	{
		public LogLevelEnum Level { get; set; }
		public string Message { get; set; }
		public string Action { get; set; }
		public DateTime Dateime { get; set; }

	}
}
