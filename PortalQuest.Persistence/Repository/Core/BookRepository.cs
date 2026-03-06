using PortalQuest.Application.Interfaces.Repository.Core;
using PortalQuest.Domain.Entities.Core;
using PortalQuest.Persistence.Context;
using PortalQuest.Persistence.Repository.Common;

namespace PortalQuest.Persistence.Repository.Core;
public class BookRepository : GenericRepository<Book>, IBookRepository
{
	private readonly PortalQuestDbContext _dbContext;
	public BookRepository(PortalQuestDbContext dbContext) : base(dbContext)
	{
		_dbContext = dbContext;
	}
}

