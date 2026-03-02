using PortalQuest.Domain.Entities.Common;
using PortalQuest.Domain.Enums.Core;

namespace PortalQuest.Domain.Entities.Core
{
	public class Duration : BaseEntity
	{
		public	DurationTypeEnum Type { get; set; }
		public Time Time { get; set; } // if type is timed
		public List<string> Ends { get; set; } // if type is Permanent
										 //dispel
										 //trigger
		public List<Spell> Spells { get; set; }
	}
}
