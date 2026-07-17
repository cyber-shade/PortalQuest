using AutoMapper;
using MediatR;
using PortalQuest.Application.DTOs.Common;
using PortalQuest.Application.DTOs.Core;
using PortalQuest.Application.Features.Core.Effect.Specifications;
using PortalQuest.Application.Interfaces.Repository.Core;
using PortalQuest.Application.Tools;
using PortalQuest.Domain.Enums.Common;
using PortalQuest.Domain.Enums.Core;

namespace PortalQuest.Application.Features.Core.Effect.Query
{
	public class GetEffectsListRequest : IRequest<ResponseDto<List<EffectDto>>>
	{
		public EffectTypesEnum? Type { get; set; }
		public LanguageCodeEnum languageCode { get; set; }
	}
	internal class GetEffectsListRequestHandler(
		IEffectRepository effectRepository, IMapper mapper
	) : IRequestHandler<GetEffectsListRequest, ResponseDto<List<EffectDto>>>
	{
		public async Task<ResponseDto<List<EffectDto>>> Handle(GetEffectsListRequest request, CancellationToken cancellationToken)
		{
			var spec = new EffectListSpec(request);
			var res = await effectRepository.GetAll(spec, cancellationToken);
			var model = mapper.Map<List<EffectDto>>(res.Items);
			return ResponseFactory.FillObject<List<EffectDto>>(model);
		}
	}
}
