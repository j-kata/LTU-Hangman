using Game.Abstractions;

namespace Game.UI;

public class ConsoleUI : IUI
{
    public void Print(string? str)
    {
        Console.Write(str);
    }

    public void PrintLine(string? str)
    {
        Console.WriteLine(str);
    }

    public string? ReadLine()
    {
        return Console.ReadLine()?.Trim();
    }
}