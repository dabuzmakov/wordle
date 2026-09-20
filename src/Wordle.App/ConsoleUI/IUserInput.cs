namespace Wordle.App.ConsoleUI;

public interface IUserInput
{
    ConsoleKey ReadKey(bool showKey);
}
