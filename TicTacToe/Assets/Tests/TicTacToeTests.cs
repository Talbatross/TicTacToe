using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Tests
{
    public class NewTestScript
    {
        // A Test behaves as an ordinary method
        [Test]
        public void TestXWins()
        {
            Game ticTacToe = new Game();
            ticTacToe.SetFirstAndCurrentPlayer(Game.Team.X);
            ticTacToe.PlaceMarker(0,0);
            ticTacToe.PlaceMarker(1,0);
            ticTacToe.PlaceMarker(1,1);
            ticTacToe.PlaceMarker(1,2);
            ticTacToe.PlaceMarker(2,2);
            Assert.That(ticTacToe.WhoWon(), Is.EqualTo(Game.Team.X));
        }
    }
}
