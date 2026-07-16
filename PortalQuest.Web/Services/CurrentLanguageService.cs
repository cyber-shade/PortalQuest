using PortalQuest.Application.Interfaces.Services;
using PortalQuest.Domain.Enums.Common;

namespace PortalQuest.Web.Services
{
	public class CurrentLanguageService : ICurrentLanguageService, ICurrentLanguageSetter
	{
		public LanguageCodeEnum LanguageCode { get; private set; } = LanguageCodeEnum.En;
		public void SetLanguage(LanguageCodeEnum languageCode) => LanguageCode = languageCode;
	}
}
