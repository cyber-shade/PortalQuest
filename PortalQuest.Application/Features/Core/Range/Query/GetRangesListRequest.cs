using AutoMapper;
using MediatR;
using PortalQuest.Application.DTOs.Common;
using PortalQuest.Application.DTOs.Core;
using PortalQuest.Application.Interfaces.Repository.Core;
using PortalQuest.Application.Tools;

namespace PortalQuest.Application.Features.Core.Range.Query
{
	public class GetRangesListRequest : IRequest<ResponseDto<List<RangeDto>>>
	{
	}
	internal class GetRangesListRequestHandler(
		IRangeRepository rangeRepository, IMapper mapper
	) : IRequestHandler<GetRangesListRequest, ResponseDto<List<RangeDto>>>
	{
		public async Task<ResponseDto<List<RangeDto>>> Handle(GetRangesListRequest request, CancellationToken cancellationToken)
		{
			var ranges = (await rangeRepository.GetAll()).items;
			var model = mapper.Map<List<RangeDto>>(ranges);
			return ResponseFactory.FillObject<List<RangeDto>>(model);
		}
	}
}
