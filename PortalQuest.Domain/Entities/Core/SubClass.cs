using PortalQuest.Domain.Entities.Core.Translations;
using PortalQuest.Domain.Interfaces;

namespace PortalQuest.Domain.Entities.Core
{
	public class SubClass : BaseRuleEntity, ITranslatable<SubClassTranslation>
	{
		#region Relations
		public List<SubClassTranslation> Translations { get; set; }
 		#endregion
	}
}
