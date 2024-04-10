using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int counter;

    public PlayerData(Player player)
    {
        counter = player.counter;
    }
}
