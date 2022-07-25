using System.Collections;
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
        
        private Board _board;
        private Game _ticTacToe;
        private EventHandler _eventHandler;
        private SquareUIBuilder _squareUIBuilder;
        private SquareUI _squareUI;

        private void Start()
        {
            _squareUIBuilder = new SquareUIBuilder(
                square,
                horizontal,
                vertical,
                canvas,
                boardSize, 
                size);
            _squareUI = _squareUIBuilder.BuildUI();
            _board = new Board(boardSize);
            var sounds = new Sounds(placeSound, koSound, tieSound);
            _eventHandler = new EventHandler(_squareUI, placeFX, sounds);
            var playerX = CreatePlayer(playerTypeX, Team.X);
            var playerO = CreatePlayer(playerTypeO, Team.O);
            _ticTacToe = new Game(Team.X, _board, _eventHandler,playerX.OnPlayerTurn,playerO.OnPlayerTurn);
            playButton.onClick.AddListener(StartGame);
            playButtonText.text = "Start Game";
        }
        
        private IPlayer CreatePlayer(PlayerType type, Team team)
        {
            if (type == PlayerType.Human)
            {
                return new HumanPlayer(ExecuteMove,_squareUI.RegisterOnClick);
            }
            return new AIPlayer(ExecuteMove, team, boardSize);
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
            _squareUI.ResetSquares();
            playButton.onClick.RemoveListener(StopGame);
            playButton.onClick.AddListener(StartGame);
            playButtonText.text = "Start Game";
        }

        public bool IsReady()
        {
            return _squareUI != null && _ticTacToe != null && _board != null;
        }

        private Team ChooseFirstPlayer()
        {
            var result = (Team)Random.Range(1, 3);
            Debug.Log($"Team {result} is first!");
            return result;
        }

        private void ExecuteMove(int row, int column)
        {
            StartCoroutine(_ticTacToe.PlaceMarker(row, column));
        }
    }
}