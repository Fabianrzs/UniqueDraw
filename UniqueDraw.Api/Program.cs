using UniqueDraw.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddInfrastructure(configuration);

var app = builder.Build();

app.UseInfrastructure();

app.Run();
