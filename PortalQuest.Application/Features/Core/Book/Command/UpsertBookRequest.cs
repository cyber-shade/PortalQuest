using AutoMapper;
using MediatR;
using PortalQuest.Application.Constants;
using PortalQuest.Application.DTOs.Common;
using PortalQuest.Application.DTOs.Core;
using PortalQuest.Application.Interfaces.Repository.Core;
using PortalQuest.Application.Tools;
using PortalQuest.Domain.Interfaces;
using Entity = PortalQuest.Domain.Entities.Core;
using PortalQuest.Domain.Entities.Core.Translations;

namespace PortalQuest.Application.Features.Core.Book.Command
{
	public class UpsertBookRequest : IRequest<ResponseDto<BookDto>>
	{
		public required BookDto Book { get; set; }
	}
	internal class UpsertBookRequestHandler(
		IGuidService guidService, IBookRepository bookRepository, IMapper mapper
	) : IRequestHandler<UpsertBookRequest, ResponseDto<BookDto>>
	{
		public async Task<ResponseDto<BookDto>> Handle(UpsertBookRequest request, CancellationToken cancellationToken)
		{
			var book = request.Book;
			var toUpdate = !guidService.IsEmpty(request.Book.Id);
			if (toUpdate)
			{
				var entity = await bookRepository.Get(book.Id);
				if (entity == null)
					return ResponseFactory.DataError<BookDto>(SystemMessages.SourceNotFound);
				entity = mapper.Map(book, entity);
				await bookRepository.Update(entity);
			}
			else
			{
				var isExists = await bookRepository.Any(x => 
					x.Translations.Any(t=> t.Name == book.Name && t.LanguageCode == book.LanguageCode)
					|| x.ShortName == book.ShortName 
				);
				book.Id = guidService.Generate();
				var entity = mapper.Map<Entity.Book>(book);
				entity.Translations = new List<BookTranslation>()
				{
					new()
					{
						LanguageCode = book.LanguageCode,
						Content = book.Content,
						Name = book.Name,
						Id = guidService.Generate()
					}
				};
				await bookRepository.Add(entity);
			}
			return new ResponseDto<BookDto>() { 
					
				Code = ResponseCodesEnum.Ok,
				Result = book
			};
		}
	}
}