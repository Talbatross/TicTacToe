using System.Collections;
using TicTacToe;

namespace Tests
{
    internal class AITester : IEventHandler
    {
        private readonly Game _game;
        private Board _board;
        private readonly AIPlayer _ai1;
        private readonly AIPlayer _ai2;

        private int _row;
        private int _column;
        private bool _endGame;
        
        public AITester(Board board)
        {
            _game = new Game(
                Team.O,
                board,
                this,
                OnNextPlayerTurn,
                OnNextPlayerTurn);
            _board = board;
            _ai1 = new AIPlayer(PlaceMarker, Team.X, board.GetSize()); 
            _ai2 = new AIPlayer(PlaceMarker, Team.O, board.GetSize());
        }

        public IEnumerator PlayGame()
        {
            AIPlayer currentPlayer = _ai2;
            while (!_endGame)
            {
                // Get Move From AI
                currentPlayer.OnPlayerTurn(_board);
                // make Move for AI
                yield return _game.PlaceMarker(_row, _column);
                // Swap AIs
                currentPlayer = currentPlayer == _ai1 ? _ai2 : _ai1;
            }
        }
        
        private void PlaceMarker(int x, int y)
        {
            _row = x;
            _column = y;
        }

        private void OnNextPlayerTurn(Board board)
        {
            _board = board;
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
            _endGame = true;
            yield break;
        }

        public IEnumerator OnTie()
        {
            _endGame = true;
            yield break;
        }
    }
}