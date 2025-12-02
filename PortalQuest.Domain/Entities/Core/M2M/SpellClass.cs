using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalQuest.Domain.Entities.Core.M2M;
public class SpellClass
{
	[Key]
	public Guid Id { get; set; }
	public Guid SpellId { get; set; }
	[ForeignKey(nameof(SpellId))]
	public Spell Spell { get; set; }
	public Guid ClassId { get; set; }
	[ForeignKey(nameof(ClassId))]
	public Class Class { get; set; }
}

