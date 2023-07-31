using System.Diagnostics.CodeAnalysis;

namespace NexignTest.Domain;

public sealed class Game
{
    private const int MaxRoundsCount = 5;

    public Guid Id { get; }
    public Guid CreatorId { get; }
    public Guid? OpponentId { get; private set; }
    public List<Round> Rounds { get; }

    private int CurrentRoundNumber => Rounds.Count;

    public Round? CurrentRound => CurrentRoundNumber >= 1 ? Rounds[^1] : null;

    private Game(Guid id, Guid creatorId)
    {
        Id = id;
        CreatorId = creatorId;
        Rounds = new List<Round>(MaxRoundsCount);
    }

    public static Game Create(Guid id, Guid creatorId)
    {
        return new Game(id, creatorId);
    }

    public void Join(Guid opponentId)
    {
        if (opponentId == CreatorId)
            throw new InvalidOperationException("You are creator! Wait for another player :)");
        if (IsGameLobbyFull())
            throw new InvalidOperationException("Game lobby is full! Try another lobby :)");
        OpponentId = opponentId;
    }

    [MemberNotNullWhen(returnValue: true, member: nameof(OpponentId))]
    public bool IsGameLobbyFull()
        => OpponentId is not null;

    [MemberNotNullWhen(returnValue: true, member: nameof(CurrentRound))]
    public bool IsGameStarted()
        => CurrentRound is not null;

    public void StartNewRound()
    {
        if (!IsGameLobbyFull())
            throw new InvalidOperationException("Wait for opponent!");

        if (CurrentRoundNumber == MaxRoundsCount)
            throw new InvalidOperationException("Current round is last!");

        // TODO: надо проверить, что перед созданием нового раунда ОБА игрока сделали ход

        Rounds.Add(Round.Create(Guid.NewGuid(), CurrentRoundNumber + 1, CreatorId, OpponentId.Value));
    }

    public void MakeTurn(Guid playerId, TurnKind turn)
    {
        if (!IsGameStarted())
            throw new InvalidOperationException("Cannot make turn because of game not started yet!");

        if (playerId == CreatorId)
            CurrentRound.MakeCreatorTurn(turn);
        else if (playerId == OpponentId)
            CurrentRound.MakeOpponentTurn(turn);
        else
            throw new InvalidOperationException("Unknown player detected!");
    }
}