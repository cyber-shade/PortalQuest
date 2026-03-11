namespace PortalQuest.Console;
public class CommandRunner(IEnumerable<IConsoleCommand> commands)
{
	public async Task Run()
	{
		System.Console.WriteLine("PortalQuest Console Utility");
		System.Console.WriteLine("available commands :");
		foreach (var cm in commands)
			System.Console.WriteLine(cm.Name);
		IConsoleCommand? command = null;
		while (command == null) {
			System.Console.WriteLine("\nEnter command");
			var commandName = System.Console.ReadLine()?.Trim();
			if (string.IsNullOrEmpty(commandName))
			{
				System.Console.WriteLine("Not valid");
				continue;
			}
			command = commands.FirstOrDefault(x => x.Name == commandName);
			if (command == null)
			{
				System.Console.WriteLine("Command not found");
				continue;
			}
		}
		await command.ExecuteAsync();
	}
}
