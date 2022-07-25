using System;

namespace TicTacToe
{
    public class HumanPlayer : IPlayer
    {
        private IReadOnlyBoard _board;
        private readonly Action<int, int> _placeMarker;
        private readonly Action<Action<int, int>> _subscribeToUI;

        public HumanPlayer(Action<int, int> placeMarker, Action<Action<int, int>> subscribeToUI)
        {
            _placeMarker = placeMarker;
            _subscribeToUI = subscribeToUI;
        }

        public void OnPlayerTurn(IReadOnlyBoard board)
        {
            _board = board;
            _subscribeToUI(SelectSquare);
        }

        private void SelectSquare(int row, int column)
        {
            if (_board.GetTeam(row, column) == Team.None)
            {
                _placeMarker(row, column);
            }
        }
    }
}