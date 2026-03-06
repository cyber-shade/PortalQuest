namespace PortalQuest.Application.DTOs.Core
{
	public record ClassDto : BaseCoreEntityDto
	{
		public Guid SourceId { get; set; }
	}
}
