using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PortalQuest.Application.DTOs.Core;
using PortalQuest.Application.Features.Core.Book.Query;
using PortalQuest.Application.Features.Core.Effect.Command;
using PortalQuest.Console.Tools;
using PortalQuest.Domain.Enums.Core;

namespace PortalQuest.Console.Commands
{
	public class GetEffectsCommand(
		IMediator mediator
	) : IConsoleCommand
	{
		public string Name => "get-effects";
		public List<BookDto> Books { get; set; }
		public async Task ExecuteAsync()
		{
			var dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");
			string path = Path.Combine(dataPath, "conditionsdiseases.json");
			string json = await File.ReadAllTextAsync(path);
			var effects = JsonConvert.DeserializeObject<Dictionary<string, JArray>>(json);
			if (effects == null)
				return;
			Books = (await mediator.Send(new GetBooksListRequest() { })).Result!;
			foreach((var effectType, var items) in effects)
			{
				if (Enum.TryParse<EffectTypesEnum>(effectType, ignoreCase: true, out var type))
					await SaveEffect(type, items);
			}
		}
		public async Task SaveEffect(EffectTypesEnum type, JArray items)
		{
			foreach (var item in items) {
				var effect = new EffectDto();
				var source = Books.FirstOrDefault(x => x.ShortName == item.GetValue<string>("source"));
				if(source == null)
					continue;
				effect.SourceId = source.Id;
				effect.Type = type;
				effect.Name = item.GetValue<string>("name");
				effect.SourcePage = item.GetValue<int>("page");
				effect.SRD = (item.GetValue<bool?>("srd") ?? item.GetValue<bool?>("srd52")) ?? false;
				effect.BasicRules = (item.GetValue<bool?>("basicRules") ?? item.GetValue<bool?>("basicRules2024")) ?? false;
				var content = JTokenTools.ParseEntry(item["entries"]);
				effect.Content = JsonConvert.SerializeObject(content);
				await mediator.Send(new UpsertEffectRequest() { Effect = effect});
			}
		}
	}
}
