namespace Game;

class Program
{
    static void Main(string[] args)
    {
        var filePath = $"{Environment.CurrentDirectory}/Data/words.txt";
        var app = new App(filePath);
        app.Run();
    }
}