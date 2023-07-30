namespace NexignTest.Features.Game;

public interface IGameRepository
{
    Task<Domain.Game> Load(Guid id, CancellationToken stoppingToken = default);

    Task Save(Domain.Game game, CancellationToken stoppingToken = default);
}