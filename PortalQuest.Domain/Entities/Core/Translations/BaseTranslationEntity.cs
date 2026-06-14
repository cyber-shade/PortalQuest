using System.ComponentModel.DataAnnotations.Schema;
using PortalQuest.Domain.Entities.Common;
using PortalQuest.Domain.Enums.Common;

namespace PortalQuest.Domain.Entities.Core.Translations
{
	public class BaseTranslationEntity<T> : BaseCoreEntity where T : BaseEntity
	{
		public Guid OriginId { get; set; }
		[ForeignKey(nameof(OriginId))]
		public T Origin { get; set; }
		public LanguageCodeEnum LanguageCode { get; set; }
	}
}
