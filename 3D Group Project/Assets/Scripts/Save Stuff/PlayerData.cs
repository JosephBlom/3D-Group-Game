using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int counter;
    public float timer;
    public List<string> itemNames = new List<string>();
    public List<string> itemDescription = new List<string>();
    public List<int> itemCurQuantity = new List<int>();
    public List<int> itemMaxQuantity = new List<int>();
    public float[] position;
    public int gold;
    public List<string> foundSecrets = new List<string>();

    public PlayerData(Player player)
    {
        counter = player.counter;
        timer = player.timer;
        gold = player.gold;
        fillInventory(player.GetComponent<Inventory>());
        position = new float[3];
        position[0] = player.position.x;
        position[1] = player.position.y;
        position[2] = player.position.z;
        foundSecrets = player.foundSecretsNames;
    }

    public PlayerData()
    {
        counter = 0;
        timer = 0;
        gold = 0;
    }

    private void fillInventory(Inventory script)
    {
        List<Slot> inventory = script.inventorySlots;
        foreach (Slot slot in inventory)
        {
            if(slot.heldItem != null)
            {
                itemNames.Add(slot.heldItem.name);
                itemDescription.Add(slot.heldItem.description);
                itemCurQuantity.Add(slot.slotQuantity);
                itemMaxQuantity.Add(slot.heldItem.maxQuantity);
            }
            else
            {
                itemNames.Add(null);
                itemDescription.Add(null);
                itemCurQuantity.Add(0);
                itemMaxQuantity.Add(0);
            }
            
        }
    }
}
