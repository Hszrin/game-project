using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private static PlayerInfo instance;
    public List<GameObject> players;
    public List<GameObject> enabledPlayers;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public static PlayerInfo Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }
    public void UpdatePlayerList()
    {
        enabledPlayers.Clear();
        for(int i = 0;i < players.Count; i++)
        {
            if (players[i].activeSelf) enabledPlayers.Add(players[i]);
        }
    }
    public void RemovePlayer(GameObject removePlayer)
    {
        if (enabledPlayers.IndexOf(removePlayer) >= 0) enabledPlayers.Remove(removePlayer);
    }
    public void AddPlayer(GameObject addPlayer)
    {
        enabledPlayers.Add(addPlayer);
    }
}

