using PortalQuest.Domain.Enums.Common;
using PortalQuest.Domain.Interfaces;

namespace PortalQuest.Application.Specifications
{
	public abstract class TranslatableSpecification<T, TTranslation> : BaseSpecification<T>
	where T : class, ITranslatable<TTranslation>
	where TTranslation : class, ITranslation
	{
		protected TranslatableSpecification(LanguageCodeEnum languageCode)
		{
			AddInclude(x => x.Translations.Where(t => t.LanguageCode == languageCode));
		}
	}
}
