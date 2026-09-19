using System.Text;

namespace Wordle.App;

public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.GetEncoding("utf-16");
        Console.InputEncoding = Encoding.GetEncoding("utf-16");

        var config = new GameConfig();
        var words = new WordDictionary(config.WordLength);

        var controller = new GameController(config, words);
        var renderer = new GameRenderer();

        var gameLoop = new GameLoop(controller, renderer, words);

        gameLoop.Run();
    }
}