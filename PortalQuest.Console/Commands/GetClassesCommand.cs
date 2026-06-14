using MediatR;
using Newtonsoft.Json;
using PortalQuest.Application.DTOs.Core;
using PortalQuest.Application.Features.Core.Book.Query;
using PortalQuest.Application.Features.Core.Class.Command;
using PortalQuest.Console.Constants;
using PortalQuest.Console.ViewModels.Class;
using PortalQuest.Domain.Enums.Common;

namespace PortalQuest.Console.Commands
{
	public class GetClassesCommand(
		IMediator mediator
	) : IConsoleCommand
	{
		public string Name => "get-classes";
		public List<ClassDto> Classes { get; set; }
		public List<BookDto> Books { get; set; }

		public async Task ExecuteAsync()
		{
			Books = (await mediator.Send(new GetBooksListRequest())).Result ?? new List<BookDto>();
			var dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "class");
			if (!Directory.Exists(dataPath))
			{
				System.Console.WriteLine(ConsoleMessages.DataNotFound);
				return;
			}
			string indexPath = Path.Combine(dataPath, "index.json");
			string indexJson = await File.ReadAllTextAsync(indexPath);
			var index = JsonConvert.DeserializeObject<Dictionary<string, string>>(indexJson);
			if (index == null)
				return;
			foreach ((var className, var classPath) in index)
			{
				if (string.Equals(className, "sidekick") || string.Equals(className, "mystic"))
					continue;
				await ProcessClasses(Path.Combine(dataPath, classPath));
			}
		}
		public async Task ProcessClasses(string path)
		{
			string classJson = await File.ReadAllTextAsync(path);
			var model = JsonConvert.DeserializeObject<ClassContainerVM>(classJson);
			foreach (var clazz in model.Class)
			{
				var res = await mediator.Send(new UpsertClassRequest() 
				{
					Class = new ClassDto()
					{
						Name = clazz.name,
						SourceId = Books.FirstOrDefault(x => x.ShortName == clazz.source)!.Id,
						Content = string.Empty,
						LanguageCode = LanguageCodeEnum.En
					}
				});
			}
		}
	}
}
