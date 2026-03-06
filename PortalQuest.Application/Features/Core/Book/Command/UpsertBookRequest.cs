using AutoMapper;
using MediatR;
using PortalQuest.Application.Constants;
using PortalQuest.Application.DTOs.Common;
using PortalQuest.Application.DTOs.Core;
using PortalQuest.Application.Interfaces.Repository.Core;
using PortalQuest.Application.Tools;
using PortalQuest.Domain.Interfaces;
using Entity = PortalQuest.Domain.Entities.Core;

namespace PortalQuest.Application.Features.Core.Book.Command
{
	public class UpsertBookRequest : IRequest<ResponseDto<BookDto>>
	{
		public required BookDto Book { get; set; }
	}
	internal class UpsertBookRequestHandler(
		IGuidService guidService, IBookRepository sourceRepository, IMapper mapper
	) : IRequestHandler<UpsertBookRequest, ResponseDto<BookDto>>
	{
		public async Task<ResponseDto<BookDto>> Handle(UpsertBookRequest request, CancellationToken cancellationToken)
		{
			var source = request.Book;
			var toUpdate = !guidService.IsEmpty(request.Book.Id);
			if (toUpdate)
			{
				var entity = await sourceRepository.Get(source.Id);
				if (entity == null)
					return ResponseFactory.DataError<BookDto>(SystemMessages.SourceNotFound);
				entity = mapper.Map(source, entity);
				await sourceRepository.Update(entity);
			}
			else
			{
				var isExists = await sourceRepository.Any(x => 
					x.ShortName == source.ShortName || x.Name == source.Name
				);
				source.Id = guidService.Generate();
				var entity = mapper.Map<Entity.Book>(source);
				await sourceRepository.Add(entity);
			}
			return new ResponseDto<BookDto>() { 
					
				Code = ResponseCodesEnum.Ok,
				Result = source
			};
		}
	}
}