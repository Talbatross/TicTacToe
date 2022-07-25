using System;

namespace TicTacToe
{
    public interface IReadOnlyBoard
    {
        public Team GetTeam(int row, int column);
        public int GetSize();
    }
    
    public class Board : IReadOnlyBoard
    {
        private readonly Team[,] _board;
        private readonly int _boardSize;
        public Board(int boardSize)
        {
            _boardSize = boardSize;
            _board = new Team[_boardSize, _boardSize];
            for (int row = 0; row < boardSize; ++row)
            {
                for (int column = 0; column < boardSize; ++column)
                {
                    _board[row, column] = Team.None;
                }
            }
        }
    
        public void Copy(IReadOnlyBoard board)
        {
            if (board.GetSize() != _boardSize)
            {
                throw new ArgumentException("Expected a new board of the same size!");
            }
            for (int row = 0; row < _boardSize; ++row)
            {
                for (int column = 0; column < _boardSize; ++column)
                {
                    _board[row, column] = board.GetTeam(row, column);
                }
            }
        }
        
        public void Clear()
        {
            ForEachSquare(ClearTeam);
        }
    
        public Team GetTeam(int row, int column)
        {
            return _board[row, column];
        }
    
        public void SetTeam(int row, int column, Team team)
        {
            _board[row, column] = team;
        }
    
        public int GetSize()
        {
            return _board.GetLength(0);
        }
    
        public void ForEachSquare(Action<int,int> action)
        {
            for (int row = 0; row < _boardSize; ++row)
            {
                for (int column = 0; column < _boardSize; ++column)
                {
                    action(row, column);
                }
            }
        }
    
        private void ClearTeam(int row, int column)
        {
            SetTeam(row,column,Team.None);
        }
    }
}