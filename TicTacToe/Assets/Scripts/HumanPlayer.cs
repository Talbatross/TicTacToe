using System;

public class HumanPlayer : IPlayer
{
    private readonly Action<int, int> _placeMarker;
    private Team[,] _board;
    private Team _team;

    public HumanPlayer(Action<int, int> placeMarker, Team team)
    {
        _placeMarker = placeMarker;
        _team = team;
    }
    public void OnPlayerTurn(Team[,] board)
    {
        // Do Nothing
    }
}