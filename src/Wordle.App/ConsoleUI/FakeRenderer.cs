namespace Wordle.App.ConsoleUI;

public class FakeRenderer : IGameRenderer
{
    public int InputLeft => 0;
    public int InputTop => 0;

    public void Clear() { }

    public void ClearErrors() { }

    public void ShowBanner(ConsoleColor color, string banner) { }

    public void ShowContinueMessage() { }

    public void ShowErrors(List<string> messages) { }

    public void ShowGuessResult(GuessResult guess, int usedAttempts) { }

    public void ShowHomeScreen(int maxAttempts, int wordLength) { }

    public void ShowInputBox(int usedAttempts, int maxAttempts) { }
}
