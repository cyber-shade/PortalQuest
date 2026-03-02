using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PortalQuest.Console.Constants;
using PortalQuest.Domain.Contents;
using PortalQuest.Domain.Entities.Core;
using PortalQuest.Domain.Enums.Core;

namespace PortalQuest.Console.Commands
{
	public class GetSpellsCommand : IConsoleCommand
	{
		public string Name => "get-spells";
		public List<Source> Sources { get; set; }

		public async Task ExecuteAsync()
		{
			var dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "spells");
			if (!Directory.Exists(dataPath))
			{
				System.Console.WriteLine(ConsoleMessages.DataNotFound);
				return;
			}

			string indexPath = Path.Combine(dataPath, "index.json");
			string indexJson = await File.ReadAllTextAsync(indexPath);
			var index = JsonConvert.DeserializeObject<Dictionary<string, string>>(indexJson);
			if (index == null)
			{
				return;
			}
			Sources = new List<Source>();
			var spells = new List<Spell>();
			foreach((var source, var spellPath) in index)
			{
				Sources.Add(new Source()
				{
					Name = source
				});
				var processedSpells = await GetSpells(Path.Combine(dataPath, spellPath));
				if (processedSpells != null)
				{
					spells.AddRange(processedSpells);
				}
			}
		}
		public async Task<List<Spell>> GetSpells(string path)
		{
			string json = await File.ReadAllTextAsync(path);
			var spells = JsonConvert.DeserializeObject<Container>(json)?.Spell;
			if (spells == null) {
				return null;
			}
			var processedSpells = new List<Spell>();
			foreach(var spell in spells)
			{
				processedSpells.Add(await ProcessSpell(spell));
			}
			return processedSpells;
		}
		public async Task<Spell> ProcessSpell(JToken json)
		{
			var spell = new Spell();
			spell.Name = json.GetValue<string>("name") ?? "";
			var sourceName = json.GetValue<string>("source");
			spell.Source = Sources.FirstOrDefault(x => x.Name == sourceName);
			spell.SourcePage = json.GetValue<int>("page");
			var srd = json.GetValue<string>("srd");
			if(srd  != null)
				if (bool.TryParse(srd, out bool isSrd))
					spell.SRD = isSrd;
				else
					spell.NameInSRD = srd;
			spell.Level = json.GetValue<int>("level");
			spell.Ritual = json.GetValue<bool>("meta","ritual");
			spell.School = json.GetValue<string>("school") switch
			{
				"A" => MagicSchoolEnum.Abjuration,
				"C" => MagicSchoolEnum.Conjuration,
				"D" => MagicSchoolEnum.Divination,
				"E" => MagicSchoolEnum.Enchantment,
				"V" => MagicSchoolEnum.Evocation,
				"I" => MagicSchoolEnum.Illusion,
				"N" => MagicSchoolEnum.Necromancy,
				"T" => MagicSchoolEnum.Transmutation,
			};
			spell.Somatic = json.GetValue<bool>("components", "s");
			spell.Verbal = json.GetValue<bool>("components", "v");
			var materialObject = json.GetValue("components", "m");
			if (materialObject != null)
			{
				if (materialObject is string m)
					spell.MaterialDescription = m;
				else
				{
					spell.MaterialCost = json.GetValue<int>("components", "m", "cost");
					var consume = json.GetValue<string>("components", "m", "consume");
					if (bool.TryParse(consume, out bool isConsume))
						spell.MaterialConsume = isConsume;
					spell.MaterialDescription = json.GetValue<string>("components", "m", "text");
				}
			}
			spell.SavingThrow = new List<AbilityScoreEnum> { };
			var savingThrows = json.GetValue<string[]>("savingThrow");
			if (savingThrows != null)
				foreach (var savingThrow in savingThrows)
					if (Enum.TryParse<AbilityScoreEnum>(savingThrow, ignoreCase: true, out var parsed))
						spell.SavingThrow.Add(parsed);

			spell.AbilityCheck = new List<AbilityScoreEnum> { };
			var abilityChecks = json.GetValue<string[]>("abilityCheck");
			if (abilityChecks != null)
				foreach (var abilityCheck in abilityChecks)
					if (Enum.TryParse<AbilityScoreEnum>(abilityCheck, ignoreCase: true, out var parsed))
						spell.AbilityCheck.Add(parsed);

			spell.DamageType = new List<DamageTypeEnum> { };
			var damageTypes = json.GetValue<string[]>("damageInflict");
			if (damageTypes != null)
				foreach (var damageType in damageTypes)
					if (Enum.TryParse<DamageTypeEnum>(damageType, ignoreCase: true, out var parsed))
						spell.DamageType.Add(parsed);
			
			var durationObjects = json.GetValue<object[]>("duration");
			if (durationObjects != null)
			{
				spell.Duration = new List<Duration>();
				for(int i =0; i<durationObjects.Length; i++)
				{
					var duration = new Duration();
					if (Enum.TryParse<DurationTypeEnum>(json.GetValue<string>("duration", i.ToString(), "type"), ignoreCase: true, out var parsed))
						duration.Type = parsed;
					else
						throw new Exception("type not found");
					switch (duration.Type)
					{
						case DurationTypeEnum.Timed:
							spell.Concentration = json.GetValue<bool>("duration", i.ToString(), "concentration");
							var time = new Time();
							if (Enum.TryParse<TimeTypeEnum>(json.GetValue<string>("duration", i.ToString(), "duration", "type"), ignoreCase: true, out var timeParsed))
								time.Type = timeParsed;
							time.Amount = json.GetValue<int>("duration", i.ToString(), "duration", "amount");
							duration.Time = time;
							break;
						case DurationTypeEnum.Permanent:
							duration.Ends = json.GetValue<List<string>>("duration", i.ToString(), "ends") ?? new List<string>();
							break;
					}
					spell.Duration.Add(duration);
				}
			}
			var rangeObject = json.GetValue<object>("range");
			if(rangeObject != null)
			{
				var range = new Domain.Entities.Core.Range();
				if (Enum.TryParse<RangeTypeEnum>(json.GetValue<string>("range", "type"), ignoreCase: true, out var parsed))
					range.Type = parsed;
				if (range.Type == RangeTypeEnum.Point || range.Type == RangeTypeEnum.Cone || range.Type == RangeTypeEnum.Radius || range.Type == RangeTypeEnum.Line)
				{
					if (Enum.TryParse<DistanceTypeEnum>(json.GetValue<string>("range", "distance", "type"), ignoreCase: true, out var distanceTypeParsed))
						range.DistanceType = distanceTypeParsed;
					if(range.DistanceType == DistanceTypeEnum.Feet || range.DistanceType == DistanceTypeEnum.Miles)
						range.Amount = json.GetValue<int>("range", "distance", "amount");
				}
				spell.Range = range;
			}
			var timeObjects = json.GetValue<object[]>("time");
			if (timeObjects != null)
			{
				spell.CastingTime = new List<Time>();
				for (int i = 0; i < timeObjects.Length ; i++)
				{
					var time = new Time();
					if (Enum.TryParse<TimeTypeEnum>(json.GetValue<string>("time", i.ToString(), "unit"), ignoreCase: true, out var parsed))
						time.Type = parsed;
					time.Amount = json.GetValue<int>("time", i.ToString(), "amount");
					spell.CastingTime.Add(time);
				}
			}
			var contents = GetContent(json);
			spell.Content = JsonConvert.SerializeObject(contents);
			return spell;
		}
		public List<ContentNode> GetContent(JToken json)
		{
			var contents = new List<ContentNode>();

			foreach (var key in new[] { "entries", "entriesHigherLevel" })
			{
				var token = json[key];
				if (token == null) continue;

				if (key == "entriesHigherLevel")
				{
					contents.Add(new HeadingNode
					{
						Children = new List<ContentNode>
				{
					new TextNode { Text = "At Higher Level" }
				}
					});
				}

				contents.AddRange(JTokenTools.ParseEntry(token));
			}
			return contents;
		}
	}
	public static class JTokenTools
	{
		public static List<ContentNode> ParseEntry(JToken token)
		{
			var result = new List<ContentNode>();

			if (token == null)
				return result;

			// اگر آرایه بود → روی تک تک آیتم‌ها recurse کن
			if (token.Type == JTokenType.Array)
			{
				foreach (var child in token)
					result.AddRange(ParseEntry(child));

				return result;
			}

			// اگر string بود
			if (token.Type == JTokenType.String)
			{
				result.Add(new ParagraphNode
				{
					Children = new List<ContentNode>
			{
				new TextNode { Text = token.ToString() }
			}
				});

				return result;
			}

			// اگر object بود
			if (token.Type == JTokenType.Object)
			{
				var type = token["type"]?.ToString();

				switch (type)
				{
					case "entries":
						var name = token["name"]?.ToString();
						if (!string.IsNullOrEmpty(name))
						{
							result.Add(new HeadingNode
							{
								Children = new List<ContentNode>
						{
							new TextNode { Text = name }
						}
							});
						}

						result.AddRange(ParseEntry(token["entries"]));
						break;

					case "list":
						var items = token["items"];
						if (items != null)
						{
							foreach (var item in items)
							{
								result.Add(new BulletedListNode
								{
									Children = ParseEntry(item)
								});
							}
						}
						break;

					case "table":
						result.Add(ParseTable(token));
						break;
				}
			}

			return result;
		}
		private static TableNode ParseTable(JToken token)
		{
			var table = new TableNode
			{
				Children = new List<TableRowNode>()
			};

			var heads = token["colLabels"];
			if (heads != null)
			{
				var headerRow = new TableRowNode
				{
					Children = heads.Select(h => new TableCellNode
					{
						IsHeading = true,
						Children = new List<TextNode>
				{
					new TextNode { Text = h.ToString() }
				}
					}).ToList()
				};

				table.Children.Add(headerRow);
			}

			var rows = token["rows"];
			if (rows != null)
			{
				foreach (var row in rows)
				{
					var rowNode = new TableRowNode
					{
						Children = row.Select(cell => new TableCellNode
						{
							Children = new List<TextNode>
					{
						new TextNode { Text = cell.ToString() }
					}
						}).ToList()
					};

					table.Children.Add(rowNode);
				}
			}

			return table;
		}


		public static T? GetValue<T>(this JToken json, params string[] keys)
		{
			JToken currentValue = json;
			foreach (var key in keys)
			{
				if (currentValue == null)
					break;
				if(int.TryParse(key, out var index))
					currentValue = currentValue[index];
				else
					currentValue = currentValue[key];
			}
			if (currentValue == null)
				return default(T);
			var result = JsonConvert.SerializeObject(currentValue);
			return JsonConvert.DeserializeObject<T>(result);
		}
		public static object? GetValue(this JToken json, params string[] keys)
		{
			return json.GetValue<object>(keys);
		}
	}
	public class Container
	{
		public JArray Spell { get; set; }
	}
}
