# Hangman Game – C# Console Project

This is a simple console-based Hangman game developed as part of a C# course assignment.
The goal of the project was to practice **object-oriented programming** and **unit testing** in a small, testable application.

## Features
- Word guessing game with limited attempts
- Game continues until the player chooses to quit
- Words are loaded from a local file
- Clear separation of concerns using interfaces and dependency injection
- Focus on testability and clean architecture

## Structure
- `App` – initializes and runs the game
- `HandmanSession` – handles game rounds and replay loop
- `HandmanManager` – coordinates the game and user interaction
- `HangmanGame` – contains the core game logic
- `HiddenWord` – handles masked word presentation and letter matching
- `WrongGuesses` – tracks and formats wrong guesses
- `IUI`, `IManager`, `IRandomizer`, etc. – interfaces to improve testability
- `ConsoleUI` – console-based implementation of user interaction

## Unit Testing
- Uses **xUnit** and **Moq** for testing
- All core logic is covered by unit tests
- Game flow, input/output, and edge cases are verified

## Word File
Words are loaded from a file located at:
`/Data/words.txt` (comma-separated)

## Technologies
- C#
- xUnit
- Moq