using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();

    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    /// <summary>
    /// Spawns a player.
    /// </summary>
    /// <param name="_id">The player's ID.</param>
    /// <param name="_username">The player's username.</param>
    /// <param name="_position">The player's position.</param>
    public void SpawnPlayer(int _id, string _username, Vector2 _position)
    {
        GameObject _player;
        if (_id == Client.instance.myId)
        {
            _player = Instantiate(localPlayerPrefab, new Vector2(_position.x, _position.y), Quaternion.identity);
        }
        else
        {
            _player = Instantiate(playerPrefab, new Vector2(_position.x, _position.y), Quaternion.identity);
        }

        _player.GetComponent<PlayerManager>().id = _id;
        _player.GetComponent<PlayerManager>().username = _username;
        players.Add(_id, _player.GetComponent<PlayerManager>());

    }
}
