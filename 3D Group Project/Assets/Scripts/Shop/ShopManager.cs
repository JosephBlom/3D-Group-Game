using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerObject;

    [Header("Canvas Objects")]
    public GameObject shopCanvas;
    public TextMeshProUGUI npcNameText;
    public TextMeshProUGUI itemDescription;
    public TextMeshProUGUI itemCost;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI playerGold;

    [Header("Shop Objects")]
    public List<ShopSlot> shopSlots = new List<ShopSlot>();

    public Shop shop;

    public Item hoveredItem;

    public void openShop()
    {
        shop = playerObject.GetComponent<PlayerManager>().currentNPC.GetComponent<Shop>();
        FindFirstObjectByType<DialogueManager>().GetComponent<DialogueManager>().dialogueCanvas.enabled = false;
        toggleShop(true);
        for(int i = 0; i < shopSlots.Count; i++)
        {
            shopSlots[i].heldItem = shop.merchandise[i];
            shopSlots[i].intialiseSlot();
        }
        npcNameText.text = shop.npcName;
    }

    public void updateMenu(string itemDescription, int itemCost, int playerGold, string itemName)
    {
        this.itemDescription.text = itemDescription;
        this.itemCost.text = itemCost.ToString();
        this.playerGold.text = playerGold.ToString();
        this.itemName.text = itemName;
    }

    public void purchaseItem()
    {
        int gold = player.GetComponent<Player>().gold;
        if (hoveredItem.cost > gold)
        {
            Debug.Log("You don't have enough gold.");
        }
        else
        {
            this.playerGold.text = (hoveredItem.cost - gold).ToString();
            player.GetComponent<Player>().gold -= hoveredItem.cost;
            player.GetComponent<Inventory>().addItemToInventory(hoveredItem, true);   
        }
    }

    private void toggleShop(bool enable)
    {
        shopCanvas.SetActive(enable);

        Cursor.lockState = enable ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = enable;

        playerObject.GetComponent<StarterAssets.StarterAssetsInputs>().cursorInputForLook = !enable;
    }
}
