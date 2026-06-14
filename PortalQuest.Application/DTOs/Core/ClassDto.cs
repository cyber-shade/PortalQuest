namespace PortalQuest.Application.DTOs.Core
{
	public record ClassDto : BaseTranslationDto
	{
		public Guid SourceId { get; set; }
	}
}
