using System.Text;
using Wordle.App.ConsoleUI;

namespace Wordle.App;

public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.GetEncoding("utf-16");
        Console.InputEncoding = Encoding.GetEncoding("utf-16");

        var config = new GameConfig();
        var input = new ConsoleInput();

        var controller = new GameController(config);
        var renderer = new GameRenderer();

        var gameLoop = new GameLoop(controller, renderer, input);

        gameLoop.Run();
    }
}