namespace PortalQuest.Application.DTOs.Common
{
	public class PagedResultDto<T>
	{
		public List<T> Items { get; set; } = new();
		public int TotalCount { get; set; }
		public int Skip { get; set; }
		public int Take { get; set; }
	}
}
