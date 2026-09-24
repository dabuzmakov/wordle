namespace Wordle.App.ConsoleUI;

public class FakeInput : IUserInput
{
    private readonly Queue<ConsoleKey> _keys;
    private readonly Queue<string> _lines;

    public FakeInput(ConsoleKey[]? keys = null, string[]? lines = null)
    {
        _keys = new Queue<ConsoleKey>(keys ?? []);
        _lines = new Queue<string>(lines ?? []);
    }

    public ConsoleKey ReadKey(bool showKey)
        => _keys.Dequeue();

    public string? ReadLine()
        => _lines.Dequeue();
}
