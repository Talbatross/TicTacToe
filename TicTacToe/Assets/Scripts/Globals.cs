using System.Collections;
using UnityEngine;

namespace TicTacToe
{
    public enum Team
    {
        None = 0,
        X = 1,
        O = 2
    }

    public interface IPlayer
    {
        void OnPlayerTurn(IReadOnlyBoard board);
    }

    public interface IEventHandler
    {
        IEnumerator OnUpdateSquare(int row, int column, Team team);
        IEnumerator OnAlreadyTaken(int row, int column, Team team);
        IEnumerator OnAlreadyOver();
        IEnumerator OnWinner(Team team);
        IEnumerator OnTie();
    }

    public class Sounds
    {
        public readonly AudioClip PlaceSound;
        public readonly AudioClip KnockoutSound;
        public readonly AudioClip TieSound;

        public Sounds(
            AudioClip placeSound,
            AudioClip knockoutSound,
            AudioClip tieSound)
        {
            PlaceSound = placeSound;
            KnockoutSound = knockoutSound;
            TieSound = tieSound;
        }
    }
    
}