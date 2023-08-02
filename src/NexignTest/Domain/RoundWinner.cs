namespace NexignTest.Domain;

/// <summary>
/// Победитель раунда
/// </summary>
public enum RoundWinner
{
    /// <summary>
    /// Ничья
    /// </summary>
    Draw = 0,
    /// <summary>
    /// Создатель игры
    /// </summary>
    Creator = 1,
    /// <summary>
    /// Оппонент
    /// </summary>
    Opponent = 2
}