using System.ComponentModel.DataAnnotations.Schema;
using PortalQuest.Domain.Entities.Common;
using PortalQuest.Domain.Enums.Core;

namespace PortalQuest.Domain.Entities.Core
{
	public class Weapon : BaseEntity
	{
		public WeaponCategoryEnum Category { get; set; }
		public Guid ItemId { get; set; }
		[ForeignKey(nameof(ItemId))]
		public Item Item { get; set; }
		public Guid? MasteryId { get; set; }
		[ForeignKey(nameof(MasteryId))]
		public ItemMastery? Mastery { get; set; }
		public List<ItemProperty> Properties { get; set; } = new List<ItemProperty>();

		/* 
		[([damage dice + number], damage type)]
		 */
	}
}
