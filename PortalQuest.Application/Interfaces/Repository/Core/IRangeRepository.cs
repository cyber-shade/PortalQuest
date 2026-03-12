using PortalQuest.Application.Interfaces.Repository.Common;
using Entities = PortalQuest.Domain.Entities.Core;

namespace PortalQuest.Application.Interfaces.Repository.Core
{
	public interface IRangeRepository : IGenericRepository<Entities.Range>
	{
	}
}
