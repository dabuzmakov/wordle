using static System.Collections.Specialized.BitVector32;

namespace Wordle.App.ConsoleUI;

public interface IGameRenderer
{
    void ShowHomeScreen(int maxAttempts, int wordLength);
    void ShowBanner(ConsoleColor color, string banner);
    void ShowInputBox(int usedAttempts, int maxAttempts);
    void ShowErrors(List<string> messages);
    void ShowGuessResult(GuessResult guess, int usedAttempts);
    void ShowContinueMessage();
    void ClearErrors();
    void Clear();

    int InputLeft { get; }
    int InputTop { get; }
}
