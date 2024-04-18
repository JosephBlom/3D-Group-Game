using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;

    [Header("Canvas Objects")]
    public GameObject shopCanvas;
    public TextMeshProUGUI npcNameText;
    public TextMeshProUGUI itemDescription;
    public TextMeshProUGUI itemCost;
    public TextMeshProUGUI playerGold;

    [Header("Shop Objects")]
    public List<Slot> shopSlots = new List<Slot>();

    public Shop shop;

    public void openShop()
    {
        shop = playerManager.currentNPC.GetComponent<Shop>();
        toggleShop(true);
        for(int i = 0; i < shopSlots.Count; i++)
        {
            shopSlots[i].heldItem = shop.merchandise[i];
            shopSlots[i].initialiseSlot();
        }
        npcNameText.text = shop.npcName;
    }

    public void updateMenu(string itemDescription, int itemCost, int playerGold)
    {
        this.itemDescription.text = itemDescription;
        this.itemCost.text = itemCost.ToString();
        this.playerGold.text = playerGold.ToString();
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

        GetComponentInChildren<StarterAssets.StarterAssetsInputs>().cursorInputForLook = !enable;
    }
}
