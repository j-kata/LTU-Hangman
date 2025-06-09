
namespace Game.Tests;

public class WrongGuessesTests
{
    [Fact]
    public void ToString_ReturnsAllValuesJoinedWithComma()
    {
        var wrongGuesses = new WrongGuesses();
        wrongGuesses.Add("guess1");
        wrongGuesses.Add("guess2");
        wrongGuesses.Add("guess3");
        Assert.Equal("guess1, guess2, guess3", wrongGuesses.ToString());
    }

    [Fact]
    public void Add_AddsUniqueValueAndReturnsTrue()
    {
        var wrongGuesses = new WrongGuesses();
        Assert.True(wrongGuesses.Add("guess"));
        Assert.Equal("guess", wrongGuesses.ToString());
    }

    [Fact]
    public void Add_ReturnsFalseIfValueWasAlreadyAdded()
    {
        var wrongGuesses = new WrongGuesses();
        wrongGuesses.Add("guess");
        Assert.False(wrongGuesses.Add("guess"));
        Assert.Equal("guess", wrongGuesses.ToString());
    }
}