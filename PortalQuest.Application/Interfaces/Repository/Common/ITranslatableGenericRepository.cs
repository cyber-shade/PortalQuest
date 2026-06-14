using System.Linq.Expressions;
using PortalQuest.Domain.Entities.Common;
using PortalQuest.Domain.Entities.Core.Translations;
using PortalQuest.Domain.Enums.Common;
using PortalQuest.Domain.Interfaces;

namespace PortalQuest.Application.Interfaces.Repository.Common
{
	public interface ITranslatableGenericRepository<T, TTranslation> : IGenericRepository<T>
	where T : BaseEntity, ITranslatable<TTranslation>
	where TTranslation : BaseTranslationEntity<T>
	{
		Task<(List<T> items, int count)> GetAllWithTranslation(Expression<Func<T, bool>> where = null,
			LanguageCodeEnum languageCode = LanguageCodeEnum.En,
			Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
			int skip = 0, int take = int.MaxValue);
	}
}
