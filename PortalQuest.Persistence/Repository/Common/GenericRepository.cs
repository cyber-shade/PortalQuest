using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PortalQuest.Application.Interfaces.Repository.Common;
using PortalQuest.Domain.Entities.Common;
using PortalQuest.Persistence.Context;

namespace PortalQuest.Persistence.Repository.Common;
public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
	private readonly PortalQuestDbContext _dbContext;
	public GenericRepository(PortalQuestDbContext dbContext)
	{
		_dbContext = dbContext;
	}
	public async Task Add(T entity)
	{
		await _dbContext.AddAsync(entity);
		await _dbContext.SaveChangesAsync();
	}

	public async Task<bool> Any(Expression<Func<T, bool>> where)
	{
		return await _dbContext.Set<T>().AnyAsync(where);
	}

	public async Task<T?> FirstOrDefault(Expression<Func<T, bool>> where, 
		Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
		Func<IQueryable<T>, IQueryable<T>>? include = null)
	{
		IQueryable<T> query = _dbContext.Set<T>();
		if(include != null)
		{
			query = include(query);

			// Optional: Split query if includes are too heavy
			query = query.AsSplitQuery();
		}
		if (orderBy != null)
			query = orderBy(query);
		return await query.FirstOrDefaultAsync(where);
	}

	public async Task<T?> Get(Guid id)
	{
		return await _dbContext.Set<T>().FindAsync(id);
	}

	public async Task<(List<T> items, int count)> GetAll(Expression<Func<T, bool>> where = null,
			Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
			Func<IQueryable<T>, IQueryable<T>>? include = null,
			int skip = 0, int take = int.MaxValue)
	{
		IQueryable<T> query = _dbContext.Set<T>();
		if (include != null)
		{
			query = include(query);

			// Optional: Split query if includes are too heavy
			query = query.AsSplitQuery();
		}
		if (where != null)
			query = query.Where(where);

		var totalCount = await query.CountAsync();

		if (orderBy != null)
			query = orderBy(query);

		var items = await query.Skip(skip).Take(take).ToListAsync();

		return (items, totalCount);
	}

	public async Task SoftDelete(Guid id)
	{
		T? entity = await Get(id);
		if (entity != null && !entity.IsDeleted)
			await SoftDelete(entity);
	}
	public async Task SoftDelete(T entity)
	{
		entity.IsDeleted = true;
		await Update(entity);
	}

	public async Task Update(T entity)
	{
		_dbContext.Update(entity);
		await _dbContext.SaveChangesAsync();
	}
}
