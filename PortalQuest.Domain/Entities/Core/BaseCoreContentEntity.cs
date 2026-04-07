using System.ComponentModel.DataAnnotations.Schema;

namespace PortalQuest.Domain.Entities.Core
{
	public class BaseCoreContentEntity : BaseCoreEntity
	{
		public bool SRD { get; set; }
		public string NameInSRD { get; set; }
		public bool BasicRules { get; set; }
		public int SourcePage { get; set; }
		public Guid SourceId { get; set; }
		[ForeignKey(nameof(SourceId))]
		public Book Source { get; set; }
	}
}
