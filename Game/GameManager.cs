using System.Text;
using Game.Helpers;

namespace Game;

class GameManager(string[] words, int attempts = 10)
{
    private readonly Randomizer<string> _randomizer = new(words);
    private readonly int _attempts = attempts;

    public void StartGame()
    {
        while (true)
        {
            var game = CreateGame();
            RunGame(game);
            Console.WriteLine(Result(game));
            Console.WriteLine("New Game!");
            Console.WriteLine();
        }
    }
    private HangmanGame CreateGame() => new(_randomizer.GetNextItem(), _attempts);

    private static void RunGame(HangmanGame game)
    {
        while (!game.IsGameOver())
        {
            Console.WriteLine(Log(game));
            Console.Write("Your guess: ");
            var guess = Console.ReadLine()?.Trim() ?? string.Empty;
            game.MakeGuess(guess);
        }
    }

    private static string Log(HangmanGame game)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"The word: {game.Word}");
        stringBuilder.AppendLine($"Attempts left: {game.AttemptsLeft}");
        if (game.Guesses.Length > 0)
            stringBuilder.AppendLine($"Wrong guesses: {game.Guesses}");
        return stringBuilder.ToString();
    }

    private static string Result(HangmanGame game) => game.IsWon() ? "You won!" : "You lost!";
}