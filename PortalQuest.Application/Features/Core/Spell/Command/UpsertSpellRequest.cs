using AutoMapper;
using MediatR;
using PortalQuest.Application.Constants;
using PortalQuest.Application.DTOs.Common;
using PortalQuest.Application.DTOs.Core.Spells;
using PortalQuest.Application.Interfaces.Repository.Core;
using PortalQuest.Application.Interfaces.UnitOfWork;
using PortalQuest.Application.Tools;
using PortalQuest.Domain.Entities.Core.M2M;
using PortalQuest.Domain.Interfaces;
using Entities = PortalQuest.Domain.Entities.Core;

namespace PortalQuest.Application.Features.Core.Spell.Command
{
	public class UpsertSpellRequest : IRequest<ResponseDto<SpellDto>>
	{
		public required SpellDto Spell { get; set; }
	}
	internal class UpsertSpellRequestHandler(
		ISpellRepository spellRepository, IMapper mapper, IGuidService guidService,
		IEffectRepository effectRepository, IUnitOfWork unitOfWork
	) : IRequestHandler<UpsertSpellRequest, ResponseDto<SpellDto>>
	{
		public async Task<ResponseDto<SpellDto>> Handle(UpsertSpellRequest request, CancellationToken cancellationToken)
		{
			var spell = request.Spell;
			var toUpdate = !guidService.IsEmpty(request.Spell.Id);
			if (toUpdate)
			{
				var entity = await spellRepository.Get(spell.Id);
				if (entity == null)
					return ResponseFactory.DataError<SpellDto>(SystemMessages.SpellNotFound);
				entity = mapper.Map(spell, entity);
				spellRepository.Update(entity);
			}
			else
			{
				var isExists = await spellRepository.Any(x =>
					x.Translations.Any(x => x.LanguageCode == spell.LanguageCode && x.Name == spell.Name)
					&& x.SourceId == spell.SourceId
				);
				spell.Id = guidService.Generate();
				var entity = mapper.Map<Entities.Spell>(spell);
				entity.Conditions = await effectRepository.GetAll(x=> spell.ConditionIds.Contains(x.Id), false) ?? new List<Entities.Effect>();
				entity.Translations = new List<Entities.Translations.SpellTranslation>()
				{
					new()
					{
						LanguageCode = spell.LanguageCode,
						Content = spell.Content,
						MaterialDescription = spell.MaterialDescription,
						Name = spell.Name,
						Id = guidService.Generate()
					}
				};
				entity.SpellClasses = spell.ClassIds.Select(x=> new SpellClass()
				{
					ClassId = x.ClassId,
					SourceId = x.SourceId,
					IsVariant = x.IsVariant,
				}).ToList();
				await spellRepository.Add(entity);
			}
			await unitOfWork.SaveChangesAsync();
			return new ResponseDto<SpellDto>()
			{
				Code = ResponseCodesEnum.Ok,
				Result = spell
			};
		}
	}
}
