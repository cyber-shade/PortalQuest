namespace PortalQuest.Console;
public interface IConsoleCommand
{
	string Name { get; }
	Task ExecuteAsync();
}
