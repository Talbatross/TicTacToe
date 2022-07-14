using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private Game ticTacToe;
    private void Start()
    {
        ticTacToe = new Game();
    }

    
}

