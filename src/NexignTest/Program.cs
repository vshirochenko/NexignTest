using NexignTest.Features.Game;
using NexignTest.Features.Player;
using NexignTest.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfra();

var app = builder.Build();

app.MapPlayerEndpoints();
app.MapGameEndpoints();

app.Run();

