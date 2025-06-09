namespace Game;

internal class HiddenWord
{
    private char[] GuessedLetters { get; set; }
    private string TargetWord { get; init; }
    private char Symbol { get; init; }

    public string GuessedWord => string.Join("", GuessedLetters);
    public bool IsWordGuessed => TargetWord.SequenceEqual(GuessedLetters);

    private static char[] HideWord(string word, char symbol) => new string(symbol, word.Length).ToCharArray();

    public HiddenWord(string targetWord, char symbol = '_')
    {
        if (string.IsNullOrWhiteSpace(targetWord))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(targetWord));

        TargetWord = targetWord.ToLowerInvariant();
        Symbol = symbol;
        GuessedLetters = HideWord(targetWord, symbol);
    }

    public bool TryMakeUniqueGuess(char letter, out bool wasCorrectGuess)
    {
        wasCorrectGuess = false;

        if (IsWordGuessed || HasAlreadyGuessed(letter))
            return false;

        wasCorrectGuess = RevealLetter(letter);
        return true;
    }

    public bool TryMakeUniqueGuess(string word, out bool wasCorrectGuess)
    {
        wasCorrectGuess = false;

        if (IsWordGuessed) return false;

        wasCorrectGuess = RevealWord(word);
        return true;
    }

    private bool HasAlreadyGuessed(char letter)
    {
        var guess = char.ToLowerInvariant(letter);
        if (letter == Symbol) return false;

        return GuessedLetters.Contains(guess);
    }

    private bool RevealLetter(char letter)
    {
        var guess = char.ToLowerInvariant(letter);

        if (!TargetWord.Contains(guess)) return false;

        for (var i = 0; i < TargetWord.Length; i++)
            if (TargetWord[i] == guess)
                GuessedLetters[i] = guess;
        return true;
    }

    private bool RevealWord(string word)
    {
        if (!word.Equals(TargetWord, StringComparison.OrdinalIgnoreCase)) return false;

        GuessedLetters = TargetWord.ToCharArray();
        return true;
    } 
}