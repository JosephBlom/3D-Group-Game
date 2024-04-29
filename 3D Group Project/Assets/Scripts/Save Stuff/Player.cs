using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Inventory & Item Variables")]
    public List<Slot> inventory;
    public List<string> itemNames = new List<string>();
    public List<string> itemDescription = new List<string>();
    public List<int> itemCurQuantity = new List<int>();
    public List<int> itemMaxQuantity = new List<int>();

    [Header("Miscellaneous Variables")]
    public int counter;
    public float timer;
    public int gold;
    public GameObject playerObject;
    public Vector3 position;

    private void Start()
    {
        inventory = GetComponent<Inventory>().inventorySlots;
        position = playerObject.transform.position;
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    public void fillInventory()
    {
        foreach (Slot slot in inventory)
        {
            if (slot.heldItem != null)
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
