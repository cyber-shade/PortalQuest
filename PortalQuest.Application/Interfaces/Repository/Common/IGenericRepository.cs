using System.Linq.Expressions;
using PortalQuest.Domain.Entities.Common;
namespace PortalQuest.Application.Interfaces.Repository.Common;
public interface IGenericRepository<T> where T : BaseEntity
{
	Task<bool> Any(Expression<Func<T, bool>> where);
	Task<(IReadOnlyList<T> items, int count)> GetAll(Expression<Func<T, bool>> where = null,
			Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
			Func<IQueryable<T>, IQueryable<T>>? include = null,
			int skip = 0, int take = int.MaxValue);
	Task<T?> Get(Guid id);
	Task<T?> FirstOrDefault(Expression<Func<T, bool>> where,
		Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
		Func<IQueryable<T>, IQueryable<T>>? include = null);
	Task Add(T entity);
	Task Update(T entity);
	Task SoftDelete(Guid id);
	Task SoftDelete(T entity);
}
