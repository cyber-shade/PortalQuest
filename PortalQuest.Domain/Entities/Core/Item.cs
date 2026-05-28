using System.ComponentModel.DataAnnotations.Schema;
using PortalQuest.Domain.Enums.Core;

namespace PortalQuest.Domain.Entities.Core
{
	public class Item : BaseCoreContentEntity
	{
		public double Weight { get; set; }
		public double Value { get; set; }
		public Guid TypeId { get; set; }
		[ForeignKey(nameof(Type))]
		public ItemType Type {  get; set; }
		public Weapon? WeaponProperty { get; set; }
		public Armor? ArmorProperty {  get; set; }
		public bool IsMagical { get; set; }
		public ItemRarityTypesEnums Rarity { get; set; }
		public bool IsSentient { get; set; }
		public string Attunement { get; set; }
	}
	/*
	 stat modifier
		ex. charisma +2
		ability score, number
	skill modifier
	saving throw modifier
	spell
	damage
	


	 */
}