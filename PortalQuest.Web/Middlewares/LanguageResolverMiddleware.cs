using PortalQuest.Application.Interfaces.Services;
using PortalQuest.Domain.Enums.Common;

namespace PortalQuest.Web.Middlewares
{
	public class LanguageResolverMiddleware(ICurrentLanguageSetter currentLanguage) : IMiddleware
	{
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			var lang = context.Request.Query["lang"].FirstOrDefault()
				   ?? context.Request.Headers["Accept-Language"].FirstOrDefault()?.Split(',')[0];
			if (!Enum.TryParse<LanguageCodeEnum>(lang, out var code))
				code = LanguageCodeEnum.En;

			currentLanguage.SetLanguage(code);
			await next(context);
		}
	}
}
