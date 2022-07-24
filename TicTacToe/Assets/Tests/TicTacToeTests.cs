using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Tests
{
    public class NewTestScript
    {
        [UnityTest]
        public IEnumerator TestXWins()
        {
            var board = new Board(3);
            var game = new Game(
                Team.X,
                board,
                new DummyHandler());
            var playerX = new HumanPlayer();
            var playerO = new HumanPlayer();
            playerX.SubscribeToUI = _ => { };
            playerO.SubscribeToUI = _ => { };
            game.HookupPlayers(playerX.OnPlayerTurn, playerO.OnPlayerTurn);
            yield return game.PlaceMarker(0, 0);
            yield return game.PlaceMarker(1, 0);
            yield return game.PlaceMarker(1, 1);
            yield return game.PlaceMarker(1, 2);
            yield return game.PlaceMarker(2, 2);
            Assert.That(Check.WhoWon(board), Is.EqualTo(Team.X));
        }

        [UnityTest]
        public IEnumerator TestTie()
        {
            var boardSize = 3;
            var board = new Board(boardSize);
            var ticTacToe = new Game(
                Team.O,
                board,
                new DummyHandler());
            AIPlayer ai1 = new AIPlayer(Team.X, boardSize);
            AIPlayer ai2 = new AIPlayer(Team.O, boardSize);
            ticTacToe.HookupPlayers(ai1.OnPlayerTurn, ai2.OnPlayerTurn);
            ai1.PlaceMarker = UpdateSquare;
            ai2.PlaceMarker = UpdateSquare;
            ticTacToe.TriggerPlayerTurn();
            Assert.That(Check.IsBoardFull(board), Is.True);
        }
        
        private void UpdateSquare(int row, int column)
        {
            
        }

        [UnityTest]
        public IEnumerator TestOWins()
        {
            var standard = new DefaultGame();
            var ticTacToe = standard.Game;
            yield return ticTacToe.PlaceMarker(0, 0);
            yield return ticTacToe.PlaceMarker(1, 0);
            yield return ticTacToe.PlaceMarker(1, 1);
            yield return ticTacToe.PlaceMarker(1, 2);
            yield return ticTacToe.PlaceMarker(2, 2);
            Assert.That(Check.WhoWon(standard.Board), Is.EqualTo(Team.O));
        }

        [UnityTest]
        public IEnumerator TestDuplicatePlacement()
        {
            var standard = new DefaultGame();
            var ticTacToe = standard.Game;
            yield return ticTacToe.PlaceMarker(0, 0);
            yield return ticTacToe.PlaceMarker(0, 0);
            Assert.That(
                () => standard.Board.GetTeam(0, 0),
                Is.EqualTo(Team.O));
        }

        [UnityTest]
        public IEnumerator TestEndOfGamePlacement()
        {
            var standard = new DefaultGame();
            var ticTacToe = standard.Game;
            yield return ticTacToe.PlaceMarker(0, 0);
            yield return ticTacToe.PlaceMarker(1, 0);
            yield return ticTacToe.PlaceMarker(1, 1);
            yield return ticTacToe.PlaceMarker(1, 2);
            yield return ticTacToe.PlaceMarker(2, 2);
            yield return ticTacToe.PlaceMarker(2, 0);
            Assert.That(
                () => standard.Board.GetTeam(2, 0),
                Is.EqualTo(Team.None));
        }

        [UnityTest]
        public IEnumerator TestRowWin()
        {
            var standard = new DefaultGame();
            var ticTacToe = standard.Game;
            yield return ticTacToe.PlaceMarker(0, 0);
            yield return ticTacToe.PlaceMarker(1, 0);
            yield return ticTacToe.PlaceMarker(0, 1);
            yield return ticTacToe.PlaceMarker(1, 1);
            yield return ticTacToe.PlaceMarker(0, 2);
            Assert.That(Check.WhoWon(standard.Board), Is.EqualTo(Team.O));
        }

        [UnityTest]
        public IEnumerator TestColumnWin()
        {
            var standard = new DefaultGame();
            var ticTacToe = standard.Game;
            yield return ticTacToe.PlaceMarker(0, 0);
            yield return ticTacToe.PlaceMarker(0, 1);
            yield return ticTacToe.PlaceMarker(1, 0);
            yield return ticTacToe.PlaceMarker(1, 1);
            yield return ticTacToe.PlaceMarker(2, 0);
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
                    new DummyHandler());
                var playerX = new HumanPlayer();
                var playerO = new HumanPlayer();
                Game.HookupPlayers(playerX.OnPlayerTurn, playerO.OnPlayerTurn);
                playerX.SubscribeToUI = _ => { };
                playerO.SubscribeToUI = _ => { };
            }
        }
    }
}
