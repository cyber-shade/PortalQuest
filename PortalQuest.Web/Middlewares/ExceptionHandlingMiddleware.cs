using PortalQuest.Web.Tools;

namespace PortalQuest.Web.Middlewares
{
	public class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) : IMiddleware
	{
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				var correlationId = context.GetCorrelationId();
				var endpoint = $"[{context.Request.Method}]{context.Request.Path}";

				logger.LogError(ex,
					"Unhandled exception Endpoint: {Endpoint}",
					endpoint);

				context.Response.StatusCode = 500;
				context.Response.ContentType = "application/json";

				await context.Response.WriteAsJsonAsync(
					new
					{
						Message =
							$"Unhandled error. RequestId: {correlationId}"
					});
			}
		}
	}
}
