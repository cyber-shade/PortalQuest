using PortalQuest.Domain.Entities.Common;

namespace PortalQuest.Domain.Entities.Core;
public class Component : BaseEntity
{
	public bool Verbal { get; set; }
	public bool Somatic { get; set; }
	public Material? Material {get; set;}
}
