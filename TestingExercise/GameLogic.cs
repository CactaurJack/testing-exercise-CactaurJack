using System;
using System.Collections.Generic;
using System.Text;

namespace TestingExercise
{
    /// <summary>
    /// A class representing a game of Hangman
    /// </summary>
    public class GameLogic
    {
        char[] secretPhrase;
        char[] revealedPhrase;
        List<char> guesses = new List<char>();

        /// <summary>
        /// The currently revealed phrase
        /// </summary>
        /// <value>
        /// A string consisting of revealed characters or underscores
        /// </value>
        public string RevealedPhrase => new String(revealedPhrase);
                
        /// <summary>
        /// The number of wrong guesses
        /// </summary>
        /// <value>
        /// An integer between 0 and 7
        /// </value>
        public int WrongGuesses { get; private set; }

        /// <summary>
        /// The previously made guesses
        /// </summary>
        public char[] PreviousGuesses => guesses.ToArray();

        /// <summary>
        /// Tell if the game has beeen won by the player
        /// </summary>
        /// <value>true if the player has won, false otherwise</value>
        /// <remarks>A game is won if the secret phrase is completely revealed</remarks>
        public bool IsWon => !Array.Exists(revealedPhrase, c => c == '_');

        /// <summary>
        /// Tells if a game has been lost by the player 
        /// </summary>
        /// <value>true if the game is lot, false otherwise</value>
        /// <remarks>A game is lost if 7 wrong guesses have been made</remarks>
        public bool IsLost => WrongGuesses == 7;

        /// <summary>
        /// Checks if the game is still in progress
        /// </summary>
        /// <value>
        /// A boolean indicting if the game is still in progress (true) or ended (false)
        /// </value>
        public bool InProgress => !(IsWon || IsLost);

        /// <summary>
        /// Constructs a new game of Hangman, using the provided <paramref name="secret"/>
        /// </summary>
        /// <param name="secret">The secret to guess</param>
        public GameLogic(string secret)
        {
            secretPhrase = new char[secret.Length];
            revealedPhrase = new char[secret.Length];
            for(int i = 0; i < secret.Length; i++)
            {
                secretPhrase[i] = secret[i];
                if (secretPhrase[i].Equals(' ')) revealedPhrase[i] = secret[i];
                else revealedPhrase[i] = '_';                
            }            
        }

        /// <summary>
        /// A method for taking a new guess in the game
        /// </summary>
        /// <param name="c">The character guessed</param>
        /// <returns>true if the guess was correct, false if not</returns>
        public bool Guess(char c)
        {
            guesses.Add(c);
            bool found = false;
            for(int i = 0; i < secretPhrase.Length; i++)
            {
                if(secretPhrase[i] == c)
                {
                    revealedPhrase[i] = c;
                    found = true;
                }
            }
            if (!found) WrongGuesses += 1;
            return found;
        }
    }
}
