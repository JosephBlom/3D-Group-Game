using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] Player player;

    private void Awake()
    {
        LoadPlayer();
    }

    private void OnApplicationQuit()
    {
        SavePlayer();
    }

    public void SavePlayer()
    {
        player.fillInventory();
        SaveSystem.SavePlayer(player);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        player.counter = data.counter;
        player.timer = data.timer;
        player.gold = data.gold;
        for(int i = 0; i < 21; i++)
        {
            player.itemNames.Add(data.itemNames[i]);
            player.itemDescription.Add(data.itemDescription[i]);
            player.itemCurQuantity.Add(data.itemCurQuantity[i]);
            player.itemMaxQuantity.Add(data.itemMaxQuantity[i]);
        }
    }
}
