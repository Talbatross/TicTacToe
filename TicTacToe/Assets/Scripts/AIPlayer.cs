using System;
using UnityEngine;

public class AIPlayer : IPlayer
{
    private readonly Action<int, int> _placeMarker;
    private Team[,] _board;
    private readonly Team _team;
    private readonly Team _opponent;
    private readonly BestMove _bestMove = new ();

    public AIPlayer(
        Action<int, int> placeMarker,
        Team team)
    {
        _placeMarker = placeMarker;
        _team = team;
        _opponent = _team == Team.X
            ? Team.O
            : Team.X;
    }

    public void OnPlayerTurn(Team[,] board)
    {
        _board = board;
        BestMove nextMove = FindBestMove();
        Debug.Log($"{_team} goes to Row:{nextMove.Row} Column:{nextMove.Column}");
        _placeMarker(nextMove.Row, nextMove.Column);
    }

    class BestMove
    {
        public int Value { get; private set; } = int.MinValue;
        public int Row { get; private set; }= -1;
        public int Column { get; private set; }= -1;
        
        public void SetBest(int val, int row, int column)
        {
            Value = val;
            Row = row;
            Column = column;
        }

        public void Clear()
        {
            Value = int.MinValue;
            Row = -1;
            Column = -1;
        }
        
    }
    
    private BestMove FindBestMove()
    {
        _bestMove.Clear();
        for (int row = 0; row < 3; ++row)
        {
            for (int column = 0; column < 3; ++column)
            {
                CheckPosition(row, column);
            }
        }

        return _bestMove;
    }

    private void CheckPosition(int row, int column)
    {
        if (_board[row, column] != Team.None)
        {
            return;
        }
        _board[row, column] = _team;
        int moveVal = GetMiniMax(0, _opponent);
        _board[row, column] = Team.None;
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
        for (int row = 0; row < 3; ++row)
        {
            for (int column = 0; column < 3; ++column)
            {
                if (_board[row, column] != Team.None)
                {
                    continue;
                }
                _board[row, column] = currentTeam;
                best = minMax(best,GetMiniMax(depth + 1, nextTeam));
                _board[row, column] = Team.None;
            }
        }

        return best;
    }
}