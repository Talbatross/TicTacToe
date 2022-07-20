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
    void OnPlayerTurn(Team[,] board);
}

public class Game
{
    private Team _currentTeam;
    private Team[,] _board;
    private IPlayer _playerX;
    private IPlayer _playerO;

    public Game(Team firstPlayer, PlayerType playerX, PlayerType playerO, int boardSize)
    {
        _board = CreateBoard(boardSize);
        _playerX = CreatePlayer(playerX, Team.X);
        _playerO = CreatePlayer(playerO, Team.O);
        _currentTeam = firstPlayer;
        if (_currentTeam == Team.O)
        {
            _playerO.OnPlayerTurn(_board);
        }
        else
        {
            _playerX.OnPlayerTurn(_board);
        }
    }
    
    public void PlaceMarker(int row, int column)
    {
        var team = _board[row, column]; 
        // Make sure Row/Column is Good
        if (team != Team.None)
        {
            // Maybe make this an event message instead
            throw new ArgumentException($"{row}, {column} already has an {team}!");
        }
        if (Check.WhoWon(_board) != Team.None)
        {
            throw new ArgumentException($"Game is already over!");
        }
        // Place Marker on Board
        _board[row, column] = _currentTeam;
        // Check For Victory
        Team winner = Check.WhoWon(_board);
        if (winner != Team.None)
        {
            Debug.Log($"Team {winner} has won!");
            return;
        }
        // Check for Tie
        if (Check.IsBoardFull(_board))
        {
            Debug.Log("Ended in tie!");
            return;
        }
        // Switch Teams if not victory
        _currentTeam = 
            _currentTeam == Team.O
                ? Team.X
                : Team.O;
        
        // Indicate Board update for Player
        if (_currentTeam == Team.O)
        {
            _playerO.OnPlayerTurn(_board);
        }
        else
        {
            _playerX.OnPlayerTurn(_board);
        }
    }

    public Team[,] GetBoard()
    {
        return _board;
    }

    private IPlayer CreatePlayer(PlayerType type, Team team)
    {
        return type == PlayerType.Human ? new HumanPlayer(PlaceMarker, team) :
            type == PlayerType.AI ? new AIPlayer(PlaceMarker, team) :
            throw new ArgumentOutOfRangeException(nameof(type));
    }

    private Team[,] CreateBoard(int boardSize)
    {
        var board = new Team[boardSize, boardSize];
        for (int row = 0; row < boardSize; ++row)
        {
            for (int column = 0; column < boardSize; ++column)
            {
                board[row, column] = Team.None;
            }
        }

        return board;
    }
}