public static class Check
{
    private static Team[,] _board;
    
    public static bool IsBoardFull(Team[,] board)
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
    
    public static Team WhoWon(Team[,] board)
    {
        _board = board;
        var winner = WhoWonRows();
        if (winner != Team.None)
        {
            return winner;
        }
        winner = WhoWonColumns();
        return winner != Team.None 
            ? winner
            : WhoWonDiagonals();
    }

    private static Team WhoWonRows()
    {
        for (int row = 0; row < 3; ++row)
        {
            Team winner = WhoWonRow(row);
            if (winner != Team.None)
            {
                return winner;
            }
        }

        return Team.None;
    }
        
    private static Team WhoWonRow(int row)
    {
        Team winner = _board[row, 0];
        for (int currentColumn = 1; currentColumn < 3; ++currentColumn)
        {
            if (_board[row,currentColumn] != winner)
            {
                return Team.None;
            }
        }
        return winner;
    }

    private static Team WhoWonColumns()
    {
        for (int column = 0; column < 3; ++column)
        {
            Team winner = WhoWonColumn(column);
            if (winner != Team.None)
            {
                return winner;
            }
        }
        return Team.None;
    }
        
    private static Team WhoWonColumn(int column)
    {
        Team winner = _board[0, column];
        for (int currentRow = 1; currentRow < 3; ++currentRow)
        {
            if (_board[currentRow,column] != winner)
            {
                return Team.None;
            }
        }
        return winner;
    }
        
    private static Team WhoWonDiagonals()
    {
        Team winner = WhoWonTopLeftDiagonal();
        return winner != Team.None
            ? winner
            : WhoWonBottomLeftDiagonal();
    }

    private static Team WhoWonTopLeftDiagonal()
    {
        Team winner = _board[0, 0];
        for (int row = 1, column = 1; row < 3; ++row, ++column)
        {
            if (_board[row, column] != winner)
            {
                return Team.None;
            }
        }
        return winner;
    }

    private static Team WhoWonBottomLeftDiagonal()
    {
        Team winner = _board[2, 0];
        for (int row = 1, column = 1; column < 3; --row, ++column)
        {
            if (_board[row, column] != winner)
            {
                return Team.None;
            }
        }
        return winner;
    }
}