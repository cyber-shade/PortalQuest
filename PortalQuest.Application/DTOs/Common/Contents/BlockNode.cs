namespace PortalQuest.Domain.Contents;

public abstract class BlockNode<TChild> : ContentNode where TChild : ContentNode
{
	public abstract string Type { get; }
	public List<TChild> Children { get; set; }
}
