using System.ComponentModel.DataAnnotations.Schema;
using PortalQuest.Domain.Entities.Common;
using PortalQuest.Domain.Enums.Core;

namespace PortalQuest.Domain.Entities.Core
{
	public class Duration : BaseEntity
	{
		public	DurationTypeEnum Type { get; set; }
		public List<string> Ends { get; set; } // if type is Permanent
											   //dispel
											   //trigger
		#region Relations
		public Guid? TimeId { get; set; } // O2M
		[ForeignKey(nameof(TimeId))]
		public Time? Time { get; set; } // if type is timed
		public List<Spell> Spells { get; set; } //M2M
		#endregion
	}
}
