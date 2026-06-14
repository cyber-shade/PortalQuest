using AutoMapper;
using MediatR;
using PortalQuest.Application.Constants;
using PortalQuest.Application.DTOs.Common;
using PortalQuest.Application.DTOs.Core;
using PortalQuest.Application.Interfaces.Repository.Core;
using PortalQuest.Application.Tools;
using PortalQuest.Domain.Entities.Core.Translations;
using PortalQuest.Domain.Interfaces;
using Entity = PortalQuest.Domain.Entities.Core;

namespace PortalQuest.Application.Features.Core.Class.Command
{
	public class UpsertClassRequest : IRequest<ResponseDto<ClassDto>>
	{
		public required ClassDto Class { get; set; }
	}
	internal class UpsertClassRequestHandler(
		IGuidService guidService, IClassRepository classRepository, IMapper mapper
	) : IRequestHandler<UpsertClassRequest, ResponseDto<ClassDto>>
	{
		public async Task<ResponseDto<ClassDto>> Handle(UpsertClassRequest request, CancellationToken cancellationToken)
		{
			var clazz = request.Class;
			var toUpdate = !guidService.IsEmpty(request.Class.Id);
			if (toUpdate)
			{
				var entity = await classRepository.Get(clazz.Id);
				if (entity == null)
					return ResponseFactory.DataError<ClassDto>(SystemMessages.ClassNotFound);
				entity = mapper.Map(clazz, entity);
				await classRepository.Update(entity);
			}
			else
			{
				var isExists = await classRepository.Any(x => 
					x.Translations.Any(t => t.LanguageCode == clazz.LanguageCode && t.Name == clazz.Name)
					&& x.SourceId == clazz.SourceId
				);
				clazz.Id = guidService.Generate();
				var entity = mapper.Map<Entity.Class>(clazz);
				entity.NameInSRD = entity.NameInSRD ?? string.Empty;
				entity.Translations = new List<ClassTranslation>()
				{
					new()
					{
						LanguageCode = clazz.LanguageCode,
						Content = clazz.Content,
						Name = clazz.Name,
						Id = guidService.Generate()
					}
				};
					await classRepository.Add(entity);


				
			}
			return new ResponseDto<ClassDto>() { 
					
				Code = ResponseCodesEnum.Ok,
				Result = clazz
			};
		}
	}
}