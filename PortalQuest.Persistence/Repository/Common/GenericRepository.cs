using PortalQuest.Application.Interfaces.Repository.Common;
using PortalQuest.Domain.Entities.Common;
using PortalQuest.Persistence.Context;

namespace PortalQuest.Persistence.Repository.Common;

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PortalQuest.Application.DTOs.Common;
using PortalQuest.Application.Specifications;
using PortalQuest.Persistence.Specifications;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
	private readonly PortalQuestDbContext _dbContext;
	private readonly DbSet<T> _set;

	public GenericRepository(PortalQuestDbContext dbContext)
	{
		_dbContext = dbContext;
		_set = dbContext.Set<T>();
	}

	public async Task<T?> Get(Guid id, CancellationToken cancellationToken = default)
		=> await _set.FindAsync(new object[] { id }, cancellationToken);

	public async Task<T?> FirstOrDefault(ISpecification<T> spec, CancellationToken cancellationToken = default)
	{
		var query = SpecificationEvaluator<T>.GetQuery(_set.AsQueryable(), spec, applyPaging: false);
		return await query.FirstOrDefaultAsync(cancellationToken);
	}

	public async Task<PagedResultDto<T>> GetAll(ISpecification<T> spec, CancellationToken cancellationToken = default)
	{
		var filteredQuery = SpecificationEvaluator<T>.GetQuery(_set.AsQueryable(), spec, applyPaging: false);
		var totalCount = await filteredQuery.CountAsync(cancellationToken);

		var pagedQuery = spec.IsPagingEnabled
			? filteredQuery.Skip(spec.Skip).Take(spec.Take)
			: filteredQuery;

		var items = await pagedQuery.ToListAsync(cancellationToken);

		return new PagedResultDto<T>
		{
			Items = items,
			TotalCount = totalCount,
			Skip = spec.Skip,
			Take = spec.Take
		};
	}
	public async Task<List<T>> GetAll(Expression<Func<T, bool>>? where = null,  CancellationToken cancellationToken = default)
		=> await _set.AsNoTracking().Where(where).ToListAsync(cancellationToken);
	public async Task<bool> Any(Expression<Func<T, bool>> where, CancellationToken cancellationToken = default)
		=> await _set.AnyAsync(where, cancellationToken);

	public async Task<int> Count(ISpecification<T> spec, CancellationToken cancellationToken = default)
	{
		var query = SpecificationEvaluator<T>.GetQuery(_set.AsQueryable(), spec, applyPaging: false);
		return await query.CountAsync(cancellationToken);
	}

	public async Task Add(T entity, CancellationToken cancellationToken = default)
		=> await _set.AddAsync(entity, cancellationToken);

	public async Task AddRange(IEnumerable<T> entities, CancellationToken cancellationToken = default)
		=> await _set.AddRangeAsync(entities, cancellationToken);

	public void Update(T entity)
		=> _set.Update(entity);

	public void UpdateRange(IEnumerable<T> entities)
		=> _set.UpdateRange(entities);

	public async Task SoftDelete(Guid id, CancellationToken cancellationToken = default)
	{
		var entity = await Get(id, cancellationToken);
		if (entity != null && !entity.IsDeleted)
			SoftDelete(entity);
	}

	public void SoftDelete(T entity)
	{
		entity.IsDeleted = true;
		_set.Update(entity);
	}

	public void Remove(T entity)
		=> _set.Remove(entity);

	public void RemoveRange(IEnumerable<T> entities)
		=> _set.RemoveRange(entities);
}
