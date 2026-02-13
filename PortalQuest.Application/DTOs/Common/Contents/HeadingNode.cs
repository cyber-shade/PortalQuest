namespace PortalQuest.Domain.Contents;
public class HeadingNode : BlockNode<ContentNode>
{
	public override string Type => "heading";
	public int Level { get; set; }
}