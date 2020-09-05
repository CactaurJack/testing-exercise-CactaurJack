using System;
using System.Collections.Generic;
using System.Text;

namespace TestingExercise
{
    /// <summary>
    /// A class for drawing an ASCII-art hangman scaffold
    /// </summary>
    public static class Renderer
    {
        /// <summary>
        /// Draws an ASCII representation of a lobster in a pot of hot water
        /// The amount of the lobster drawn is based on <paramref name="wrongGuesses"/>
        /// </summary>
        /// <param name="wrongGuesses"></param>
        public static void DrawPot(int wrongGuesses)
        {
            // Draw the pot top
            Console.WriteLine("|~~~~~~~~|");

            // Draw the first segment
            if (wrongGuesses >= 1) Console.WriteLine("| (]  [) |");
            else Console.WriteLine("|        |");

            // Draw the second segment
            if (wrongGuesses >= 2) Console.WriteLine("|  \\oo/  |");
            else Console.WriteLine("|        |");

            // Draw the third segment
            if (wrongGuesses >= 3) Console.WriteLine("| >{^^}< |");
            else Console.WriteLine("|        |");

            // Draw the fourth segment
            if (wrongGuesses >= 4) Console.WriteLine("| >{^^}< |");
            else Console.WriteLine("|        |");

            // Draw the fifth segment
            if (wrongGuesses >= 5) Console.WriteLine("|  {^^}  |");
            else Console.WriteLine("|        |");

            // Draw the sixth segment
            if (wrongGuesses >= 6) Console.WriteLine("|  {^^}  |");
            else Console.WriteLine("|        |");

            // Draw the seventh segment 
            if (wrongGuesses >= 7) Console.WriteLine("|  /MMM\\ |");
            else Console.WriteLine("|        |");

            // Draw the pot bottom
            Console.WriteLine("|________|");
            Console.WriteLine(" wwwwwwww ");
        }

        /// <summary>
        /// Draws the <paramref name="phrase"/> with a space of padding between each character
        /// </summary>
        /// <param name="phrase">The phrase to draw</param>
        public static void DrawRevealedPhrase(string phrase)
        {
            for(int i = 0; i < phrase.Length; i++)
            {
                Console.Write(phrase[i]);
                Console.Write(" ");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Draws the already-guessed characters 
        /// </summary>
        /// <param name="priorGuesses"></param>
        public static void DrawGuesses(char[] priorGuesses)
        {
            Console.Write("[");
            for(int i = 0; i < priorGuesses.Length; i++)
            {
                Console.Write(priorGuesses[i]);
                Console.Write(",");
            }
            Console.WriteLine("]");
        }
    }
}
