namespace PortalQuest.Domain.Interfaces;
public interface IGuidService
{
	Guid Generate();
	bool IsEmpty(Guid id);
}
