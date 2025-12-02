using System.ComponentModel.DataAnnotations;

namespace PortalQuest.Domain.Entities.Common;
public class BaseEntity
{
	[Key]
	public Guid Id { get; set; }
	public bool IsDeleted { get; set; }
}
