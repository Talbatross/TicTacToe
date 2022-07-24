using System;

public class HumanPlayer : IPlayer
{
    private IReadOnlyBoard _board;
    public Action<int, int> PlaceMarker { get; set; }

    public Action<Action<int,int>> SubscribeToUI;

    public void OnPlayerTurn(IReadOnlyBoard board)
    {
        _board = board;
        SubscribeToUI(SelectSquare);
    }

    private void SelectSquare(int row, int column)
    {
        if (_board.GetTeam(row, column) != Team.None)
        {
            return;
        }
        PlaceMarker(row, column);
    }
}