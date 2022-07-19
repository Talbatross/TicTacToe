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
            Game ticTacToe = new Game(Team.X);
            ticTacToe.PlaceMarker(0,0);
            ticTacToe.PlaceMarker(1,0);
            ticTacToe.PlaceMarker(1,1);
            ticTacToe.PlaceMarker(1,2);
            ticTacToe.PlaceMarker(2,2);
            Assert.That(Check.WhoWon(ticTacToe.board), Is.EqualTo(Team.X));
        }

        [Test]
        public void TestTie()
        {
            Game ticTacToe = new Game(Team.O);
            ticTacToe.PlaceMarker(0,0);
            ticTacToe.PlaceMarker(1,1);
            ticTacToe.PlaceMarker(0,1);
            ticTacToe.PlaceMarker(0,2);
            ticTacToe.PlaceMarker(2,0);
            ticTacToe.PlaceMarker(1,0);
            ticTacToe.PlaceMarker(1,2);
            ticTacToe.PlaceMarker(2,1);
            ticTacToe.PlaceMarker(2,2);
            Assert.That(Check.IsBoardFull(ticTacToe.board), Is.True);
        }

        [Test]
        public void TestOWins()
        {
            Game ticTacToe = new Game(Team.O);
            ticTacToe.PlaceMarker(0,0);
            ticTacToe.PlaceMarker(1,0);
            ticTacToe.PlaceMarker(1,1);
            ticTacToe.PlaceMarker(1,2);
            ticTacToe.PlaceMarker(2,2);
            Assert.That(Check.WhoWon(ticTacToe.board), Is.EqualTo(Team.O));
        }

        [Test]
        public void TestDuplicatePlacement()
        {
            Game ticTacToe = new Game(Team.O);
            ticTacToe.PlaceMarker(0, 0);
            Assert.That(
                () => ticTacToe.PlaceMarker(0,0), 
                Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TestEndOfGamePlacement()
        {
            Game ticTacToe = new Game(Team.O);
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
            Game ticTacToe = new Game(Team.O);
            ticTacToe.PlaceMarker(0,0);
            ticTacToe.PlaceMarker(1,0);
            ticTacToe.PlaceMarker(0,1);
            ticTacToe.PlaceMarker(1,1);
            ticTacToe.PlaceMarker(0,2);
            Assert.That(Check.WhoWon(ticTacToe.board),Is.EqualTo(Team.O));
        }

        [Test]
        public void TestColumnWin()
        {
            Game ticTacToe = new Game(Team.O);
            ticTacToe.PlaceMarker(0,0);
            ticTacToe.PlaceMarker(0,1);
            ticTacToe.PlaceMarker(1,0);
            ticTacToe.PlaceMarker(1,1);
            ticTacToe.PlaceMarker(2,0);
            Assert.That(Check.WhoWon(ticTacToe.board),Is.EqualTo(Team.O));
        }
    }
}
