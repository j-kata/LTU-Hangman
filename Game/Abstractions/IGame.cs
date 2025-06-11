namespace Game.Abstractions;

public interface IGame
{
    public bool IsGameOver();
    public void Input(string input);
}