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
    public void Constructor_HidesTargetWordWithSymbols()
    {
        var word = new HiddenWord("galaxy", '#');
        Assert.Equal("######", word.GuessedWord);
    }


    [Fact]
    public void Constructor_HidesTargetWordWithDefaultSymbols()
    {
        var word = new HiddenWord("galaxy");
        Assert.Equal("______", word.GuessedWord);
    }

    [Fact]
    public void MakeUniqueGuess_ReturnsTrueWhenLetterGuessIsNew()
    {
        var word = GalaxyWord();
        Assert.True(word.MakeUniqueGuess('A')); // correct guess (case insensivity)
        Assert.True(word.MakeUniqueGuess('v')); // incorrect guess
    }

    [Fact]
    public void MakeUniqueGuess_RevealsLetterWhenGuessedCorrectly()
    {
        var word = GalaxyWord();
        Assert.True(word.MakeUniqueGuess('a'));
        Assert.Equal("_a_a__", word.GuessedWord);
    }

    [Fact]
    public void MakeUniqueGuess_ReturnsFalseWhenCorrectLetterWasAlreadyGuessed()
    {
        var word = GalaxyWord();
        Assert.True(word.MakeUniqueGuess('y'));
        Assert.False(word.MakeUniqueGuess('y'));
        Assert.Equal("_____y", word.GuessedWord);
    }

    [Fact]
    public void MakeUniqueGuess_ReturnsTrueWhenWordGuessIsNew()
    {
        var word = GalaxyWord();
        Assert.True(word.MakeUniqueGuess("apples")); // incorrect guess
        Assert.True(word.MakeUniqueGuess("Galaxy")); // correct guess case insensitivity
    }

    [Fact]
    public void MakeUniqueGuess_RevealsWordWhenGuessedCorrectly()
    {
        var word = GalaxyWord();
        Assert.True(word.MakeUniqueGuess("galaxy"));
        Assert.Equal("galaxy", word.GuessedWord);
    }

    [Fact]
    public void MakeUniqueGuess_AlwaysReturnsFalseIfWordWasAlreadyGuesses()
    {
        var word = GalaxyWord();
        word.MakeUniqueGuess("galaxy");
        Assert.False(word.MakeUniqueGuess('y')); // correct guess
        Assert.False(word.MakeUniqueGuess("galaxy")); // correct guess
    }

    [Fact]
    public void MakeUniqueGuess_ThrowsWhenTheWordIsNull()
    {
        var word = GalaxyWord();
        Assert.Throws<ArgumentException>(() => word.MakeUniqueGuess(null));
        Assert.Throws<ArgumentException>(() => word.MakeUniqueGuess("  "));
    }
}