using System;
using System.Collections;

namespace TicTacToe
{
    public class Game
    {
        private readonly Board _board;
        private Action<Board> _onPlayerXTurn;
        private Action<Board> _onPlayerOTurn;
        private Team _currentTeam;
        private readonly IEventHandler _eventHandler;
        private bool _valid = true;
        private bool _executing;


        public Game(
            Team firstPlayer,
            Board board,
            IEventHandler eventHandler)
        {
            _board = board;
            _currentTeam = firstPlayer;
            _eventHandler = eventHandler;
        }

        public void Clear()
        {
            _executing = false;
        }
    
        public void TriggerPlayerTurn()
        {
            if (_currentTeam == Team.O)
            {
                _onPlayerOTurn(_board);
            }
            else
            {
                _onPlayerXTurn(_board);
            }
        }

        public IEnumerator PlaceMarker(int row, int column)
        {
            if (_executing)
            {
                yield break;
            }

            _executing = true;
            _valid = true;
            yield return CanMove(row, column);
            if (!_valid)
            {
                _executing = false;
                yield break;
            }
            yield return UpdateSquare(row, column);
            yield return GameCompleted();
        }

        public void HookupPlayers(Action<Board> onXTurn, Action<Board> onOTurn)
        {
            _onPlayerXTurn = onXTurn;
            _onPlayerOTurn = onOTurn;
        }
    
        public void SetCurrentTeam(Team currentTeam)
        {
            _currentTeam = currentTeam;
        }

        private IEnumerator CanMove(int row, int column)
        {
            var team = _board.GetTeam(row, column);
            if (team != Team.None)
            {
                yield return _eventHandler.OnAlreadyTaken(row, column, team);
                _valid = false;
            }
            else if (Check.WhoWon(_board) != Team.None)
            {
                yield return _eventHandler.OnAlreadyOver();
                _valid = false;
            }
        }

        private IEnumerator UpdateSquare(int row, int column)
        {
            _board.SetTeam(row, column, _currentTeam);
            yield return _eventHandler.OnUpdateSquare(row, column, _currentTeam);
        }

        private IEnumerator GameCompleted()
        {
            var winner = Check.WhoWon(_board);
            if (winner != Team.None)
            {
                yield return _eventHandler.OnWinner(winner);
            }
            else if (Check.IsBoardFull(_board))
            {
                yield return _eventHandler.OnTie();
            }
            else
            {
                _executing = false;
                PrepareNextTurn();
            }
        }

        private void PrepareNextTurn()
        {
            _currentTeam =
                _currentTeam == Team.O
                    ? Team.X
                    : Team.O;
            TriggerPlayerTurn();
        }
    }
}