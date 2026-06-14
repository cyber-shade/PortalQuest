using PortalQuest.Application.Interfaces.Repository.Core;
using PortalQuest.Domain.Entities.Core;
using PortalQuest.Domain.Entities.Core.Translations;
using PortalQuest.Persistence.Context;
using PortalQuest.Persistence.Repository.Common;

namespace PortalQuest.Persistence.Repository.Core
{
	public class EffectRepository : TranslatableGenericRepository<Effect, EffectTranslation>, IEffectRepository
	{
		private readonly PortalQuestDbContext _dbContext;
		public EffectRepository(PortalQuestDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}
	}
}
