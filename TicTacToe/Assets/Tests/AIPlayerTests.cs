using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class AIPlayerTests
    {
        [Test]
        public void TestTie()
        {
            Game ticTacToe = new Game(Team.X);
            AIPlayer player1 = new AIPlayer(ticTacToe.PlaceMarker, Team.X);
            AIPlayer player2 = new AIPlayer(ticTacToe.PlaceMarker, Team.O);
            player1.OnPlayerTurn(ticTacToe.board);
            player2.OnPlayerTurn(ticTacToe.board);
            player1.OnPlayerTurn(ticTacToe.board);
            player2.OnPlayerTurn(ticTacToe.board);
            player1.OnPlayerTurn(ticTacToe.board);
            player2.OnPlayerTurn(ticTacToe.board);
            player1.OnPlayerTurn(ticTacToe.board);
            player2.OnPlayerTurn(ticTacToe.board);
            player1.OnPlayerTurn(ticTacToe.board);
            Assert.That(Check.IsBoardFull(ticTacToe.board), Is.True);
        }
    }
}