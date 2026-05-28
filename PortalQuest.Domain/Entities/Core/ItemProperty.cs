namespace PortalQuest.Domain.Entities.Core
{
	public class ItemProperty : BaseCoreContentEntity
	{
		public List<Weapon> Weapons { get; set; } = new List<Weapon>();
	}
}
