using Microsoft.EntityFrameworkCore;
using PortalQuest.Application.Specifications;
using PortalQuest.Domain.Entities.Common;

namespace PortalQuest.Persistence.Specifications
{
	public static class SpecificationEvaluator<T> where T : BaseEntity
	{
		public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec, bool applyPaging = true)
		{
			var query = inputQuery;

			if (spec.Criteria != null)
				query = query.Where(spec.Criteria);

			query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
			query = spec.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

			if (spec.OrderBy != null)
				query = query.OrderBy(spec.OrderBy);
			else if (spec.OrderByDescending != null)
				query = query.OrderByDescending(spec.OrderByDescending);

			if (spec.AsSplitQuery)
				query = query.AsSplitQuery();

			if (spec.AsNoTracking)
				query = query.AsNoTracking();

			if (applyPaging && spec.IsPagingEnabled)
				query = query.Skip(spec.Skip).Take(spec.Take);

			return query;
		}
	}
}
