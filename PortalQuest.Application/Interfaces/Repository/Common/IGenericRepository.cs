using System.Linq.Expressions;
using PortalQuest.Application.DTOs.Common;
using PortalQuest.Application.Specifications;
using PortalQuest.Domain.Entities.Common;
namespace PortalQuest.Application.Interfaces.Repository.Common;
public interface IGenericRepository<T> where T : BaseEntity
{
	Task<T?> Get(Guid id, CancellationToken cancellationToken = default);
	Task<T?> FirstOrDefault(ISpecification<T> spec, CancellationToken cancellationToken = default);
	Task<PagedResultDto<T>> GetAll(ISpecification<T> spec, CancellationToken cancellationToken = default);
	Task<List<T>> GetAll(Expression<Func<T, bool>>? where = null, bool asNoTracking = true, CancellationToken cancellationToken = default);

	Task<bool> Any(Expression<Func<T, bool>> where, CancellationToken cancellationToken = default);
	Task<int> Count(ISpecification<T> spec, CancellationToken cancellationToken = default);

	Task Add(T entity, CancellationToken cancellationToken = default);
	Task AddRange(IEnumerable<T> entities, CancellationToken cancellationToken = default);

	void Update(T entity);
	void UpdateRange(IEnumerable<T> entities);

	Task SoftDelete(Guid id, CancellationToken cancellationToken = default);
	void SoftDelete(T entity);
	void Remove(T entity);
	void RemoveRange(IEnumerable<T> entities);
}
