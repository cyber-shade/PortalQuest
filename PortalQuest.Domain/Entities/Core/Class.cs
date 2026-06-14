using PortalQuest.Domain.Entities.Core.M2M;
using PortalQuest.Domain.Entities.Core.Translations;
using PortalQuest.Domain.Interfaces;

namespace PortalQuest.Domain.Entities.Core;
public class Class : BaseRuleEntity, ITranslatable<ClassTranslation>
{	
	#region Relations
	public List<SubClass> Subs { get; set; }
	public List<ClassFeature> Features { get; set; } // O2M
	public List<SpellClass> SpellClasses { get; set; } // M2M
	public List<ClassTranslation> Translations { get; set; }
	#endregion
}

