using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalQuest.Domain.Entities.Core.M2M;
public class SpellTag
{
	[Key]
	public Guid Id { get; set; }
	public Guid SpellId {  get; set; }
	[ForeignKey(nameof(SpellId))]
	public Spell Spell { get; set; }
	public Guid TagId { get; set; }
	[ForeignKey(nameof(TagId))]
	public Tag Tag { get; set; }
}
