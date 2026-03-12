using PortalQuest.Domain.Entities.Common;
using PortalQuest.Domain.Enums.Core;

namespace PortalQuest.Domain.Entities.Core
{
	public class Range : BaseEntity
	{
		public RangeTypeEnum Type { get; set; }
		public DistanceTypeEnum DistanceType { get; set; }
		public int Amount { get; set; }
		#region Relations
		public List<Spell> Spells { get; set; }
		#endregion
	}
}
