using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalQuest.Domain.Entities.Core.M2M;
public class SpellClass
{
	[Key]
	public int Id { get; set; }
	public bool IsVariant { get; set; }
	public Guid SpellId { get; set; }
	[ForeignKey(nameof(SpellId))]
	public Spell Spell { get; set; }
	public Guid ClassId { get; set; }
	[ForeignKey(nameof(ClassId))]
	public Class Class { get; set; }
	public Guid? SourceId { get; set; }
	[ForeignKey(nameof(SourceId))]
	public Book? DefinedInSource { get; set; }
}

