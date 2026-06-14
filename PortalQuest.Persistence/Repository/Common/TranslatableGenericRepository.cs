using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PortalQuest.Application.Interfaces.Repository.Common;
using PortalQuest.Domain.Entities.Common;
using PortalQuest.Domain.Entities.Core.Translations;
using PortalQuest.Domain.Enums.Common;
using PortalQuest.Domain.Interfaces;
using PortalQuest.Persistence.Context;

namespace PortalQuest.Persistence.Repository.Common
{
	public class TranslatableGenericRepository<T, TTranslation> : GenericRepository<T>, ITranslatableGenericRepository<T, TTranslation>
	where T : BaseEntity, ITranslatable<TTranslation>
	where TTranslation : BaseTranslationEntity<T>
	{
		private readonly PortalQuestDbContext _dbContext;
		public TranslatableGenericRepository(PortalQuestDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<(List<T> items, int count)> GetAllWithTranslation(
			Expression<Func<T, bool>> where = null,
			LanguageCodeEnum languageCode = LanguageCodeEnum.En,
			Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
			int skip = 0, int take = int.MaxValue
		){
			return await GetAll(where, orderBy, q => q.Include(x => x.Translations.Where(t => t.LanguageCode == languageCode)));
		}
	}
}
