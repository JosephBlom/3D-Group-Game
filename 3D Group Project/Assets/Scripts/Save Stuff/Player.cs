using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int counter;
    public float timer;
    public List<string> itemNames = new List<string>();
    public List<string> itemDescription = new List<string>();
    public List<int> itemCurQuantity = new List<int>();
    public List<int> itemMaxQuantity = new List<int>();

    public List<Slot> inventory;

    private void Start()
    {
        inventory = GetComponent<Inventory>().inventorySlots;
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    public void fillInventory()
    {
        foreach (Slot slot in inventory)
        {
            Debug.Log("Ran");
            if (slot.heldItem != null)
            {
                itemNames.Add(slot.heldItem.name);
                itemDescription.Add(slot.heldItem.description);
                itemCurQuantity.Add(slot.heldItem.currentQuantity);
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
