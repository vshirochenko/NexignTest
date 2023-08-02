namespace NexignTest.Domain;

/// <summary>
/// Результат раунда
/// </summary>
public enum RoundResult
{
    /// <summary>
    /// Результат еще не готов (кто-то из игроков не походил)
    /// </summary>
    NotReady = 0,
    /// <summary>
    /// Победа!
    /// </summary>
    Won = 1,
    /// <summary>
    /// Ничья
    /// </summary>
    Draw = 2,
    /// <summary>
    /// Поражение :(
    /// </summary>
    Lost = 3
}