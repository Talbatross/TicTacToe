using System;
using UnityEngine;

namespace TicTacToe
{
    public class AIPlayer : IPlayer
    {
        private readonly Team _team;
        private readonly Team _opponent;
        private readonly BestMove _bestMove = new ();
        private readonly int _boardSize;
        private readonly Board _board;
        
        public Action<int, int> PlaceMarker { get; set; }

        public AIPlayer(
            Team team,
            int boardSize)
        {
            _team = team;
            _opponent = _team == Team.X
                ? Team.O
                : Team.X;
            _boardSize = boardSize;
            _board = new Board(boardSize);
        }

        public void OnPlayerTurn(IReadOnlyBoard board)
        {
            _board.Copy(board);
            var nextMove = FindBestMove();
            Debug.Log($"{_team} goes to Row:{nextMove.Row} Column:{nextMove.Column}");
            PlaceMarker(nextMove.Row, nextMove.Column);
        }

        private BestMove FindBestMove()
        {
            _bestMove.Clear();
            _board.ForEachSquare(CheckPosition);
            return _bestMove;
        }

        private void CheckPosition(int row, int column)
        {
            if (_board.GetTeam(row, column) != Team.None)
            {
                return;
            }
            _board.SetTeam(row, column, _team);
            var moveVal = GetMiniMax(0, _opponent);
            _board.SetTeam(row, column,Team.None);
            if (moveVal > _bestMove.Value)
            {
                _bestMove.SetBest(moveVal,row,column);
            }
        }

        private int GetMiniMax(int depth, Team currentTeam)
        {
            Team winner = Check.WhoWon(_board);
            if (winner == _team)
            {
                return 10 - depth;
            }
            if (winner != Team.None)
            {
                return -10 + depth;
            }
            return Check.IsBoardFull(_board)
                ? 0
                : CalculateMiniMax(depth, currentTeam);
        }

        private int CalculateMiniMax(int depth, Team currentTeam)
        {
            int best = int.MinValue;
            Func<int, int, int> minMax = Math.Max;
            Team nextTeam = _opponent;
            if (currentTeam != _team)
            {
                best = int.MaxValue;
                minMax = Math.Min;
                nextTeam = _team;
            }
            for (int row = 0; row < _boardSize; ++row)
            {
                for (int column = 0; column < _boardSize; ++column)
                {
                    if (_board.GetTeam(row, column) != Team.None)
                    {
                        continue;
                    }
                    _board.SetTeam(row, column, currentTeam);
                    best = minMax(best,GetMiniMax(depth + 1, nextTeam));
                    _board.SetTeam(row, column, Team.None);
                }
            }

            return best;
        }
    }
}