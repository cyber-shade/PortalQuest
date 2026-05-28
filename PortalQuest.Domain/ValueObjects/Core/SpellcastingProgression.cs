using PortalQuest.Domain.Enums.Core;

namespace PortalQuest.Domain.ValueObjects.Core
{
	public record SpellsPerCharacterLevel(
		int? CantripsKnown,
		int? SpellsKnown,
		CharacterLevelsEnum Level,
		List<SpellSlots> SpellSlots
	);
	public record SpellSlots(SpellLevelsEnum Level, int SlotsNumber);
}
