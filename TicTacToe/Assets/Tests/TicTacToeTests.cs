using System;
using NUnit.Framework;

namespace Tests
{
    public class NewTestScript
    {
        // A Test behaves as an ordinary method
        [Test]
        public void TestXWins()
        {
            var game = new Game(
                Team.X, 
                PlayerType.Human, 
                PlayerType.Human, 
                3);
            game.PlaceMarker(0,0);
            game.PlaceMarker(1,0);
            game.PlaceMarker(1,1);
            game.PlaceMarker(1,2);
            game.PlaceMarker(2,2);
            Assert.That(Check.WhoWon(game.GetBoard()), Is.EqualTo(Team.X));
        }

        [Test]
        public void TestTie()
        {
            var ticTacToe = new Game(
                Team.O, 
                PlayerType.AI, 
                PlayerType.AI, 
                3);
            Assert.That(Check.IsBoardFull(ticTacToe.GetBoard()), Is.True);
        }

        [Test]
        public void TestOWins()
        {
            var ticTacToe = CreateDefaultGame();
            ticTacToe.PlaceMarker(0,0);
            ticTacToe.PlaceMarker(1,0);
            ticTacToe.PlaceMarker(1,1);
            ticTacToe.PlaceMarker(1,2);
            ticTacToe.PlaceMarker(2,2);
            Assert.That(Check.WhoWon(ticTacToe.GetBoard()), Is.EqualTo(Team.O));
        }

        [Test]
        public void TestDuplicatePlacement()
        {
            var ticTacToe = CreateDefaultGame();
            ticTacToe.PlaceMarker(0, 0);
            Assert.That(
                () => ticTacToe.PlaceMarker(0,0), 
                Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TestEndOfGamePlacement()
        {
            var ticTacToe = CreateDefaultGame();
            ticTacToe.PlaceMarker(0,0);
            ticTacToe.PlaceMarker(1,0);
            ticTacToe.PlaceMarker(1,1);
            ticTacToe.PlaceMarker(1,2);
            ticTacToe.PlaceMarker(2,2);
            Assert.That(
                () => ticTacToe.PlaceMarker(2,0), 
                Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TestRowWin()
        {
            var ticTacToe = CreateDefaultGame();
            ticTacToe.PlaceMarker(0,0);
            ticTacToe.PlaceMarker(1,0);
            ticTacToe.PlaceMarker(0,1);
            ticTacToe.PlaceMarker(1,1);
            ticTacToe.PlaceMarker(0,2);
            Assert.That(Check.WhoWon(ticTacToe.GetBoard()),Is.EqualTo(Team.O));
        }

        [Test]
        public void TestColumnWin()
        {
            var ticTacToe = CreateDefaultGame();
            ticTacToe.PlaceMarker(0,0);
            ticTacToe.PlaceMarker(0,1);
            ticTacToe.PlaceMarker(1,0);
            ticTacToe.PlaceMarker(1,1);
            ticTacToe.PlaceMarker(2,0);
            Assert.That(Check.WhoWon(ticTacToe.GetBoard()),Is.EqualTo(Team.O));
        }

        private Game CreateDefaultGame()
        {
            return new Game(
                Team.O, 
                PlayerType.Human, 
                PlayerType.Human, 
                3);
        }
    }
}
