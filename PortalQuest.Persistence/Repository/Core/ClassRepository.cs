using PortalQuest.Application.Interfaces.Repository.Core;
using PortalQuest.Domain.Entities.Core;
using PortalQuest.Persistence.Context;

namespace PortalQuest.Persistence.Repository.Core;
public class ClassRepository : GenericRepository<Class>, IClassRepository
{
	private readonly PortalQuestDbContext _dbContext;
	public ClassRepository(PortalQuestDbContext dbContext) : base(dbContext)
	{
		_dbContext = dbContext;
	}
}
