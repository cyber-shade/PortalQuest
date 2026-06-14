using PortalQuest.Application.DTOs.Common;
using PortalQuest.Domain.Enums.Common;

namespace PortalQuest.Application.DTOs.Core
{
	public record BaseTranslationDto : BaseEntityDto
	{
		public string Name { get; set; }
		public string Content { get; set; }
		public LanguageCodeEnum LanguageCode { get; set; }
	}
}
