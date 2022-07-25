using System.Collections;
using TicTacToe;
using UnityEngine;

namespace Tests
{
    internal class DummyHandler : IEventHandler
    {
        public IEnumerator OnUpdateSquare(int row, int column, Team team)
        {
            Debug.Log($"OnUpdateSquare {row}, {column}, {team}");
            yield break;
        }

        public IEnumerator OnAlreadyTaken(int row, int column, Team team)
        {
            Debug.Log($"OnAlreadyTaken {row}, {column}, {team}");
            yield break;
        }

        public IEnumerator OnAlreadyOver()
        {
            Debug.Log("OnAlreadyOver");
            yield break;
        }

        public IEnumerator OnWinner(Team team)
        {
            Debug.Log($"OnWinner {team}");
            yield break;
        }

        public IEnumerator OnTie()
        {
            Debug.Log("OnTie");
            yield break;
        }
    }
}