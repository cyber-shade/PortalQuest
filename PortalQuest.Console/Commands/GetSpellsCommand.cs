using HtmlAgilityPack;
using PortalQuest.Application.Dto.Core.Spell;
namespace PortalQuest.Console.Commands
{
	public class GetSpellsCommand : IConsoleCommand
	{
		public string Name => "get-spells";
		private HttpClient _httpClient = new HttpClient();
		private string website = "https://www.aidedd.org/";

		public async Task ExecuteAsync()
		{
			var listUrl = website+"en/rules/spells/";
			string html = await _httpClient.GetStringAsync(listUrl);
			var doc = new HtmlDocument();
			doc.LoadHtml(html);
			var spellList = doc.DocumentNode.SelectSingleNode("//div[@class='liste']");
			if (spellList == null)
			{
				System.Console.WriteLine("Failed Investigation Check");
				return;
			}
			var spells = spellList.SelectNodes(".//a");
			if (spells == null)
			{
				System.Console.WriteLine("Failed Investigation Check");
				return;
			}
			var maxConcurrent = 10;
			var semaphore = new SemaphoreSlim(maxConcurrent, maxConcurrent);
			var tasks = spells.Select(async spell =>
			{
				UpsertSimpleSpellDto model = null;
				await semaphore.WaitAsync();
				try
				{
					string title = spell.InnerText?.Trim() ?? "";
					string href = spell.GetAttributeValue("href", "");
					System.Console.WriteLine($"link: {href}");
					if (!string.IsNullOrEmpty(href))
						model = await GetSpell(href, title);
				}
				finally { 
					semaphore.Release(); 
				}
				return model;
			});
			var models = (await Task.WhenAll(tasks)).Where(x=> x != null).ToList();
			return;
		}
		private async Task<UpsertSimpleSpellDto> GetSpell(string url, string title)
		{
			url = url.Replace("../", website);
			var spell = new UpsertSimpleSpellDto();
			string html = await _httpClient.GetStringAsync(url);
			var doc = new HtmlDocument();
			doc.LoadHtml(html);
			var container = doc.DocumentNode.SelectSingleNode("//div[@class='col1']");
			spell.Title = container.SelectSingleNode(".//h1").InnerText ?? title;
			var levelType = container.SelectSingleNode("//div[@class='ecole']").InnerText.Split(" - ");
			(spell.Level, spell.Type) = (levelType[0], levelType[1]);
			spell.CastingTime = container.SelectSingleNode("//div[@class='t']").InnerText;
			spell.Range = container.SelectSingleNode("//div[@class='r']").InnerText;
			spell.Components = container.SelectSingleNode("//div[@class='c']").InnerText;
			spell.Duration = container.SelectSingleNode("//div[@class='d']").InnerText;
			spell.Description = container.SelectSingleNode("//div[@class='description']").InnerText;
			var classes = container.SelectNodes("//div[@class='classe']");
			spell.Classes = classes?.Select(x => x.InnerHtml).ToList() ?? new List<string>();
			spell.Source = container.SelectSingleNode("//div[@class='source']").InnerText;
			return spell;
		}
	}
}
