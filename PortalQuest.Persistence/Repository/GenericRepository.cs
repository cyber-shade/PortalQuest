using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PortalQuest.Application.Interfaces;
using PortalQuest.Domain.Entities.Common;
using PortalQuest.Persistence.Context;

namespace PortalQuest.Persistence.Repository;
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

	public async Task<bool> Any(Expression<Func<T, bool>> where = null)
	{
		return await _dbContext.Set<T>().AnyAsync(where);
	}

	public async Task<T?> FirstOrDefault(Expression<Func<T, bool>> where)
	{
		return await _dbContext.Set<T>().FirstOrDefaultAsync(where);
	}

	public async Task<T?> Get(Guid id)
	{
		return await _dbContext.Set<T>().FindAsync(id);
	}

	public async Task<List<T>> GetAll(Expression<Func<T, bool>> where = null, int skip = 0, int take = int.MaxValue)
	{
		return await _dbContext.Set<T>().Where(where != null ? where : t => true)
			.Skip(skip).Take(take).ToListAsync();
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
