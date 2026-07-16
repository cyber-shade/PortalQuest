using AutoMapper;
using MediatR;
using PortalQuest.Application.Constants;
using PortalQuest.Application.DTOs.Common;
using PortalQuest.Application.DTOs.Core;
using PortalQuest.Application.Interfaces.Repository.Core;
using PortalQuest.Application.Interfaces.UnitOfWork;
using PortalQuest.Application.Tools;
using PortalQuest.Domain.Interfaces;
using Entities = PortalQuest.Domain.Entities.Core;

namespace PortalQuest.Application.Features.Core.Effect.Command
{
	public class UpsertEffectRequest : IRequest<ResponseDto<EffectDto>>
	{
		public required EffectDto Effect { get; set; }
	}
	internal class UpsertEffectRequestHandler(
		IEffectRepository effectRepository, IMediator mediator, IMapper mapper, IGuidService guidService, IUnitOfWork unitOfWork
	) : IRequestHandler<UpsertEffectRequest, ResponseDto<EffectDto>>
	{
		public async Task<ResponseDto<EffectDto>> Handle(UpsertEffectRequest request, CancellationToken cancellationToken)
		{
			var effect = request.Effect;
			var toUpdate = !guidService.IsEmpty(request.Effect.Id);
			if (toUpdate)
			{
				var entity = await effectRepository.Get(effect.Id);
				if (entity == null)
					return ResponseFactory.DataError<EffectDto>(SystemMessages.EffectNotFound);
				entity = mapper.Map(effect, entity);
				effectRepository.Update(entity);
			}
			else
			{
				var isExists = await effectRepository.Any(x =>
					x.Type == effect.Type 
					&& x.Translations.Any(t =>  t.LanguageCode == effect.LanguageCode && t.Name == effect.Name)
					&& x.SourceId == effect.SourceId
				);
				effect.Id = guidService.Generate();
				var entity = mapper.Map<Entities.Effect>(effect);
				entity.Translations = new List<Entities.Translations.EffectTranslation>()
				{
					new()
					{
						Content = effect.Content,
						Id = guidService.Generate(),
						LanguageCode = effect.LanguageCode,
						Name = effect.Name,
					}
				};
				await effectRepository.Add(entity);
			}
			await unitOfWork.SaveChangesAsync();
			return new ResponseDto<EffectDto>()
			{
				Code = ResponseCodesEnum.Ok,
				Result = effect
			};
		}
	}
}
