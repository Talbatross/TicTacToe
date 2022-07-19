using System;
using UnityEngine;

public enum Team
{
    None = 0,
    X = 1,
    O = 2
}

public interface IPlayer
{
    
}

public class Game
{
    public Team[,] board = new Team [3, 3]; // Move this
    private Team _currentTeam;
    private IPlayer _playerX;
    private IPlayer _playerY;

    public Game(Team firstPlayer)
    {
        InitializeBoard();
        _currentTeam = firstPlayer;
    }

    public void PlaceMarker(int row, int column)
    {
        var team = board[row, column]; 
        // Make sure Row/Column is Good
        if (team != Team.None)
        {
            // Maybe make this an event message instead
            throw new ArgumentException($"{row}, {column} already has an {team}!");
        }
        if (Check.WhoWon(board) != Team.None)
        {
            throw new ArgumentException($"Game is already over!");
        }
        // Place Marker on Board
        board[row, column] = _currentTeam;
        // Check For Victory
        Team winner = Check.WhoWon(board);
        if (winner != Team.None)
        {
            Debug.Log($"Team {winner} has won!");
            return;
        }
        // Check for Tie
        if (Check.IsBoardFull(board))
        {
            Debug.Log("Ended in tie!");
        }
        // Switch Teams if not victory
        _currentTeam = 
            _currentTeam == Team.O
                ? Team.X
                : Team.O;
        
        // Notify team it is their turn
    }

    private void InitializeBoard()
    {
        for(int row = 0; row < 3; ++row)
        {
            for (int column = 0; column < 3; ++column)
            {
                board[row, column] = Team.None;
            }
        }
    }
}
