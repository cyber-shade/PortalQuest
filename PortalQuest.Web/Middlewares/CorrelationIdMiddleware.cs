using Microsoft.Extensions.Primitives;
using Serilog.Context;

namespace PortalQuest.Web.Middlewares
{
	public class CorrelationIdMiddleware : IMiddleware
	{
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			var correlationId =
				context.Request.Headers["X-Correlation-Id"].FirstOrDefault()
				?? Guid.NewGuid().ToString();

			context.Items["CorrelationId"] = correlationId;
			context.Response.Headers["X-Correlation-Id"] = correlationId;

			using (LogContext.PushProperty("CorrelationId", correlationId))
			{
				await next(context);
			}
		}
	}
}
