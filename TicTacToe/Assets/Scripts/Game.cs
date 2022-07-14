using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Game
{
    private Team[,] board = new Team [3, 3];
    private Team currentTeam = Team.None;
    
    private List<Move> moves = new List<Move>(9);
    private Team firstTeam = Team.None;

    public enum Team
    {
        None = 0,
        X = 1,
        O = 2
    }

    public Game()
    {
        InitializeBoard();
        moves.Clear();
        InitializeFirstPlayer();
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

    public void SetFirstAndCurrentPlayer(Team team)
    {
        firstTeam = team;
        currentTeam = team;
    }

    private void InitializeFirstPlayer()
    {
        firstTeam = (Team)Random.Range(1, 3);
        currentTeam = firstTeam;
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

        if (WhoWon() != Team.None)
        {
            throw new ArgumentException($"Game is already over!");
        }
        // Record Action
        moves.Add(new Move(row,column));
        // Place Marker on Board
        board[row, column] = currentTeam;
        // Check For Victory
        Team winner = WhoWon();
        if (winner != Team.None)
        {
            Debug.Log($"Team {winner} has won!");
        }
        else
        {
            // Switch Teams if not victory
            currentTeam = 
                currentTeam == Team.O
                    ? Team.X
                    : Team.O;
        }
        // Check for Tie
        if (BoardIsFull())
        {
            Debug.Log("Ended in tie!");
        }
    }

    private bool BoardIsFull()
    {
        for (int row = 0; row < 3; ++row)
        {
            for (int column = 0; column < 3; ++column)
            {
                if (board[row,column] == Team.None)
                {
                    return false;
                }
            }
        }

        return true;
    }
    
    private Team WhoWonRow(int row)
    {
        Team winner = board[row, 0];
        for (int currentColumn = 1; currentColumn < 3; ++currentColumn)
        {
            Team nextWinner = board[row,currentColumn];
            if (nextWinner != winner)
            {
                return Team.None;
            }
        }
        return winner;
    }
    
    private Team WhoWonColumn(int column)
    {
        Team winner = board[0, column];
        for (int currentRow = 1; currentRow < 3; ++currentRow)
        {
            Team nextWinner = board[currentRow,column];
            if (nextWinner != winner)
            {
                return Team.None;
            }
        }
        return winner;
    }

    private Team WhoWonDiagonals()
    {
        Team winner = board[0, 0];
        for (int row = 1, column = 1; row < 3; ++row, ++column)
        {
            Team owner = board[row, column];
            if (owner != winner)
            {
                winner = Team.None;
                break;
            }
        }
        if (winner != Team.None)
        {
            return winner;
        }
        
        winner = board[2, 0];
        for (int row = 1, column = 1; column < 3; --row, ++column)
        {
            Team owner = board[row, column];
            if (owner != winner)
            {
                winner = Team.None;
                break;
            }
        }

        return winner;
    }
    
    public Team WhoWon()
    {
        // Check Rows
        for (int row = 0; row < 3; ++row)
        {
            Team winner = WhoWonRow(row);
            if (winner != Team.None)
            {
                return winner;
            }
        }
        // Check Columns
        for (int column = 0; column < 3; ++column)
        {
            Team winner = WhoWonColumn(column);
            if (winner != Team.None)
            {
                return winner;
            }
        }
        // Check Diagonals
        return WhoWonDiagonals();
    }

    public struct Move
    {
        public int row;
        public int column;
        
        public Move(int row, int column)
        {
            this.row = row;
            this.column = column;
        }
    }

    public class Player
    {
    
    }
}