namespace PortalQuest.Domain.Contents;

public class TableNode : BlockNode<TableRowNode>
{
	public override string Type => "table";
}
public class TableRowNode : BlockNode<TableCellNode>
{
	public override string Type => "table-row";
}
public class TableCellNode : BlockNode<TextNode>
{
	public override string Type => "table-cell";
	public bool IsHeading { get; set; }
}