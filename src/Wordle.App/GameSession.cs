namespace Wordle.App;

public class GameSession
{
    public int MaxAttempts { get; init; }
    public string Answer { get; init; }
    public string[] History { get; init; }
    public GameStatus GameStatus { get; private set; }
    public int UsedAttempts { get; private set; }

    public GameSession(WordSelector wordSelector, int maxAttempts)
    {
        if (maxAttempts <= 0)
            throw new ArgumentOutOfRangeException($"Количество попыток должно быть целым положительным числом");

        MaxAttempts = maxAttempts;
        Answer = wordSelector.GetRandomWord();
        History = new string[MaxAttempts];
        GameStatus = GameStatus.InProgress;
    }

    public void IncrementAttempts()
    {
        if (UsedAttempts >= MaxAttempts)
            throw new InvalidOperationException("Лимит попыток исчерпан.");

        UsedAttempts++;
    }
}
