namespace PortalQuest.Console.Commands
{
	public class GetSpellsCommand : IConsoleCommand
	{
		public string Name => "get-spells";

		public Task ExecuteAsync()
		{
			return Task.CompletedTask;
		}
	}
}
