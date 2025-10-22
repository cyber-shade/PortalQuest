using System.ComponentModel.DataAnnotations;

namespace PortalQuest.Domain.Entities.Common;
public class BaseCoreEntity
{
	[Key]
	public int Id { get; set; }
	public string Name { get; set; }
	public string? Description { get; set; }
}
