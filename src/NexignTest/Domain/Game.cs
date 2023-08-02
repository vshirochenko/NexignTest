using System.Diagnostics.CodeAnalysis;

namespace NexignTest.Domain;

public sealed class Game : IAggregate
{
    public Guid Id { get; }
    public Guid CreatorId { get; }
    public Guid? OpponentId { get; private set; }
    public int MaxRoundsCount { get; }

    // Need to handle these events later :)
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

    private List<Round> _rounds;
    public IReadOnlyCollection<Round> Rounds => _rounds;

    private int CurrentRoundNumber => Rounds.Count;

    public Round? CurrentRound => CurrentRoundNumber >= 1 ? _rounds[^1] : null;
    
    public Guid? WinnerId { get; private set; }
    
    public bool IsDraw { get; private set; }

    public bool IsOver => WinnerId is not null || IsDraw;

    private Game(Guid id, Guid creatorId, int maxRoundsCount)
    {
        Id = id;
        CreatorId = creatorId;
        MaxRoundsCount = maxRoundsCount;
        _rounds = new List<Round>(maxRoundsCount);
    }

    public static Game Create(Guid id, Guid creatorId, int maxRoundsCount = 5)
    {
        if (maxRoundsCount <= 0)
            throw new InvalidOperationException("We need at least 1 round, bro :)");
        
        return new Game(id, creatorId, maxRoundsCount);
    }

    public static Game Load(Guid id, Guid creatorId, int maxRoundsCount, Guid? opponentId, List<Round> rounds, Guid? winnerId)
    {
        var game = new Game(id, creatorId, maxRoundsCount)
        {
            OpponentId = opponentId,
            _rounds = rounds,
            WinnerId = winnerId
        };
        return game;
    }

    public void Join(Guid opponentId)
    {
        ThrowIfGameIsOver();
        
        if (opponentId == CreatorId)
            throw new InvalidOperationException("You are creator! Wait for another player :)");
        if (IsGameLobbyFull())
            throw new InvalidOperationException("Game lobby is full! Try another lobby :)");
        OpponentId = opponentId;
    }

    public void StartNewRound()
    {
        ThrowIfGameIsOver();
        
        if (!IsGameLobbyFull())
            throw new InvalidOperationException("Wait for opponent!");

        if (IsGameStarted() && IsCurrentRoundActive())
            throw new InvalidOperationException("Current round is not over yet!");

        _rounds.Add(Round.Create(Guid.NewGuid(), CurrentRoundNumber + 1));
    }

    public RoundResult MakeTurn(Guid playerId, TurnKind turn)
    {
        ThrowIfGameIsOver();
        
        if (!IsGameStarted())
            throw new InvalidOperationException("Cannot make turn because of game not started yet!");

        if (playerId == CreatorId)
        {
            if (CurrentRound.HasCreatorMadeTurn())
                throw new InvalidOperationException("You've made turn already! Please wait for your opponent :)");
            var result = CurrentRound.MakeCreatorTurn(turn);
            if (CanGetWinner())
            {
                WinnerId = GetGameWinnerId();
                _domainEvents.Add(new GameIsOverEvent(Id, WinnerId));
            }
            else if (IsCurrentRoundLastAndOver())
            {
                IsDraw = true;
                _domainEvents.Add(new GameIsOverEvent(Id, null));
            }

            return result;
        }

        if (playerId == OpponentId)
        {
            if (CurrentRound.HasOpponentMadeTurn())
                throw new InvalidOperationException("You've made turn already! Please wait for your opponent :)");

            var result = CurrentRound.MakeOpponentTurn(turn);
            if (CanGetWinner())
            {
                WinnerId = GetGameWinnerId();
                _domainEvents.Add(new GameIsOverEvent(Id, WinnerId));
            }
            else if (IsCurrentRoundLastAndOver())
            {
                IsDraw = true;
                _domainEvents.Add(new GameIsOverEvent(Id, null));
            }

            return result;
        }

        throw new InvalidOperationException("Unknown player detected!");
    }

    [MemberNotNullWhen(returnValue: true, member: nameof(OpponentId))]
    private bool IsGameLobbyFull()
        => OpponentId is not null;

    [MemberNotNullWhen(returnValue: true, member: nameof(CurrentRound))]
    private bool IsGameStarted()
        => CurrentRound is not null;

    [MemberNotNullWhen(returnValue: true, member: nameof(CurrentRound))]
    private bool IsCurrentRoundLast()
    {
        return CurrentRoundNumber == MaxRoundsCount;
    }

    private bool IsCurrentRoundLastAndOver()
    {
        return IsCurrentRoundLast() && CurrentRound.IsRoundOver();
    }

    private bool IsCurrentRoundActive()
    {
        return CurrentRound is not null && (!CurrentRound.HasCreatorMadeTurn() || !CurrentRound.HasOpponentMadeTurn());
    }

    private bool CanGetWinner()
    {
        var draws = _rounds.Count(x => x.Winner == RoundWinner.Draw);
        var creatorWins = _rounds.Count(x => x.Winner == RoundWinner.Creator);
        var opponentWins = _rounds.Count(x => x.Winner == RoundWinner.Opponent);

        return Math.Abs(creatorWins - opponentWins) > (MaxRoundsCount - draws) / 2;
    }
    
    private void ThrowIfGameIsOver()
    {
        if (WinnerId is not null || IsDraw)
            throw new InvalidOperationException("Game is over :(");
    }

    private Guid GetGameWinnerId()
    {
        var creatorWins = _rounds.Count(x => x.Winner == RoundWinner.Creator);
        var opponentWins = _rounds.Count(x => x.Winner == RoundWinner.Opponent);

        if (creatorWins > opponentWins)
            return CreatorId;
        if (creatorWins < opponentWins)
            return OpponentId!.Value;
        throw new InvalidOperationException("Draw - is not possible result!");
    }
}