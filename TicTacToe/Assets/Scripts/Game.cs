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
    void OnPlayerTurn(IReadOnlyBoard board);
    Action<int,int> PlaceMarker { get; set; }
}

public class Game
{
    private readonly Board _board;
    private readonly Action<int, int, Team> _updateSquare;

    private Action<Board> _onPlayerXTurn;
    private Action<Board> _onPlayerOTurn;
    private readonly Action _onWinner;
    private readonly Action _onTie;
    private Team _currentTeam;

    public Game(
        Team firstPlayer,
        Board board,
        Action<int,int,Team> updateSquare,
        Action onWinner,
        Action onTie)
    {
        _board = board;
        _currentTeam = firstPlayer;
        _updateSquare = updateSquare;
        _onWinner = onWinner;
        _onTie = onTie;
    }

    public void SetCurrentTeam(Team currentTeam)
    {
        _currentTeam = currentTeam;
    }

    public void HookupPlayers(Action<Board> onXTurn, Action<Board> onOTurn)
    {
        _onPlayerXTurn = onXTurn;
        _onPlayerOTurn = onOTurn;
    }

    public void TriggerPlayerTurn()
    {
        if (_currentTeam == Team.O)
        {
            _onPlayerOTurn(_board);
        }
        else
        {
            _onPlayerXTurn(_board);
        }
    }
    
    public void PlaceMarker(int row, int column)
    {
        if (CanMove(row,column) == false)
        {
            return;
        }
        UpdateSquare(row,column);
        if (GameCompleted())
        {
            return;
        }
        PrepareNextTurn();
    }
    
    private bool CanMove(int row, int column)
    {
        var team = _board.GetTeam(row, column);
        if (team != Team.None)
        {
            Debug.Log($"{row}, {column} already has an {team}!");
            return false;
        }

        if (Check.WhoWon(_board) == Team.None)
        {
            return true;
        }
        Debug.Log($"Game is already over!");
        return false;

    }
    
    private void UpdateSquare(int row, int column)
    {
        _board.SetTeam(row, column, _currentTeam);
        _updateSquare(row, column, _currentTeam);
    }
    
    private bool GameCompleted()
    {
        var winner = Check.WhoWon(_board);
        if (winner != Team.None)
        {
            _onWinner();
            Debug.Log($"Team {winner} has won!");
            return true;
        }
        if (!Check.IsBoardFull(_board))
        {
            return false;
        }
        _onTie();
        Debug.Log("Ended in tie!");
        return true;

    }

    private void PrepareNextTurn()
    {
        _currentTeam = 
            _currentTeam == Team.O
                ? Team.X
                : Team.O;
        TriggerPlayerTurn();
    }
}