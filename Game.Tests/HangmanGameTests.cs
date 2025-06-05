namespace Game.Tests;

public class HangmanGameTests
{
    private static HangmanGame StartGame(string word = "galaxy") => new(word);

    [Fact]
    public void Constructor_ThrowsWhenWordIsNull()
    {
        Assert.Throws<ArgumentException>(() => new HangmanGame(null));
        Assert.Throws<ArgumentException>(() => new HangmanGame("   "));
    }

    [Fact]
    public void Constructor_HidesTargetWordWithSymbols()
    {
        var game = StartGame();
        Assert.Equal("______", game.HiddenWord);
    }

    [Fact]
    public void MakeUniqueGuess_ReturnsTrueWhenLetterGuessIsNewAndCorrect()
    {
        var game = StartGame();
        Assert.True(game.MakeUniqueGuess('A')); // case insensivity
        Assert.True(game.MakeUniqueGuess('x'));
        Assert.Equal("_a_ax_", game.HiddenWord);
        Assert.Equal("", game.WrongGuessesDisplay);
    }

    [Fact]
    public void MakeUniqueGuess_ReturnsTrueWhenLetterGuessIsNewAndIncorrect()
    {
        var game = StartGame();
        Assert.True(game.MakeUniqueGuess('v'));
        Assert.True(game.MakeUniqueGuess('u'));
        Assert.Equal("______", game.HiddenWord);
        Assert.Equal("v, u", game.WrongGuessesDisplay);
    }

    [Fact]
    public void MakeUniqueGuess_ReturnsFalseWhenLetterWasAlreadyGuessed()
    {
        var game = StartGame();
        Assert.True(game.MakeUniqueGuess('v')); // unique incorrect guess
        Assert.True(game.MakeUniqueGuess('y')); // unique correct guess
        Assert.False(game.MakeUniqueGuess('v'));
        Assert.False(game.MakeUniqueGuess('y'));
        Assert.Equal("_____y", game.HiddenWord);
        Assert.Equal("v", game.WrongGuessesDisplay);
    }


    [Fact]
    public void MakeUniqueGuess_ReturnsTrueWhenWordGuessIsNewAndCorrect()
    {
        var game = StartGame();
        Assert.True(game.MakeUniqueGuess('g'));
        Assert.True(game.MakeUniqueGuess("Galaxy")); // case insensitivity
        Assert.Equal("galaxy", game.HiddenWord);
        Assert.Equal("", game.WrongGuessesDisplay);
    }

    [Fact]
    public void MakeUniqueGuess_ReturnsTrueWhenWordGuessIsNewAndIncorrect()
    {
        var game = StartGame();
        Assert.True(game.MakeUniqueGuess("table"));
        Assert.Equal("______", game.HiddenWord);
        Assert.Equal("table", game.WrongGuessesDisplay);
    }

    [Fact]
    public void MakeUniqueGuess_ReturnFalseWhenWordGuessWasAlreadyGuessed()
    {
        var game = StartGame();
        Assert.True(game.MakeUniqueGuess("table"));
        Assert.False(game.MakeUniqueGuess("table"));
    }

    [Fact]
    public void MakeUniqueGuess_ReturnFalseWhenWordWasGuessed()
    {
        var game = StartGame();
        Assert.True(game.MakeUniqueGuess("table"));
        Assert.True(game.MakeUniqueGuess('g'));
        Assert.True(game.MakeUniqueGuess("galaxy"));
        Assert.False(game.MakeUniqueGuess('v')); // unique incorrect letter-guess
        Assert.False(game.MakeUniqueGuess('a')); // unique correct letter-guess
        Assert.False(game.MakeUniqueGuess("rat")); // unique incorrect word-guess
    }
}