using System;
using System.Linq.Expressions;
using Moq;
using Game.Abstractions;
using Game.Helpers;
using Game.Tests.MockHelpers;

namespace Game.Tests;

public class HandmanSessionTests
{
    private readonly MockUI _ui = new();
    private readonly Mock<IManager> _manager = new();
    private readonly Func<string, int, IUI, IManager> _factory;
    private readonly Mock<IRandomizer<string>> _randomizer = new();
    private readonly HandmanSession _session;
    private int _countRounds;

    public HandmanSessionTests()
    {
        _countRounds = 0;
        _randomizer.Setup(r => r.GetNextItem()).Returns("galaxy");
        _factory = (words, attempts, ui) => _manager.Object;
        _session = new HandmanSession(_randomizer.Object, _factory, _ui);
        _manager.Setup(m => m.RunRound()).Callback(() => _countRounds++);
    }

    [Fact]
    public void Run_AlwaysRunsAtLeastOnce()
    {
        _ui.SetInputs(["n"]);
        _session.Run();

        Assert.Equal(1, _countRounds);
    }

    [Fact]
    public void Run_PrintsPromt_AfterRound()
    {
        _ui.SetInputs(["n"]);
        _session.Run();

        Assert.Contains("Do you want to play again? (y/n) ", _ui.Outputs);
    }

    [Fact]
    public void Run_ContinuesGame_IfUserSaysYes()
    {
        _ui.SetInputs(["y", "n"]);
        _session.Run();

        Assert.Equal(2, _countRounds);
    }

    [Fact]
    public void Run_StopsGame_IfUserSaysAnythingButYes()
    {
        _ui.SetInputs(["any thing"]);
        _session.Run();

        Assert.Equal(1, _countRounds);
    }

    [Fact]
    public void Run_WorksAsExpected_WhenUserTypesInUpperCase()
    {
        _ui.SetInputs(["Y", "N"]);
        _session.Run();

        Assert.Equal(2, _countRounds);
    }

    [Fact]
    public void Run_RandomizerIsCalledOnEachRound()
    {
        _ui.SetInputs(["y", "y", "n"]);
        _session.Run();

        _randomizer.Verify(r => r.GetNextItem(), Times.Exactly(3));
    }
}