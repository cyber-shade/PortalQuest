using AutoMapper;
using MediatR;
using PortalQuest.Application.DTOs.Common;
using PortalQuest.Application.DTOs.Core.Spells;
using PortalQuest.Application.Features.Core.Spell.Specifications;
using PortalQuest.Application.Interfaces.Repository.Core;
using PortalQuest.Application.Interfaces.Services;

namespace PortalQuest.Application.Features.Core.Spell.Query
{
	public class GetAllSpellsRequest : SpellListFilterDto, IRequest<PagedResultDto<SpellDto>>
	{
	}
	internal class GetAllSpellsRequestHandler(
		ISpellRepository spellRepository, IMapper mapper, ICurrentLanguageService currentLanguageService
	) : IRequestHandler<GetAllSpellsRequest, PagedResultDto<SpellDto>>
	{
		public async Task<PagedResultDto<SpellDto>> Handle(GetAllSpellsRequest request, CancellationToken cancellationToken)
		{
			request.LanguageCode = currentLanguageService.LanguageCode;
			var spec = new SpellListSpec(request);
			var res = await spellRepository.GetAll(spec, cancellationToken: cancellationToken);
			var items = mapper.Map<List<SpellDto>>(res.Items);
			return new PagedResultDto<SpellDto>()
			{
				Items = items,
				Skip = res.Skip,
				Take = res.Take,
				TotalCount = res.TotalCount
			};

		}
	}
}
