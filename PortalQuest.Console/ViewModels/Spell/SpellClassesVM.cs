namespace PortalQuest.Console.ViewModels.Spell
{
	internal class SpellClassesVM
	{
		public List<SpellClassDetailVM> Class { get; set; } = new List<SpellClassDetailVM>();
		public List<SpellClassDetailVM> ClassVariant { get; set; } = new List<SpellClassDetailVM>();
	}
}
