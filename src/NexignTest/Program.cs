using NexignTest.Features.Game;
using NexignTest.Features.User;
using NexignTest.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfra();

var app = builder.Build();

app.MapUserEndpoints();
app.MapGameEndpoints();

app.Run();

