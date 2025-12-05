using PortalQuest.Application.Interfaces.Repository.Core;
using PortalQuest.Domain.Entities.Core;
using PortalQuest.Persistence.Context;

namespace PortalQuest.Persistence.Repository.Core;
public class SourceRepository : GenericRepository<Source>, ISourceRepository
{
	private readonly PortalQuestDbContext _dbContext;
	public SourceRepository(PortalQuestDbContext dbContext) : base(dbContext)
	{
		_dbContext = dbContext;
	}
}

