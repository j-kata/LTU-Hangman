using System.Text;
using Game.UI;

namespace Game;

class HandmanManager(string word, int attempts, IUI ui)
{
    private HangmanGame Game { get; } = new HangmanGame(word, attempts);

    public void RunRound()
    {
        while (!Game.IsGameOver())
        {
            ui.PrintLine(GetGameLog());

            ui.Print("Your guess: ");
            var guess = ui.ReadLine() ?? string.Empty;

            Game.MakeGuess(guess);
        }
        ui.PrintLine(GetGameResult());
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