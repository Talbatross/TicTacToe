using System;
using UnityEngine;
using Random = UnityEngine.Random;

public enum PlayerType
{
    Human,
    AI
}

public class Bootstrap : MonoBehaviour
{
    public PlayerType playerX;
    public PlayerType playerY;
    public int boardSize = 3;

    private Game ticTacToe;
    
    private void Start()
    {
        Team firstPlayer = ChooseFirstPlayer();
        ticTacToe = new Game(firstPlayer, playerX, playerY, boardSize);
    }
    
    private Team ChooseFirstPlayer()
    {
        return (Team)Random.Range(1, 3);
    }
}

