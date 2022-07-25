using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe
{
    public class SquareUIBuilder
    {
        private readonly GameObject _square;
        private readonly GameObject _horizontal;
        private readonly GameObject _vertical;
        private readonly Canvas _canvas;
        private readonly int _boardSize;
        private readonly int _uiSize;
        
        private readonly int _lineNumber;
        private readonly float _squareSize;
        private readonly float _halfSquareSize;
        private readonly float _lineSize;
        private readonly float _halfLineSize;
        private readonly float _offset;

        public SquareUIBuilder(
            GameObject square,
            GameObject horizontal,
            GameObject vertical,
            Canvas canvas,
            int boardSize,
            int uiSize)
        {
            _square = square;
            _horizontal = horizontal;
            _vertical = vertical;
            _canvas = canvas;
            _boardSize = boardSize;
            _uiSize = uiSize;
            _lineNumber = _boardSize - 1;
            _squareSize = (float)(_uiSize * 0.9 / _boardSize);
            _halfSquareSize = _squareSize * .5f;
            _lineSize = (float)(_uiSize * 0.1 / _lineNumber);
            _halfLineSize = _lineSize * .5f;
            _offset = _uiSize * .5f;
        }

        public SquareUI BuildUI()
        {
            var lines = GenerateLines();
            var squares = GenerateSquares();
            var squareUI = new SquareUI(squares, lines);
            return squareUI;
        }

        private Square[,] GenerateSquares()
        {
            Square[,] squares = new Square[_boardSize, _boardSize];
            for (var row = 0; row < _boardSize; ++row)
            {
                var y = _offset - (_squareSize * row + _lineSize * row) - _halfSquareSize;
                for (var column = 0; column < _boardSize; ++column)
                {
                    squares[row, column] = GenerateSquare(row, column, y);
                }
            }

            return squares;
        }

        private Square GenerateSquare(int row, int column, float yPos)
        {
            var newSquare = Object.Instantiate(_square,_canvas.transform);
            var squareComponent = newSquare.GetComponent<Square>();
            squareComponent.Setup(row,column);
            var rect = newSquare.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(_squareSize, _squareSize);
            var x = (_squareSize * column + _lineSize * column - _offset + _halfSquareSize);
            Debug.Log($"Placing Square at: {x},{yPos}" );
            newSquare.transform.localPosition = new Vector3(x,yPos,0);
            return squareComponent;
        }
        
        private List<GameObject> GenerateLines()
        {
            List<GameObject> lines = new List<GameObject>();
            for (var i = 0; i < _lineNumber; ++i)
            {
                lines.Add(GenerateVertical(i));
                lines.Add(GenerateHorizontal(i));
            }
            return lines;
        }

        private GameObject GenerateVertical(int index)
        {
            var vertLine = Object.Instantiate(_vertical, _canvas.transform);
            var vertRect = vertLine.GetComponent<RectTransform>();
            vertRect.sizeDelta = new Vector2(_lineSize, _uiSize);
            var x = _squareSize * (index + 1) + _lineSize * index - _offset + _halfLineSize;
            Debug.Log($"Placing Vertical Line at: {x}");
            vertLine.transform.localPosition = new Vector3(x, 0, 0);
            return vertLine;
        }

        private GameObject GenerateHorizontal(int index)
        {
            var line = Object.Instantiate(_horizontal, _canvas.transform);
            var rect = line.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(_uiSize, _lineSize);
            var y = _offset - (_squareSize * (index + 1) + _lineSize * index) - _halfLineSize;
            Debug.Log($"Placing Horizontal Line at: {y}");
            line.transform.localPosition = new Vector3(0, y, 0);
            return line;
        }
    }
}