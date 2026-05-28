namespace PortalQuest.Domain.Entities.Core
{
	public class ItemMastery : BaseCoreContentEntity
	{
		public List<Weapon> Weapons { get; set; } = new List<Weapon>();
	}
}
