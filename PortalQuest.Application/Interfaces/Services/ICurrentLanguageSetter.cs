using PortalQuest.Domain.Enums.Common;

namespace PortalQuest.Application.Interfaces.Services
{
	public interface ICurrentLanguageSetter
	{
		void SetLanguage(LanguageCodeEnum languageCode);
	}
}
