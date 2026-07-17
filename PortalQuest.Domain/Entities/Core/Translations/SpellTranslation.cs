using PortalQuest.Domain.Enums.Common;
using PortalQuest.Domain.Interfaces;

namespace PortalQuest.Domain.Entities.Core.Translations
{
	public class SpellTranslation : BaseTranslationEntity<Spell>, ITranslation
	{
		public LanguageCodeEnum LanguageCode { get; set; }
		public string? MaterialDescription { get; set; }
	}
}
