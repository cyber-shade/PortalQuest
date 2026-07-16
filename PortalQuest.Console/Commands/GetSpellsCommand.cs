using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PortalQuest.Application.DTOs.Core;
using PortalQuest.Application.Features.Core.Book.Query;
using PortalQuest.Application.Features.Core.Class.Query;
using PortalQuest.Application.Features.Core.Duration.Command;
using PortalQuest.Application.Features.Core.Duration.Query;
using PortalQuest.Application.Features.Core.Range.Command;
using PortalQuest.Application.Features.Core.Range.Query;
using PortalQuest.Application.Features.Core.Spell.Command;
using PortalQuest.Application.Features.Core.Time.Command;
using PortalQuest.Application.Features.Core.Time.Query;
using PortalQuest.Console.Constants;
using PortalQuest.Console.ViewModels.Spell;
using PortalQuest.Domain.Contents;
using PortalQuest.Domain.Enums.Core;
using PortalQuest.Console.Tools;
using PortalQuest.Application.Features.Core.Effect.Query;
using PortalQuest.Domain.Enums.Common;
using PortalQuest.Application.DTOs.Core.Spells;

namespace PortalQuest.Console.Commands
{
	public class GetSpellsCommand(
		IMediator mediator
	) : IConsoleCommand
	{
		public string Name => "get-spells";
		private List<BookDto> Books { get; set; }
		private List<ClassDto> Classes { get; set; }
		private List<EffectDto> Conditions { get; set; }
		private List<RangeDto> Ranges { get; set; }
		private List<TimeDto> Times { get; set; }
		private List<DurationDto> Durations { get; set; }
		private Dictionary<string, Dictionary<string, SpellClassesVM>> SpellClasses { get; set; }

		public async Task ExecuteAsync()
		{
			Books = (await mediator.Send(new GetBooksListRequest())).Result ?? new List<BookDto>();
			Classes = (await mediator.Send(new GetClassesListRequst())).Result ?? new List<ClassDto>();
			Ranges = (await mediator.Send(new GetRangesListRequest())).Result ?? new List<RangeDto>();
			Times = (await mediator.Send(new GetTimesListRequest())).Result ?? new List<TimeDto>();
			Durations = (await mediator.Send(new GetDurationsListRequest())).Result ?? new List<DurationDto>();
			Conditions = (await mediator.Send(new GetEffectsListRequest()
			{
				Type = EffectTypesEnum.Condition
			})).Result ?? new List<EffectDto>();

			var dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "spells");
			if (!Directory.Exists(dataPath))
			{
				System.Console.WriteLine(ConsoleMessages.DataNotFound);
				return;
			}
			string sourcesPath = Path.Combine(dataPath, "sources.json");
			string sourcesJson = await File.ReadAllTextAsync(sourcesPath);
			var sources = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, SpellClassesVM>>>(sourcesJson);
			if (sources == null)
				return;
			SpellClasses = sources;

			string indexPath = Path.Combine(dataPath, "index.json");
			string indexJson = await File.ReadAllTextAsync(indexPath);
			var index = JsonConvert.DeserializeObject<Dictionary<string, string>>(indexJson);
			if (index == null)
				return;

			foreach ((var source, var spellPath) in index)
				await GetSpells(Path.Combine(dataPath, spellPath));
		}
		private async Task GetSpells(string path)
		{
			string json = await File.ReadAllTextAsync(path); ;
			var spells = JsonConvert.DeserializeObject<SpellContainerVM>(json)?.Spell;
			if (spells == null)
				return;

			foreach (var spell in spells)
				await SaveSpell(spell);
		}
		private async Task SaveSpell(JToken json)
		{
			var spell = new SpellDto();
			spell.LanguageCode = LanguageCodeEnum.En;
			spell.Name = json.GetValue<string>("name") ?? "";
			var sourceName = json.GetValue<string>("source");
			var source = Books.FirstOrDefault(x => x.ShortName == sourceName);
			if (source == null)
				return;
			spell.SourceId = source.Id;
			spell.SourcePage = json.GetValue<int>("page");
			spell.NameInSRD = string.Empty;
			var srd = json.GetValue<string>("srd") ?? json.GetValue<string>("srd52");
			if (srd != null)
				if (bool.TryParse(srd, out bool isSrd))
					spell.SRD = isSrd;
				else
					spell.NameInSRD = srd;
			spell.BasicRules = (json.GetValue<bool?>("basicRules") ?? json.GetValue<bool?>("basicRules2024")) ?? false;
			spell.Level = json.GetValue<int>("level");
			spell.Ritual = json.GetValue<bool>("meta", "ritual");
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

			await GetDurations(spell, json);
			await GetTimes(spell, json);
			await GetRange(spell, json);
			await GetSpellClasses(spell);
			await GetConditions(spell, json);
			GetContent(spell, json);
			await mediator.Send(new UpsertSpellRequest()
			{
				Spell = spell
			});
		}
		private async Task GetDurations(SpellDto spell, JToken json)
		{
			spell.DurationIds = new List<Guid>();
			var durationObjects = json.GetValue<object[]>("duration");
			if (durationObjects != null)
			{
				for (int i = 0; i < durationObjects.Length; i++)
				{
					var duration = new DurationDto();
					if (Enum.TryParse<DurationTypeEnum>(json.GetValue<string>("duration", i.ToString(), "type"), ignoreCase: true, out var parsed))
						duration.Type = parsed;
					else
						throw new Exception("type not found");
					switch (duration.Type)
					{
						case DurationTypeEnum.Timed:
							spell.Concentration = json.GetValue<bool>("duration", i.ToString(), "concentration");
							var time = new TimeDto();
							if (Enum.TryParse<TimeTypeEnum>(json.GetValue<string>("duration", i.ToString(), "duration", "type"), ignoreCase: true, out var timeParsed))
								time.Type = timeParsed;
							time.Amount = json.GetValue<int>("duration", i.ToString(), "duration", "amount");
							time.Condition = json.GetValue<string>("duration", i.ToString(), "duration", "condition") ?? string.Empty;
							duration.TimeId = await SaveTime(time);
							break;
						case DurationTypeEnum.Permanent:
							duration.Ends = json.GetValue<List<string>>("duration", i.ToString(), "ends") ?? new List<string>();
							break;
					}
					spell.DurationIds.Add(await SaveDuration(duration));
				}
			}
		}
		private async Task GetTimes(SpellDto spell, JToken json)
		{
			spell.CastingTimeIds = new List<Guid>();
			var timeObjects = json.GetValue<object[]>("time");
			if (timeObjects != null)
			{
				for (int i = 0; i < timeObjects.Length; i++)
				{
					var time = new TimeDto();
					if (Enum.TryParse<TimeTypeEnum>(json.GetValue<string>("time", i.ToString(), "unit"), ignoreCase: true, out var parsed))
						time.Type = parsed;
					time.Amount = json.GetValue<int>("time", i.ToString(), "number");
					time.Condition = json.GetValue<string>("time", i.ToString(), "condition") ?? string.Empty;
					spell.CastingTimeIds.Add(await SaveTime(time));
				}
			}
		}
		private async Task GetRange(SpellDto spell, JToken json)
		{
			var rangeObject = json.GetValue<object>("range");
			if (rangeObject != null)
			{
				var range = new RangeDto();
				if (Enum.TryParse<RangeTypeEnum>(json.GetValue<string>("range", "type"), ignoreCase: true, out var parsed))
					range.Type = parsed;
				if (range.Type == RangeTypeEnum.Point || range.Type == RangeTypeEnum.Cone || range.Type == RangeTypeEnum.Radius || range.Type == RangeTypeEnum.Line)
				{
					if (Enum.TryParse<DistanceTypeEnum>(json.GetValue<string>("range", "distance", "type"), ignoreCase: true, out var distanceTypeParsed))
						range.DistanceType = distanceTypeParsed;
					if (range.DistanceType == DistanceTypeEnum.Feet || range.DistanceType == DistanceTypeEnum.Miles)
						range.Amount = json.GetValue<int>("range", "distance", "amount");
				}
				spell.RangeId = await SaveRange(range);
			}
		}
		private async Task GetSpellClasses(SpellDto spell)
		{
			var spellSource = Books.FirstOrDefault(x=> x.Id == spell.SourceId);
			if(SpellClasses.TryGetValue(spellSource.ShortName, out var spells))
			{
				if (spells.TryGetValue(spell.Name, out var spellClasses))
				{
					foreach (var spellClass in spellClasses.Class.Concat(spellClasses.ClassVariant))
					{
						var classSource = Books.FirstOrDefault(x=> x.ShortName == spellClass.source);
						var clazz = Classes.FirstOrDefault(x=> x.Name == spellClass.name && x.SourceId == classSource.Id);
						if (clazz == null)
							continue;

						BookDto definedInSource = null;
						if(!string.IsNullOrEmpty(spellClass.definedInSource))
							definedInSource = Books.FirstOrDefault(x => x.ShortName == spellClass.definedInSource);
						spell.ClassIds.Add(new SpellClassDto()
						{
							ClassId = clazz.Id,
							SourceId = definedInSource?.Id,
							IsVariant = spellClasses.ClassVariant?.Contains(spellClass) ?? false,
						});
					}
				}
			}
		}
		private async Task GetConditions(SpellDto spell, JToken json)
		{
			var conditions = json.GetValue<string[]>("conditionInflict");
			if (conditions == null)
				return;
			 spell.ConditionIds = Conditions.Where(x=> conditions.Contains(x.Name.ToLower())).Select(x=> x.Id).ToList();
		}
		private void GetContent(SpellDto spell, JToken json)
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
			spell.Content = JsonConvert.SerializeObject(contents);
		}
		private async Task<Guid> SaveTime(TimeDto time)
		{
			var saved = Times.FirstOrDefault(x => 
				x.Type == time.Type
				&& x.Amount == time.Amount
				&& x.Condition == time.Condition
			);
			if (saved != null) 
				return saved.Id;
			var res = await mediator.Send(new UpsertTimeRequest() { Time = time});
			saved = res.Result!;
			Times.Add(saved);
			return saved.Id;
		}
		private async Task<Guid> SaveDuration(DurationDto duration)
		{
			var saved = Durations.FirstOrDefault(x =>
				x.TimeId == duration.TimeId
				&& x.Type == duration.Type
				&& x.Ends == duration.Ends
			);
			if (saved != null)
				return saved.Id;
			var res = await mediator.Send(new UpsertDurationRequest() { Duration = duration });
			saved = res.Result!;
			Durations.Add(saved);
			return saved.Id;
		}
		private async Task<Guid> SaveRange(RangeDto range)
		{
			var saved = Ranges.FirstOrDefault(x =>
				x.Type == range.Type
				&& x.DistanceType == range.DistanceType
				&& x.Amount == range.Amount
			);
			if (saved != null)
				return saved.Id;
			var res = await mediator.Send(new UpsertRangeRequest() { Range = range });
			saved = res.Result!;
			Ranges.Add(saved);
			return saved.Id;
		}
	}		
}
