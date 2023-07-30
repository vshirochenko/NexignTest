using Microsoft.EntityFrameworkCore;
using NexignTest.Features.Game;

namespace NexignTest.Data;

internal static class ServiceCollectionExtensions
{
    public static void AddInfra(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("rock_scissors_paper_db"));
        services.AddScoped<IGameRepository, GameRepository>();
    }
}