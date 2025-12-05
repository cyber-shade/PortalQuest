using PortalQuest.Application.Interfaces.Repository.Core;
using PortalQuest.Domain.Entities.Core;
using PortalQuest.Persistence.Context;

namespace PortalQuest.Persistence.Repository.Core;
public class SpellRepository : GenericRepository<Spell>, ISpellRepository
{
	private readonly PortalQuestDbContext _dbContext;
	public SpellRepository(PortalQuestDbContext dbContext) : base(dbContext)
	{
		_dbContext = dbContext;
	}
}
