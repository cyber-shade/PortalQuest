using AutoMapper;
using MediatR;
using PortalQuest.Application.Constants;
using PortalQuest.Application.DTOs.Common;
using PortalQuest.Application.DTOs.Core;
using PortalQuest.Application.Interfaces.Repository.Core;
using PortalQuest.Application.Tools;
using Entities = PortalQuest.Domain.Entities.Core;
using PortalQuest.Domain.Interfaces;
using PortalQuest.Application.Interfaces.UnitOfWork;

namespace PortalQuest.Application.Features.Core.Duration.Command
{
	public class UpsertDurationRequest : IRequest<ResponseDto<DurationDto>>
 	{
		public required DurationDto Duration { get; set; }
	}
	internal class UpsertDurationRequestHandler(
		IDurationRepository durationRepository, IMapper mapper, IGuidService guidService, IUnitOfWork unitOfWork
	) : IRequestHandler<UpsertDurationRequest, ResponseDto<DurationDto>>
	{
		public async Task<ResponseDto<DurationDto>> Handle(UpsertDurationRequest request, CancellationToken cancellationToken)
		{
			var duration = request.Duration;
			var toUpdate = !guidService.IsEmpty(request.Duration.Id);
			if (toUpdate)
			{
				var entity = await durationRepository.Get(duration.Id);
				if (entity == null)
					return ResponseFactory.DataError<DurationDto>(SystemMessages.TimeNotFound);
				entity = mapper.Map(duration, entity);
				durationRepository.Update(entity);
			}
			else
			{
				var isExists = await durationRepository.Any(x =>
					x.TimeId == duration.TimeId
					&& x.Type == duration.Type
					&& x.Ends == duration.Ends
				);
				duration.Id = guidService.Generate();
				var entity = mapper.Map<Entities.Duration>(duration);
				await durationRepository.Add(entity);
			}
			await unitOfWork.SaveChangesAsync();
			return new ResponseDto<DurationDto>()
			{
				Code = ResponseCodesEnum.Ok,
				Result = duration
			};
		}
	}
}
