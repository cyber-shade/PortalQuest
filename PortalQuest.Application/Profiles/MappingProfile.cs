using AutoMapper;
using PortalQuest.Application.DTOs.Core;
using PortalQuest.Domain.Entities.Core;
using PortalQuest.Domain.Entities.Core.M2M;

namespace PortalQuest.Application.Profiles;
public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<BookDto, Book>().ReverseMap();
		CreateMap<SpellDto, Spell>().ReverseMap();
		CreateMap<SpellClassDto, SpellClass>().ReverseMap();
		CreateMap<ClassDto, Class>().ReverseMap();
		CreateMap<RangeDto, Domain.Entities.Core.Range>().ReverseMap();
		CreateMap<TimeDto, Time>().ReverseMap();
		CreateMap<DurationDto, Duration>().ReverseMap();
		CreateMap<EffectDto, Effect>().ReverseMap();
	}
}
