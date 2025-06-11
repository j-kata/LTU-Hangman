namespace Game.Abstractions;

public interface IManager
{
    public IGame CreateGame();
    public void RunGame(IGame game);
}