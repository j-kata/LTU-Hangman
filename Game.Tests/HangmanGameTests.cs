namespace Game.Tests;

public class HangmanGameTests
{
    private const string Word = "galaxy";
    private const int Attempts = 3;
    private readonly HangmanGame _game;

    public HangmanGameTests()
    {
        _game = new HangmanGame(Word, Attempts);
    }

    [Fact]
    public void AttemptLeft_ReturnsExpectedResult()
    {
        Assert.Equal(Attempts, _game.AttemptsLeft);

        _game.MakeGuess("c");
        Assert.Equal(Attempts - 1, _game.AttemptsLeft);
    }

    [Fact]
    public void TargetWord_ReturnsExpectedResult()
    {
        Assert.Equal(Word, _game.TargetWord);
        _game.MakeGuess("c");
        Assert.Equal(Word, _game.TargetWord);
    }

    [Fact]
    public void GuessedWord_ReturnsExpectedResult()
    {
        Assert.Equal("______", _game.GuessedWord);
        _game.MakeGuess("a");
        Assert.Equal("_a_a__", _game.GuessedWord);
    }

    [Fact]
    public void WrongGuessesList_ReturnsExpectedResult()
    {
        Assert.Equal("", _game.WrongGuessesList);
        _game.MakeGuess("b");
        _game.MakeGuess("b");
        _game.MakeGuess("f");
        _game.MakeGuess("a");
        Assert.Equal("b, f", _game.WrongGuessesList);
    }

    [Fact]
    public void MakeGuess_ReducesAttempts_IfLetterIsCorrect()
    {
        _game.MakeGuess("a");
        Assert.Equal(Attempts - 1, _game.AttemptsLeft);
    }

    [Fact]
    public void MakeGuess_DoesNotReduceAttempts_IfCorrectLetterAlreadyGuessed()
    {
        _game.MakeGuess("a");
        Assert.Equal(Attempts - 1, _game.AttemptsLeft);
        _game.MakeGuess("a");
        Assert.Equal(Attempts - 1, _game.AttemptsLeft);
    }

    [Fact]
    public void MakeGuess_ReducesAttemptsAsExpected_IfCorrectLetterIsInUppercase()
    {
        _game.MakeGuess("A");
        Assert.Equal(Attempts - 1, _game.AttemptsLeft);
        _game.MakeGuess("a");
        Assert.Equal(Attempts - 1, _game.AttemptsLeft);
    }

    [Fact]
    public void MakeGuess_ReducesAttempts_IfLetterIsWrong()
    {
        _game.MakeGuess("b");
        Assert.Equal(Attempts - 1, _game.AttemptsLeft);
    }

    [Fact]
    public void MakeGuess_DoesNotReduceAttempts_IfWrongLetterAlreadyGuessed()
    {
        _game.MakeGuess("b");
        Assert.Equal(Attempts - 1, _game.AttemptsLeft);
        _game.MakeGuess("b");
        Assert.Equal(Attempts - 1, _game.AttemptsLeft);
    }

    [Fact]
    public void MakeGuess_ReducesAttemptsAsExpected_IfWrongLetterIsInUppercase()
    {
        _game.MakeGuess("B");
        Assert.Equal(Attempts - 2, _game.AttemptsLeft);
        _game.MakeGuess("b");
        Assert.Equal(Attempts - 2, _game.AttemptsLeft);
    }

    [Fact]
    public void MakeGuess_ReducesAttempts_IfWordIsCorrect()
    {
        _game.MakeGuess(Word);
        Assert.Equal(Attempts - 1, _game.AttemptsLeft);
    }

    [Fact]
    public void MakeGuess_ReducesAttempts_IfCorrectWordIsInUppercase()
    {
        _game.MakeGuess("GALAXY");
        Assert.Equal(Attempts - 2, _game.AttemptsLeft);
    }

    [Fact]
    public void MakeGuess_ReducesAttempts_IfWordIsWrong()
    {
        _game.MakeGuess("board");
        Assert.Equal(Attempts - 1, _game.AttemptsLeft);
    }

    [Fact]
    public void MakeGuess_DoesNotReduceAttempts_IfWrongWordAlreadyGuessed()
    {
        _game.MakeGuess("board");
        Assert.Equal(Attempts - 1, _game.AttemptsLeft);
        _game.MakeGuess("board");
        Assert.Equal(Attempts - 1, _game.AttemptsLeft);
    }

    [Fact]
    public void MakeGuess_ReducesAttemptsAsExpected_IfWrongWordIsInUppercase()
    {
        _game.MakeGuess("BOARD");
        Assert.Equal(Attempts - 1, _game.AttemptsLeft);
        _game.MakeGuess("board");
        Assert.Equal(Attempts - 1, _game.AttemptsLeft);
    }

    [Fact]
    public void MakeGuess_DoesNotReduceAttempts_AfterGameIsOver()
    {
        _game.MakeGuess("galaxy");
        Assert.Equal(Attempts - 2, _game.AttemptsLeft);
        _game.MakeGuess("c");
        Assert.Equal(Attempts - 2, _game.AttemptsLeft);
        Assert.True(_game.IsGameOver());
    }

    [Fact]
    public void MakeGuess_DoesNotReduceAttempts_IfWordIsEmpty()
    {
        _game.MakeGuess("b");
        Assert.Equal(Attempts - 1, _game.AttemptsLeft);
        _game.MakeGuess("");
        Assert.Equal(Attempts - 1, _game.AttemptsLeft);
        _game.MakeGuess(null);
        Assert.Equal(Attempts - 1, _game.AttemptsLeft);
    }

    [Fact]
    public void IsWon_ReturnsExpectedResult()
    {
        _game.MakeGuess("b");
        Assert.False(_game.IsWon());
        _game.MakeGuess("galaxy");
        Assert.True(_game.IsWon());
    }

    [Fact]
    public void IsGameOver_ReturnsTrue_IfNoAttemptsLeft()
    {
        _game.MakeGuess("b");
        _game.MakeGuess("a");
        _game.MakeGuess("c");
        Assert.True(_game.IsGameOver());
    }


    [Fact]
    public void IsGameOver_ReturnsTrue_IfWordWasGuessed()
    {
        _game.MakeGuess("galaxy");
        Assert.True(_game.IsGameOver());
    }


    [Fact]
    public void IsGameOver_ReturnsFalse_WhenGameIsOn()
    {
        _game.MakeGuess("g");
        Assert.False(_game.IsGameOver());
        _game.MakeGuess("table");
        Assert.False(_game.IsGameOver());
    }
}