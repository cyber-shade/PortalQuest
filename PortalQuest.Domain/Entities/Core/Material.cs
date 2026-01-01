using PortalQuest.Domain.Entities.Common;

namespace PortalQuest.Domain.Entities.Core
{
	public class Material : BaseEntity
	{
		public int Cost { get; set; }
		public string Description { get; set; }
		public bool Consume {  get; set; }
	}
}
