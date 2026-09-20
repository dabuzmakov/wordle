namespace Wordle.App.ConsoleUI;

public class ConsoleInput : IUserInput
{
    public ConsoleKey ReadKey(bool showKey) 
        => Console.ReadKey(showKey).Key;
}
