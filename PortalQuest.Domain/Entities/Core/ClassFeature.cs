using System.ComponentModel.DataAnnotations.Schema;

namespace PortalQuest.Domain.Entities.Core
{
	public class ClassFeature : BaseCoreEntity
	{
		#region Relations
		public Guid ClassId { get; set; }
		[ForeignKey(nameof(ClassId))]
		public Class Class { get; set; }
		#endregion
	}
}
