using PortalQuest.Application.Interfaces.Repository.Core;
using PortalQuest.Domain.Entities.Core;
using PortalQuest.Persistence.Context;
using PortalQuest.Persistence.Repository.Common;

namespace PortalQuest.Persistence.Repository.Core
{
	public class DurationRepository : GenericRepository<Duration>, IDurationRepository
 	{
		private readonly PortalQuestDbContext _dbContext;
		public DurationRepository(PortalQuestDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;	
		}
	}
}
