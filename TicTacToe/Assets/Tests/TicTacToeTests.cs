using System;
using System.Collections;
using NUnit.Framework;
using TicTacToe;
using UnityEngine.TestTools;

namespace Tests
{
    public class TicTacToeTests
    {
        [UnityTest]
        public IEnumerator TestXWins()
        {
            var board = new Board(3);
            var tester = new HumanTester(board);
            yield return tester.PlayGame();
            Assert.That(Check.WhoWon(board), Is.EqualTo(Team.X));
        }

        [UnityTest]
        public IEnumerator TestTie()
        {
            var boardSize = 3;
            var board = new Board(boardSize);
            var bootstrap = new AITester(board);
            yield return bootstrap.PlayGame();
            Assert.That(Check.IsBoardFull(board), Is.True);
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

        [Test]
        public void TestBoardClear()
        {
            var board = new Board(3);
            board.SetTeam(0,0,Team.X);
            board.Clear();
            Assert.That(board.GetTeam(0,0), Is.EqualTo(Team.None));
        }

        [Test]
        public void TestBadBoardCopy()
        {
            var board1 = new Board(3);
            var board2 = new Board(4);
            Assert.Throws<ArgumentException>(() =>
            {
                board1.Copy(board2);
            });
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
                    new DummyHandler(),
                    OnPlayerTurn,
                    OnPlayerTurn);
            }

            private void OnPlayerTurn(Board obj)
            {
            }
        }
    }
}
