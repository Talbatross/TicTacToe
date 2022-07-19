using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private Game ticTacToe;
    private void Start()
    {
        Team firstPlayer = ChooseFirstPlayer();
        ticTacToe = new Game(firstPlayer);
    }
    private Team ChooseFirstPlayer()
    {
        return (Team)Random.Range(1, 3);
    }
    
}

