using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PortalQuest.Domain.Contents;

namespace PortalQuest.Console.Tools
{
	public static class JTokenTools
	{
		public static List<ContentNode> ParseEntry(JToken token)
		{
			var result = new List<ContentNode>();

			if (token == null)
				return result;

			// اگر آرایه بود → روی تک تک آیتم‌ها recurse کن
			if (token.Type == JTokenType.Array)
			{
				foreach (var child in token)
					result.AddRange(ParseEntry(child));

				return result;
			}

			// اگر string بود
			if (token.Type == JTokenType.String)
			{
				result.Add(new ParagraphNode
				{
					Children = new List<ContentNode>
			{
				new TextNode { Text = token.ToString() }
			}
				});

				return result;
			}

			// اگر object بود
			if (token.Type == JTokenType.Object)
			{
				var type = token["type"]?.ToString();

				switch (type)
				{
					case "entries":
						var name = token["name"]?.ToString();
						if (!string.IsNullOrEmpty(name))
						{
							result.Add(new HeadingNode
							{
								Children = new List<ContentNode>
						{
							new TextNode { Text = name }
						}
							});
						}

						result.AddRange(ParseEntry(token["entries"]));
						break;

					case "list":
						var items = token["items"];
						if (items != null)
						{
							foreach (var item in items)
							{
								result.Add(new BulletedListNode
								{
									Children = ParseEntry(item)
								});
							}
						}
						break;

					case "table":
						result.Add(ParseTable(token));
						break;
				}
			}

			return result;
		}
		private static TableNode ParseTable(JToken token)
		{
			var table = new TableNode
			{
				Children = new List<TableRowNode>()
			};

			var heads = token["colLabels"];
			if (heads != null)
			{
				var headerRow = new TableRowNode
				{
					Children = heads.Select(h => new TableCellNode
					{
						IsHeading = true,
						Children = new List<TextNode>
				{
					new TextNode { Text = h.ToString() }
				}
					}).ToList()
				};

				table.Children.Add(headerRow);
			}

			var rows = token["rows"];
			if (rows != null)
			{
				foreach (var row in rows)
				{
					var rowNode = new TableRowNode
					{
						Children = row.Select(cell => new TableCellNode
						{
							Children = new List<TextNode>
					{
						new TextNode { Text = cell.ToString() }
					}
						}).ToList()
					};

					table.Children.Add(rowNode);
				}
			}

			return table;
		}


		public static T? GetValue<T>(this JToken json, params string[] keys)
		{
			JToken currentValue = json;
			foreach (var key in keys)
			{
				if (currentValue == null)
					break;
				if (int.TryParse(key, out var index))
					currentValue = currentValue[index];
				else
					currentValue = currentValue[key];
			}
			if (currentValue == null)
				return default(T);
			var result = JsonConvert.SerializeObject(currentValue);
			return JsonConvert.DeserializeObject<T>(result);
		}
		public static object? GetValue(this JToken json, params string[] keys)
		{
			return json.GetValue<object>(keys);
		}
	}
}
