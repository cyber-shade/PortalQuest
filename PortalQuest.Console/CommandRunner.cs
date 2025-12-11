namespace PortalQuest.Console;
public class CommandRunner(IEnumerable<IConsoleCommand> commands)
{
	public async Task Run()
	{
		System.Console.WriteLine("PortalQuest Console Utility");
		IConsoleCommand? command = null;
		while (command == null) {
			System.Console.WriteLine("\nEnter Command");
			var commandName = System.Console.ReadLine()?.Trim();
			if (string.IsNullOrEmpty(commandName))
			{
				System.Console.WriteLine("Not Valid");
				continue;
			}
			command = commands.FirstOrDefault(x => x.Name == commandName);
				if (command == null)
			{
				System.Console.WriteLine("Command Not Found");
				continue;
			}
		}
		await command.ExecuteAsync();
	}
}
