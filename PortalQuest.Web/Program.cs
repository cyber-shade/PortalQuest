using PortalQuest.Application;
using PortalQuest.Infrastructure;
using PortalQuest.Persistence;
using PortalQuest.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddWebServices(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCustomMiddlewares();

app.MapControllers();


app.Run();

