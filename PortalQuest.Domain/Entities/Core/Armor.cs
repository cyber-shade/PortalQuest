using PortalQuest.Domain.Entities.Common;

namespace PortalQuest.Domain.Entities.Core
{
	public class Armor : BaseEntity
	{
		public int ArmorClass { get; set; }
		public int RequiredStrength { get; set; }
		public bool StealthDisAdvantage { get; set; }
	}
}
