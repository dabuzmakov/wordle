using System.Text;
using Wordle.App.ConsoleUI;

namespace Wordle.App;

public class Program
{
    public static void Main(string[] args)
    {
        int? seed = null;

        if (args.Length == 2 && args[0] == "--seed")
        {
            if (!int.TryParse(args[1], out int parsedSeed))
            {
                Console.WriteLine("Seed должен быть целым числом");
                return;
            }

            seed = parsedSeed;
        }

        Console.OutputEncoding = Encoding.GetEncoding("utf-16");
        Console.InputEncoding = Encoding.GetEncoding("utf-16");

        var config = new GameConfig { DeterminedSeed = seed };
        var input = new ConsoleInput();

        var controller = new GameController(config);
        var renderer = new GameRenderer();

        var gameLoop = new GameLoop(controller, renderer, input);

        gameLoop.Run();
    }
}