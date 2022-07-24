public static class Check
{
    private static IReadOnlyBoard _board;
    private static int _boardSize;

    public static bool IsBoardFull(IReadOnlyBoard board)
    {
        _board = board;
        _boardSize = _board.GetSize();
        for (int row = 0; row < _boardSize; ++row)
        {
            for (int column = 0; column < _boardSize; ++column)
            {
                if (board.GetTeam(row,column) == Team.None)
                {
                    return false;
                }
            }
        }
        return true;
    }
    
    public static Team WhoWon(IReadOnlyBoard board)
    {
        _board = board;
        _boardSize = _board.GetSize();
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
        for (int row = 0; row < _boardSize; ++row)
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
        var winner = _board.GetTeam(row, 0);
        for (int currentColumn = 1; currentColumn < _boardSize; ++currentColumn)
        {
            if (_board.GetTeam(row,currentColumn) != winner)
            {
                return Team.None;
            }
        }
        return winner;
    }

    private static Team WhoWonColumns()
    {
        for (int column = 0; column < _boardSize; ++column)
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
        var winner = _board.GetTeam(0, column);
        for (int currentRow = 1; currentRow < _boardSize; ++currentRow)
        {
            if (_board.GetTeam(currentRow,column) != winner)
            {
                return Team.None;
            }
        }
        return winner;
    }
        
    private static Team WhoWonDiagonals()
    {
        var winner = WhoWonTopLeftDiagonal();
        return winner != Team.None
            ? winner
            : WhoWonBottomLeftDiagonal();
    }

    private static Team WhoWonTopLeftDiagonal()
    {
        var winner = _board.GetTeam(0,0);
        for (int row = 1, column = 1; row < _boardSize; ++row, ++column)
        {
            if (_board.GetTeam(row, column) != winner)
            {
                return Team.None;
            }
        }
        return winner;
    }

    private static Team WhoWonBottomLeftDiagonal()
    {
        var winner = _board.GetTeam(2, 0);
        for (int row = 1, column = 1; column < _boardSize; --row, ++column)
        {
            if (_board.GetTeam(row, column) != winner)
            {
                return Team.None;
            }
        }
        return winner;
    }
}