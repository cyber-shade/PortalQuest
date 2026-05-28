using System.ComponentModel.DataAnnotations.Schema;
using PortalQuest.Domain.Entities.Core.M2M;
using PortalQuest.Domain.Enums.Core;
using PortalQuest.Domain.ValueObjects.Core;

namespace PortalQuest.Domain.Entities.Core;
public class Class : BaseCoreContentEntity
{
	public Dice HitDice { get; init; }
	public List<AbilityScoreEnum> Proficiency { get; set; } = new();
	public AbilityScoreEnum? SpellcastingAbility { get; set; }
	public SpellcastingProgressionTypeEnum? SpellcastingProgressionType { get; set; }
	public SpellPreparationMechanicEnum? SpellPreparationMechanic { get; set; }
	public List<SpellsPerCharacterLevel> SpellcastingProgression { get; set; } = new();

	#region Relations
	public List<SubClass> Subs { get; set; }
	public List<ClassFeature> Features { get; set; } // O2M
	public List<SpellClass> SpellClasses { get; set; } // M2M
	#endregion
	#region Business Logic
	public int GetPreparableOrKnownSpells(CharacterLevelsEnum classLevel, int spellcastingAbilityModifier)
	{
		// none spellcasters
		if (SpellcastingAbility == null || SpellPreparationMechanic == null)
			return 0;

		var levelProgression = SpellcastingProgression.FirstOrDefault(x => x.Level == classLevel);
		// for some half spellcaster that don't have spell slot in first level
		if (levelProgression == null || !levelProgression.SpellSlots.Any(s => s.SlotsNumber > 0))
			return 0;

		if (SpellPreparationMechanic == SpellPreparationMechanicEnum.Known) // knowen spellcasters
		{
			var progression = SpellcastingProgression?.FirstOrDefault(x=> x.Level == classLevel);
			return progression == null ? 0 : (progression.SpellsKnown ?? 0);
		}
		else // prepared spellcasters
		{
			int divider = SpellcastingProgressionType switch
			{
				SpellcastingProgressionTypeEnum.FullCaster => 1,
				SpellcastingProgressionTypeEnum.HalfCaster => 2,
				SpellcastingProgressionTypeEnum.ThirdCaster => 3,
				_ => 1
			};
			int preparedCount = ((int)classLevel / divider) + spellcastingAbilityModifier;
			return Math.Max(1, preparedCount);  // prepared spell casters atleast have 1 spell prepared
		}
	}
	#endregion
}

