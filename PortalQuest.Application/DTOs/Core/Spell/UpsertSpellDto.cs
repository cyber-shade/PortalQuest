using System.Text.Json.Nodes;
using PortalQuest.Application.DTOs.Core.Component;
using PortalQuest.Application.DTOs.Core.Duration;
using PortalQuest.Application.DTOs.Core.Range;
using PortalQuest.Application.DTOs.Core.Time;

namespace PortalQuest.Application.DTOs.Core.Spell
{
	public class UpsertSpellDto
	{
		public string Name { get; set; }
		public string Source { get; set; }
		public int Page { get; set; }
		public bool Srd { get; set; }
		public bool BasicRules { get; set; }
		public int Level { get; set; }
		public string School { get; set; }
		public List<TimeDto> time { get; set; }
		public UpsertRangeDto Range { get; set; }
		public ComponentDto Components { get; set; }
		public List<DurationDto> Duration { get; set; }
		public List<object> Entries { get; set; }
		public Dictionary<string, string> ScalingLevel { get; set; }
		public List<string> Damages { get; set; }
		public List<string> SavingThrows { get; set; }
		public List<string> miscTags { get; set; }
		public List<string> areaTags { get; set; }
	}
}
