namespace Wordle.App.ConsoleUI;

public interface IGameRenderer
{
    void ShowGuessResult(GuessResult guess, int usedAttempts);
    void ShowHomeScreen(int maxAttempts, int wordLength);
    void ShowInputBox(int usedAttempts, int maxAttempts);
    void ShowBanner(ConsoleColor color, string banner);
    void ClearInput(int left, int top, int length);
    void SetCursorPosition(int left, int top);
    void ShowErrors(List<string> messages);
    void ShowContinueMessage();
    void ClearErrors();
    void ShowCursor();
    void HideCursor();
    void Clear();

    int InputLeft { get; }
    int InputTop { get; }
}
