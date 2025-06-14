namespace Game.Tests;

public class HiddenWordTests
{
    private static HiddenWord GalaxyWord(string word = "galaxy") => new(word);

    [Fact]
    public void Constructor_ThrowsWhenWordIsNull()
    {
        Assert.Throws<ArgumentException>(() => new HiddenWord(null));
        Assert.Throws<ArgumentException>(() => new HiddenWord("   "));
    }

    [Fact]
    public void Constructor_HidesTargetWord_WithSymbols()
    {
        var word = new HiddenWord("galaxy", '#');
        Assert.Equal("######", word.GuessedWord);
    }


    [Fact]
    public void Constructor_HidesTargetWord_WithDefaultSymbols()
    {
        var word = new HiddenWord("galaxy");
        Assert.Equal("______", word.GuessedWord);
    }

    [Fact]
    public void MakeUniqueGuess_ReturnsTrue_WhenLetterGuessIsNew()
    {
        var word = GalaxyWord();
        Assert.True(word.TryMakeUniqueGuess('A', out bool _isCorrect)); // correct guess (case insensivity)
        Assert.True(word.TryMakeUniqueGuess('v', out _isCorrect)); // incorrect guess
    }

    [Fact]
    public void MakeUniqueGuess_RevealsLetter_WhenGuessedCorrectly()
    {
        var word = GalaxyWord();
        Assert.True(word.TryMakeUniqueGuess('a', out bool _isCorrect));
        Assert.Equal("_a_a__", word.GuessedWord);
    }

    [Fact]
    public void MakeUniqueGuess_OutsExpectedResult_AfterLetterGuess()
    {
        var word = GalaxyWord();
        Assert.True(word.TryMakeUniqueGuess('A', out bool isCorrect));
        Assert.True(isCorrect);
        Assert.True(word.TryMakeUniqueGuess('v', out isCorrect));
        Assert.False(isCorrect);
    }

    [Fact]
    public void MakeUniqueGuess_ReturnsFalse_WhenCorrectLetterWasAlreadyGuessed()
    {
        var word = GalaxyWord();
        Assert.True(word.TryMakeUniqueGuess('y', out bool _isCorrect));
        Assert.False(word.TryMakeUniqueGuess('y', out _isCorrect));
        Assert.Equal("_____y", word.GuessedWord);
    }

    [Fact]
    public void MakeUniqueGuess_ReturnsTrue_WhenWordGuessIsNew()
    {
        var word = GalaxyWord();
        Assert.True(word.TryMakeUniqueGuess("apples", out bool _isCorrect)); // incorrect guess
        Assert.True(word.TryMakeUniqueGuess("Galaxy", out _isCorrect)); // correct guess case insensitivity
    }

    [Fact]
    public void MakeUniqueGuess_RevealsWord_WhenGuessedCorrectly()
    {
        var word = GalaxyWord();
        Assert.True(word.TryMakeUniqueGuess("galaxy", out bool _isCorrect));
        Assert.Equal("galaxy", word.GuessedWord);
    }

    [Fact]
    public void MakeUniqueGuess_OutsExpectedResult_AfterWordGuess()
    {
        var word = GalaxyWord();
        Assert.True(word.TryMakeUniqueGuess("apples", out bool isCorrect));
        Assert.False(isCorrect);
        Assert.True(word.TryMakeUniqueGuess("galaxy", out isCorrect));
        Assert.True(isCorrect);
    }

    [Fact]
    public void MakeUniqueGuess_AlwaysReturnsFalse_IfWordWasAlreadyGuessed()
    {
        var word = GalaxyWord();
        word.TryMakeUniqueGuess("galaxy", out bool _isCorrect);
        Assert.False(word.TryMakeUniqueGuess('y', out _isCorrect)); // correct guess
        Assert.False(word.TryMakeUniqueGuess("galaxy", out _isCorrect)); // correct guess
    }
}