using Game.Abstractions;
using Moq;

namespace Game.Tests;

public class HandmanManagerTests
{
    private const string Word = "galaxy";
    private const int Attempts = 3;
    private readonly Mock<IUI> _ui = new();
    private readonly HandmanManager _manager;

    public HandmanManagerTests()
    {
        _manager = new(Word, Attempts, _ui.Object);
    }

    [Fact]
    public void RunRound_PrintsPrompt_BeforeUserInput()
    {
        _ui.SetupSequence(x => x.ReadLine())
            .Returns("g")
            .Returns("a")
            .Returns("c");

        _manager.RunRound();
        _ui.Verify(x => x.Print(It.Is<string>(s => s.Equals("Your guess: "))), Times.Exactly(3));
    }

    [Fact]
    public void RunRound_ReadsUserInput_UntilGameIsOver()
    {
        _ui.SetupSequence(x => x.ReadLine())
            .Returns("g")
            .Returns("galaxy");

        _manager.RunRound();
        _ui.Verify(x => x.ReadLine(), Times.Exactly(2));
    }

    [Fact]
    public void RunRound_PrintsPartialWord_BeforeNewGuess()
    {
        _ui.SetupSequence(x => x.ReadLine())
            .Returns("g")
            .Returns("a")
            .Returns("y");

        _manager.RunRound();
        _ui.Verify(x => x.PrintLine(It.Is<string>(s => s.Contains("The word: ______"))), Times.Exactly(1));
        _ui.Verify(x => x.PrintLine(It.Is<string>(s => s.Contains("The word: g_____"))), Times.Exactly(1));
        _ui.Verify(x => x.PrintLine(It.Is<string>(s => s.Contains("The word: ga_a__"))), Times.Exactly(1));
    }

    [Fact]
    public void RunRound_PrintsAttemptsLeft_BeforeNewGuess()
    {
        _ui.SetupSequence(x => x.ReadLine())
            .Returns("g")
            .Returns("a")
            .Returns("a")
            .Returns("l");

        _manager.RunRound();
        _ui.Verify(x => x.PrintLine(It.Is<string>(s => s.Contains("Attempts left: 3"))), Times.Exactly(1));
        _ui.Verify(x => x.PrintLine(It.Is<string>(s => s.Contains("Attempts left: 2"))), Times.Exactly(1));
        _ui.Verify(x => x.PrintLine(It.Is<string>(s => s.Contains("Attempts left: 1"))), Times.Exactly(2));
    }

    [Fact]
    public void RunRound_PrintsWrongGuesses_BeforeNewGuess()
    {
        _ui.SetupSequence(x => x.ReadLine())
            .Returns("j")
            .Returns("j")
            .Returns("g")
            .Returns("v");

        _manager.RunRound();
        _ui.Verify(x => x.PrintLine(It.Is<string>(s => s.Contains("Wrong guesses: j"))), Times.Exactly(3));
    }

    [Fact]
    public void RunRound_DoesNotPrintWrongGuesses_IfEmpty()
    {
        _ui.SetupSequence(x => x.ReadLine())
            .Returns("g")
            .Returns("a")
            .Returns("a")
            .Returns("l");

        _manager.RunRound();
        _ui.Verify(x => x.PrintLine(It.Is<string>(s => s.Contains("Wrong guesses:"))), Times.Never());
    }

    [Fact]
    public void RunRound_DoesNotPrintLog_IfGameIsOver()
    {
        _ui.SetupSequence(x => x.ReadLine())
            .Returns("g")
            .Returns("galaxy");

        _manager.RunRound();
        _ui.Verify(x => x.PrintLine(It.Is<string>(s => s.Contains("The word:"))), Times.Exactly(2));
        _ui.Verify(x => x.PrintLine(It.Is<string>(s => s.Contains("Attempts left:"))), Times.Exactly(2));
    }

    [Fact]
    public void RunRound_PrintsYouWon_IfUserHasWon()
    {
        _ui.SetupSequence(x => x.ReadLine())
            .Returns("g")
            .Returns("a")
            .Returns("galaxy");

        _manager.RunRound();
        _ui.Verify(x => x.PrintLine(It.Is<string>(s => s.Contains("You won!"))));
    }

    [Fact]
    public void RunRound_PrintsYouLost_IfUserHasLost()
    {
        _ui.SetupSequence(x => x.ReadLine())
            .Returns("g")
            .Returns("a")
            .Returns("parrot");

        _manager.RunRound();
        _ui.Verify(x => x.PrintLine(It.Is<string>(s => s.Contains("You lost!"))));
    }

    [Fact]
    public void RunRound_PrintsGameOver_WhenGameIsOver()
    {
        _ui.SetupSequence(x => x.ReadLine())
            .Returns("g")
            .Returns("a")
            .Returns("c");

        _manager.RunRound();
        _ui.Verify(x => x.PrintLine(It.Is<string>(s => s.Contains("Game over!"))));
    }

    [Fact]
    public void RunRound_PrintsTargetWord_WhenGameIsOver()
    {
        _ui.SetupSequence(x => x.ReadLine())
            .Returns("g")
            .Returns("a")
            .Returns("c");

        _manager.RunRound();
        _ui.Verify(x => x.PrintLine(It.Is<string>(s => s.Contains("The word was: galaxy"))));
    }

    [Fact]
    public void RunRound_PrintsResults_OnlyOnce()
    {
        _ui.SetupSequence(x => x.ReadLine())
            .Returns("g")
            .Returns("a")
            .Returns("c");

        _manager.RunRound();
        _ui.Verify(x => x.PrintLine(It.Is<string>(s => s.Contains("Game over!"))), Times.Exactly(1));
        _ui.Verify(x => x.PrintLine(It.Is<string>(s => s.Contains("The word was: galaxy"))), Times.Exactly(1));
    }
}