using AutoMapper;
using PortalQuest.Application.DTOs.Core;
using PortalQuest.Application.DTOs.Core.Spells;
using PortalQuest.Domain.Entities.Core;
using PortalQuest.Domain.Entities.Core.M2M;
using PortalQuest.Domain.ValueObjects.Core;

namespace PortalQuest.Application.Profiles;
public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<BookDto, Book>().ReverseMap();
		CreateMap<Spell, SpellDto>().ForMember(dest => dest.Name,
				opt => opt.MapFrom(
					src => src.Translations.FirstOrDefault()!.Name
			))
			.ForMember(dest => dest.Content,
				opt => opt.MapFrom(
					src => src.Translations.FirstOrDefault()!.Content
			))
			.ReverseMap();
		CreateMap<SpellClassDto, SpellClass>().ReverseMap();
		CreateMap<ClassDto, Class>().ReverseMap();
		CreateMap<RangeDto, Domain.ValueObjects.Core.Range>().ReverseMap();
		CreateMap<TimeDto, Time>().ReverseMap();
		CreateMap<DurationDto, Duration>().ReverseMap();
		CreateMap<Effect, EffectDto>()
			.ForMember(dest => dest.Name,
				opt => opt.MapFrom(
					src => src.Translations.FirstOrDefault()!.Name
			))
			.ForMember(dest => dest.Content,
				opt => opt.MapFrom(
					src => src.Translations.FirstOrDefault()!.Content
			))
			.ReverseMap();
	}
}
