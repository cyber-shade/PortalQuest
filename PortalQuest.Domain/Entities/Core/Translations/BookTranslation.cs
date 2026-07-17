using PortalQuest.Domain.Enums.Common;
using PortalQuest.Domain.Interfaces;

namespace PortalQuest.Domain.Entities.Core.Translations
{
	public class BookTranslation : BaseTranslationEntity<Book>, ITranslation
	{
		public LanguageCodeEnum LanguageCode { get; set; }
	}
}
