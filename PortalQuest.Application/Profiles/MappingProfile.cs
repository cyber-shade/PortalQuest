using AutoMapper;
using PortalQuest.Application.DTOs.Core;
using PortalQuest.Domain.Entities.Core;

namespace PortalQuest.Application.Profiles;
public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<BookDto, Book>().ReverseMap();
	}
}
