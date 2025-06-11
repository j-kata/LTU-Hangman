using Game.Helpers;

namespace Game;

class Program
{
    private static readonly string[] _words = [
        "apple", "jungle", "breeze", "rocket", "python",
        "island", "puzzle", "galaxy", "whisper", "shadow",
        "castle", "oxygen", "flamingo", "volcano", "cyclone",
        "diamond", "fortune", "phantom", "cactus", "harvest",
    ];

    static void Main(string[] args)
    {
        GameManager gameManager = new GameManager(_words, 10);
        gameManager.StartGame();
    }
}
