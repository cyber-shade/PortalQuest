namespace PortalQuest.Domain.Contents;
public class BulletedListNode : BlockNode<ContentNode>
{
	public override string Type => "bulleted-list";
}
public class NumberedListNode : BlockNode<ContentNode>
{
	public override string Type => "numbered-list";
}
