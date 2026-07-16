using System.Linq.Expressions;
using PortalQuest.Application.DTOs.Core.Spells;
using PortalQuest.Application.Specifications;

namespace PortalQuest.Application.Features.Core.Spell.Specifications
{
	public class SpellListSpec: BaseSpecification<Domain.Entities.Core.Spell>
	{
		public SpellListSpec(SpellListFilterDto filter)
		{
			Expression<Func<Domain.Entities.Core.Spell, bool>> criteria =
				s => 
					!s.IsDeleted
					&& (
						string.IsNullOrEmpty(filter.Name) || s.Translations.Where(t => t.LanguageCode == filter.LanguageCode).Select(t => t.Name).Any(x => x.Contains(filter.Name))
					)
					&& (
						filter.Classes.Count() == 0 || 
						s.SpellClasses.Select(sp => sp.ClassId)
							.Any(c => filter.Classes.Contains(c))
					)
					&& (
						filter.Sources.Count() == 0 || filter.Sources.Contains(s.SourceId)
					)
					&& (
						filter.Schools.Count() == 0 || filter.Schools.Contains(s.School)					
					)
					&& (
						filter.MinLevel == null || s.Level >= filter.MinLevel
					)
					&& (
						filter.MaxLevel == null || s.Level <= filter.MaxLevel
					)
					&& (
						filter.Ritual == null || s.Ritual == filter.Ritual
					)
					&& (
						filter.Concentration == null || s.Concentration == filter.Concentration
					)
					&& (
						filter.Conditions.Count() == 0 ||
						s.Conditions.Any(c => filter.Conditions.Contains(c.Id))
					)
					&& (
						filter.DamageTypes.Count() == 0 ||
						s.DamageType.Any(d => filter.DamageTypes.Contains(d))
					)
					//&& (
					//	filter.AttackTypes.Count() == 0 ||
					//	s.At.Any(a => filter.AttackTypes.Contains(a))
					//)
					&& (
						filter.SavingThrows.Count() == 0 ||
						s.SavingThrow.Any(st => filter.SavingThrows.Contains(st))
					)
					&& (
						filter.AbilityCheck.Count() == 0 ||
						s.AbilityCheck.Any(ab => filter.AbilityCheck.Contains(ab))
					)
					// To be Continued 
				;
			ApplyCriteria(criteria);

			AddInclude(s => s.Translations.Where(t => t.LanguageCode == filter.LanguageCode));
			AddInclude(s => s.Source);
			AddInclude(s => s.SpellClasses);
			AddInclude(s=> s.Range);
			AddInclude(s=> s.CastingTime);
			AddInclude(s=> s.Conditions);
			AddInclude(s=> s.Duration);


			bool desc = filter.Order?.StartsWith("-") ?? false;
			if (desc)
				filter.Order.Remove(0);
			Expression<Func<Domain.Entities.Core.Spell, object>> orderBy;
			switch (filter.Order)
			{
				case "Name":
					orderBy = x => x.Translations.First().Name;
					break;
				case "Level":
					orderBy = x => x.Level;
					break;
				case "CastingTime":
					orderBy = x => x.CastingTime.First().Amount;
					break;
				case "Ramge":
					orderBy = x => x.Range.Amount;
					break;
				case "Duration":
					orderBy = x => x.Duration.First().Time.Amount;
					break;
				default:
					orderBy = x => x.Translations.First().Name;
					break;
			}
			if (desc)
				ApplyOrderByDescending(orderBy);
			else
				ApplyOrderBy(orderBy);
			ApplyPaging(filter.Skip, filter.Take);
		}
	}
}
