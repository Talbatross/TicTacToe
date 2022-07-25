using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace TicTacToe
{
    public enum PlayerType
    {
        Human,
        AI
    }
    
    public class Bootstrap : MonoBehaviour
    {
        public PlayerType playerTypeX;
        public PlayerType playerTypeO;
        public int boardSize = 3;
        public int size = 1000;
        public Canvas canvas;
        public GameObject square;
        public GameObject horizontal;
        public GameObject vertical;
        public Button playButton;
        public TextMeshProUGUI playButtonText;
        public AudioSource placeFX;
        public AudioClip placeSound;
        public AudioClip koSound;
        public AudioClip tieSound;
        public AudioClip startSound;


        private int _lineNumber;
        private float _squareSize;
        private float _halfSquareSize;
        private float _lineSize;
        private float _halfLineSize;
        private float _offset;
        private List<GameObject> _lines;

        private readonly List<HumanPlayer> _humans = new();
        private Board _board;
        private Square[,] _squares;
        private Game _ticTacToe;
        private EventHandler _eventHandler;

        private void Start()
        {
            CalculateDimensions();
            
            // Setup Game
            var sounds = new Sounds(placeSound, koSound, tieSound);
            _board = new Board(boardSize);
            _squares = new Square[boardSize, boardSize];
            _eventHandler = new EventHandler(
                _squares,
                placeFX,
                sounds);
            Team firstPlayer = ChooseFirstPlayer();
            _ticTacToe = new Game(firstPlayer, _board, _eventHandler);
            var playerX = CreatePlayer(playerTypeX, Team.X);
            var playerO = CreatePlayer(playerTypeO, Team.O);

            // Hookup Player To Board
            // Hookup Boards to Player
            playerX.PlaceMarker = ExecuteMove;
            playerO.PlaceMarker = ExecuteMove;
            _ticTacToe.HookupPlayers(playerX.OnPlayerTurn,playerO.OnPlayerTurn);
            GenerateVisualBoard();
            
            playButton.onClick.AddListener(StartGame);
            playButtonText.text = "Start Game";
        }

        private void CalculateDimensions()
        {
            _lineNumber = boardSize - 1;
            _squareSize = (float)(size * 0.9 / boardSize);
            _halfSquareSize = _squareSize * .5f;
            _lineSize = (float)(size * 0.1 / _lineNumber);
            _halfLineSize = _lineSize * .5f;
            _offset = size * .5f;
        }

        private void StartGame()
        {
            StartCoroutine(OnStartGame());
        }

        private IEnumerator OnStartGame()
        {
            playButton.onClick.RemoveListener(StartGame);
            placeFX.PlayOneShot(startSound);
            while (placeFX.isPlaying)
            {
                yield return null;
            }
            playButton.onClick.AddListener(StopGame);
            playButtonText.text = "Stop Game";
            _ticTacToe.SetCurrentTeam(ChooseFirstPlayer());
            _ticTacToe.TriggerPlayerTurn();
        }

        private void StopGame()
        {
            StopAllCoroutines();
            _board.Clear();
            _ticTacToe.Clear();
            ResetSquares();
            playButton.onClick.RemoveListener(StopGame);
            playButton.onClick.AddListener(StartGame);
            playButtonText.text = "Start Game";
        }

        private void ResetSquares()
        {
            // Teardown Squares
            // Teardown Lines
            for (var row = 0; row < boardSize; ++row)
            {
                for (var column = 0; column < boardSize; ++column)
                {
                    _squares[row, column].Reset();
                }
            }
        }

        public bool IsReady()
        {
            return _squares != null && _ticTacToe != null && _board != null;
        }

        private IPlayer CreatePlayer(PlayerType type, Team team)
        {
            if (type == PlayerType.Human)
            {
                HumanPlayer human = new HumanPlayer();
                _humans.Add(human);
                return human;
            }
            AIPlayer ai = new AIPlayer(team, boardSize);
            return ai;
        }
        
        private Team ChooseFirstPlayer()
        {
            var result = (Team)Random.Range(1, 3);
            Debug.Log($"Team {result} is first!");
            return result;
        }

        private void GenerateVisualBoard()
        {
            for (var row = 0; row < boardSize; ++row)
            {
                var y = _offset - (_squareSize * row + _lineSize * row) - _halfSquareSize;
                for (var column = 0; column < boardSize; ++column)
                {
                    GenerateSquare(row, column,y);
                }
            }
            for (var i = 0; i < _lineNumber; ++i)
            {
                GenerateLines(i);
            }
        }

        private void GenerateSquare(int row, int column, float yPos)
        {
            var newSquare = Instantiate(square,canvas.transform);
            var squareComponent = newSquare.GetComponent<Square>();
            squareComponent.Setup(row,column);
            foreach (var humanPlayer in _humans)
            {
                humanPlayer.SubscribeToUI = RegisterPlayerToUI;
            }

            var rect = newSquare.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(_squareSize, _squareSize);
            var x = (_squareSize * column + _lineSize * column - _offset + _halfSquareSize);
            Debug.Log($"Placing Square at: {x},{yPos}" );
            newSquare.transform.localPosition = new Vector3(x,yPos,0);
            _squares[row, column] = squareComponent;
        }

        private void GenerateLines(int index)
        {
            var line = Instantiate(horizontal, canvas.transform);
            var vertLine = Instantiate(vertical, canvas.transform);
            var rect = line.GetComponent<RectTransform>();
            var vertRect = vertLine.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(size, _lineSize);
            vertRect.sizeDelta = new Vector2(_lineSize, size);
            var x = _squareSize * (index + 1) + _lineSize * index - _offset + _halfLineSize;
            var y = _offset - (_squareSize * (index + 1) + _lineSize * index) - _halfLineSize;
            Debug.Log($"Placing Horizontal Line at: {y}");
            Debug.Log($"Placing Vertical Line at: {x}");
            line.transform.localPosition = new Vector3(0, y, 0);
            vertLine.transform.localPosition = new Vector3(x, 0, 0);
        }

        private void RegisterPlayerToUI(Action<int,int> callback)
        {
            for (var row = 0; row < boardSize; ++row)
            {
                for (var column = 0; column < boardSize; ++column)
                {
                    _squares[row, column].RegisterOnClick(callback);
                }
            }
        }
        
        private void ExecuteMove(int row, int column)
        {
            StartCoroutine(_ticTacToe.PlaceMarker(row, column));
        }
    }
}


