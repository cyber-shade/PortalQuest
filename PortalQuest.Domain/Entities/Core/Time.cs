using PortalQuest.Domain.Entities.Common;
using PortalQuest.Domain.Enums.Core;

namespace PortalQuest.Domain.Entities.Core
{
	public class Time : BaseEntity
	{
		public int Amount { get; set; }
		public TimeTypeEnum Type {get; set;}
		public string Condition { get; set;}
		public List<Spell> Spells { get; set;}
	}
}
