using AutoMapper;
using MediatR;
using PortalQuest.Application.DTOs.Common;
using PortalQuest.Application.DTOs.Core;
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
			var effects = (await effectRepository.GetAllWithTranslation(x=>
				request.Type == null || x.Type == request.Type, request.languageCode
			)).items;
			var model = mapper.Map<List<EffectDto>>(effects);
			foreach(var effect in effects)
			{
				var item = model.FirstOrDefault(x=> x.Id == effect.Id);
				item.Name = effect.Translations.FirstOrDefault()?.Name;
				item.Content = effect.Translations.FirstOrDefault()?.Content;
			}
			return ResponseFactory.FillObject<List<EffectDto>>(model);
		}
	}
}
