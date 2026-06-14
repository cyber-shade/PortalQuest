using System.ComponentModel.DataAnnotations.Schema;
using PortalQuest.Domain.Entities.Common;

namespace PortalQuest.Domain.Entities.Core
{
	public class BaseRuleEntity : BaseEntity
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
