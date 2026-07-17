using System.ComponentModel.DataAnnotations.Schema;
using PortalQuest.Domain.Enums.Common;

namespace PortalQuest.Domain.Interfaces
{
	public interface ITranslation
	{
		public LanguageCodeEnum LanguageCode { get; set; }
	}
}
