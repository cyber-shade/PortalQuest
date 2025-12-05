namespace PortalQuest.Domain.Entities.Common;
public class BaseEntity
{
	public Guid Id { get; set; }
	public bool IsDeleted { get; set; }
}
