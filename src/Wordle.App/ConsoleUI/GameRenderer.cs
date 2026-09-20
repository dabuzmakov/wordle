using Wordle.App.ConsoleUI;

namespace Wordle.App;

public class GameRenderer
{
    private static readonly int _inputBoxLeft = 2;
    private static readonly int _inputBoxTop = 7;

    public int ErrorLeft => _inputBoxLeft + 30;
    public int ErrorTop => _inputBoxTop;

    public int HistoryLeft => _inputBoxLeft;
    public int HistoryTop => _inputBoxTop + 3;

    public int InputLeft => _inputBoxLeft + 16;
    public int InputTop => _inputBoxTop + 1;

    public void Clear() => Console.Clear();

    public void ShowLine(ConsoleColor color)
    {
        Console.ForegroundColor = color;

        Console.WriteLine();
        Console.WriteLine("  ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
        Console.WriteLine();
    }

    public void ShowBanner(ConsoleColor color, string text)
    {
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ResetColor();
    }

    public void ShowInputBox(int usedAttempts, int maxAttempts)
    {
        Console.CursorVisible = true;

        Console.SetCursorPosition(_inputBoxLeft, _inputBoxTop);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"ПОПЫТКА {usedAttempts + 1} ИЗ {maxAttempts}");

        Console.SetCursorPosition(_inputBoxLeft, _inputBoxTop + 1);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Введите слово → ");

        Console.ResetColor();
    }

    public void ShowErrors(List<string> messages)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        ClearErrors();

        for (var i = 0; i < messages.Count(); i++)
        {
            Console.SetCursorPosition(ErrorLeft, ErrorTop + i);
            Console.WriteLine(messages[i]);
        }

        Console.ResetColor();
    }

    public void ClearErrors()
    {
        for (var i = 0; i < 5; i++)
        {
            Console.SetCursorPosition(ErrorLeft, ErrorTop + i);
            Console.Write(new string(' ', 60));
        }
    }

    public void ShowGuessResult(GuessResult result, int attempt)
    {
        Console.SetCursorPosition(HistoryLeft, HistoryTop + (attempt - 1) * 2);
        Console.Write($"{attempt}. ");

        for (var i = 0; i < result.Colors.Length; i++)
        {
            var color = result.Colors[i] switch
            {
                LetterResult.Green => ConsoleColor.Green,
                LetterResult.Yellow => ConsoleColor.Yellow,
                LetterResult.Red => ConsoleColor.DarkGray,
                _ => ConsoleColor.White
            };

            Console.BackgroundColor = color;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.Write($" {char.ToUpper(result.Word[i])} ");

            Console.ResetColor();
        }
    }

    public void ShowHomeScreen(int maxAttempts, int wordLength)
    {
        Console.Clear();
        Console.CursorVisible = false;

        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Yellow;

        ShowBanner(ConsoleColor.Yellow, ConsoleBanner.LogoBanner);
        ShowLine(ConsoleColor.DarkGray);
        ShowRules(maxAttempts, wordLength);
        ShowLine(ConsoleColor.DarkGray);
        ShowHomeMenu();
        ShowLine(ConsoleColor.DarkGray);

        Console.ResetColor();
    }

    private void ShowColoredSquare(ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write($"■");
        Console.ResetColor();
    }

    private void ShowHomeMenu()
    {
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
    }

    private void ShowRules(int maxAttempts, int wordLength)
    {
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
    }
}
