using System;
using System.Collections;
using TicTacToe;

namespace Tests
{
    public class HumanTester : IEventHandler
    {
        private readonly Game _game;
        private readonly Board _board;
        private readonly HumanPlayer _x;
        private readonly HumanPlayer _o;
        private int _row;
        private int _column;
        private Action<int, int> _selectSquare;

        public HumanTester(Board board)
        {
            _game = new Game(
                Team.X,
                board,
                this,
                OnNextPlayerTurn,
                OnNextPlayerTurn);
            _board = board;
            _x = new HumanPlayer(PlaceMarker, SubscribeToUI); 
            _o = new HumanPlayer(PlaceMarker, SubscribeToUI);
        }
        
        public IEnumerator PlayGame()
        {
            _x.OnPlayerTurn(_board);
            _selectSquare(0, 0);
            yield return _game.PlaceMarker(_row, _column);
            _o.OnPlayerTurn(_board);
            _selectSquare(1, 0);
            yield return _game.PlaceMarker(_row, _column);
            _x.OnPlayerTurn(_board);
            _selectSquare(1, 1);
            yield return _game.PlaceMarker(_row, _column);
            _o.OnPlayerTurn(_board);
            _selectSquare(1, 2);
            yield return _game.PlaceMarker(_row, _column);
            _x.OnPlayerTurn(_board);
            _selectSquare(2, 2);
            yield return _game.PlaceMarker(_row, _column);
        }

        private void SubscribeToUI(Action<int, int> obj)
        {
            _selectSquare = obj;
        }

        private void PlaceMarker(int x, int y)
        {
            _row = x;
            _column = y;
        }

        private static void OnNextPlayerTurn(Board board)
        {
        }

        public IEnumerator OnUpdateSquare(int row, int column, Team team)
        {
            yield break;
        }

        public IEnumerator OnAlreadyTaken(int row, int column, Team team)
        {
            yield break;
        }

        public IEnumerator OnAlreadyOver()
        {
            yield break;
        }

        public IEnumerator OnWinner(Team team)
        {
            yield break;
        }

        public IEnumerator OnTie()
        {
            yield break;
        }
    }
}