using PortalQuest.Domain.Enums.Core;

namespace PortalQuest.Domain.Entities.Core
{
	public class ItemType : BaseCoreContentEntity
	{
		public ItemCategoryEnum Category { get; set; }
		public List<Item> Items { get; set; } = new List<Item>();
	}
}
