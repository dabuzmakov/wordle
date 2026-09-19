namespace Wordle.App;

public class Guess
{
    private readonly HashSet<char> _correctСharacters = new() 
    {
        'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 
        'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 
        'х', 'ц', 'ч', 'ш','щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я'
    };

    private readonly int _wordLength;
    public string Word { get; private set; } = string.Empty;

    public Guess(string word, int wordLength)
    {
        ChangeWord(word);
        _wordLength = wordLength;
    }

    public bool IsValid(out List<string> messages)
    {
        var isValid = true;
        messages = new List<string>();

        if (string.IsNullOrEmpty(Word))
        {
            messages.Add($"Слово не может быть пустым ((");
            return false;
        }

        if (Word.Length != _wordLength)
        {
            messages.Add($"Длина слова должна быть {_wordLength} символов,");
            isValid = false;
        }

        foreach (var sym in Word)
        {
            if (!_correctСharacters.Contains(sym))
            {
                messages.Add($"Слово должно содерать буквы русского алфавита");
                isValid = false;
                break;
            }
        }

        return isValid;
    }

    public void ChangeWord(string word)
    {
        if (!string.IsNullOrEmpty(word))
            Word = word.ToLowerInvariant().Trim();
        else Word = string.Empty;
    }
}
