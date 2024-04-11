using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int counter;
    public float timer;

    public PlayerData(Player player)
    {
        counter = player.counter;
        timer = player.timer;
    }
}
