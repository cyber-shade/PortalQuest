using PortalQuest.Domain.Entities.Common;
using PortalQuest.Domain.Enums.Core;

namespace PortalQuest.Domain.Entities.Core
{
	public class Time : BaseEntity
	{
		public int Amount { get; set; }
		public TimeTyprEnum Type {get; set;}
		public string Condition { get; set;}
	}
}
