namespace Wordle.App;

public class GuessResult
{
    public string Word { get; init; }
    public LetterResult[] Colors { get; private set; }

    public GuessResult(string word)
    {
        Word = word;
        Colors = new LetterResult[word.Length];
    }

    public bool IsAllGreen()
        => Colors.All(color => color == LetterResult.Green);
}
