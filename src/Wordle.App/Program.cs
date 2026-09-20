using System.Text;

namespace Wordle.App;

public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.GetEncoding("utf-16");
        Console.InputEncoding = Encoding.GetEncoding("utf-16");

        var config = new GameConfig();
        var controller = new GameController(config);
        var renderer = new GameRenderer();

        var gameLoop = new GameLoop(controller, renderer);

        gameLoop.Run();
    }
}