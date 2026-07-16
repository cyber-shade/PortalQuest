using PortalQuest.Domain.Enums.Common;

namespace PortalQuest.Application.Interfaces.Services
{
	public interface ICurrentLanguageService
	{
		LanguageCodeEnum LanguageCode { get; }
	}

}
