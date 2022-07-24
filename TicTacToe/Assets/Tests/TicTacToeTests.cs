using NUnit.Framework;

namespace Tests
{
    public class NewTestScript
    {
        // A Test behaves as an ordinary method
        [Test]
        public void TestXWins()
        {
            var board = new Board(3);
            var game = new Game(
                Team.X,
                board,
                UpdateSquare,
                () => {},
                () => {});
            var playerX = new HumanPlayer();
            var playerO = new HumanPlayer();
            playerX.SubscribeToUI = _ => { };
            playerO.SubscribeToUI = _ => { };
            game.HookupPlayers(playerX.OnPlayerTurn, playerO.OnPlayerTurn);
            playerX.PlaceMarker = game.PlaceMarker;
            playerO.PlaceMarker = game.PlaceMarker;
            playerX.PlaceMarker(0, 0);
            playerO.PlaceMarker(1, 0);
            playerX.PlaceMarker(1, 1);
            playerO.PlaceMarker(1, 2);
            playerX.PlaceMarker(2, 2);
            Assert.That(Check.WhoWon(board), Is.EqualTo(Team.X));
        }

        [Test]
        public void TestTie()
        {
            var boardSize = 3;
            var board = new Board(boardSize);
            var ticTacToe = new Game(
                Team.O,
                board,
                UpdateSquare,
                () => {},
                () => {});
            AIPlayer ai1 = new AIPlayer(Team.X, boardSize);
            AIPlayer ai2 = new AIPlayer(Team.O, boardSize);
            ticTacToe.HookupPlayers(ai1.OnPlayerTurn, ai2.OnPlayerTurn);
            ai1.PlaceMarker = ticTacToe.PlaceMarker;
            ai2.PlaceMarker = ticTacToe.PlaceMarker;
            ticTacToe.TriggerPlayerTurn();
            Assert.That(Check.IsBoardFull(board), Is.True);
        }

        [Test]
        public void TestOWins()
        {
            var standard = new DefaultGame();
            var ticTacToe = standard.Game;
            ticTacToe.PlaceMarker(0, 0);
            ticTacToe.PlaceMarker(1, 0);
            ticTacToe.PlaceMarker(1, 1);
            ticTacToe.PlaceMarker(1, 2);
            ticTacToe.PlaceMarker(2, 2);
            Assert.That(Check.WhoWon(standard.Board), Is.EqualTo(Team.O));
        }

        [Test]
        public void TestDuplicatePlacement()
        {
            var standard = new DefaultGame();
            var ticTacToe = standard.Game;
            ticTacToe.PlaceMarker(0, 0);
            ticTacToe.PlaceMarker(0, 0);
            Assert.That(
                () => standard.Board.GetTeam(0, 0),
                Is.EqualTo(Team.O));
        }

        [Test]
        public void TestEndOfGamePlacement()
        {
            var standard = new DefaultGame();
            var ticTacToe = standard.Game;
            ticTacToe.PlaceMarker(0, 0);
            ticTacToe.PlaceMarker(1, 0);
            ticTacToe.PlaceMarker(1, 1);
            ticTacToe.PlaceMarker(1, 2);
            ticTacToe.PlaceMarker(2, 2);
            ticTacToe.PlaceMarker(2, 0);
            Assert.That(
                () => standard.Board.GetTeam(2, 0),
                Is.EqualTo(Team.None));
        }

        [Test]
        public void TestRowWin()
        {
            var standard = new DefaultGame();
            var ticTacToe = standard.Game;
            ticTacToe.PlaceMarker(0, 0);
            ticTacToe.PlaceMarker(1, 0);
            ticTacToe.PlaceMarker(0, 1);
            ticTacToe.PlaceMarker(1, 1);
            ticTacToe.PlaceMarker(0, 2);
            Assert.That(Check.WhoWon(standard.Board), Is.EqualTo(Team.O));
        }

        [Test]
        public void TestColumnWin()
        {
            var standard = new DefaultGame();
            var ticTacToe = standard.Game;
            ticTacToe.PlaceMarker(0, 0);
            ticTacToe.PlaceMarker(0, 1);
            ticTacToe.PlaceMarker(1, 0);
            ticTacToe.PlaceMarker(1, 1);
            ticTacToe.PlaceMarker(2, 0);
            Assert.That(Check.WhoWon(standard.Board), Is.EqualTo(Team.O));
        }

        private class DefaultGame
        {
            public readonly Board Board;
            public readonly Game Game;

            public DefaultGame()
            {
                Board = new Board(3);
                Game = new Game(
                    Team.O,
                    Board,
                    (_, _, _) => { },
                    () => {},
                    () => {});
                var playerX = new HumanPlayer();
                var playerO = new HumanPlayer();
                Game.HookupPlayers(playerX.OnPlayerTurn, playerO.OnPlayerTurn);
                playerX.SubscribeToUI = _ => { };
                playerO.SubscribeToUI = _ => { };
            }
        }

        private void UpdateSquare(int row, int column, Team team)
        {

        }
    }
}
