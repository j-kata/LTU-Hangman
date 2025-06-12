namespace Game.UI;

public interface IUI
{
    public void Print(string? str);
    public void PrintLine(string? str);
    public string? ReadLine();
}