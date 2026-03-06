using System.ComponentModel.DataAnnotations.Schema;
using PortalQuest.Domain.Entities.Core.M2M;

namespace PortalQuest.Domain.Entities.Core;
public class Class : BaseCoreEntity
{	
	#region Relations
	public Guid SourceId { get; set; }
	[ForeignKey(nameof(SourceId))]
	public Book Source { get; set; }
	public List<SubClass> Subs { get; set; }
	public List<ClassFeature> Features { get; set; } // O2M
	public List<SpellClass> SpellClasses { get; set; } // M2M
	#endregion
}

