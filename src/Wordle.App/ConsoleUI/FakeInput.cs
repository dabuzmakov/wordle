namespace Wordle.App.ConsoleUI;

public class FakeInput : IUserInput
{
    private readonly Queue<ConsoleKey> _keys;

    public FakeInput(params ConsoleKey[] keys)
    {
        _keys = new Queue<ConsoleKey>(keys);
    }

    public ConsoleKey ReadKey(bool showKey) 
        => _keys.Dequeue();
}
