using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public List<Item> itemDrops = new List<Item>();
    public List<Item> itemsToDrop = new List<Item>();
    [Tooltip("This is the amount of gold the enemy drops.")]
    public int goldAmount;
    [Tooltip("This is the amount of exp the enemy drops.")]
    public int expAmount;

    public void dropItems(Transform dropPosition)
    {
        foreach(Item item in itemDrops)
        {
            float number = Random.Range(1, item.dropChance);
            if(number == item.dropChance)
            {
                itemsToDrop.Add(item);
            }
        }
        foreach(Item item in itemDrops)
        {
            Instantiate(item, dropPosition);
        }
    }
}
