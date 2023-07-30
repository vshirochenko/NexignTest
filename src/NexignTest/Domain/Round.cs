namespace NexignTest.Domain;

public sealed class Round
{
    public Guid Id { get; }
    public int Number { get; }

    private Round(Guid id, int number)
    {
        Id = id;
        Number = number;
    }

    public static Round Create(Guid id, int number)
    {
        return new Round(id, number);
    }
}