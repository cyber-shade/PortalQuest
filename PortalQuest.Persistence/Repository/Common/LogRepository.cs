using PortalQuest.Application.Interfaces.Repository.Common;
using PortalQuest.Domain.Entities.Common;
using PortalQuest.Domain.Enums.Common;
using PortalQuest.Domain.Interfaces;
using PortalQuest.Persistence.Context;

namespace PortalQuest.Persistence.Repository.Common
{
	public class LogRepository(PortalQuestDbContext dbContext, IGuidService guidService) : ILogRepository
	{
		public async Task Log(string message, string action, LogLevelEnum level)
		{
			var entity = new Log() { 
				Id = guidService.Generate(),
				Message = message,
				Action = action,
				Dateime = DateTime.UtcNow,
				Level = level
			};
			await dbContext.AddAsync(entity);
			await dbContext.SaveChangesAsync();
		}
	}
}
