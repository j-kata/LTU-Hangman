using System.Text;
using Game.Helpers;
using Game.Abstractions;

namespace Game;

class HandmanManager(string[] words, int attempts = 10) : IManager
{
    private readonly Randomizer<string> _randomizer = new(words);
    private readonly int _attempts = attempts;

    public void StartGame()
    {
        while (true)
        {
            var game = CreateGame();
            RunGame(game);
            Console.WriteLine("New Game!");
            Console.WriteLine();
        }
    }

    public IGame CreateGame() => new HangmanGame(_randomizer.GetNextItem(), _attempts);

    public void RunGame(IGame game)
    {
        while (!game.IsGameOver())
        {
            Console.WriteLine(GetLog(game as HangmanGame));
            Console.Write("Your guess: ");
            var guess = Console.ReadLine()?.Trim() ?? string.Empty;
            game.Input(guess);
        }
    }

    public static string GetLog(HangmanGame? game)
    {
        if (game == null) return "";

        var stringBuilder = new StringBuilder();

        if (game.IsGameOver())
            stringBuilder.Append(game.IsWon() ? "You won!" : "You lost!");
        else
        {
            stringBuilder.AppendLine($"The word: {game.Word}");
            stringBuilder.AppendLine($"Attempts left: {game.AttemptsLeft}");
            if (game.Guesses.Length > 0)
                stringBuilder.AppendLine($"Wrong guesses: {game.Guesses}");
        }
        return stringBuilder.ToString();
    }
}