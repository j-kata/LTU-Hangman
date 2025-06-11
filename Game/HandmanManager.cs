using System.Text;

namespace Game;

class HandmanManager(string word, int attempts = 10)
{
    private HangmanGame Game { get; } = new HangmanGame(word, attempts);

    public void RunRound()
    {
        while (!Game.IsGameOver())
        {
            Console.WriteLine(GetGameLog());

            Console.Write("Your guess: ");
            var guess = Console.ReadLine()?.Trim() ?? string.Empty;

            Game.MakeGuess(guess);
        }
        Console.WriteLine(GetGameResult());
    }

    private string GetGameResult()
    {
        if (!Game.IsGameOver()) return "";

        var stringBuilder = new StringBuilder();
        string result = Game.IsWon() ? "You won!" : "You lost!";
        stringBuilder.AppendLine($"Game over! {result}");
        stringBuilder.AppendLine($"The word was: {Game.TargetWord}");
        return stringBuilder.ToString();
    }

    private string GetGameLog()
    {
        if (Game.IsGameOver()) return "";

        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine($"The word: {Game.GuessedWord}");
        stringBuilder.AppendLine($"Attempts left: {Game.AttemptsLeft}");
        if (Game.WrongGuessesList.Length > 0)
            stringBuilder.AppendLine($"Wrong guesses: {Game.WrongGuessesList}");

        return stringBuilder.ToString();
    }
}