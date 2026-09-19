using System.Text;

namespace Wordle.App;

public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        var renderer = new GameRenderer();
        var config = new GameConfig();

        var controller = new GameController(config, renderer);
        var gameLoop = new GameLoop(controller, renderer);

        gameLoop.Run();
    }
}