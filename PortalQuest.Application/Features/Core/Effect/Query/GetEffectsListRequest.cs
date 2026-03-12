using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PortalQuest.Application.DTOs.Common;
using PortalQuest.Application.DTOs.Core;
using PortalQuest.Application.Interfaces.Repository.Core;
using PortalQuest.Application.Tools;
using PortalQuest.Domain.Enums.Core;

namespace PortalQuest.Application.Features.Core.Effect.Query
{
	public class GetEffectsListRequest : IRequest<ResponseDto<List<EffectDto>>>
	{
		public EffectTypesEnum? Type { get; set; }
	}
	internal class GetEffectsListRequestHandler(
		IEffectRepository effectRepository, IMapper mapper
	) : IRequestHandler<GetEffectsListRequest, ResponseDto<List<EffectDto>>>
	{
		public async Task<ResponseDto<List<EffectDto>>> Handle(GetEffectsListRequest request, CancellationToken cancellationToken)
		{
			var effects = (await effectRepository.GetAll(x=> request.Type == null || x.Type == request.Type)).items;
			var model = mapper.Map<List<EffectDto>>(effects);
			return ResponseFactory.FillObject<List<EffectDto>>(model);
		}
	}
}
