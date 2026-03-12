using AutoMapper;
using MediatR;
using PortalQuest.Application.Constants;
using PortalQuest.Application.DTOs.Common;
using PortalQuest.Application.DTOs.Core;
using PortalQuest.Application.Interfaces.Repository.Core;
using PortalQuest.Application.Tools;
using Entities = PortalQuest.Domain.Entities.Core;
using PortalQuest.Domain.Interfaces;
using PortalQuest.Domain.Entities.Core.M2M;

namespace PortalQuest.Application.Features.Core.Spell.Command
{
	public class UpsertSpellRequest : IRequest<ResponseDto<SpellDto>>
	{
		public required SpellDto Spell { get; set; }
	}
	internal class UpsertSpellRequestHandler(
		ISpellRepository spellRepository, IMapper mapper, IGuidService guidService,
		IDurationRepository durationRepository, ITimeRepository timeRepository, IEffectRepository effectRepository
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
				await spellRepository.Update(entity);
			}
			else
			{
				var isExists = await spellRepository.Any(x =>
					x.Name == spell.Name && x.SourceId == spell.SourceId
				);
				spell.Id = guidService.Generate();
				var entity = mapper.Map<Entities.Spell>(spell);
				entity.Duration = (await durationRepository.GetAll(x => spell.DurationIds.Contains(x.Id))).items ?? new List<Entities.Duration>();
				entity.CastingTime = (await timeRepository.GetAll(x => spell.CastingTimeIds.Contains(x.Id))).items ?? new List<Entities.Time>();				
				entity.Conditions = (await effectRepository.GetAll(x=> spell.ConditionIds.Contains(x.Id))).items ?? new List<Entities.Effect>();	
				entity.SpellClasses = spell.ClassIds.Select(x=> new SpellClass()
				{
					ClassId = x.ClassId,
					SourceId = x.SourceId,
					IsVariant = x.IsVariant,
				}).ToList();	
				await spellRepository.Add(entity);
			}
			return new ResponseDto<SpellDto>()
			{
				Code = ResponseCodesEnum.Ok,
				Result = spell
			};
		}
	}
}
