using System.Text.Json.Serialization;

namespace PortalQuest.Domain.Contents;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(ParagraphNode), "paragraph")]
[JsonDerivedType(typeof(TableNode), "table")]
[JsonDerivedType(typeof(TableRowNode), "table-row")]
[JsonDerivedType(typeof(TableCellNode), "table-cell")]
[JsonDerivedType(typeof(LinkNode), "link")]
[JsonDerivedType(typeof(HeadingNode), "heading")]
[JsonDerivedType(typeof(QuoteNode), "quote")]
[JsonDerivedType(typeof(BulletedListNode), "bulleted-list")]
[JsonDerivedType(typeof(NumberedListNode), "numbered-list")]
[JsonDerivedType(typeof(TextNode), "text")]
public class ContentNode { 

}
