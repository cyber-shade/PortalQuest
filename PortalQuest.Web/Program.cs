using PortalQuest.Application;
using PortalQuest.Infrastructure;
using PortalQuest.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddPersistenceServices(builder.Configuration);

var app = builder.Build();



app.Run();

