using PortalQuest.Application.Interfaces.Repository.Core;
using PortalQuest.Domain.Entities.Core;
using PortalQuest.Persistence.Context;
using PortalQuest.Persistence.Repository.Common;

namespace PortalQuest.Persistence.Repository.Core
{
	public class TimeRepository : GenericRepository<Time>, ITimeRepository
 	{
		private readonly PortalQuestDbContext _dbContext;
		public TimeRepository(PortalQuestDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}
	}
}
