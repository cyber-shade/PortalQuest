using FluentValidation;
using PortalQuest.Web.Tools;

namespace PortalQuest.Web.Middlewares
{
	public class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) : IMiddleware
	{
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			var correlationId = context.GetCorrelationId();
			var endpoint = $"[{context.Request.Method}]{context.Request.Path}";
			try
			{
				await next(context);
			}
			catch(ValidationException ex)
			{
				context.Response.StatusCode = 400;
				context.Response.ContentType = "application/json";

				await context.Response.WriteAsJsonAsync(
					new
					{
						Message =
							$"{ex.Message}"
					});
			}
			catch (Exception ex)
			{
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
