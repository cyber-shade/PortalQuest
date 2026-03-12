namespace PortalQuest.Application.DTOs.Core
{
	public record SpellClassDto
	{
		public Guid ClassId { get; set; }
		public bool IsVariant { get; set; }
		public Guid? SourceId { get; set; }
	}
}
