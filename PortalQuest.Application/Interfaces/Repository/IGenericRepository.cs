using System.Linq.Expressions;
using PortalQuest.Domain.Entities.Common;
namespace PortalQuest.Application.Interfaces.Repository;
public interface IGenericRepository<T> where T : BaseEntity
{
	Task<bool> Any(Expression<Func<T, bool>> where = null);
	Task<List<T>> GetAll(Expression<Func<T, bool>> where = null, int skip = 0, int take = int.MaxValue);
	Task<T?> Get(Guid id);
	Task<T?> FirstOrDefault(Expression<Func<T, bool>> where = null);
	Task Add(T entity);
	Task Update(T entity);
	Task SoftDelete(Guid id);
}
