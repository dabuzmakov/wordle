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
    private string _word = string.Empty;

    public Guess(string word, int wordLength)
    {
        ChangeWord(word);
        _wordLength = wordLength;
    }

    public bool IsValid(out List<string> messages)
    {
        var isValid = true;
        messages = new List<string>();

        if (string.IsNullOrEmpty(_word))
        {
            messages.Add($"Слово не может быть пустым ((");
            return false;
        }

        if (_word.Length != _wordLength)
        {
            messages.Add($"Длина слова должна быть {_wordLength} символов,");
            isValid = false;
        }

        foreach (var sym in _word)
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
            _word = word.ToLower().Trim();
    }
}
