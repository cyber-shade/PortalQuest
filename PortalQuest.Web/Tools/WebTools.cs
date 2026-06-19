namespace PortalQuest.Web.Tools
{
	public static class WebTools
	{
		public static string? GetCorrelationId(this HttpContext context)
		{
			return context.Items["CorrelationId"]?.ToString();
		}
	}
}
