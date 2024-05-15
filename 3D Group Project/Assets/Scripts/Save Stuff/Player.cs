using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Inventory & Item Variables")]
    public List<Slot> inventory;
    public List<string> itemNames = new List<string>();
    public List<string> itemDescription = new List<string>();
    public List<int> itemCurQuantity = new List<int>();
    public List<int> itemMaxQuantity = new List<int>();
    public List<Secret> foundSecrets = new List<Secret>();
    public List<string> foundSecretsNames = new List<string>();

    [Header("Miscellaneous Variables")]
    public int counter;
    public float timer;
    public float fastestTime;
    public int gold;
    public GameObject playerObject;
    public Vector3 position;
    [SerializeField] public GameObject horse;

    public bool timerOn;

    private void Start()
    {
        inventory = GetComponent<Inventory>().inventorySlots;
        if (SceneManager.GetActiveScene().buildIndex != 0)
            position = playerObject.transform.position;

    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
            position = playerObject.transform.position;
        if(timerOn)
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
