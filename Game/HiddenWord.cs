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

    public bool MakeUniqueGuess(char letter)
    {
        if (IsWordGuessed || HasAlreadyGuessed(letter))
            return false;

        RevealLetter(letter);
        return true;
    }

    public bool MakeUniqueGuess(string word)
    {
        if (IsWordGuessed) return false;

        RevealWord(word);
        return true;
    }

    private bool HasAlreadyGuessed(char letter)
    {
        var guess = char.ToLower(letter);
        if (letter == Symbol) return false;

        return GuessedLetters.Contains(guess);
    }

    private void RevealLetter(char letter)
    {
        var guess = char.ToLower(letter);
        for (var i = 0; i < TargetWord.Length; i++)
            if (TargetWord[i] == guess)
                GuessedLetters[i] = guess;
    }

    private void RevealWord(string word)
    {
        if (string.IsNullOrWhiteSpace(word))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(word));

        if (word.Equals(TargetWord, StringComparison.OrdinalIgnoreCase))
            GuessedLetters = TargetWord.ToCharArray();
    } 
}