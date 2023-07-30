namespace NexignTest.Domain;

public static class RoundJudgement
{
    public static RoundWinner GetWinner(MoveKind firstMove, MoveKind secondMove)
        => (firstMove, secondMove) switch
        {
            // Первый игрок показывает "камень"
            (MoveKind.Rock, MoveKind.Rock) => RoundWinner.Draw,
            (MoveKind.Rock, MoveKind.Scissors) => RoundWinner.FirstPlayer,
            (MoveKind.Rock, MoveKind.Paper) => RoundWinner.SecondPlayer,

            // Первый игрок показывает "ножницы"
            (MoveKind.Scissors, MoveKind.Rock) => RoundWinner.SecondPlayer,
            (MoveKind.Scissors, MoveKind.Scissors) => RoundWinner.Draw,
            (MoveKind.Scissors, MoveKind.Paper) => RoundWinner.FirstPlayer,

            // Первый игрок показывает "бумагу"
            (MoveKind.Paper, MoveKind.Rock) => RoundWinner.FirstPlayer,
            (MoveKind.Paper, MoveKind.Scissors) => RoundWinner.SecondPlayer,
            (MoveKind.Paper, MoveKind.Paper) => RoundWinner.Draw,

            _ => throw new ArgumentOutOfRangeException($"Unrecognized move kind")
        };
}