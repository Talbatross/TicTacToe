using System.Collections;
using UnityEngine;

public class EventHandler : IEventHandler
{
    private readonly Square[,] _squares;
    private readonly AudioSource _audio;
    private readonly Sounds _sounds;

    public EventHandler(Square[,] squares, AudioSource audio, Sounds sounds)
    {
        _squares = squares;
        _audio = audio;
        _sounds = sounds;
    }
    
    public IEnumerator OnUpdateSquare(int row, int column, Team team)
    {
        _squares[row, column].SetTeam(team);
        _audio.PlayOneShot(_sounds.PlaceSound);
        while (_audio.isPlaying)
        {
            yield return null;
        }
    }

    public IEnumerator OnAlreadyTaken(int row, int column, Team team)
    {
        Debug.Log($"{row}, {column} already has an {team}!");
        yield break;
    }

    public IEnumerator OnAlreadyOver()
    {
        Debug.Log($"Game is already over!");
        yield break;
    }

    public IEnumerator OnWinner(Team winner)
    {
        Debug.Log($"Team {winner} has won!");
        _audio.PlayOneShot(_sounds.KnockoutSound);
        while (_audio.isPlaying)
        {
            yield return null;
        }
    }

    public IEnumerator OnTie()
    {
        Debug.Log("Ended in tie!");
        _audio.PlayOneShot(_sounds.TieSound);
        while (_audio.isPlaying)
        {
            yield return null;
        }
    }
}