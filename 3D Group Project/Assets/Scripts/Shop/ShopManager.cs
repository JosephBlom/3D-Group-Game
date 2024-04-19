using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField] GameObject player;

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

    public void openShop()
    {
        shop = player.GetComponent<PlayerManager>().currentNPC.GetComponent<Shop>();
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

    public void purchaseItem(int itemCost, int playerGold)
    {
        this.playerGold.text = (itemCost - playerGold).ToString();
    }

    private void toggleShop(bool enable)
    {
        shopCanvas.SetActive(enable);

        Cursor.lockState = enable ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = enable;

        player.GetComponent<StarterAssets.StarterAssetsInputs>().cursorInputForLook = !enable;
    }
}
