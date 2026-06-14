namespace PortalQuest.Application.DTOs.Core
{
	public record BookDto : BaseTranslationDto
	{
		public string ShortName { get; set; }
		public string Author { get; set; }
		public DateTime PublishedDateTime { get; set; }
	}
}
