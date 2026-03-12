using AutoMapper;
using MediatR;
using PortalQuest.Application.DTOs.Common;
using PortalQuest.Application.DTOs.Core;
using PortalQuest.Application.Interfaces.Repository.Core;
using PortalQuest.Application.Tools;

namespace PortalQuest.Application.Features.Core.Duration.Query
{
	public class GetDurationsListRequest : IRequest<ResponseDto<List<DurationDto>>>
	{
	}
	internal class GetDurationsListRequestHandler(
		IDurationRepository durationRepository, IMapper mapper
	) : IRequestHandler<GetDurationsListRequest, ResponseDto<List<DurationDto>>>
	{
		public async Task<ResponseDto<List<DurationDto>>> Handle(GetDurationsListRequest request, CancellationToken cancellationToken)
		{
			var durations = (await durationRepository.GetAll()).items;		
			var model = mapper.Map<List<DurationDto>>(durations);
			return ResponseFactory.FillObject<List<DurationDto>>(model);
		}
	}
}
