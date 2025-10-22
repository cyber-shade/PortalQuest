using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PortalQuest.Domain.Entities.Core;

namespace PortalQuest.Domain.Entities.Core.M2M;
public class SpellClass
{
	[Key]
	public int Id { get; set; }
	public int SpellId { get; set; }
	[ForeignKey(nameof(SpellId))]
	public Spell Spell { get; set; }
	public int ClassId { get; set; }
	[ForeignKey(nameof(ClassId))]
	public Class Class { get; set; }
}

