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

namespace PortalQuest.Application.Features.Core.Range.Command
{
	public class UpsertRangeRequest : IRequest<ResponseDto<RangeDto>>
	{
		public required RangeDto Range { get; set; }
	}
	internal class UpsertRangeRequestHandler(
		IRangeRepository rangeRepository, IMapper mapper, IGuidService guidService, IUnitOfWork unitOfWork	
	) : IRequestHandler<UpsertRangeRequest, ResponseDto<RangeDto>>
	{
		public async Task<ResponseDto<RangeDto>> Handle(UpsertRangeRequest request, CancellationToken cancellationToken)
		{
			var range = request.Range;
			var toUpdate = !guidService.IsEmpty(request.Range.Id);
			if (toUpdate)
			{
				var entity = await rangeRepository.Get(range.Id);
				if (entity == null)
					return ResponseFactory.DataError<RangeDto>(SystemMessages.RangeNotFound);
				entity = mapper.Map(range, entity);
				rangeRepository.Update(entity);
			}
			else
			{
				var isExists = await rangeRepository.Any(x =>
					x.Type == range.Type
					&& x.DistanceType == range.DistanceType
					&& x.Amount == range.Amount
				);
				range.Id = guidService.Generate();
				var entity = mapper.Map<Entities.Range>(range);
				await rangeRepository.Add(entity);
			}
			await unitOfWork.SaveChangesAsync();
			return new ResponseDto<RangeDto>()
			{
				Code = ResponseCodesEnum.Ok,
				Result = range
			};
		}
	}
}
