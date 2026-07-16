using PortalQuest.Application.Interfaces.UnitOfWork;
using PortalQuest.Persistence.Context;

namespace PortalQuest.Persistence.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly PortalQuestDbContext _dbContext;
		public UnitOfWork(PortalQuestDbContext dbContext) => _dbContext = dbContext;

		public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
			=> await _dbContext.SaveChangesAsync(cancellationToken);
	}
}
