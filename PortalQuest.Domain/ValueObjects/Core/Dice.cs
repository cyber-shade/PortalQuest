using PortalQuest.Domain.Enums.Core;

namespace PortalQuest.Domain.ValueObjects.Core
{
	public record Dice(int Number, DiceEnum Faces)
	{
		public string DisplayName => $"{Number}{Faces.ToString()}";
		private static readonly Random _random = new Random();
		public int Roll()
		{
			var res = SplitRoll().Sum();
			return res;
		}
		public int[] SplitRoll()
		{
			var res = new int[Number];
			for (int i = 0; i < Number; i++)
				res[i] = _random.Next(1, (int)Faces + 1);
			return res;	
		}
	}
}
