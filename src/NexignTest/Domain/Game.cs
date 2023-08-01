using System.Diagnostics.CodeAnalysis;

namespace NexignTest.Domain;

public sealed class Game
{
    private const int MaxRoundsCount = 5;

    public Guid Id { get; }
    public Guid CreatorId { get; }
    public Guid? OpponentId { get; private set; }

    private List<Round> _rounds;
    public IReadOnlyCollection<Round> Rounds => _rounds;

    private int CurrentRoundNumber => Rounds.Count;

    public Round? CurrentRound => CurrentRoundNumber >= 1 ? _rounds[^1] : null;

    private Game(Guid id, Guid creatorId)
    {
        Id = id;
        CreatorId = creatorId;
        _rounds = new List<Round>(MaxRoundsCount);
    }

    public static Game Create(Guid id, Guid creatorId)
    {
        return new Game(id, creatorId);
    }

    public static Game Load(Guid id, Guid creatorId, Guid? opponentId, List<Round> rounds)
    {
        var game = new Game(id, creatorId)
        {
            OpponentId = opponentId,
            _rounds = rounds
        };
        return game;
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
    private bool IsGameLobbyFull()
        => OpponentId is not null;

    [MemberNotNullWhen(returnValue: true, member: nameof(CurrentRound))]
    private bool IsGameStarted()
        => CurrentRound is not null;

    public void StartNewRound()
    {
        if (!IsGameLobbyFull())
            throw new InvalidOperationException("Wait for opponent!");

        if (IsGameStarted() && IsCurrentRoundActive())
            throw new InvalidOperationException("Current round is not over yet!");
        
        if (IsCurrentRoundLast())
            throw new InvalidOperationException("Current round is last!");

        _rounds.Add(Round.Create(Guid.NewGuid(), CurrentRoundNumber + 1));
    }

    private bool IsCurrentRoundLast()
    {
        return CurrentRoundNumber == MaxRoundsCount;
    }

    private bool IsCurrentRoundActive()
    {
        return CurrentRound is not null && (CurrentRound.CreatorTurn is null || CurrentRound.OpponentTurn is null);
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