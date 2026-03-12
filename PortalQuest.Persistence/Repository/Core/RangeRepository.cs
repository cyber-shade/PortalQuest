using PortalQuest.Application.Interfaces.Repository.Core;
using PortalQuest.Persistence.Context;
using PortalQuest.Persistence.Repository.Common;
using Entities = PortalQuest.Domain.Entities.Core;

namespace PortalQuest.Persistence.Repository.Core
{
	public class RangeRepository : GenericRepository<Entities.Range>, IRangeRepository
	{
		private readonly PortalQuestDbContext _dbContext;
		public RangeRepository(PortalQuestDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}
	}
}