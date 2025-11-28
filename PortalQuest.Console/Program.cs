using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PortalQuest.Application;
using PortalQuest.Console;
using PortalQuest.Console.Commands;
using PortalQuest.Persistence;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((context, services) => {
	services.AddApplicationServices();
	services.AddPersistenceServices(context.Configuration);
	services.AddTransient<IConsoleCommand, GetSpellsCommand>();
	services.AddTransient<CommandRunner>();
});
var app = builder.Build();

var runner = app.Services.GetRequiredService<CommandRunner>();
await runner.Run();
app.Dispose();