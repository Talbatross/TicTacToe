using System;
using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe
{
    public class SquareUI
    {
        private readonly Square[,] _squares;
        private List<GameObject> _lines;
        private readonly int _boardSize;

        public SquareUI(Square[,] squares, List<GameObject> lines)
        {
            _squares = squares;
            _lines = lines;
            _boardSize = squares.GetLength(0);
        }

        public Square GetSquare(int row, int column)
        {
            return _squares[row, column];
        }

        public void RegisterOnClick(Action<int,int> callback)
        {
            for (var row = 0; row < _boardSize; ++row)
            {
                for (var column = 0; column < _boardSize; ++column)
                {
                    _squares[row, column].RegisterOnClick(callback);
                }
            }
        }
        
        public void ResetSquares()
        {
            // Teardown Squares
            // Teardown Lines
            for (var row = 0; row < _boardSize; ++row)
            {
                for (var column = 0; column < _boardSize; ++column)
                {
                    _squares[row, column].Reset();
                }
            }
        }
    }
}