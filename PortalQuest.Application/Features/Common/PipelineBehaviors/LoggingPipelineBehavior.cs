using MediatR;
using PortalQuest.Application.Interfaces.Repository.Common;
using PortalQuest.Application.Tools;

namespace PortalQuest.Application.Features.Common.Pipeline
{
	public class LoggingPipelineBehavior<TRequest, TResponse>(ILogger logger) : IPipelineBehavior<TRequest, TResponse>
	where TRequest : IRequest<TResponse>
	{
		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			try
			{
				var response = await next();
				return response;
			}
			catch (Exception ex) {
				await logger.Log($"{ex.Message}; {ex.InnerException};", typeof(TRequest).Name, Domain.Enums.Common.LogLevelEnum.Error);
				var errorResponse = ResponseFactory.ServerError();
				return (TResponse)(object)errorResponse;
			}
		}
	}
}
