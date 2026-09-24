namespace Wordle.App;

public class WordSelector
{
    private readonly Dictionary<int, string> _words;
    private readonly Random _random;

    public WordSelector(Dictionary<int, string> words, int seed)
    {
        _random = new Random(seed);
        _words = words;
    }

    public string GetRandomWord()
    {
        var key = _random.Next(_words.Count);

        return _words[key];
    }
}
