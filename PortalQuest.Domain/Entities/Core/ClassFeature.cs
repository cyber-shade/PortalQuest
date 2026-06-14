using System.ComponentModel.DataAnnotations.Schema;
using PortalQuest.Domain.Entities.Core.Translations;
using PortalQuest.Domain.Interfaces;

namespace PortalQuest.Domain.Entities.Core
{
	public class ClassFeature : BaseRuleEntity, ITranslatable<ClassFeatureTranslation>
	{
		#region Relations
		public Guid ClassId { get; set; }
		[ForeignKey(nameof(ClassId))]
		public Class Class { get; set; }
		public List<ClassFeatureTranslation> Translations { get; set; }
		#endregion
	}
}
