using System.Linq.Expressions;

namespace PortalQuest.Application.Specifications
{
	public interface ISpecification<T>
	{
		Expression<Func<T, bool>>? Criteria { get; }
		List<Expression<Func<T, object>>> Includes { get; }
		List<string> IncludeStrings { get; }
		Expression<Func<T, object>>? OrderBy { get; }
		Expression<Func<T, object>>? OrderByDescending { get; }
		int Skip { get; }
		int Take { get; }
		bool IsPagingEnabled { get; }
		bool AsNoTracking { get; }
		bool AsSplitQuery { get; }
	}
}
