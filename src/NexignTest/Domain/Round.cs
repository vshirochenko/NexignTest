namespace NexignTest.Domain;

public sealed class Round
{
    public Guid Id { get; }
    public int Number { get; }
    public Guid CreatorId { get; }
    public Guid OpponentId { get; }
    public TurnKind? CreatorTurn { get; private set; }
    public TurnKind? OpponentTurn { get; private set; }

    private Round(Guid id, int number, Guid creatorId, Guid opponentId)
    {
        Id = id;
        Number = number;
        CreatorId = creatorId;
        OpponentId = opponentId;
    }

    public static Round Create(Guid id, int number, Guid creatorId, Guid opponentId)
    {
        return new Round(id, number, creatorId, opponentId);
    }

    public void MakeCreatorTurn(TurnKind turn)
    {
        CreatorTurn = turn;
    }
    
    public void MakeOpponentTurn(TurnKind turn)
    {
        OpponentTurn = turn;
    }
}