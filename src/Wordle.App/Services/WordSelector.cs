namespace Wordle.App;

public class WordSelector
{
    private readonly Dictionary<int, string> _words;
    private readonly int _seed;

    public WordSelector(Dictionary<int, string> words, int seed)
    {
        if (words == null || words.Count == 0)
            throw new ArgumentException("Словарь не может быть пустым.");

        _words = words;
        _seed = seed;
    }

    public string GetRandomWord()
    {
        var random = new Random(_seed);
        var key = random.Next(_words.Count);

        return _words[key];
    }
}
