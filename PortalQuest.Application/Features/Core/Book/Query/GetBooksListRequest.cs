using AutoMapper;
using MediatR;
using PortalQuest.Application.DTOs.Common;
using PortalQuest.Application.DTOs.Core;
using PortalQuest.Application.Interfaces.Repository.Core;
using PortalQuest.Application.Tools;

namespace PortalQuest.Application.Features.Core.Book.Query
{
	public class GetBooksListRequest : IRequest<ResponseDto<List<BookDto>>>
	{
	}
	internal class GetBooksListRequestHandler(
		IBookRepository bookRepository, IMapper mapper
	) : IRequestHandler<GetBooksListRequest, ResponseDto<List<BookDto>>>
	{
		public async Task<ResponseDto<List<BookDto>>> Handle(GetBooksListRequest request, CancellationToken cancellationToken)
		{
			var books = (await bookRepository.GetAll()).items;
			var model = mapper.Map<List<BookDto>>(books);	
			return ResponseFactory.FillObject<List<BookDto>>(model);
		}
	}
}
