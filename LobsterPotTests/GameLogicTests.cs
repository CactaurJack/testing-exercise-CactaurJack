using System;
using Xunit;
using TestingExercise;

namespace LobsterPotTests
{
    public class GameLogicTests
    {
        [Fact]
        public void GuessesShouldBeEmptyAtStartOfGame()
        {
            GameLogic game = new GameLogic("secret");
            Assert.Empty(game.PreviousGuesses);
        }

        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("aa")]
        [InlineData("aaaa")]
        public void SecretPhraseMustBeAtLeastFiveCharactersLong(string secret)
        {
            Assert.Throws<System.ArgumentException>(() =>
           {
               GameLogic game = new GameLogic(secret);

           });
        }

        [Fact]
        public void WrongGuessesShouldStartAtZero()
        {
            GameLogic game = new GameLogic("secret");
            Assert.Equal(0, game.WrongGuesses);
        }

        [Fact]
        public void NewGameShouldNotBeWon()
        {
            GameLogic game = new GameLogic("secret");
            Assert.False(game.IsWon);
        }

        [Fact]
        public void NewGameShouldNotBeLost()
        {
            GameLogic game = new GameLogic("secret");
            Assert.False(game.IsLost);
        }

        [Fact]
        public void NewGameShouldBeInProgress()
        {
            GameLogic game = new GameLogic("secret");
            Assert.True(game.InProgress);
        }

        [Theory]
        [InlineData("It's a hot time in the old town tonight!","__'_ _ ___ ____ __ ___ ___ ____ _______!")]
        [InlineData("Hello World","_____ _____")]
        [InlineData("Jacob Jingleheimer Schmidt" ,"_____ ____________ _______" )]
        public void SecretPhraseShouldEncodeProvidedSecret(string secret, string revealed)
        {
            GameLogic game = new GameLogic(secret);
            Assert.Equal(revealed, game.RevealedPhrase);

        }

        [Theory]
        [InlineData('s')]
        [InlineData('f')]
        public void GuessesShouldBeAddedToPreviousGuesses(char guess)
        {
            GameLogic game = new GameLogic("secret");
            game.Guess(guess);
            Assert.Contains(guess, game.PreviousGuesses);
        }

        [Fact]
        public void WrongGuessShouldIncrementWrongGuess()
        {
            GameLogic game = new GameLogic("secret");
            game.Guess('w');
            Assert.Equal(1, game.WrongGuesses);
            game.Guess('f');
            Assert.Equal(2, game.WrongGuesses);
            game.Guess('x');
            Assert.Equal(3, game.WrongGuesses);
            game.Guess('y');
            Assert.Equal(4, game.WrongGuesses);
            game.Guess('q');
            Assert.Equal(5, game.WrongGuesses);
            game.Guess('p');
            Assert.Equal(6, game.WrongGuesses);
            game.Guess('i');
            Assert.Equal(7, game.WrongGuesses);
        }

        [Fact]
        public void SevenWrongGuessesShouldEndTheGame()
        {
            GameLogic game = new GameLogic("secret");
            game.Guess('w');
            game.Guess('f');
            game.Guess('x');
            game.Guess('y');
            game.Guess('q');
            game.Guess('p');
            game.Guess('i');
            Assert.False(game.InProgress);
        }

        [Fact]
        public void SevenWrongGuessesLosses()
        {
            GameLogic game = new GameLogic("secret");
            game.Guess('w');
            game.Guess('f');
            game.Guess('x');
            game.Guess('y');
            game.Guess('q');
            game.Guess('p');
            game.Guess('i');
            Assert.True(game.IsLost);
            Assert.False(game.IsWon);
        }

        [Fact]
        public void CorrectGuessesShouldWinGame()
        {
            GameLogic game = new GameLogic("secret");
            game.Guess('s');
            game.Guess('e');
            game.Guess('c');
            //game.Guess('e');
            game.Guess('r');
            game.Guess('t');
            Assert.False(game.InProgress);
            Assert.True(game.IsWon);
            Assert.False(game.IsLost);
        }

        [Fact]
        public void GuessingTheSameCharacterTwiceShouldNotCount()
        {
            GameLogic game = new GameLogic("secret");
            game.Guess('s');
            Assert.Equal(1, game.PreviousGuesses.Length);
            Assert.Equal(GuessResult.Multiple, game.Guess('s'));
            game.Guess('s');
            Assert.Equal(1, game.PreviousGuesses.Length);
            game.Guess('a');
            Assert.Equal(1, game.WrongGuesses);
            Assert.Equal(GuessResult.Multiple, game.Guess('a'));
            Assert.Equal(1, game.WrongGuesses);
        }

        [Theory]
        [InlineData('!')]
        [InlineData('3')]
        [InlineData('&')]
        public void GuessShouldBeALetter(char guess)
        {
            GameLogic game = new GameLogic("secret");
            Assert.Equal(GuessResult.NotLetter, game.Guess(guess));
        }

        [Fact]
        public void CorrectGuessShouldReturnEnumValue()
        {
            GameLogic game = new GameLogic("secret");
            Assert.Equal(GuessResult.Correct, game.Guess('s'));
        }

        [Fact]
        public void IncorrectGuessShouldReturnFalseEnumValue()
        {
            GameLogic game = new GameLogic("secret");
            Assert.Equal(GuessResult.Wrong, game.Guess('w'));
        }

        [Fact]
        public void GuessesShouldBeCaseSensitive()
        {
            GameLogic game = new GameLogic("Secret");
            Assert.Equal(GuessResult.Correct, game.Guess('s'));
            Assert.Equal(GuessResult.Correct, game.Guess('R'));

        }

    }
}
