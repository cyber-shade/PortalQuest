using System.Linq.Expressions;
using PortalQuest.Application.Features.Core.Effect.Query;
using PortalQuest.Application.Specifications;
using PortalQuest.Domain.Entities.Core.Translations;

namespace PortalQuest.Application.Features.Core.Effect.Specifications
{
	public class EffectListSpec : TranslatableSpecification<Domain.Entities.Core.Effect, EffectTranslation>
	{
		public EffectListSpec(GetEffectsListRequest filter) : base(filter.languageCode)
		{
			Expression<Func<Domain.Entities.Core.Effect, bool>> criteria = e => filter.Type == null || e.Type == filter.Type;
			ApplyCriteria(criteria);
		}
	}
}
