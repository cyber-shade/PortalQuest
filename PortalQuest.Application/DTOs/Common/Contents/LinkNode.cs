namespace PortalQuest.Domain.Contents;
public class LinkNode : BlockNode<ContentNode>
{
	public override string Type => "link";
	public string Url { get; set; }
}
