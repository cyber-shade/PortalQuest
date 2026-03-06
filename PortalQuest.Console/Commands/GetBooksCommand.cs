using MediatR;
using Newtonsoft.Json;
using PortalQuest.Application.DTOs.Core;
using PortalQuest.Application.Features.Core.Book.Command;
using PortalQuest.Console.ViewModels.Book;

namespace PortalQuest.Console.Commands
{
	public class GetBooksCommand(
		IMediator mediator
	) : IConsoleCommand
	{
		public string Name => "get-books";

		public async Task ExecuteAsync()
		{
			string booksPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "books.json");
			string booksJson = await File.ReadAllTextAsync(booksPath);
			var model = JsonConvert.DeserializeObject<BookContainerVM>(booksJson);
			foreach (var book in model.book)
			{
				await mediator.Send(new UpsertBookRequest() { 
					Book = new BookDto()
					{
						Name = book.name,
						ShortName = book.id,
						Author = book.author,
						PublishedDateTime = book.published,
						Content = string.Empty
					}
				});
			}
		}
	}
}
