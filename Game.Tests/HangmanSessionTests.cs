using Moq;
using Game.Abstractions;

namespace Game.Tests;

public class HangmanSessionTests
{
    private readonly Mock<IUI> _ui = new();
    private readonly Mock<IManager> _manager = new();
    private readonly Func<string, int, IUI, IManager> _factory;
    private readonly Mock<IRandomizer<string>> _randomizer = new();
    private readonly HangmanSession _session;
    private int _countRounds;

    public HangmanSessionTests()
    {
        _countRounds = 0;
        _randomizer.Setup(r => r.GetNextItem()).Returns("galaxy");
        _factory = (words, attempts, ui) => _manager.Object;
        _session = new HangmanSession(_randomizer.Object, _factory, _ui.Object);
        _manager.Setup(m => m.RunRound()).Callback(() => _countRounds++);
    }

    [Fact]
    public void Run_AlwaysRunsAtLeastOnce()
    {
        _ui.Setup(x => x.ReadLine()).Returns("n");
        _session.Run();

        Assert.Equal(1, _countRounds);
    }

    [Fact]
    public void Run_PrintsPromt_AfterRound()
    {
        _ui.Setup(x => x.ReadLine()).Returns("n");
        _session.Run();

        _ui.Verify(x => x.Print(It.Is<string>(s => s.Contains("Do you want to play again? (y/n) "))));
    }

    [Fact]
    public void Run_ContinuesGame_IfUserSaysYes()
    {
        _ui.SetupSequence(x => x.ReadLine()).Returns("y").Returns("n");
        _session.Run();

        Assert.Equal(2, _countRounds);
    }

    [Fact]
    public void Run_StopsGame_IfUserSaysAnythingButYes()
    {
        _ui.Setup(x => x.ReadLine()).Returns("anything");
        _session.Run();

        Assert.Equal(1, _countRounds);
    }

    [Fact]
    public void Run_WorksAsExpected_WhenUserTypesInUpperCase()
    {
        _ui.SetupSequence(x => x.ReadLine()).Returns("Y").Returns("N");
        _session.Run();

        Assert.Equal(2, _countRounds);
    }

    [Fact]
    public void Run_RandomizerIsCalledOnEachRound()
    {
        _ui.SetupSequence(x => x.ReadLine()).Returns("y").Returns("y").Returns("n");
        _session.Run();

        _randomizer.Verify(r => r.GetNextItem(), Times.Exactly(3));
    }
}