using System;
using System.IO;

namespace TestingExercise
{
    class Program
    {
        /// <summary>
        /// Runs a single game of "Lobster Pot"
        /// </summary>
        /// <remarks>
        /// In this game, the player guesses one character at a time
        /// to reveal a secret phrase.  You win if you reveal the 
        /// whole phrase, and lose if you make seven wrong guesses.
        /// </remarks>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var random = new Random();
            var phrases = File.OpenText("secret-phrases.txt").ReadToEnd().Split('\n');
            var index = random.Next(phrases.Length);
            var game = new GameLogic(phrases[index].Trim());
            Console.WriteLine("Welcome to Lobster Pot");
            
            while (game.InProgress)
            {
                Renderer.DrawPot(game.WrongGuesses);
                
                Console.WriteLine("Your phrase to guess is:");
                Renderer.DrawRevealedPhrase(game.RevealedPhrase);

                Console.WriteLine("Your previous guesses are:");
                Renderer.DrawGuesses(game.PreviousGuesses);

                Console.Write("Please Guess a character:");
                
                ConsoleKeyInfo keyinfo = Console.ReadKey();
                if (keyinfo.Key == ConsoleKey.Escape) break;

                Console.Clear();

                if (game.Guess(keyinfo.KeyChar))
                {
                    Console.Beep(2000, 10);
                    Console.WriteLine("Great guess!");
                }
                else
                {
                    Console.Beep(100, 10);
                    Console.WriteLine("Oh no! Things are heating up!");
                }
            }
            Renderer.DrawPot(game.WrongGuesses);
            if (game.IsWon) Console.WriteLine("You won!");
            if (game.IsLost) Console.WriteLine("You lost!");
            Console.WriteLine("Thanks for Playing!");
        }
    }
}
