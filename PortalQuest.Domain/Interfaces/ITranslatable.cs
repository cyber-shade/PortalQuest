namespace PortalQuest.Domain.Interfaces
{
	public interface ITranslatable<TTranslation> where TTranslation : class, ITranslation
	{
		List<TTranslation> Translations { get; set; }
	}
}
