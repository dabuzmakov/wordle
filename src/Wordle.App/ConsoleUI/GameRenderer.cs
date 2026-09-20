using Wordle.App.ConsoleUI;

namespace Wordle.App;

public class GameRenderer
{   
    public void ShowBanner(int left, int top, ConsoleColor color, string text)
    {
        Console.SetCursorPosition(left, top);
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ResetColor();
    }

    private void ShowColoredSquare(ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write($"■");
        Console.ResetColor();
    }

    public void ShowHomeScreen(int maxAttempts, int wordLength)
    {
        Console.Clear();
        Console.CursorVisible = false;

        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Yellow;

        ShowBanner(0, 0, ConsoleColor.Yellow, ConsoleBanner.LogoBanner);

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("  ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("  ПРАВИЛА ИГРЫ");

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine($"  На разгадку у вас {maxAttempts} попыток");
        Console.WriteLine($"  необходимо угадать слово из {wordLength} букв");
        Console.WriteLine($"  после каждой попытки цвет букв меняется");
        Console.WriteLine($"  вот что означают цвета:");

        Console.WriteLine();
        Console.Write("  ");
        ShowColoredSquare(ConsoleColor.Green);
        Console.Write($" - буква на месте");

        Console.WriteLine();
        Console.Write("  ");
        ShowColoredSquare(ConsoleColor.Yellow);
        Console.Write($" - буква есть в слове, но стоит в другом месте");

        Console.WriteLine();
        Console.Write("  ");
        ShowColoredSquare(ConsoleColor.Red);
        Console.Write($" - свободных вхождений буквы в слове нет");

        Console.WriteLine();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("  ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("  Выбери действие → ");
        Console.WriteLine();
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("  [1] ");

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Новая игра");

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("  [2] ");

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Выход");

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("  ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");

        Console.ResetColor();
    }
}
