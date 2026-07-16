using AutoMapper;
using MediatR;
using PortalQuest.Application.DTOs.Common;
using PortalQuest.Application.DTOs.Core;
using PortalQuest.Application.Interfaces.Repository.Core;
using PortalQuest.Application.Tools;

namespace PortalQuest.Application.Features.Core.Time.Query
{
	public class GetTimesListRequest : IRequest<ResponseDto<List<TimeDto>>>
	{
	}
	internal class GetTimeListRequestHandler(
		ITimeRepository timeRepository, IMapper mapper
	) : IRequestHandler<GetTimesListRequest, ResponseDto<List<TimeDto>>>
	{
		public async Task<ResponseDto<List<TimeDto>>> Handle(GetTimesListRequest request, CancellationToken cancellationToken)
		{
			var times = await timeRepository.GetAll(cancellationToken: cancellationToken);
			var model = mapper.Map<List<TimeDto>>(times);
			return ResponseFactory.FillObject<List<TimeDto>>(model);
		}
	}
}
