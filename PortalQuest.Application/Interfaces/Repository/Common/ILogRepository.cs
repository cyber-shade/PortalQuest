using PortalQuest.Domain.Enums.Common;

namespace PortalQuest.Application.Interfaces.Repository.Common
{
	public interface ILogRepository
	{
		Task Log(string message, string action, LogLevelEnum level);
	}
}
