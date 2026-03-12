using AutoMapper;
using MediatR;
using PortalQuest.Application.Constants;
using PortalQuest.Application.DTOs.Common;
using PortalQuest.Application.DTOs.Core;
using PortalQuest.Application.Interfaces.Repository.Core;
using PortalQuest.Application.Tools;
using Entities = PortalQuest.Domain.Entities.Core;
using PortalQuest.Domain.Interfaces;

namespace PortalQuest.Application.Features.Core.Time.Command
{
	public class UpsertTimeRequest : IRequest<ResponseDto<TimeDto>>
	{
		public required TimeDto Time { get; set; }
	}
	internal class UpsertTimeRequestHandler(
		ITimeRepository timeRepository, IMapper mapper, IGuidService guidService	
	) : IRequestHandler<UpsertTimeRequest, ResponseDto<TimeDto>>
	{
		public async Task<ResponseDto<TimeDto>> Handle(UpsertTimeRequest request, CancellationToken cancellationToken)
		{
			var time = request.Time;
			var toUpdate = !guidService.IsEmpty(request.Time.Id);
			if (toUpdate)
			{
				var entity = await timeRepository.Get(time.Id);
				if (entity == null)
					return ResponseFactory.DataError<TimeDto>(SystemMessages.TimeNotFound);
				entity = mapper.Map(time, entity);
				await timeRepository.Update(entity);
			}
			else
			{
				var isExists = await timeRepository.Any(x =>
					x.Type == time.Type
					&& x.Amount == time.Amount
					&& x.Condition == time.Condition
				);
				time.Id = guidService.Generate();
				var entity = mapper.Map<Entities.Time>(time);
				await timeRepository.Add(entity);
			}
			return new ResponseDto<TimeDto>()
			{

				Code = ResponseCodesEnum.Ok,
				Result = time
			};
		}
	}
}
